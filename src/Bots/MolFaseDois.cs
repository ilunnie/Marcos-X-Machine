using System;
using System.Drawing;
using System.Threading;

public class MolFaseDois : Bot
{
    private float oldLife = 0;
    private int frames = 0;
    Rectangle MapSize = Rectangle.Empty;
    private int angleCanto = 0;

    bool started = false;

    public MolFaseDois()
    {
        Sound.StopMusics();
        var s1 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/guitarraMolFase2.wav");
        var s2 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/fase2Music.wav");

        s1.Wait(s2.Play);
        s1.Play();

        this.MaxLife = 200;
        this.Life = 200;
        this.Speed = 0.00095f;
        this.oldLife = this.Life;
    }

    public override void OnInit()
    {
        this.Entity.AddAnimation(new MelBotPlayingGuitar());
        this.Entity.Size = new SizeF(120, 130);
    }

    public override void OnDestroy()
    {
        Memory.Level.Event = new MolDying();
    }

    public override void OnFrame()
    {
        OnInit();
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

            PointF center = new PointF(this.Entity.Position.X + this.Entity.Size.Width / 2, this.Entity.Position.Y + this.Entity.Size.Height / 2);

            float angle = (float)(center.AngleTo(player.Entity.Position + player.Entity.Size / 2) * (180 / Math.PI));

            switch (frames)
            {
                case < 100:
                    this.Entity.Move(new PointF(1320, 900));

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

                    case < 350:
                    if (frames % 4 == 0)
                    {
                        switch(angleCanto){
                            case < 180:
                            this.Entity.Move(new PointF(900, 600));
                            new LaserProjectile(new PointF(900, 600))
                            {
                                Mob = this,
                                cooldown = 9000,
                                Angle = angleCanto,
                                Speed = 0.4f,
                            };
                            break;

                            case >= 180 and <= 360:
                            this.Entity.Move(new PointF(900, 1700));
                            new LaserProjectile(new PointF(900, 1700))
                            {
                                Mob = this,
                                cooldown = 9000,
                                Angle = angleCanto,
                                Speed = 0.4f,
                            };
                            break;
                            
                            
                        }
                        angleCanto += 5;
                    }
                    break;

                case < 450:
                    if (frames % 4 == 0)
                    {
                         this.Entity.Move(new PointF(1100, 1200));

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

                case < 500:
                    if (frames % 4 == 0)
                    {
                        this.Entity.Move(new PointF(1000, 800));

                        for (int i = 0; i <= 2000; i += 200)
                        {

                            new LaserProjectile(new PointF(1630, 620 + i))
                            {
                                Mob = this,
                                cooldown = 90000,
                                Angle = 180,
                                Speed = 0.6f,
                            };
                           
                        }

                    }
                    break;

                case < 600:
                    this.Entity.Move(new PointF(1300, 800));

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

                case < 700:
                    this.Entity.Move(new PointF(900, 1000));

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
                
                case < 800 : 
                    frames = 0;
                    break;


                
            }
            frames++;
        }

    }
}
