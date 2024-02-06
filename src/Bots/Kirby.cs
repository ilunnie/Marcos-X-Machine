using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

public class Kirby : Mob
{
    private bool isMoving = false;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    PointF lastPlayerPosition = PointF.Empty;
    Stack<int> nextMoves;
    Player player = null;

    public Kirby()
    {
        this.MaxLife = 50;
        this.Life = 50;
        this.Speed = 0.001f;
    }

    public override void OnFrame()
    {
        if (player == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;
            }
            return;
        }

        if (player.Life > 0)
        {
            isMoving = true;
            int width = (int)TileSets.ColumnLength;
            
            nextMoves = Functions.GetNextMoves(
                player.Entity.Position,
                this.Entity.Position,
                Memory.ArrayMap,
                width
            );
            if (nextMoves.Count > 2)
            {
                nextMoves.Pop();
                nextMoves.Pop();
                var next = nextMoves.Pop();
                nextPosition = new PointF(
                    (next % width + 1) * TileSets.spriteMapSize.Width,
                    (next / width + 1) * TileSets.spriteMapSize.Height
                );
            }
        }
        else
        {
            rectangle = new Rectangle(
                (int)this.Entity.Position.X - 200,
                (int)this.Entity.Position.Y - 200,
                400, 400);
            isMoving = true;
            VerifyPosition(this.Entity.Position, this.nextPosition);

            if (nextPosition == PointF.Empty || this.Entity.Position.Distance(nextPosition) < 100)
                nextPosition = new PointF(
                    Random.Shared.Next(rectangle.X, rectangle.X + rectangle.Width),
                    Random.Shared.Next(rectangle.Y, rectangle.Y + rectangle.Height)
                );
        }

        if (isMoving)
            this.Entity.AddWalkingAnimation("enemies/kirby/kirby-sprites.png", Direction);
        else
            this.Entity.AddStaticAnimation("enemies/kirby/kirby-sprites.png", Direction);
        this.Entity.Animation = this.Entity.Animation.Skip();

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed * Memory.Frame
        ));
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}