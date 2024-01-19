using System.Drawing;
using Microsoft.VisualBasic.Logging;

public static class Camera
{
    public static PointF Position { get; set; } = new PointF(0, 0);
    public static SizeF Size { get; set; }
    private static float zoom = 1;
    private static float speed = 0.9f;
    public static float Zoom
    {
        get => zoom;
        set => zoom = value <= 0 ? Zoom : value;
    }
    public static float Speed
    {
        get => speed;
        set => speed = value < 0 ? 0 : value;
    }
    public static PointF Destiny { get; private set; }

    public static void OnFrame()
    {
        if (Destiny.IsEmpty || Position.Distance(Destiny) == 0) return;

        Move();
    }
    public static void Move()
    {
        double distance = Position.Distance(Destiny);

        double t = speed * (distance / (Camera.Size.Width / Zoom));
        Position = Position.LinearInterpolation(Destiny, t);
    }
    public static void FocusCam(this Entity entity, bool motion = true)
    {
        float x = entity.Position.X + entity.Size.Width / 2 - Camera.Size.Width / (2 * Zoom);
        float y = entity.Position.Y + entity.Size.Height / 2 - Camera.Size.Height / (2 * Zoom);

        if (!motion)
            Position = new PointF(x, y);
        else
            Destiny = new PointF(x, y);
    }
    public static PointF PositionOnCam(this PointF point)
    {
        float x = (point.X - Camera.Position.X) * Zoom;
        float y = (point.Y - Camera.Position.Y) * Zoom;

        return new PointF(x, y);
    }
}