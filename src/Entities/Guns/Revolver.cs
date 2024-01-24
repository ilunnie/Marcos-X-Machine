using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class RevolverEntity : Entity
{
    public RevolverEntity(PointF position)
    {
        this.Name = "Revolver";

        this.Size = new SizeF(100, 50);
        this.Position = position;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        Image sprite = SpriteBuffer.Current.Get("src/Sprites/guns/revolver.png");
        this.AddAnimation(new StaticAnimation(){
            Image = sprite
        });
    }
    public RevolverEntity() : this(new PointF(0, 0)) {}

    public override void Draw(float angle = 0, int layer = 1)
    {
        StaticAnimation animation = (StaticAnimation)this.Animation;
        animation.AnchorPosition = new PointF(0, Size.Height - (Size.Height / 4));
        this.Animation = animation;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        MessageBox.Show("Booom");
    }
}