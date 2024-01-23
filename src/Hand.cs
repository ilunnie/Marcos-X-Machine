using System.Drawing;

public class Hand
{
    public Mob Mob { get; set; }
    public Entity Entity { get; set; }
    public double Distance { get; set; }
    public PointF Destiny { get; private set; }

    public Hand(Mob mob, Entity entity, double distance)
    {
        this.Mob = mob;
        this.Entity = entity;
        this.Distance = distance;
    }

    public void Draw()
    {
        Entity.Move(Destiny);
        Entity.Draw(layer: 2);
    }

    public void Set(PointF point)
    {
        PointF mobPosition = Mob.Entity.Position.PositionOnCam();
        double distance = mobPosition.Distance(point);
        double t = Distance / distance;

        Destiny = Mob.Entity.Position.LinearInterpolation(point, t);
    }
}