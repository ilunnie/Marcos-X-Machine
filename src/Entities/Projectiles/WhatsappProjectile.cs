using System.Drawing;

public class WhatsappProjectile : Projectile
{
    public WhatsappProjectile(PointF position)
    {
        this.Name = "Whatsapp Projectile";

        this.Inicial = position;
        this.Size = new SizeF(50, 50);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 2;

        Image sprite = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new SpinAnimation() {
            Image = sprite.Cut(15, 1, 16, 3),
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }
}