using System.Collections.Generic;

public static class Memory
{
    public static long Frame { get; set; }
    public static string Mode { get; set; }
    public static List<Entity> Entities { get; set; } = new List<Entity>();
}