using System.Drawing;

public class Anchor
{
    public PointF Position { get; set; }
    public bool ScreenReference { get; set; }

    public float X => Position.X;
    public float Y => Position.Y;

    public Anchor(PointF position, bool screenReference = true)
    {
        this.Position = position;
        this.ScreenReference = screenReference;
    }
}