using System.Collections.Generic;
using System.Drawing;
using NAudio.Gui;

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

        this.Thumbnail = SpriteBuffer.Current.Get("src/sprites/marcos/marcos-talk.png");
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

        this.Mob.OnDamage(entity);
        if (this.Mob.Life <= 0)
        {
            this.Destroy();
            Sound.OpenFrom(SoundType.Effect, "src/Sounds/Marcos/marcosDying.wav").PlayOnce();
        }
    }
    public override void Destroy()
    {
        this.Animation = new MarcosDying();

        if (Mob is not null) Mob.OnDestroy();
        this.Mob = null;
    }
}