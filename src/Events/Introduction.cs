using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Introduction : IEvent
{
    public IEvent next = null;
    public IEvent Next { set => next = value; }

    private static Player player = null;
    private static RobertBosch robertBosch = null;

    private static PointF door = new PointF(
        14.5f * TileSets.spriteMapSize.Width,
        6.7f * TileSets.spriteMapSize.Height
    );

    private static IAnimation playerAnimation = null;
    private static long frame = 0;

    private static bool WaitingClick = false;
    private static bool Clicking = false;
    private static int state = 0;
    private List<Action> states = new() {
        () => {
            robertBosch = new RobertBosch(PointF.Empty);
            robertBosch.Move(new PointF(
                player.Entity.Position.X - player.Entity.Size.Width * 1.5f,
                player.Entity.Position.Y - player.Entity.Size.Height / 2
            ));
            Camera.Zoom = 3;
            robertBosch.Name = "???";
            state++;
        },
        () => {
            robertBosch.FocusCam(false);
            robertBosch.Talk("Maaarcos!!!");
            WaitingClick = true;
        },
        () =>
        {
            if (frame > 30) state++;
            frame += 1 + Memory.Frame;
        },
        () =>
        {
            frame = 0;
            player.Entity.Animation = new MarcosSleep() { State = 3 };
            state++;
        },
        () =>
        {
            if (frame > 30) state++;
            frame += 1 + Memory.Frame;
        },
        () =>
        {
            frame = 0;
            player.Entity.Animation = new MarcosSleep() { State = 2 };
            state++;
        },
        () =>
        {
            if (frame > 30) state++;
            frame += 1 + Memory.Frame;
        },
        () =>
        {
            frame = 0;
            player.Entity.Animation = new MarcosSleep() { State = 1 };
            state++;
        },
        () =>
        {
            if (frame > 200) state++;
            frame += 1 + Memory.Frame;
        },
        () =>
        {
            frame = 0;
            player.Entity.Animation = new StaticAnimation()
            {
                Image = SpriteBuffer.Current.Get("src/sprites/marcos/marcos-sprites-old.png")
                    .Cut(1)
            };
            player.Entity.FocusCam();
            state++;
        },
        () =>
        {
            if (frame > 600) state++;
            frame += 1 + Memory.Frame;
        },
        () =>
        {
            player.Entity.FocusCam();
            player.Entity.Talk("Que merda, eu to alucinando de novo");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Finalmente consegui, você veio mesmo!");
            WaitingClick = true;
        },
        () =>
        {
            player.Entity.FocusCam();
            player.Entity.Talk("O que ta acontecendo? Por que você ta no meu sonho? Eu to maluco?");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Meu Jovem, isso não é um sonho, eu preciso de ajuda para salvar a Bosche®");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Eu trouxe um funcionário para o futuro para me ajudar");
            WaitingClick = true;
        },
        () =>
        {
            player.Entity.FocusCam();
            player.Entity.Talk("Como assim funcionário? Como assim futuro? Quem é você e que lugar é esse?");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.Name = "Roberto Bosche";
            robertBosch.FocusCam();
            robertBosch.Talk("Eu sou o Robertinho, criador da Bosche.");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Nós estamos no ano 2077 e as IAs dominaram quase tudo que existe...");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Mas eu não vou deixar que peguem minha empresa!");
            WaitingClick = true;
        },
        () =>
        {
            player.Entity.FocusCam();
            player.Entity.Talk("Eu sabia que não deveria ter cheirado tanto nescau ontem, eu to ficando louco mesmo");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Que papo é esse?");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.FocusCam();
            robertBosch.Talk("Enfim, não quero saber, apenas venha comigo");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.Move(new PointF(
                robertBosch.Position.X + 10,
                robertBosch.Position.Y
            ));
            if (robertBosch.Position.X > player.Entity.Position.X)
                player.Entity.Animation = new StaticAnimation()
                {
                    Image = SpriteBuffer.Current.Get("src/sprites/marcos/marcos-sprites-old.png")
                        .Cut(1)
                };
            if (robertBosch.Position.PositionOnCam().X > Camera.Size.Width * 1.5)
                state++;
        },
        () =>
        {
            robertBosch.Destroy();
            state++;
        },
        () => 
        {
            Camera.Zoom = 1;
            player.Entity.FocusCam(false);
            player.Destiny = door;
            state++;
        },
        () => 
        {
            player.Entity.Animation = playerAnimation;
            state++;
        }
    };

    public IEvent OnFrame()
    {
        if (state >= states.Count) return next;
        if (player == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;

                if (player != null) break;
            }
            if (player is not null)
            {
                playerAnimation = player.Entity.Animation;
                player.Entity.Animation = new MarcosSleep();
            }
            return this;
        }
        states[state].Invoke();
        return this;
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {

    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {

    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.None) Clicking = false;
        if (WaitingClick && e.Button == MouseButtons.Left && !Clicking)
        {
            state++;
            WaitingClick = false;
            Clicking = true;
        }
    }
}
