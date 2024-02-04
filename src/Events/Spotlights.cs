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

    public IEvent OnFrame()
    {
        Screen.Filters.Add(new Lighting() { Illuminators = Illuminators });
        // Screen.Filters.Add(new Lighting() { Illuminators = Illuminators });
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnFrame();
        }
        return this;
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyDown(o, e);
        }
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyUp(o, e);
        }
    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnMouseMove(o, e);
        }
    }
}
