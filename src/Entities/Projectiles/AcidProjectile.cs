using System.Drawing;

public class AcidProjectile : Projectile
{
    public AcidProjectile(PointF position)
    {
        this.Name = "Acid Projectile";

        this.Inicial = position;
        this.Size = new SizeF(100, 100);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 10;

        Image image = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new AcidAnimation() {
            Image = image,
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}