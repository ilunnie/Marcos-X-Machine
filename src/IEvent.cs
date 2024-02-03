using System.Windows.Forms;

public interface IEvent
{
    void NextEvent();
    void OnFrame();
    void OnMouseMove(object o, MouseEventArgs e);
    void OnKeyDown(object o, KeyEventArgs e);
    void OnKeyUp(object o, KeyEventArgs e);
}