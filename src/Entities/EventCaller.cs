using System.Drawing;

public class EventCaller : Entity
{
    private IEvent Event { get; set; }
    private bool IsUnique { get; set; }
    public EventCaller(PointF position, SizeF size, IEvent ievent, bool isUnique = false)
    {
        this.Position = position;
        this.Size = size;
        this.Event = ievent;

        this.Hitbox.rectangles.Add(
            new RectangleF(
                0, 0,
                Size.Width, Size.Height
            )
        );
        this.IsUnique = isUnique;
    }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob is not Player || entity is Projectile) return;
        Memory.Level.Event = this.Event;
        if (this.IsUnique) Memory.Entities.Remove(this);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        PointF camPosition = Position.PositionOnCam();

        Sprite sprite = new Sprite(null, this.Hitbox, camPosition, Size);
        Screen.Queue.Add(sprite);
    }
}