using System.Collections.Generic;
using System.Drawing;

public class KirbyEntity : Entity
{
    public KirbyEntity(PointF position)
    {
        this.Name = "Kirby";

        this.Size = new SizeF(120, 100);
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 1;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };

        this.Hitbox = new Hitbox(rectangles);

        this.AddStaticAnimation("enemies/kirby/kirby-sprites.png");
    }
    public KirbyEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        Memory.PostProcessing.Enqueue(() => { new Drop(this.Position); });
        Memory.ToDelete.Add(this);
    }
}