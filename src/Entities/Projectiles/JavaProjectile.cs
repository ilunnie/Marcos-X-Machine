using System.Drawing;

public class JavaProjectile : Projectile
{
    public JavaProjectile(PointF position)
    {
        this.Name = "Java Projectile";

        this.Inicial = position;
        this.Size = new SizeF(50, 50);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 10;

        Image sprite = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new SpinAnimation() {
            Image = sprite.Cut(12, 1, 16, 3),
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }
}