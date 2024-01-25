using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;

public static class TileSets
{
    public static int spriteWidth { get; set; }
    public static int spriteHeight { get; set; }

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

                RectangleF rectangle = new RectangleF(0, 0, 120, 120); 
                Hitbox hitbox = new Hitbox();
                hitbox.rectangles.Add(rectangle);
                CalcMap sprite = new CalcMap(subImage, new PointF(), new SizeF(120, 120), hitbox, layer);
                sprites[index] = sprite;

                index++;
            }
        }
        Memory.Tileset = sprites;
    }

    public static void DrawFromFile() {
        string filePath = "src/Area/Ets.csv";

        using (StreamReader reader = new StreamReader(filePath))
        {
            int countLine = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] columns = line.Split(',');

                for (int column = 0; column < columns.Length; column++)
                {
                    string spriteCode = columns[column];
                    int spriteIndex = int.Parse(new string(spriteCode.Where(char.IsDigit).ToArray()));

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
                    
                    if(spriteCode.Contains('h') || spriteCode.Contains('H'))
                    {
                        Memory.MapWithCollision.Add(clone);
                        continue;
                    }
                    Memory.MapWithoutCollision.Add(clone);
                }

                countLine++;
            }
        }
    }
}

