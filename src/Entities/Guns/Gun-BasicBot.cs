using System;
using System.Collections.Generic;
using System.Drawing;

public class GunBasicBotEntity : Entity
{
    private PointF Anchor { get; set; }
    private float Angle = 0;
    public GunBasicBotEntity(PointF position)
    {
        this.Name = "GunBasicBot";

        this.Size = new SizeF(70, 40);
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

        Image sprite = SpriteBuffer.Current.Get("src/Sprites/guns/basic-bot-gun.png");

        this.AddAnimation(new StaticAnimation()
        {
            Image = sprite,
            AnchorPosition = Anchor
        });

    }
    public GunBasicBotEntity() : this(new PointF(0, 0)) {}
    public override void Draw(float angle = 0, int layer = 1)
    {
        Angle = angle;
        this.Animation.Draw(new PointF(Position.X, Position.Y - (Size.Height - (Size.Height / 4))), Size, Hitbox, angle, layer);
        Animation = Animation.NextFrame();
    }

    public override void Interact()
    {
        if (cooldown > 0) return;

        this.cooldown = 1000;
        
        var size = this.Size.Width * 0.5f;

        var alturaSlKkk = this.Size.Height * 0.5f;

        var cos = MathF.Cos(MathF.PI * Angle / 180);

        var sin = MathF.Sin(MathF.PI * Angle / 180);

        var happyPoint = new PointF(Position.X + cos * size + alturaSlKkk * sin, Position.Y + sin * size - alturaSlKkk * cos);

        new BlueProjectile(happyPoint){
            Mob = this.Mob,
            cooldown = 10000,
            Angle = Angle,
            Speed = 1,
        };
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}