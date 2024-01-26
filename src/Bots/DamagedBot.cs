using System;
using System.Diagnostics;
using System.Drawing;

public class DamagedBot : Mob
{
    private bool isMoving = false;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    Player player = null;

    public DamagedBot()
    {
        this.MaxLife = 20;
        this.Life = 20;
        this.Speed = 0.5f;
    }

    public override void OnFrame()
    {
        if (player == null)
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;
            }

        if (player.Life > 0)
        {
            isMoving = true;
            nextPosition = player.Entity.Position;
        }
        else
        {
            rectangle = new Rectangle(
                (int)this.Entity.Position.X - 200,
                (int)this.Entity.Position.Y - 200,
                400, 400);
            isMoving = true;

            if (nextPosition == PointF.Empty || this.Entity.Position.Distance(nextPosition) < 100)
                nextPosition = new PointF(
                    Random.Shared.Next(rectangle.X, rectangle.X + rectangle.Width),
                    Random.Shared.Next(rectangle.Y, rectangle.Y + rectangle.Height)
                );
        }

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed)
        );
    }
}