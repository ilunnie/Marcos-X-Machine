using System;
using System.Drawing;

public abstract class Projectile : Entity
{
    public float Speed { get; set; }
    public PointF Inicial { get; set; }
    public float Angle { get; set; }

    public override void Spawn() => Memory.Projectiles.Add(this);
    public override void Destroy() => Memory.Projectiles.Remove(this);
    public virtual void Move()
    {
        double distance = this.Position.Distance(Inicial);

        this.Position = new PointF(
            (float)(Inicial.X + (distance + Speed * Memory.Frame) * Math.Cos(Angle)),
            (float)(Inicial.Y + (distance + Speed * Memory.Frame) * Math.Sin(Angle))
        );
    }
}