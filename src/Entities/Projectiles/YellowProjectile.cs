using System.Drawing;

public class YellowProjectile : Projectile
{
    public YellowProjectile(PointF position)
    {
        this.Name = "Yellow Projectile";

        this.Inicial = position;
        this.Size = new SizeF(30, 30);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 5;

        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 8, 1, 16, 3);
    }
}
