using System;
using System.Collections.Generic;
using System.Drawing;

public class AnaoGun : Entity
{

    // public Sound sound;
    private float Angle = 0;
    public AnaoGun(PointF position)
    {
        this.Name = "Anão";

        this.Size = new SizeF(100, 50);
        this.Position = position;
        
        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        Anchor = new PointF(0, Size.Height * .75f);
        Image sprite = SpriteBuffer.Current.Get("src/sprites/guns/anaopg-gun.png");
        this.AddAnimation(new StaticAnimation(){
            Image = sprite,
            AnchorPosition = Anchor
        });
        this.Thumbnail = sprite;
    }
    public AnaoGun() : this(new PointF(0, 0)) {}

    public override void Draw(float angle = 0, int layer = 1)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 1700;
        this.recoil = 5000;
        
        var size = this.Size.Width * 0.7f;
        var altura = this.Size.Height * 0.8f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + altura * sin, Position.Y + sin * size - altura * cos);

        new AnaoProjectile(happyPoint){
            Mob = this.Mob,
            cooldown = 10000,
            Angle = Angle,
            Speed = 1.5f,
        };
        
        Sound.OpenFrom(SoundType.Effect, "src/Sounds/Guns/AnaoPG/anaoPG.wav").Play();
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}