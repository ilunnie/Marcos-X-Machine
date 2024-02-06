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
        this.Entity.Mob = mob;
        this.Distance = distance;
    }

    public void Draw()
    {
        if (Entity.recoil > 0)
        {
            Entity.recoil -= (int)Memory.Frame * 3;
        }

        if (Entity.cooldown > 0)
        {
            Entity.cooldown -= (int)Memory.Frame * 3;
        }
        
        Entity.Move(Destiny);
        Entity.Draw(angle: Angle);
    }

    public void Set(PointF point, bool ScreenRef = false)
    {
        float x = Mob.Entity.Position.X + Mob.Anchor.X;
        float y = Mob.Entity.Position.Y + Mob.Anchor.Y;
        PointF centerPosition = new PointF(x, y);

        double angle = ScreenRef ? centerPosition.PositionOnCam().AngleTo(point) : centerPosition.AngleTo(point);
        double distance = Distance - Math.Max(Entity.recoil, 500) / 50;

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