using System;
using System.Drawing;

public class TextImage
{
    public string Text { get; set; }
    public Font Font { get; set; }
    public SolidBrush Brush { get; set; }
    public PointF Position { get; set; }
    public SizeF Size { get; set; }

    public TextImage(string text, Font font, SolidBrush brush, PointF position, SizeF? size = null)
    {
        this.Text = text;
        this.Font = font;
        this.Brush = brush;
        this.Position = position;
        this.Size = size is null ? SizeF.Empty : size.Value;
    }

    internal void Draw(Graphics g, PointF position)
        => g.DrawString(Text, Font, Brush, new RectangleF(
            Position.X + position.X,
            Position.Y + position.Y,
            Size.Width, Size.Height
        ));
}