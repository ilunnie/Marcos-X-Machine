using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

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
    public static int ColumnLength { get; private set; } = 0;
    private static byte[] mapArray { get; set; }

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
        mapArray = new byte[Count];
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
                if (values.Length > ColumnLength) ColumnLength = values.Length;
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
        string[] tilesets = value.Split('+');
        
        for (int i = 0; i < tilesets.Length; i++)
        {
            string[] tileset = tilesets[i].Split('h');
            if (tileset[0] == "") continue;
            int index = int.Parse(tileset[0]);
            if (index == -1) continue;

            CalcMap clone = Memory.Tileset[index].Clone();
            clone.Layer = i;
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
            {
                clone.ReadHitBox(tileset[1]);
                // mapArray[column + (row - 1) * ColumnLength] = 1;
            }

            Memory.Map.Add(clone);
        }
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
            {
                clone.Hitbox.rectangles.Add(new RectangleF(
                    clone.Size.Width / 3 * (i % 3),
                    clone.Size.Width / 3 * (i / 3),
                    clone.Size.Width / 3,
                    clone.Size.Height / 3
                ));  
            }
        }
    }

    public static void CloseFile()
    {
        Memory.ArrayMap = mapArray;
        Reader.Close();
        Reader.Dispose();
        Reader = null;
    }
}

