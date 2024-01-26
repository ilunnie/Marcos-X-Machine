using System.Drawing;

public class YellowProjectile : Projectile
{
    public YellowProjectile(PointF position)
    {
        this.Name = "Yellow Projectile";

        this.Inicial = position;
        this.Size = new SizeF(15, 15);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.damage = 5;

        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 8, 1, 10, 4);
    }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob?.Life > 0 || entity is CalcMap)
            this.Destroy();
    }
}
