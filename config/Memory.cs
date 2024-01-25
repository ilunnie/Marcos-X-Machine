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
    public static List<CalcMap> MapWithCollision { get; set ;} = new List<CalcMap>();
    public static List<Entity> ToDelete { get; set; } = new List<Entity>();
    public static CalcMap[] Tileset { get; set; }

    public static void Collide()
    {
        foreach (var map in MapWithCollision)
        {
            Colliders.Add(map);
        }
        foreach (var entity in Entities)
        {
            Colliders.Add(entity);
        }
        foreach (var projectile in Projectiles)
        {
            projectile.Move();
            projectile.cooldown -= (int)Frame * 3;
            if (projectile.cooldown <= 0) projectile.Destroy();
        }
    }

    public static void Update()
    {
        Collide();
        Collision.VerifyCollision();
        Colliders.Clear();
        foreach (var map in MapWithCollision)
        {
            map.Draw();
        }
        foreach (var entity in Entities)
        {
            entity.Draw();
        }
        foreach (var projectile in Projectiles)
        {
            projectile.Draw();
        }
        foreach (var entity in ToDelete)
        {
            Entities.Remove(entity);
            Projectiles.Remove((Projectile)entity);
            Colliders.Remove(entity);
        }
    }
}