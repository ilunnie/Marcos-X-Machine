using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MolSecondState : IEvent
{
    public IEvent next = null;
    public IEvent Next { set => next = value; }

    private static PointF EndMap = new PointF(
        (TileSets.ColumnLength + 1) * TileSets.spriteMapSize.Width,
        (Memory.ArrayMap.Length / TileSets.ColumnLength + 1) * TileSets.spriteMapSize.Height);

    private static Player player = null;
    private static Mob mol = null;

    private static Image molPlaying = SpriteBuffer.Current.Get("src/sprites/bosses/mel-bot/mel-bot-playing-sprites.png");

    private static float CameraSpeed = Camera.Speed;
    private static float CameraZoom = Camera.Zoom;
    
    private static int frame = 0;

    private static bool WaitingClick = false;
    private static int state = 0;
    private List<Action> states = new() 
    {
        () => {
            mol.Entity.Animation = new StaticAnimation() { Image = molPlaying.Cut(6, 0, 12) };
            state++;
        },
        () => {
            mol.Entity.FocusCam();
            if (Camera.Position.Distance(Camera.Destiny) < 50) state++;
        },
        () => {
            Camera.Zoom = 3f;
            mol.Entity.FocusCam(false);
            state++;
        },
        () => {
            mol.Entity.Talk("Beep-Bop Eu,-beep-Rock Boop");
            WaitingClick = true;
        },
        () => {
            if (frame + 6 >= 12) state++;
            mol.Entity.Animation = new StaticAnimation() { Image = molPlaying.Cut(6 + (frame % 6), 0, 12) };
            frame++;
        },
        () => {
            mol.Entity.Move(new PointF(EndMap.X / 2 - mol.Entity.Size.Width / 2, EndMap.Y / 2 - mol.Entity.Size.Height / 2));
            state++;
        },
        () => {
            new MolFaseDois() {
                Entity = mol.Entity
            };
            state++;
        },
        () => {
            Camera.Zoom = CameraZoom;
            Camera.Speed = CameraSpeed;
            state++;
        }
    };

    public IEvent OnFrame()
    {
        if (state >= states.Count) return next;
        if (player == null || mol == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;

                if (entity.Mob is Mol)
                    mol = (Mol)entity.Mob;

                if (player != null && mol != null) break;
            }
            if (player is not null) player.resetMove();
            return this;
        }
        states[state].Invoke();
        return this;
    }

    public void OnKeyDown(object o, KeyEventArgs e) { }

    public void OnKeyUp(object o, KeyEventArgs e) { }

    public void OnMouseMove(object o, MouseEventArgs e) 
    {
        if (WaitingClick && e.Button == MouseButtons.Left)
        {
            state++;
            WaitingClick = false;
        }
    }
}
