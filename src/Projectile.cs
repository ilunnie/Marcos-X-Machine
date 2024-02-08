using System;
using System.Drawing;

public abstract class Projectile : Entity
{
    public Entity entity {get; set; }
    public float Speed { get; set; }
    public PointF Inicial { get; set; }
    public float Angle { get; set; }

    public override void Spawn() => Memory.Projectiles.Add(this);
    public override void Destroy() => Memory.ToDelete.Add(this);
    public virtual void Move()
    {
        double distance = this.Position.Distance(Inicial);

        this.Position = new PointF(
            (float)(Inicial.X + (distance + Speed * Memory.Frame) * Math.Cos(Angle * (Math.PI / 180))),
            (float)(Inicial.Y + (distance + Speed * Memory.Frame) * Math.Sin(Angle * (Math.PI / 180)))
        );
    }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob == this.Mob)
            return;


        if (entity is Projectile)
            return;

        if (this.Mob is Bot && entity.Mob is Bot) return;

        if (entity.Mob?.Life > 0 || entity is CalcMap)
            this.Destroy();
    }
}