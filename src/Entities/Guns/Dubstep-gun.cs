using System;
using System.Collections.Generic;
using System.Drawing;

public class DubstepGun : Entity
{
    private PointF Anchor { get; set; }
    private float Angle = 0;
    public DubstepGun(PointF position)
    {
        this.Name = "DubstepGun";

        this.Size = new SizeF(120, 90);
        this.Position = position;


        var rectangles = new List<RectangleF> {
            new RectangleF(
                -3, 0,
                Size.Width,
                Size.Height
            )
        };
        this.Hitbox = new Hitbox(rectangles);

        Anchor = new PointF(0, Size.Height / 2);

        Image sprite = SpriteBuffer.Current.Get("src/Sprites/guns/dubstep-gun.png");

        this.AddAnimation(new StaticAnimation()
        {
            Image = sprite,
            AnchorPosition = Anchor
        });

    }
    public DubstepGun() : this(new PointF(0, 0)) {}
    public override void Draw(float angle = 0, int layer = 3)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 800;
        this.recoil = 2000;
        
        var size = this.Size.Width * 0.5f;
        var altura = this.Size.Height * 0.5f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + altura * sin, Position.Y + sin * size - altura * cos);

        new NoteProjectile(happyPoint){
            Mob = this.Mob,
            cooldown = 5000,
            Angle = Angle,
            Speed = 1f,
        };
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}