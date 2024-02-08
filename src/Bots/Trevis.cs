using System;
using System.Drawing;

public class Trevis : Bot
{
    private int frames = 0;
    private float angle = 40;
    public Trevis()
    {
        Sound.StopMusics();
        Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/TreviBot/Hiroyuki-Sawano-Exorcist.wav")
            .Play();

        var gun = new PogSharkGun();
        this.Hands.Add(new Hand(this, gun, 0));

        this.MaxLife = 500;
        this.Life = 500;
        this.Speed = 0.0006f;
    }

    public override void OnFrame()
    {
        if (this.Life <= 0)
            Sound.StopMusics();

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
            if (frames > 700)
                frames = 0;

            else if (frames < 300)
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
            }

            else if (frames < 400 && frames > 300)
            {
                PointF centerMap = new PointF(6.5f * 3 * (this.Entity.Size.Width / 3), 5.7f * 3 * (this.Entity.Size.Height / 3));
                nextPosition = centerMap;
            }

            else if (frames < 500 && frames > 400)
            {
                for (int i = 0; i < 360; i += 45)
                {
                    if (frames % 2 == 0)
                        new BlueProjectile(new PointF(1000, 1000))
                        {
                            Mob = this,
                            cooldown = 9000,
                            Speed = 0.7f,
                            Angle = i + 45

                        };
                }
            }

            else if (frames < 700 && frames > 500)
            {
                

                if (frames % 3 == 0 )
                    for (int i = 0; i < 7; i++)
                    {
                        new BlueProjectile(new PointF(1000, 1000))
                        {
                            Mob = this,
                            cooldown = 10000,
                            Speed = 0.7f,
                            Angle = angle 

                        };
                        new BlueProjectile(new PointF(1000, 1000))
                        {
                            Mob = this,
                            cooldown = 10000,
                            Speed = 0.7f,
                            Angle = 90 + angle 

                        };
                        new BlueProjectile(new PointF(1000, 1000))
                        {
                            Mob = this,
                            cooldown = 10000,
                            Speed = 0.7f,
                            Angle = 180 + angle 

                        };
                        new BlueProjectile(new PointF(1000, 1000))
                        {
                            Mob = this,
                            cooldown = 10000,
                            Speed = 0.7f,
                            Angle = 270 + angle 

                        };

                        angle++;
                    }
            }

            else
            {
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
            this.Entity.AddWalkingAnimation("bosses/trevi-bot/trevi-bot.png", Direction);
        else
            this.Entity.AddStaticAnimation("bosses/trevi-bot/trevi-bot.png", Direction);
        this.Entity.Animation = this.Entity.Animation.Skip();

        this.Entity.Move(
            this.Entity.Position.LinearInterpolation(
                nextPosition,
                this.Speed * Memory.Frame)
        );

        frames++;
    }
}