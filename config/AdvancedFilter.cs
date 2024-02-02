using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

public abstract class AdvancedFilter : IFilter
{
    public void Add()
        => Screen.Filters.Add(this);

    public void Remove()
        => Screen.Filters.Remove(this);

    public void Apply(Graphics graphics, Bitmap input)
    {
        long[] r, g, b;
        (r, g, b) = extract(input);

        var result = input.Clone() as Bitmap;
        var data = result.LockBits(
            new Rectangle(0, 0, result.Width, result.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        ApplyFilter(data, r, g, b);

        result.UnlockBits(data);
        graphics.DrawImage(result, new Point(0, 0));
    }

    protected abstract unsafe void Apply(byte* im, long* r, long* g, long* b, int width, int height, int stride);

    private (long[] r, long[] g, long[] b) extract(Bitmap input)
    {
        int width = input.Width;
        int height = input.Height;

        var data = input.LockBits(
            new Rectangle(0, 0, width, height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        long[] r = new long[width * height];
        long[] g = new long[width * height];
        long[] b = new long[width * height];

        unsafe
        {
            byte* img = (byte*)data.Scan0;
            int stride = data.Stride;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    int offset = y * stride + x * 3;

                    r[index] = img[offset + 2];
                    g[index] = img[offset + 1];
                    b[index] = img[offset];
                }
            }
        }

        input.UnlockBits(data);
        return (r, g, b);
    }
    private unsafe void ApplyFilter(BitmapData data, long[] r, long[] g, long[] b)
    {
        fixed (long* rPointer = r, gPointer = g, bPointer = b)
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            long* rp = rPointer, gp = gPointer, bp = bPointer;
            Apply(p, rp, gp, bp, data.Width, data.Height, data.Stride);
        }
    }
}