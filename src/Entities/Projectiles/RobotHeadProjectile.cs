using System.Drawing;

public class RobotHeadProjectile : Projectile
{
    public RobotHeadProjectile(PointF position)
    {
        this.Name = "Robot head";

        this.Inicial = position;
        this.Size = new SizeF(90, 70);
        this.Hitbox.rectangles.Add(new RectangleF(
            23, 0,
            this.Size.Width / 2,
            this.Size.Height / 4
        ));
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 1;
        
        Image image = SpriteBuffer.Current.Get("src/sprites/projectiles/projectiles-sprite.png");
        this.AddAnimation(new LoopAnimation() {
            Image = image,
            SpritesQuantity = 2,
            SpritesLine = 1,
            AnchorPosition = new PointF(Size.Width / 2, Size.Height / 2)
        });
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        base.Draw(Angle, layer);
    }
}