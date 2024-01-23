using System.Collections.Generic;
using System.Windows.Forms;

public class Mob
{
    private Entity entity = null;
    public Entity Entity 
    {
        get => entity;
        set {
            this.entity = value;
            value.Mob = this;
        }
    }
    public List<Hand> Hands { get; set; } = new List<Hand>();
    public float MaxLife;
    public float Life;
    public float Speed = 2;

    public virtual void OnDestroy() {}
    public virtual void OnInit() {}
    public virtual void OnFrame() {}
    public virtual void OnMouseMove(object o, MouseEventArgs e) {}
    public virtual void OnKeyDown(object o, KeyEventArgs e) {}
    public virtual void OnKeyUp(object o, KeyEventArgs e) {}
}