using System.Drawing;

public class MarcosDying : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }

    private Image Image = SpriteBuffer.Current.Get("src/Sprites/marcos/marcos-dying-sprites.png");

    int Frame = 0;
    int State = 0;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, float angle = 0, int layer = 1)
    {
        int state = State >= 16 ? 15 : State % 16;
        SubImage frame = Image.Cut(state, 0, 16);

        SizeF relativeSize = Functions.ProportionalSize(frame.Width, frame.Height, size);
        PointF camPosition = position.PositionOnCam();

        Sprite sprite = new Sprite(frame, hitbox, camPosition, relativeSize, angle, layer);
        Screen.Queue.Add(sprite);
    }

    public IAnimation NextFrame()
    {
        this.Frame++;
        if (this.Frame % 8 == 0)
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

    public IAnimation Clone() => new MarcosDying()
    {
        Next = this.Next
    };
}