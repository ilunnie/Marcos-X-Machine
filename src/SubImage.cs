using System.Drawing;
using System.Windows.Forms;

public class SubImage
{
    private Image img;
    private RectangleF rect;

    public float Width => rect.Width;
    public float Height => rect.Height;

    public SubImage(Image img, RectangleF rect)
    {
        this.img = img;
        this.rect = rect;
    }

    public void Draw(Graphics g, RectangleF dest)
    {
        g.DrawImage(img, dest, rect, GraphicsUnit.Pixel);
    }
}