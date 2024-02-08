using System;
using System.Drawing;
using System.Windows.Forms;

public class GunProjectile : Projectile
{
    public static Random rand = new Random();
    public GunProjectile(PointF position)
    {
        this.Name = "Gun";

        this.Inicial = position;
        this.Size = new SizeF(100, 80);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 8;
        
        Image sprite = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new SpinAnimation() {
            Image = sprite.Cut(10, 1, 16, 3),
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }
}