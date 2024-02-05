using System.Drawing;

public class SpinAnimation : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }
    public SubImage Image { get; set; } = null;
    public PointF AnchorPosition { get; set; } = PointF.Empty;
    public float Angle { get; set; } = 0;
    public bool AnchorScreenReference = false;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, float angle = 0, int layer = 1)
    {
        SizeF relativeSize = Functions.ProportionalSize(Image.Width, Image.Height, size);
        PointF camPosition = position.PositionOnCam();

        Sprite sprite = new Sprite(Image, hitbox, camPosition, relativeSize, Angle, layer);
        sprite.SetAnchor(AnchorPosition, AnchorScreenReference);
        Screen.Queue.Add(sprite);
    }

    public IAnimation NextFrame()
    {
        Angle += 20;

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