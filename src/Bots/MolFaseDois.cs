using System;
using System.Drawing;
using System.Threading;

public class MolFaseDois : Bot
{
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

            if (this.Life < 30)
            {



                this.Entity.Move(new PointF(this.Entity.Position.X + 400, this.Entity.Position.Y + 400));


                for (int i = 0; i < 360; i += 15)
                    new LaserProjectile(center)
                    {
                        Mob = this,
                        cooldown = 5000,
                        Angle = i,
                        Speed = 0.5f,
                    };


                float dx = player.Entity.Position.X - this.Entity.Position.X;
                float dy = player.Entity.Position.Y - this.Entity.Position.Y;

                float distanceToPlayer = (float)Math.Sqrt(dx * dx + dy * dy);


                if (distanceToPlayer > distanceFromPlayer)
                {
                    
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
        }
    }

}