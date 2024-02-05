using System.Drawing;

public class LaserProjectile : Projectile
{
    public LaserProjectile(PointF position)
    {
        this.Name = "Laser Projectile";

        this.Inicial = position;
        this.Size = new SizeF(50, 50);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 8;
        
        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 9, 3, 10, 4);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}