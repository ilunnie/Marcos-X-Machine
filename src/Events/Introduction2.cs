using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Introduction2 : IEvent
{
    public IEvent next = null;
    public IEvent Next { set => next = value; }

    private static Player player = null;
    private static RobertBosch robertBosch = null;

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
            state++;
        },
        () => {
            robertBosch.FocusCam(false);
            robertBosch.Talk("Pega isso aqui");
            WaitingClick = true;
        },
        () => {
            new Drop(
                new Revolver(),
                robertBosch.Position + robertBosch.Size * .5f
            );
            state++;
        },
        () => {
            robertBosch.FocusCam();
            robertBosch.Talk("Você vai precisar!");
            WaitingClick = true;
        },
        () => {
            player.Entity.FocusCam();
            player.Entity.Talk("Que pistolinha ruim é essa?\nPra que eu vou usar isso?");
            WaitingClick = true;
        },
        () =>
        {
            robertBosch.Destroy();
            Camera.Zoom = 1;
            player.Entity.FocusCam(false);
            state++;
        },
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
                player.resetMove();
                if (!player.tp.IsEmpty)
                {
                    player.Entity.Move(player.tp);
                    player.tp = PointF.Empty;
                }
                player.Entity.Animation = new StaticAnimation()
                {
                    Image = SpriteBuffer.Current.Get("src/sprites/marcos/marcos-sprites-old.png").Cut(1)
                };
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