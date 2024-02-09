using System;
using System.Collections.Generic;
using System.Drawing;

public class VandalReaver : Entity
{
    private float Angle = 0;
    public VandalReaver(PointF position)
    {
        this.Name = "GunBasicBot";

        this.Size = new SizeF(150, 50);
        this.Position = position;


        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };

        this.Hitbox = new Hitbox(rectangles);

        Anchor = new PointF(0, Size.Height );

        Image sprite = SpriteBuffer.Current.Get("src/sprites/guns/vandal-reaver.png");

        this.AddAnimation(new StaticAnimation()
        {
            Image = sprite,
            AnchorPosition = Anchor
        });
        this.Thumbnail = sprite;
    }
    public VandalReaver() : this(new PointF(0, 0)) {}
    public override void Draw(float angle = 0, int layer = 3)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 1000;
        this.recoil = 1000;

        var size = this.Size.Width * 0.5f;
        var altura = this.Size.Height * 1f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + altura * sin, Position.Y + sin * size - altura * cos);

        new VandalBulletProjectile(happyPoint){
            Mob = this.Mob,
            cooldown = 4000,
            Angle = Angle,
            Speed = 1f,
        };

        Sound.OpenFrom(SoundType.Effect, "src/Sounds/Guns/VandalReaver/saqueadora.wav").Play();
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}