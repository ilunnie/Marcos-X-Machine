using System;
using System.Drawing;

public class Teleport : Entity
{
    private PointF TpPosition = PointF.Empty;
    private ILevel Level = null;
    
    public Teleport(PointF position, SizeF size, PointF playerPosition, ILevel level)
    {
        this.Position = position;
        this.Size = size;
        this.TpPosition = playerPosition;
        this.Level = level;

        this.Level.IsLoaded = false;

        this.Hitbox.rectangles.Add(
            new RectangleF(
                0, 0,
                Size.Width, Size.Height
            )
        );
    }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob is not Player || entity is Projectile) return;

        ((Player)entity.Mob).tp = TpPosition;
        Memory.Level = this.Level;

    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        PointF camPosition = Position.PositionOnCam();

        Sprite sprite = new Sprite(null, this.Hitbox, camPosition, Size);
        Screen.Queue.Add(sprite);
    }
}