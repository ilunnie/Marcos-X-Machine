using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class RevolverEntity : Entity
{
    private float Angle = 0;
    public RevolverEntity(PointF position)
    {
        this.Name = "Revolver";

        this.Size = new SizeF(100, 50);
        this.Position = position;

        Image sprite = SpriteBuffer.Current.Get("src/Sprites/guns/revolver.png");
        this.AddAnimation(new StaticAnimation(){
            Image = sprite
        });
    }
    public RevolverEntity() : this(new PointF(0, 0)) {}

    public override void Draw(float angle = 0, int layer = 1)
    {
        Angle = angle;
        StaticAnimation animation = (StaticAnimation)this.Animation;
        animation.AnchorPosition = new PointF(0, Size.Height - (Size.Height / 4));
        this.Animation = animation;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 1000;

        PointF inicial = this.Position;
        var projectile = new YellowProjectile(inicial){
            cooldown = 10000,
            Angle = Angle,
            Speed = 1
        };
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}