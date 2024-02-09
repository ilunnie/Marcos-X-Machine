using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MolDying : IEvent
{
    private IEvent next = null;
    public IEvent Next { set => next = value; }

    private static Mob mol = null;

    private static int quadro = 0;
    private static long frame = 0;
    private static int state = 0;
    private List<Action> states = new()  {
        () => {
            Camera.Zoom = 3;
            mol.Entity.FocusCam(false);

            mol.Entity.Animation = new MolDyingAnimation() { State = quadro };
            if (frame > 100) {
                quadro++;
                frame = 0;
                if (quadro >= 28) state++;
            };
            frame += 1 + Memory.Frame;
        },
        () => {
            mol.Entity.Animation = new MolDyingAnimation() { State = 28 };
            var deadPosition = mol.Entity.Position;
            var deadSize = mol.Entity.Size;
            var guitar = new ElectricRoboticGuitar();
            Memory.PostProcessing.Enqueue(() => new Drop(guitar, new PointF(deadPosition.X, deadPosition.Y + deadSize.Height - guitar.Size.Height / 2)));
            mol.Entity.Mob = null;
            mol.Entity = null;
            state++;
        },
        () => {
            Camera.Zoom = 1;
            state++;
        },
    };

    public IEvent OnFrame()
    {
        if (state >= states.Count) return next;
        if (mol == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is MolFaseDois)
                    mol = (MolFaseDois)entity.Mob;

                if (mol != null) break;
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
        
    }
}