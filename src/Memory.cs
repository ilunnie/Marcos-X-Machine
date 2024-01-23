using System.Collections.Generic;
using System.Drawing;

public static class Memory
{
    public static long Frame { get; set; }
    public static string Mode { get; set; }
    public static PointF Cursor { get; set; }
    public static List<Entity> Entities { get; set; } = new List<Entity>();

    public static Sprite[,] Tileset { get; set; }
}