using System;
using System.Drawing;

public class NoteProjectile : Projectile
{
    public static Random rand = new Random();
    public NoteProjectile(PointF position)
    {
        this.Name = "Music note";

        this.Inicial = position;
        this.Size = new SizeF(50, 50);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 20;
        
        Image sprite = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new SpinAnimation() {
            Image = sprite.Cut(rand.Next(2, 7), 1, 16, 3)
        });
    }
}