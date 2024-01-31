using System.Drawing;
using System.Windows.Forms;

public abstract class Loader
{
    public int Xmax = int.MinValue;
    public int Ymax = int.MinValue;
    public int Xmin = int.MaxValue;
    public int Ymin = int.MaxValue;
    public void LoadScreen(Player player, Graphics g, PictureBox pb)
    {
        LoadProcessBuilder builder = new LoadProcessBuilder();
        Init(player, builder);

        var background = Background.Random()
            .GetThumbnailImage(pb.Width, pb.Height, null, nint.Zero);
        g.DrawImage(background, Point.Empty);
        pb.Refresh();
        
        int loadPercent = 0;
        int oldLoadPercent = -1;
        while (loadPercent < 100)
        {
            if (loadPercent != oldLoadPercent)
            {
                oldLoadPercent = loadPercent;
                g.FillRectangle(Brushes.Black, 0, 0, 80, 80);
                g.DrawString(loadPercent.ToString("0") + "%", 
                    SystemFonts.MenuFont, Brushes.White, Point.Empty);
                pb.Refresh();
            }

            loadPercent = builder.LoadNext();
        }
        Memory.Frame = 0;
    }
    protected abstract void Init(Player player, LoadProcessBuilder builder);
}