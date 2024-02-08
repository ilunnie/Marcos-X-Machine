using System.Collections.Generic;
using System.Drawing;

public class TrevisEntity : Entity
{
    public TrevisEntity(PointF position){
        this.Name = "Trevis";

        this.Size = new SizeF(150, 160);
        this.Position = position;
        this.OldPosition = this.Position;
        this.damage = 1;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width / 2,
                Size.Height
            )
        };

        this.Hitbox = new Hitbox(rectangles);

        this.AddStaticAnimation("bosses/trevi-bot/trevi-bot.png");
    }

    public TrevisEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        Memory.ToDelete.Add(this);
    }
}