using System.Collections.Generic;
using System.Drawing;

public class Hitbox
{
    public List<RectangleF> rectangles { get; set; } = new List<RectangleF>();
    public Pen Pen = Pens.Blue;

    public Hitbox(List<RectangleF> rectangles)
        => this.rectangles = rectangles;
    public Hitbox() : this(new List<RectangleF>()) { }

    public void Draw(Graphics g, PointF position)
    {
        foreach (var rectangle in this.rectangles)
        {
            PointF location = new PointF(
                rectangle.Location.X + position.X,
                rectangle.Location.X + position.Y
            );
            Rectangle rect = new Rectangle(
                (int)location.X,
                (int)location.Y,
                (int)(rectangle.Width * Camera.Zoom),
                (int)(rectangle.Height * Camera.Zoom)
            );
            g.DrawRectangle(Pen, rect);
        }
    }
}