using System;
using System.Collections.Generic;
using System.Drawing;

public class ZagoBotEntity : Entity
{
    public ZagoBotEntity(PointF position){
        this.Name = "Zago bot";

        this.Size = new SizeF(110, 100);
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

        this.AddStaticAnimation("enemies/zago-bot/zago-bot-sprites.png");
    }

    public ZagoBotEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        Memory.ToDelete.Add(this);
        if (Random.Shared.Next(0, 10) == 7)
            new Drop(new VandalReaver(), this.Position);
    }
}