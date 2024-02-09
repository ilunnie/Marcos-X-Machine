using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class Memory
{
    public static App App { get; set; }
    public static ILevel Level { get; set; } = new SalaDigitalLevel();
    public static long Frame { get; set; }
    public static string Mode { get; set; }
    public static PointF Cursor { get; set; }
    public static MouseButtons MouseButton { get; set; }
    public static List<Entity> Entities { get; set; } = new List<Entity>();
    public static List<Projectile> Projectiles { get; set; } = new List<Projectile>();
    public static List<Entity> Colliders { get; set; } = new List<Entity>();
    public static List<CalcMap> Map { get; set ;} = new List<CalcMap>();
    public static byte[] ArrayMap { get; set; }
    public static List<Sound> Sounds { get; set; } = new List<Sound>();
    public static List<Entity> ToDelete { get; set; } = new List<Entity>();
    public static CalcMap[] Tileset { get; set; }
    public static Queue<Action> PostProcessing { get; set; } = new();

    public static void Collide()
    {
        foreach (var map in Map)
        {
            Colliders.Add(map);
        }
        foreach (var entity in Entities)
        {
            if(entity.cooldown > 0)
                entity.cooldown -= 1 * (int)Frame;
                
            Colliders.Add(entity);
        }
        foreach (var projectile in Projectiles)
        {
            projectile.Move();
            projectile.cooldown -= (int)Frame * 3;
            if (projectile.cooldown <= 0) projectile.Destroy();
            Colliders.Add(projectile);
        }
    }

    public static void Update()
    {
        Drop.Clear();
        Collide();
        Collision.VerifyCollision();
        Colliders.Clear();
        foreach (var entity in ToDelete)
        {
            Entities.Remove(entity);
            Projectiles.Remove(entity as Projectile);
            Colliders.Remove(entity);
        }
        
        foreach (var map in Map)
            map.Draw();

        foreach (var entity in Entities)
        {
            entity.Draw();
            if (entity.Mob?.Life <= 0) entity.Destroy();
        }
        foreach (var projectile in Projectiles)
            projectile.Draw();

        while (PostProcessing.Count > 0)
            PostProcessing.Dequeue().Invoke();
    }

    public static bool AllEnemiesDead => !Entities.Any(entity => entity.Mob is Bot);
}