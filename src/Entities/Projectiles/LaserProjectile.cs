using System.Drawing;

public class LaserProjectile : Projectile
{
    public LaserProjectile(PointF position)
    {
        this.Name = "Laser Projectile";

        this.Inicial = position;
        this.Size = new SizeF(90, 70);
        this.Hitbox.rectangles.Add(new RectangleF(
            23, 0,
            this.Size.Width / 2,
            this.Size.Height / 4
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 8;
        
        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 13, 1, 16, 3);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}