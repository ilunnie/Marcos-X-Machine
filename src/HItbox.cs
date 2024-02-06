using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Hitbox
{
    public List<RectangleF> rectangles { get; set; } = new List<RectangleF>();
    public double Angle { get; set; } = 0;
    public Pen Pen = Pens.Blue;

    public Hitbox(List<RectangleF> rectangles)
        => this.rectangles = rectangles;
    public Hitbox() : this(new List<RectangleF>()) { }

    public void Draw(Graphics g, Sprite sprite)
    {
        foreach (var rectangle in this.rectangles)
        {
<<<<<<< HEAD
            RectangleF rect = new RectangleF(
                (int)rectangle.Location.X,
                (int)rectangle.Location.Y,
=======
            PointF location = new PointF(
                rectangle.Location.X + position.X,
                rectangle.Location.Y + position.Y
            );
            Rectangle rect = new Rectangle(
                (int)location.X,
                (int)location.Y,
>>>>>>> 321ff39214a8b46d49911e6b020bec88c9d348db
                (int)(rectangle.Width * Camera.Zoom),
                (int)(rectangle.Height * Camera.Zoom)
            );
            PointF[] vertices = rect.ToPolygon(sprite.Anchor.Position, Angle);
            vertices = vertices.Select(v => new PointF(v.X + sprite.Position.X, v.Y + sprite.Position.Y)).ToArray();

            g.DrawPolygon(Pen, vertices);
        }
    }
}