using System.Windows.Forms;

public interface IEvent
{
    IEvent Next { set; }
    IEvent OnFrame();
    void OnMouseMove(object o, MouseEventArgs e);
    void OnKeyDown(object o, KeyEventArgs e);
    void OnKeyUp(object o, KeyEventArgs e);
}