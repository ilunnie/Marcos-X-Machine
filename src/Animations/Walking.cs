using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.Layout;

public class WalkingAnimation : IAnimation
{
    private IAnimation next = null;
    public IAnimation Next { get => next; set => next = value; }
    public SubImage Image { get; set; } = null;
    private Sprite sprite = null;
    public Sprite Sprite
    {
        get {
            if (sprite is null)
                sprite = new Sprite(Image, PointF.Empty, SizeF.Empty, 0, 1);
            return sprite;
        }
        set => sprite = value;
    }
    public Walk Direction = Walk.Front;
    public float Speed = .05f;
    public int Frame = 0;

    public void Draw(PointF position, SizeF size, Hitbox hitbox, int angle = 0, int layer = 1)
    {
        SizeF relativeSize = Functions.ProportionalSize(Image.Width, Image.Height, size);
        PointF camPosition = position.PositionOnCam();

        Sprite.SetAnchor(new PointF(relativeSize.Width / 2, relativeSize.Height));
        Sprite.Hitbox = hitbox;
        Sprite.Size = relativeSize;
        Sprite.Position = camPosition;
        Screen.Queue.Add(Sprite);
    }

    public IAnimation NextFrame()
    {
        Sprite.Angle += Speed * (int)Direction * Memory.Frame;

        if (Sprite.Angle > 5)
            Direction = Walk.Back;
        else if (Sprite.Angle < -5)
            Direction = Walk.Front;

        if (Sprite.Angle < Speed &&  Sprite.Angle > -Speed && Frame > 0)
            return this.Skip();

        Frame++;
        return this;
    }

    public IAnimation Clone() => new WalkingAnimation()
    {
        Next = this.Next,
        Image = this.Image,
        Sprite = this.Sprite,
        Speed = this.Speed,
        Direction = this.Direction
    };

    public IAnimation Skip()
    {
        if (this.Next is null)
            return this.Clone();

        return this.Next;
    }
}
