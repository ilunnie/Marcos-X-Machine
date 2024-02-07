using System.Drawing;
using System.Windows.Forms;

public interface ILevel
{
    IEvent Event { get; set; }
    Player Player { get; }
    Loader Loader { get; }
    bool IsLoaded { get; set; }
    bool IsClear { get; set; }
    Image BackgroundLoad { get; set; }
    void Load(Graphics g, PictureBox pb)
    {
        Loader.LoadScreen(Player, g, pb, BackgroundLoad);
        IsLoaded = true;
    }
    void OnFrame();
    void OnMouseMove(object o, MouseEventArgs e);
    void OnKeyDown(object o, KeyEventArgs e);
    void OnKeyUp(object o, KeyEventArgs e);
}