using System.Drawing;
using System.Windows.Forms;

public interface ILevel
{
    public IEvent Event { get; set; }
    public Player Player { get; }
    public byte LoadPercent { get; }
    public bool Load();
    public void OnFrame();
    public void OnMouseMove(object o, MouseEventArgs e);
    public void OnKeyDown(object o, KeyEventArgs e);
    public void OnKeyUp(object o, KeyEventArgs e);
}