using System.Drawing;

public class AnaoProjectile : Projectile
{
    public AnaoProjectile(PointF position)
    {
        this.Name = "Laser Projectile";

        this.Inicial = position;
        this.Size = new SizeF(70, 60);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 20,
            this.Size.Width,
            this.Size.Height - 35
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 8;
        
        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 10, 0, 16, 3);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}