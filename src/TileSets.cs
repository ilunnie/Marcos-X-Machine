using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

public static class TileSets
{
    public static int spriteWidth { get; set; } = 24;
    public static int spriteHeight { get; set; } = 24;

    public static void tileSets()
    {
        Image img = SpriteBuffer.Current.Get("src/sprites/tileset/Tile.png");

        int spritesRows = img.Width / spriteWidth;
        int spritesColumns = img.Height / spriteHeight;

        var sprites = new CalcMap[spritesRows * spritesColumns];
        var index = 0;

        for (int i = 0; i < spritesColumns; i++)
        {
            for (int j = 0; j < spritesRows; j++)
            {
                int x = j * spriteWidth;
                int y = i * spriteHeight;
                int layer = 0;

                RectangleF spriteRect = new RectangleF(x, y, spriteWidth, spriteHeight);
                SubImage subImage = new SubImage(img, spriteRect);

                if (spritesRows == 13)
                    layer = 1;

                CalcMap sprite = new CalcMap(subImage, new PointF(), new SizeF(120, 120), new Hitbox(), layer);
                sprites[index] = sprite;

                index++;
            }
        }
        Memory.Tileset = sprites;
    }

    public static void ReadFile() {
        string filePath = "src/Area/Ets.csv";

        using (StreamReader reader = new StreamReader(filePath))
        {
            int countLine = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace(" ", "");
                string[] columns = line.Split(',');

                for (int column = 0; column < columns.Length; column++)
                {
                    string spriteCode = columns[column];
                    int spriteIndex = int.Parse(spriteCode.Split('h')[0]);

                    CalcMap clone = Memory.Tileset[spriteIndex].Clone();
                    SizeF cloneSize = clone.Size;
                    clone.Move(
                        new PointF(
                            column * cloneSize.Width, 
                            countLine * cloneSize.Height
                        )
                    );

                    clone.AddAnimation(
                        new StaticAnimation() 
                        {
                            Image = clone.Image 
                        }
                    );

                    if(spriteCode.Contains('h'))
                    {
                        string getHexa = spriteCode.Split('h')[1];

                        if(getHexa != "")
                        {
                            string getBinary = Convert.ToString(Convert.ToInt32(getHexa, 16), 2).PadLeft(9, '0');;
                            for (int i = 0; i < getBinary.Length; i++)
                            {
                                if (getBinary[i] == '1')
                                    clone.Hitbox.rectangles.Add(new RectangleF(
                                        clone.Size.Width / 3 * (i % 3),
                                        clone.Size.Height / 3 * (i / 3),
                                        clone.Size.Width / 3,
                                        clone.Size.Height / 3
                                    ));
                            }
                        } else {
                            clone.Hitbox.rectangles.Add(new RectangleF(
                                0, 0,
                                clone.Size.Width,
                                clone.Size.Height
                            ));
                        }
                    }
                    Memory.Map.Add(clone);
                }

                countLine++;
            }
        }
    }
}

