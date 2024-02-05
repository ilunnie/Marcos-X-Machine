using System;
using System.Drawing;
using System.Windows.Forms;

public abstract class Loader
{
    public int Xmax = int.MinValue;
    public int Ymax = int.MinValue;
    public int Xmin = int.MaxValue;
    public int Ymin = int.MaxValue;
    public void LoadScreen(Player player, Graphics g, PictureBox pb, Image background_image = null, bool percent = false)
    {
        LoadProcessBuilder builder = new LoadProcessBuilder();
        Init(player, builder);

        // var background = Background.Random()
        //     .GetThumbnailImage(pb.Width, pb.Height, null, nint.Zero);
        Action background = background_image != null ?
            () => g.DrawImage(background_image, Point.Empty) :
            () => g.FillRectangle(Brushes.Black, 0, 0, pb.Width, pb.Height);
        var gap = 10;
        var size = 30;
        var font = new Font("Arial", size);
        
        int loadPercent = 0;
        int oldLoadPercent = -1;
        while (loadPercent < 100)
        {
            if (loadPercent != oldLoadPercent)
            {
                oldLoadPercent = loadPercent;
                background.Invoke();
                if (percent)
                    g.DrawString(loadPercent.ToString("0") + "%", 
                        font, Brushes.White,
                        gap, Camera.Size.Height - size * 1.5f - gap);
                pb.Refresh();
            }

            loadPercent = builder.LoadNext();
        }
        Memory.Frame = 0;
    }
    protected abstract void Init(Player player, LoadProcessBuilder builder);
}