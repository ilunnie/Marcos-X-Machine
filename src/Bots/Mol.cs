using System;
using System.Drawing;

public class Mol : Mob
{
    public bool FaseOne = true;
    private bool isMoving = false;
    private float distanceFromPlayer = 300;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    Player player = null;

    public Mol()
    {
        var gun = new ElectricRoboticGuitar();
        this.Hands.Add(new Hand(this, gun, 0));

        this.MaxLife = 20;
        this.Life = 100;
        this.Speed = 0.00095f;
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
            if (FaseOne)
            {

                float dx = player.Entity.Position.X - this.Entity.Position.X;
                float dy = player.Entity.Position.Y - this.Entity.Position.Y;

                float distanceToPlayer = (float)Math.Sqrt(dx * dx + dy * dy);

                this.Hands[hand].Set(new PointF(player.Entity.Position.X + player.Entity.Size.Width / 2, player.Entity.Position.Y + player.Entity.Size.Height / 2));
                this.Hands[hand].Click();
                this.Hands[hand].Draw();

                if (distanceToPlayer > distanceFromPlayer)
                {
                    isMoving = true;
                    PointF idealPosition = new PointF(
                        player.Entity.Position.X - (dx / distanceToPlayer) * distanceFromPlayer,
                        player.Entity.Position.Y - (dy / distanceToPlayer) * distanceFromPlayer
                    );


                    isMoving = true;
                    VerifyPosition(this.Entity.Position, this.nextPosition);
                    nextPosition = idealPosition;
                }
                else
                {
                    this.Speed = 0;
                    isMoving = false;
                }


                if (this.Life < 10)
                {
                    FaseOne = false;
                }
            }
            else
            {
                isMoving = false;

                this.Entity.Move(new PointF(400, 400));
                this.Entity.AddAnimation(new MelBotPlayingGuitar());
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
            this.Entity.AddWalkingAnimation("bosses/mel-bot/mel-bot-sprites.png", Direction);
        else
            this.Entity.AddStaticAnimation("bosses/mel-bot/mel-bot-sprites.png", Direction);
        this.Entity.Animation = this.Entity.Animation.Skip();

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed * Memory.Frame)
        );
    }
}