using System;
using System.Drawing;

public class TextImage
{
    public string Text { get; set; }
    public Font Font { get; set; }
    public SolidBrush Brush { get; set; }
    public PointF Position { get; set; }

    public TextImage(string text, Font font, SolidBrush brush, PointF position)
    {
        this.Text = text;
        this.Font = font;
        this.Brush = brush;
        this.Position = position;
    }

    internal void Draw(Graphics g, PointF position)
        => g.DrawString(Text, Font, Brush, new PointF(
            Position.X + position.X,
            Position.Y + position.Y
        ));
}