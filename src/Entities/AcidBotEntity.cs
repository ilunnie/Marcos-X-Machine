using System.Collections.Generic;
using System.Drawing;

public class AcidBotEntity : Entity
{
    public AcidBotEntity(PointF position){
        this.Name = "Basic bot";

        this.Size = new SizeF(90, 115);
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

        this.AddStaticAnimation("enemies/acid-bot/acid-bot-sprites.png");
    }

    public AcidBotEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        Memory.ToDelete.Add(this);
    }
}