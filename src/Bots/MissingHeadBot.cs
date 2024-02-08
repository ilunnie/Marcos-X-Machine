using System;
using System.Collections.Generic;
using System.Drawing;

public class MissingHeadBot : Bot
{
    public MissingHeadBot()
    {
        this.Hands.Add(new Hand(this, new RobotHead(), 0));

        this.MaxLife = 35;
        this.Life = 35;
        this.Speed = 0.0007f;
        this.distanceFromPlayer = 250;
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
            float dx = player.Entity.Position.X - this.Entity.Position.X;
            float dy = player.Entity.Position.Y - this.Entity.Position.Y;

            float distanceToPlayer = (float)Math.Sqrt(dx * dx + dy * dy);

            this.Hands[hand].Set(new PointF(player.Entity.Position.X + player.Entity.Size.Width / 2, player.Entity.Position.Y + player.Entity.Size.Height / 2));
            this.Hands[hand].Click();
            this.Hands[hand].Draw();

            if (distanceToPlayer > distanceFromPlayer )
            {
                isMoving = true;
                PointF idealPosition = new PointF(
                    player.Entity.Position.X - dx / distanceToPlayer * distanceFromPlayer,
                    player.Entity.Position.Y - dy / distanceToPlayer * distanceFromPlayer
                );
                
                int width = (int)TileSets.ColumnLength;
            
                nextMoves = Functions.GetNextMoves(
                    idealPosition,
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
            else {
                isMoving = false;
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
            this.Entity.AddWalkingAnimation("enemies/missing-head-bot/missing-head-bot-sprites.png", Direction);
        else
            this.Entity.AddStaticAnimation("enemies/missing-head-bot/missing-head-bot-sprites.png", Direction);

        this.Entity.Animation = this.Entity.Animation.Skip();

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed * Memory.Frame)
        );
    }
}