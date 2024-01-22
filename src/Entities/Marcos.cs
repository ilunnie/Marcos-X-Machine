using System.Collections.Generic;
using System.Drawing;

public class Marcos : Entity
{
    public Marcos(PointF position)
    {
        this.Name = "Marcos";

        this.Size = new SizeF(75, 100);
        this.Position = position;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        this.AddWalkingAnimation("marcos/marcos-sprites-old.png");
    }
    public Marcos() : this(new PointF(0, 0)) {}
}