using System.Collections.Generic;
using System.Drawing;

public static class Memory
{
    public static long Frame { get; set; }
    public static string Mode { get; set; }
    public static PointF Cursor { get; set; }
    public static List<Entity> Entities { get; set; } = new List<Entity>();
    public static List<Projectile> Projectiles { get; set; } = new List<Projectile>();
    public static List<Entity> Colliders { get; set; } = new List<Entity>();
    public static CalcMap[] Tileset { get; set; }

    public static void Update()
    {
        foreach (var entity in Entities)
        {
            entity.Draw();
            Colliders.Add(entity);
        }
        foreach (var projectile in Projectiles)
        {
            projectile.Move();
            if (projectile.cooldown <= 0) projectile.Destroy();
        }
        Collision.VerifyCollision();
        Colliders.Clear();
    }
}