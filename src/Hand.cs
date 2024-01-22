using System.Drawing;

public class Hand
{
    public Mob Mob { get; set; }
    public Entity Entity { get; set; }
    public double Distance { get; set; }

    public Hand(Mob mob, Entity entity, double distance)
    {
        this.Mob = mob;
        this.Entity = entity;
        this.Distance = distance;
    }

    public void Draw(PointF point)
    {
        double distance = Mob.Entity.Position.Distance(point);
        double t = Distance / distance;

        Entity.Move(Mob.Entity.Position.LinearInterpolation(point, t));
        Entity.Draw();
    }
}