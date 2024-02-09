using System;
using System.Collections.Generic;
using System.Drawing;

public class IceStaff : Entity
{
    private float Angle = 0;
    public IceStaff(PointF position)
    {
        this.Name = "Ice Staff";

        this.Size = new SizeF(60, 130);
        this.Position = position;


        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        this.Anchor = new PointF(0, Size.Height / 2);

        Image sprite = SpriteBuffer.Current.Get("src/sprites/guns/ice-staff.png");

        this.AddAnimation(new LoopAnimation()
        {
            Image = sprite,
            SpritesQuantity = 10,
            SpritesLine = 0,
            ImageWidth = 10,
            ImageHeight = 1,
            AnchorPosition = Anchor
        });
        this.Thumbnail = sprite.Cut(0, 0, 10);
    }
    public IceStaff() : this(new PointF(0, 0)) {}
    public override void Draw(float angle = 0, int layer = 1)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 1200;
        this.recoil = 0;
        
        var size = this.Size.Width * 0.5f;
        var altura = this.Size.Height * 0.7f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + altura * sin, Position.Y + sin * size - altura * cos);

        new D20Projectile(happyPoint){
            Mob = this.Mob,
            cooldown = 5000,
            Angle = Angle,
            Speed = 1f,
        };
        
        Sound.OpenFrom(SoundType.Effect, "src/Sounds/Enemies/BolemBot/iceWand.wav").Play();
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}