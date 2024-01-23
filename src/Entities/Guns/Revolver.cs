using System.Collections.Generic;
using System.Drawing;

public class RevolverEntity : Entity
{
    public RevolverEntity(PointF position)
    {
        this.Name = "Revolver";

        this.Size = new SizeF(1000, 500);
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
}