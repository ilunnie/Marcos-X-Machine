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
        var random = Random.Shared.Next(1, 100);
        if (random <= 10)
            Memory.PostProcessing.Enqueue(() => new Drop(new VandalReaver(), this.Position));
        Memory.ToDelete.Add(this);
    }
}