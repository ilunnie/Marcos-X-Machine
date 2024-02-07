using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Spotlights : IEvent
{
    public IEvent Next { set => throw new System.NotImplementedException(); }
    public List<PointF> Illuminators = new List<PointF>() {
        new PointF(0, 0),
        new PointF(1000, 600)
    };

    private Player player = null;
    private Mob mol = null;

    public IEvent OnFrame()
    {
        // Screen.Filters.Add(new Lighting() { Illuminators = Illuminators });
        return null;
        if (player == null || mol == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;

                if (entity.Mob is Mol or MolFaseDois)
                    mol = entity.Mob;

                if (player != null && mol != null) break;
            }
            return this;
        }
        mol.Entity.Talk("nois da o cu poha kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        mol.Entity.FocusCam(false);
        Camera.Zoom = 2;
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
