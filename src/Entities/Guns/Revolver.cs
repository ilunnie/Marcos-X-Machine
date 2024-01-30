using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Windows.Forms;

public class RevolverEntity : Entity
{
    private PointF Anchor { get; set; }
    private float Angle = 0;
    public RevolverEntity(PointF position)
    {
        this.Name = "Revolver";

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
        Image sprite = SpriteBuffer.Current.Get("src/Sprites/guns/revolver.png");
        this.AddAnimation(new StaticAnimation(){
            Image = sprite,
            AnchorPosition = Anchor
        });
    }
    public RevolverEntity() : this(new PointF(0, 0)) {}

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
        
        var size = this.Size.Width * 0.7f;
        var alturaSlKkk = this.Size.Height * 0.8f;
        var cos = MathF.Cos(MathF.PI * Angle / 180);
        var sin = MathF.Sin(MathF.PI * Angle / 180);
        var happyPoint = new PointF(Position.X + cos * size + alturaSlKkk * sin, Position.Y + sin * size - alturaSlKkk * cos);
        var projectile = new YellowProjectile(happyPoint){
            cooldown = 10000,
            Angle = Angle,
            Speed = 5,
        };
    }

    public override void Spawn() => Memory.Colliders.Add(this);
}