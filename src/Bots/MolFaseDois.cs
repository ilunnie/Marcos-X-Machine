using System;
using System.Drawing;
using System.Threading;

public class MolFaseDois : Mob
{
    private bool isMoving = false;
    private float distanceFromPlayer = 300;
    Rectangle rectangle = Rectangle.Empty;
    PointF nextPosition = PointF.Empty;
    Player player = null;

    bool started = false;

    public MolFaseDois()
    {
        var s1 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/guitarraMolFase2.wav");
        var s2 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/fase2Music.wav");

        s1.Wait(() => s2.Play());
        s1.Play();

        this.MaxLife = 20;
        this.Life = 10;
        this.Speed = 0.00095f;

        var Leser = new LaserProjectile(new PointF(200, 200))
        {
            Speed = 1
        };
        this.Hands.Add(new Hand(this, Leser, 90));
    }

    public override void OnInit()
    {
        this.Entity.AddAnimation(new MelBotPlayingGuitar());
    }

    public override void OnFrame()
    {


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

        if(this.Life == 0)
            SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Enemies/MelBot/guitarraMolFase2.wav", 0);


        if (player.Life > 0)
        {
            this.Hands[hand].Set(new PointF(player.Entity.Position.X + player.Entity.Size.Width / 2, player.Entity.Position.Y + player.Entity.Size.Height / 2));
            this.Hands[hand].Click();
            this.Hands[hand].Draw();
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