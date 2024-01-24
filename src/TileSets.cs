using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class TileSets
{
    public static void tileSets()
    {
        Image img = SpriteBuffer.Current.Get("src/sprites/tileset/Tile.png");

        int spriteWidth = 24;
        int spriteHeight = 24;

        int spritesRows = img.Width / spriteWidth;
        int spritesColums = img.Height / spriteHeight;

        var sprites = new CalcMap[spritesRows * spritesColums];
        var index = 0;

        for (int i = 0; i < spritesColums; i++)
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

                CalcMap sprite = new CalcMap(subImage, new PointF(0, 0), new SizeF(120, 120), null, layer);
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
                string linha = reader.ReadLine();

                string[] colunas = linha.Split(',');

                for (int i = 0; i < colunas.Length; i++)
                {
                    CalcMap clone = Memory.Tileset[int.Parse(colunas[i])].Clone();
                    SizeF cloneSize = clone.Size;
                    clone.Position = new PointF(i * cloneSize.Width, countLine * cloneSize.Height);
                    clone.Draw();
                }

                countLine++;
            }
        }
    }
}

