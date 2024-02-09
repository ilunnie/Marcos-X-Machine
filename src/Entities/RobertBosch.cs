using System.Drawing;

public class RobertBosch : Entity
{
    public RobertBosch(PointF position)
    {
        this.Name = "Roberto Bosche";

        this.Size = new SizeF(80, 100);
        this.Position = position;
        this.OldPosition = this.Position;

        this.AddStaticAnimation("roberto/roberto-sprite.png", 0, 0, 1);
        this.Thumbnail = SpriteBuffer.Current.Get("src/sprites/roberto/roberto-talk.png");
    }

    public override void Draw(float angle = 0, int layer = 10)
    {
        base.Draw(angle, layer);
    }
}