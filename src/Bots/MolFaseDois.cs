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
        s1.Play();

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

            float angle = (float)(center.AngleTo(player.Entity.Position + player.Entity.Size / 2) * (180 / Math.PI));

            switch (frames)
            {
                case < 100:
                    this.Entity.Move(new PointF(1320,900));

                    if (frames % 4 == 0)
                    {
                        for (int i = 0; i <= 2000; i += 200)
                            new LaserProjectile(new PointF(150, 600 + i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                            };
                    }
                    break;

                case < 300:
                    if (frames % 4 == 0)
                    {
                        this.Entity.Move(new PointF(1200, 1200));

                        for (int i = 0; i <= 2000; i += 200)
                            new LaserProjectile(new PointF(150 + i, 600))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                                Angle = 90
                            };
                    }
                    break;

                case < 400:
                    if (frames % 4 == 0)
                    {
                        this.Entity.Move(new PointF(1000, 800));

                        for (int i = 0; i <= 2000; i += 200){

                            new LaserProjectile(new PointF(1630, 620 + i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Angle = 180,
                                Speed = 0.6f,
                            };
                            new LaserProjectile(new PointF(150 + i, 600))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Speed = 0.6f,
                                Angle = 90
                            };
                        }

                    }
                    break;

                case < 500:
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

                case < 600:
                    this.Entity.Move(new PointF(900, 780));

                    if (frames % 4 == 0)
                    {

                        for (int i = 0; i < 360; i += 45)
                            new LaserProjectile(center)
                            {
                                Mob = this,
                                cooldown = 9000,
                                Angle = i,
                                Speed = 0.4f,
                            };
                    }
                    break;

                case < 700:
                    frames = 0;
                    break;



            }

            frames++;
        }

    }
}
