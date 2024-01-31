using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

public class DamagedBot : Mob
{
    private bool isMoving = false;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    PointF lastPlayerPosition = PointF.Empty;
    Queue<int> nextMoves;
    Player player = null;

    public DamagedBot()
    {
        this.MaxLife = 20;
        this.Life = 20;
        this.Speed = 0.0004f;
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
            VerifyPosition(this.Entity.Position, this.nextPosition);
            nextPosition = player.Entity.Position;
            // if (player.Entity.Position != lastPlayerPosition)
            // {
            //     nextMoves = Functions.GetNextMoves(player.Entity.Position, this.Entity.Position, Memory.ArrayMap, TileSets.ColumnLength * 3);
            // }

            // if (nextMoves.Count == 0)
            //     return;
            
            // var next = nextMoves.Dequeue();
            // nextPosition = new Point(
            //     next / TileSets.ColumnLength,
            //     next % TileSets.ColumnLength
            // );

            // lastPlayerPosition = player.Entity.Position;
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

        if(isMoving)
            this.Entity.AddWalkingAnimation("enemies/damaged-bot/damaged-bot-sprites.png", Direction);
        else
            this.Entity.AddStaticAnimation("enemies/damaged-bot/damaged-bot-sprites.png", Direction);
        this.Entity.Animation = this.Entity.Animation.Skip();

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed * Memory.Frame)
        );
    }
}