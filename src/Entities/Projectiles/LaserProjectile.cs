using System.Drawing;

public class LaserProjectile : Projectile
{
    public LaserProjectile(PointF position)
    {
        this.Name = "Laser Projectile";

        this.Inicial = position;
        this.Size = new SizeF(12, 12);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 3;

        this.AddStaticAnimation("projectiles/projectiles-sprite.png", 9, 3, 10, 4);
    }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob?.Life > 0 || entity is CalcMap)
            this.Destroy();
    }
}