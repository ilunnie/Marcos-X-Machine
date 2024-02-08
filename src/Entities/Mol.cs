using System.Collections.Generic;
using System.Drawing;

public class MolEntity : Entity
{
    public MolEntity(PointF position){
        this.Name = "Mol";

        this.Size = new SizeF(150, 160);
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

        this.AddStaticAnimation("bosses/mel-bot/mel-bot-sprites.png");
        this.Thumbnail = SpriteBuffer.Current.Get("src/sprites/bosses/mel-bot/mel-talk.png");
    }

    public MolEntity() : this(new PointF(0, 0)) {}

    public override void Destroy()
    {
        if (Mob is not null) Mob.OnDestroy();
    }
}