using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class WhatsappGun : Entity
{
    private float Angle = 0;
    public WhatsappGun(PointF position)
    {
        this.Name = "Whatsapp gun";

        this.Size = new SizeF(120, 80);
        this.Position = position;


        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        Anchor = new PointF(0, Size.Height / 1.5f);

        Image sprite = SpriteBuffer.Current.Get("src/sprites/guns/whatsapp-gun.png");

        this.AddAnimation(new StaticAnimation()
        {
            Image = sprite,
            AnchorPosition = Anchor
        });
        this.Thumbnail = sprite;
    }
    public WhatsappGun() : this(new PointF(0, 0)) { }
    public override void Draw(float angle = 0, int layer = 3)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 400;
        this.recoil = 1500;

        var size = this.Size.Width * 0.5f;
        var altura = this.Size.Height * 0.55f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + altura * sin, Position.Y + sin * size - altura * cos);

        new WhatsappProjectile(happyPoint)
        {
            Mob = this.Mob,
            cooldown = 3000,
            Angle = Angle,
            Speed = 1f,
        };


        var s1 = Sound.OpenFrom(SoundType.Effect, "src/Sounds/Guns/ZapGun/zapzap.wav");
        // s1.Wait(s1.Play);
        s1.Play();
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}