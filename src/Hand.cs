using System;
using System.Drawing;
using System.Security.Policy;

public class Hand
{
    public Mob Mob { get; set; }
    public Entity Entity { get; set; }
    public double Distance { get; set; }
    public PointF Destiny { get; private set; }
    public float Angle { get; private set; }

    public Hand(Mob mob, Entity entity, double distance)
    {
        this.Mob = mob;
        this.Entity = entity;
        this.Distance = distance;
    }

    public void Draw()
    {
        if (Entity.cooldown > 0)
            Entity.cooldown -= (int)Memory.Frame * 3;
        Entity.Move(Destiny);
        Entity.Draw(angle: Angle);
    }

    public void Set(PointF point)
    {
        float x = Mob.Entity.Position.X + Mob.Entity.Size.Width / 2;
        float y = Mob.Entity.Position.Y + Mob.Entity.Size.Height / 2;
        PointF centerPosition = new PointF(x, y);
        PointF mobPosition = centerPosition.PositionOnCam();

        double angle = mobPosition.AngleTo(point);
        double distance = Distance - Math.Max(Entity.cooldown, 500) / 50;

        Destiny = new PointF(
            (float)(x + distance * Math.Cos(angle)),
            (float)(y + distance * Math.Sin(angle))
        );
        Angle = (float)(angle * (180f / Math.PI));
    }

    public void Click()
    {
        Entity.Interact();
    }
}