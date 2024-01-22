using System.Drawing;

public interface IAnimation
{
    public IAnimation Next { get; set; }
    public IAnimation NextFrame();
    public void Draw(PointF position, SizeF size, Hitbox hitbox, int angle = 0, int layer = 1);
    public IAnimation Clone();
    public IAnimation Skip();
}