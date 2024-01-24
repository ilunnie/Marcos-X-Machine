using System.Drawing;

public abstract class Entity
{
    public string Name { get; set; }
    public PointF Position { get; protected set; }
    public SizeF Size { get; set; }
    public int damage { get; set; } = 0;
    public int cooldown { get; set; } = 0;

    public Mob Mob = null;
    public IAnimation Animation { get; set; }
    public Hitbox Hitbox { get; set; }

    public Entity() {
        this.Spawn();
    }

    public virtual void Interact() {}
    public virtual void Spawn() => Memory.Entities.Add(this);
    public virtual void Destroy() => Memory.ToDelete.Add(this);
    public virtual void OnHit(Entity entity) {}
    public virtual void OnCollision(Entity entity) {}
    public virtual void Move(PointF position)
        => this.Position = position;
    public virtual void Draw(float angle = 0, int layer = 1) {
        this.Animation.Draw(Position, Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }
}