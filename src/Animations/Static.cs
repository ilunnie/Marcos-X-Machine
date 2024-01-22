using System.Drawing;

public class StaticAnimation : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }

    public Bitmap Image = null;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, int angle = 0, int layer = 1)
    {
        SizeF relativeSize = Functions.ProportionalSize(Image.Width, Image.Height, size);
        PointF camPosition = position.PositionOnCam();

        Sprite sprite = new Sprite(Image, hitbox, camPosition, relativeSize, angle, layer);
        Screen.Queue.Add(sprite);
    }

    public IAnimation NextFrame()
    {
        if (Next is null) return this;
        return Next;
    }
    public IAnimation Clone() => new StaticAnimation()
    {
        Next = this.Next,
        Image = this.Image
    };

    public IAnimation Skip()
    {
        if (this.Next is null)
            return this.Clone();

        return this.Next;
    }
}