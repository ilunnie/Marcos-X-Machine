using System.Drawing;

public class VandalBulletProjectile : Projectile
{
    public VandalBulletProjectile(PointF position)
    {
        this.Name = "Vandal Projectile";

        this.Inicial = position;
        this.Size = new SizeF(70, 60);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 20,
            this.Size.Width,
            this.Size.Height - 35
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 10;
        
        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 14, 1, 16, 3);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}