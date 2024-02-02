using System.Collections.Generic;
using System.Drawing;

public class Marcos : Entity
{
    public Marcos(PointF position)
    {
        this.Name = "Marcos";
        this.cooldown = 60;
        this.Size = new SizeF(75, 100);
        this.Position = position;
        this.OldPosition = this.Position;

        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        this.AddStaticAnimation("marcos/marcos-sprites-old.png", spritesQuantX: 4);
    }
    public Marcos() : this(new PointF(0, 0)) {}

    public override void OnHit(Entity entity)
    {
        if (entity is CalcMap)
        {
            this.Position = this.OldPosition;
        }
        
        if(this.Mob is null || this.cooldown > 0)
            return;

        if(this == entity.Mob?.Entity) return;

        this.Mob.Life -= entity.damage;
        this.cooldown = entity.damage > 0 && this.Mob.Life > 0 ? 120 : 0;
        if (this.Mob.Life <= 0)
            this.Destroy();
    }
    public override void Destroy()
    {
        this.AddAnimation(new MarcosDying());
    }
}