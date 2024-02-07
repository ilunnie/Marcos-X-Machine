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

    public IEvent OnFrame()
    {
        // Screen.Filters.Add(new Lighting() { Illuminators = Illuminators });
        // return null;
        if (player == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    { player = (Player)entity.Mob; break; }
            }
            return this;
        }
        player.Entity.Talk("nois da o cu poha kkkkkk");
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
