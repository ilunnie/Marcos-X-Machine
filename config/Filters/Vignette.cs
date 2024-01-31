using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Vignette : IFilter
{
    private float intensity = .2f;
    private int Light = 100;
    public float Intensity
    {
        get => intensity;
        set
        {
            if (value <= 1.0f && value >= 0.0f)
                intensity = value;
        }
    }
    public void Add()
        => Screen.Filters.Add(this);

    public void Remove()
        => Screen.Filters.Remove(this);

    public void Apply(Graphics g)
    {
        float width = g.VisibleClipBounds.Width;
        float height = g.VisibleClipBounds.Height;
        
        GraphicsPath path = new GraphicsPath();

        float radius = MathF.Sqrt(width * width + height * height) / 2;

        path.AddEllipse(
            width / 2 - radius,
            height / 2 - radius,
            2 * radius,
            2 * radius
        );

        ColorBlend blend = new ColorBlend();
        blend.Colors = new[] {
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255 - this.Light, 0, 0, 0),
        };
        blend.Positions = new float[] {
            0f, this.Intensity, 1f
        };

        var brush = new PathGradientBrush(path)
        {
            InterpolationColors = blend
        };
        g.FillRectangle(brush, g.VisibleClipBounds);
    }
}