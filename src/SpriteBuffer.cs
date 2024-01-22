using System.Collections.Generic;
using System.Drawing;

public class SpriteBuffer
{
    private SpriteBuffer() { }
    private static SpriteBuffer crr = new();
    public static SpriteBuffer Current => crr;
    public static void Reset()
        => crr = new();

    private Dictionary<string, Image> map = new();

    public Image Get(string file)
    {
        if (map.ContainsKey(file))
            return map[file];
        
        var newImage = Image.FromFile(file);
        map.Add(file, newImage);
        return newImage;
    }
}