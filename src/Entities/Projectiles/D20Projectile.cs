using System.Drawing;

public class D20Projectile : Projectile
{
    public D20Projectile(PointF position)
    {
        this.Name = "Ice D20";

        this.Inicial = position;
        this.Size = new SizeF(40, 40);
        this.Hitbox.rectangles.Add(new RectangleF(
            0, 0,
            this.Size.Width,
            this.Size.Height
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 12;
        
        Image image = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new LoopAnimation() {
            Image = image,
            SpritesQuantity = 10,
            SpritesLine = 0,
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}