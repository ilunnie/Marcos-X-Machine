using System.Drawing;

public class LoopAnimation : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }
    public Image Image { get; set; } = null;
    public PointF AnchorPosition { get; set; } = PointF.Empty;
    public int SpritesQuantity { get; set; } = 0;
    public int SpritesLine { get; set; } = 0;
    public int ImageWidth { get; set; } = 16;
    public int ImageHeight { get; set; } = 3;
    public bool AnchorScreenReference = false;

    int Frame = 0;
    int State = 0;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, float angle = 0, int layer = 1)
    {
        int state = State % SpritesQuantity;
        SubImage frame = Image.Cut(state, SpritesLine, ImageWidth, ImageHeight);

        SizeF relativeSize = Functions.ProportionalSize(frame.Width, frame.Height, size * 1.2f);
        PointF camPosition = position.PositionOnCam();

        Sprite sprite = new Sprite(frame, hitbox, camPosition, relativeSize, angle, layer);
        sprite.SetAnchor(AnchorPosition, AnchorScreenReference);
        Screen.Queue.Add(sprite);
    }

    public IAnimation NextFrame()
    {
        this.Frame++;
        
        if (this.Frame % 2 == 0)
        {
            this.State++;
        }
        return this;
    }

    public IAnimation Skip()
    {
        if (this.Next is null)
            return this.Clone();

        return this.Next;
    }

    public IAnimation Clone() => new AcidAnimation()
    {
        Next = this.Next
    };
}