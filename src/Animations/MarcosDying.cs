using System.Drawing;

public class MarcosDying : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }

    private Image image = SpriteBuffer.Current.Get("src/Sprites/marcos/marcos-dying-sprites.png");
    public SubImage Image { get; private set; }

    int Frame = 0;
    int State = 0;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, int angle = 0, int layer = 1)
    {
        throw new System.NotImplementedException();
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
        Next = this.Next,
        Image = this.Image
    };
}