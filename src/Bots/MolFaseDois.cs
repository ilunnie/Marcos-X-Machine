using System;
using System.Drawing;
using System.Threading;

public class MolFaseDois : Bot
{
    private bool isMoving = false;
    private float distanceFromPlayer = 300;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    private float oldLife = 0;
    private int frames = 0;
    Player player = null;
    Rectangle MapSize = Rectangle.Empty;

    bool started = false;

    public MolFaseDois()
    {
        Sound.StopMusics();
        var s1 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/guitarraMolFase2.wav");
        var s2 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/fase2Music.wav");

        s1.Wait(s2.Play);
        // s1.Play();

        this.MaxLife = 200;
        this.Life = 100;
        this.Speed = 0.00095f;
        this.oldLife = this.Life;
    }

    public override void OnInit()
    {
        this.Entity.AddAnimation(new MelBotPlayingGuitar());
        this.Entity.Size = new SizeF(120, 130);

        if (player == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;
            }
            return;
        }

    }

    public override void OnFrame()
    {

        if (player.Life > 0)
        {



            PointF center = new PointF(this.Entity.Position.X + this.Entity.Size.Width / 2, this.Entity.Position.Y + this.Entity.Size.Height / 2);

            PointF PositionNow = new PointF(this.Entity.Position.X, this.Entity.Position.Y);

            float angle = (float)(center.AngleTo(player.Entity.Position + player.Entity.Size / 2) * (180 / Math.PI));

            switch (frames)
            {
                case < 100:
                    if (frames % 2 == 0)
                    {

                        if (frames == 10)
                        {
                            new LaserProjectile(new PointF(150, 150))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };


                        }

                        else if (frames == 20)
                        {
                            new LaserProjectile(new PointF(150, 250))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };
                        }
                        else if (frames == 30)
                        {
                            new LaserProjectile(new PointF(150, 550))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };
                        }

                    }
                    break;

                case < 200 and > 100:
                    if (frames % 4 == 0)
                    {

                        for (int i = 0; i <= 10; i++)
                            new LaserProjectile(new PointF(150, 50 * i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };
                    }
                    break;

                case < 300 and > 200:
                    if (frames % 4 == 0)
                    {

                        this.Entity.Move(new PointF(1650, 900));
                        for (int i = 0; i <= 10; i++)
                            new LaserProjectile(new PointF(150, 200 * i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };
                    }
                    break;

                case < 400 and > 300:
                    if (frames % 4 == 0)
                    {
                        this.Entity.Move(new PointF(1100, 500));

                        for (int i = 0; i <= 10; i++)
                            new LaserProjectile(new PointF(1870, 300 * i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Angle = 180,
                                Speed = 0.6f,
                            };

                        break;
                    }
                    break;

                case < 500 and > 400:
                    this.Entity.Move(new PointF(1400, 800));

                    if (frames % 2 == 0)
                    {


                        new LaserProjectile(center)
                        {
                            Mob = this,
                            cooldown = 5000,
                            Angle = angle,
                            Speed = 1f,
                        };
                    }

                    break;

                case < 600 and > 500:
                    this.Entity.Move(new PointF(150, 150));
                    

                        for (int i = 0; i <= 10; i++)
                            new LaserProjectile(new PointF(300 * i, 300))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Angle = 90,
                                Speed = 0.6f,
                            };
                    
                    break;

                    // case 70:
                    //     this.Entity.Move(new PointF(400, 400));

                    //     for (int i = 0; i <= 10; i++)
                    //         new LaserProjectile(new PointF(1870, 200 * i))
                    //         {
                    //             Mob = this,
                    //             cooldown = 90000,
                    //             Angle = 180,
                    //             Speed = 0.6f,
                    //         };

                    //     break;

                    // case 20:
                    //     for (int i = 0; i < 360; i += 15)
                    //         new LaserProjectile(center)
                    //         {
                    //             Mob = this,
                    //             cooldown = 4000,
                    //             Angle = i,
                    //             Speed = 0.4f,
                    //         };
                    //     break;

            }

            frames++;
        }

    }
}


//ataque em sol

// for (int i = 0; i < 360; i += 15)
// new LaserProjectile(center)
// {
// Mob = this,
// cooldown = 4000,
// Angle = i,
// Speed = 0.4f,
// };

// distancia para o player

// float dx = player.Entity.Position.X - this.Entity.Position.X;
// float dy = player.Entity.Position.Y - this.Entity.Position.Y;
// float distanceToPlayer = (float)Math.Sqrt(dx * dx + dy * dy);
// if (distanceToPlayer > distanceFromPlayer)
// {

// }