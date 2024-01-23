using System.Collections.Generic;
using System.Drawing;

public class RevolverEntity : Entity
{
    public RevolverEntity(PointF position)
    {
        this.Name = "Revolver";

        this.Size = new SizeF(100, 50);
        this.Position = position;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        this.AddStaticAnimation(@"guns/revolver.png", spritesQuantX: 1);
    }
    public RevolverEntity() : this(new PointF(0, 0)) {}

    public override void Draw(float angle = 0, int layer = 1)
    {
        StaticAnimation animation = (StaticAnimation)this.Animation;
        animation.AnchorPosition = new PointF(0, Size.Height / 2);
        this.Animation = animation;
        this.Animation.Draw(Position, Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }
}