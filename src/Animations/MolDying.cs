using System.Drawing;

public class MolDyingAnimation : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }

    private Image Image = SpriteBuffer.Current.Get("src/sprites/bosses/mel-bot/mel-bot-dying.png");

    public int State = 0;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, float angle = 0, int layer = 1)
    {
        int state = State >= 29 ? 28 : State % 29;
        SubImage frame = Image.Cut(state, 0, 29);

        SizeF relativeSize = Functions.ProportionalSize(frame.Width, frame.Height, size * 1.2f);
        PointF camPosition = position.PositionOnCam();

        Sprite sprite = new Sprite(frame, hitbox, new PointF(camPosition.X -10, camPosition.Y), relativeSize, angle, layer);
        Screen.Queue.Add(sprite);
    }

    public IAnimation NextFrame()
    {
        return this;
    }

    public IAnimation Skip()
    {
        if (this.Next is null)
            return this.Clone();

        return this.Next;
    }

    public IAnimation Clone() => new MarcosSleep()
    {
        Next = this.Next
    };
}