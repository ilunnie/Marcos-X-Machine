using System.Drawing;

public class YellowProjectile : Projectile
{
    public YellowProjectile(PointF position)
    {
        this.Name = "Yellow Projectile";

        this.Inicial = position;
        this.Size = new SizeF(15, 15);
        this.Position = position;

        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 8, 1, 10, 4);
    }
}
