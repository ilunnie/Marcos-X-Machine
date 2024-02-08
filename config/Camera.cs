using System.Drawing;
using Microsoft.VisualBasic.Logging;

public static class Camera
{
    public static PointF Position { get; set; } = new PointF(0, 0);
    public static SizeF Size { get; set; }
    public static float? MinimumLimitX { get; set; } = null;
    public static float? MaxLimitX { get; set; } = null;
    public static float? MinimumLimitY { get; set; } = null;
    public static float? MaxLimitY { get; set; } = null;
    private static float zoom = 1;
    private static float speed = 0.9f;
    public static float Zoom
    {
        get => zoom;
        set => zoom = value <= 0 || value >= 10 ? Zoom : value;
    }
    public static float Speed
    {
        get => speed;
        set => speed = value < 0 ? 0 : value;
    }
    public static PointF Destiny { get; set; }

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

    public static void MoveTo(float X, float Y, bool motion = true, float? zoom = null, bool inLimit = true)
    {
        Zoom = zoom ?? Zoom;
        float x = X - (Camera.Size.Width / (2 * Zoom));
        float y = Y - (Camera.Size.Height / (2 * Zoom));

        if (inLimit)
        {
            if (MaxLimitX - MinimumLimitX > Size.Width)
            {
                x = (float)(x + Size.Width / Zoom > MaxLimitX ? MaxLimitX - Size.Width : x);
                x = (float)(x < MinimumLimitX ? MinimumLimitX : x);
            }
            if (MaxLimitY - MinimumLimitY > Size.Height)
            {
                y = (float)(y + Size.Height / Zoom > MaxLimitY ? MaxLimitY - Size.Height : y);
                y = (float)(y < MinimumLimitY ? MinimumLimitY : y);
            }
        }

        if (!motion)
        {
            Position = new PointF(x, y);
            Destiny = Position;
        }
        else
            Destiny = new PointF(x, y);
    }
    public static void MoveTo(PointF position, bool motion = true, float? zoom = null, bool inLimit = true)
        => MoveTo(position.X, position.Y, motion, zoom, inLimit);
    public static void FocusCam(this Entity entity, bool motion = true, float? zoom = null, bool inLimit = true)
        => MoveTo(
            entity.Position.X + entity.Size.Width / 2,
            entity.Position.Y + entity.Size.Height / 2,
        motion, zoom, inLimit);
    public static PointF PositionOnCam(this PointF point)
    {
        float x = (point.X - Camera.Position.X) * Zoom;
        float y = (point.Y - Camera.Position.Y) * Zoom;

        return new PointF(x, y);
    }
}