using System.Drawing;

public abstract class Entity
{
    public string Name { get; set; }
    public PointF Position { get; protected set; }
    public SizeF Size { get; set; }
    public int damage { get; set; } = 0;
    public int cooldown { get; set; } = 0;

    public Entity(Graphics g) {
        this.Spawn();
    }

    public virtual void Interact() {}
    public virtual void Spawn() {}
    public virtual void Destroy() {}
    public virtual void OnHit() {}
    public virtual void OnCollision(Entity entity) {}
    public virtual void Move(PointF position)
        => this.Position = position;
    public virtual void Draw() {}
}