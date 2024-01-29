using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

public static class TileSets
{
    public static int spriteWidth { get; set; } = 24;
    public static int spriteHeight { get; set; } = 24;

    public static SizeF spriteMapSize { get; private set; } = new SizeF(120, 120);

    public static StreamReader Reader { get; set; } = null;
    public static int Count { get; private set; }
    private static Queue<string> Line { get; set; } = new Queue<string>();
    private static int readingX = 0;
    private static int readingY = 0;

    public static void SetSprites(string file)
    {
        Image img = SpriteBuffer.Current.Get(file);

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

                CalcMap sprite = new CalcMap(subImage, new PointF(), new SizeF(spriteMapSize.Width, spriteMapSize.Height), new Hitbox(), layer);
                sprites[index] = sprite;

                index++;
            }
        }
        Memory.Tileset = sprites;
    }

    public static void ReadFile(string file)
    {
        if (Reader != null) CloseFile();

        Count = Length(new StreamReader(file));
        Reader = new StreamReader(file);
        readingX = 0;
        readingY = 0;
    }

    private static int Length(StreamReader reader)
    {
        int total = 0;
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine().Replace(" ", "");
            if (!string.IsNullOrEmpty(line))
            {
                string[] values = line.Split(',');
                total += values.Length;
            }
        }
        reader.Close();
        reader.Dispose();
        return total;
    }

    public static (string value, int column, int row)? Next()
    {
        if (Line.Count == 0)
        {
            string line = Reader.ReadLine();
            if (line == null) return null;

            Line = new Queue<string>(line.Replace(" ", "").Split(','));
            readingX = 0;
            readingY++;
        }

        readingX++;
        return (Line.Dequeue(), readingX - 1, readingY);
    }

    public static void Set(string value, int column, int row)
    {
        string[] tileset = value.Split('h');
        int index = int.Parse(tileset[0]);

        CalcMap clone = Memory.Tileset[index].Clone();
        clone.Move(
            new PointF(
                column * clone.Size.Width,
                row * clone.Size.Height
            )
        );
        clone.AddAnimation(
            new StaticAnimation()
            {
                Image = clone.Image
            }
        );

        if (tileset.Length > 1)
            clone.ReadHitBox(tileset[1]);

        Memory.Map.Add(clone);
    }

    public static void ReadHitBox(this CalcMap clone, string hexa)
    {
        if (hexa == "")
        {
            clone.Hitbox.rectangles.Add(new RectangleF(
                0, 0,
                clone.Size.Width,
                clone.Size.Height
            ));
            return;
        }

        string bin = Convert.ToString(Convert.ToInt32(hexa, 16), 2).PadLeft(9, '0');
        for (int i = 0; i < bin.Length; i++)
        {
            if (bin[i] == '1')
                clone.Hitbox.rectangles.Add(new RectangleF(
                    clone.Size.Width / 3 * (i % 3),
                    clone.Size.Width / 3 * (i / 3),
                    clone.Size.Width / 3,
                    clone.Size.Height / 3
                ));
        }

    }

    public static void CloseFile()
    {
        Reader.Close();
        Reader.Dispose();
        Reader = null;
    }

    public static void DrawFromFile() {
        string filePath = "src/Area/Engenharia.csv";

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

                    if (spriteCode.Contains('h'))
                    {
                        string getHexa = spriteCode.Split('h')[1];

                        if (getHexa != "")
                        {
                            string getBinary = Convert.ToString(Convert.ToInt32(getHexa, 16), 2).PadLeft(9, '0');
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
                        }
                        else
                        {
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

