using System.Drawing;

public abstract class Entity
{
    public string Name { get; set; }
    public PointF Position { get; protected set; }
    private PointF oldPosition = PointF.Empty;
    public PointF OldPosition
    {
        get => oldPosition.IsEmpty ? Position : oldPosition;
        protected set { oldPosition = value; }
    }

    public SizeF Size { get; set; }
    public int damage { get; set; } = 0;
    public int cooldown { get; set; } = 0;
    public int recoil { get; set; } = 0;

    public Mob Mob = null;
    public IAnimation Animation { get; set; }
    public Hitbox Hitbox { get; set; } = new Hitbox();
    public PointF Anchor { get; protected set; }

    public SubImage Thumbnail { get; protected set; } = SpriteBuffer.Current.Get("src/sprites/default-talk.png");

    public Entity()
    {
        this.Spawn();
    }

    public virtual void Interact() { }
    public virtual void Spawn()
    {
        if (Mob is not null) Mob.OnInit();
        Memory.Entities.Add(this);
    }

    public virtual void Destroy()
    {
        if (Mob is not null) Mob.OnDestroy();
        this.Mob = null;
        Memory.ToDelete.Add(this);
    }

    public virtual void OnHit(Entity entity)
    {
        if (entity is CalcMap)
        {
            this.Position = this.OldPosition;
        }
        
        if (this.Mob is null) return;
        if(this == entity.Mob?.Entity) return;
        if((this.Mob is Player) == (entity.Mob is Player)) return;

        this.Mob.Life -= entity.damage;
    }
    public virtual void OnCollision(Entity entity) { }
    public virtual void Move(PointF position)
    {
        this.OldPosition = this.Position;
        this.Position = position;
    }
    public virtual void Draw(float angle = 0, int layer = 1)
    {
        this.Animation.Draw(Position, Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }
}