using System.Collections.Generic;
using System.Drawing;

public class DamagedBotEntity : Entity
{
    public DamagedBotEntity(PointF position)
    {
        this.Name = "Damaged bot";

        this.Size = new SizeF(90, 100);
        this.Position = position;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };

        this.Hitbox = new Hitbox(rectangles);

        this.AddStaticAnimation("damaged-bot/damaged-bot-sprites.png");
    }
    public DamagedBotEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        Memory.ToDelete.Add(this);
    }
}