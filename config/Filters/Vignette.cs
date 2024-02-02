using System.Threading.Tasks;

public class Vignette : AdvancedFilter
{
    private float intensity = .2f;
    private int Light = 100;
    public float Intensity
    {
        get => intensity;
        set
        {
            if (value <= 1.0f && value >= 0.0f)
                intensity = value;
        }
    }

    protected override unsafe void Apply(byte* im, long* r, long* g, long* b, int width, int height, int stride)
    {
        // Parallel.For(0, height, j =>
        // {
        //     var lwidth = width;
        //     var lstride = stride;

        //     var lr = r;
        //     var lg = g;
        //     var lb = b;
        //     var lim = im;

        //     var jlwidth = j * lwidth;

        //     lr += jlwidth;
        //     lg += jlwidth;
        //     lb += jlwidth;
        //     lim += j * lstride;

        //     for (int i = 0; i < lwidth; i++, lim += 3, lr++, lg++, lb++)
        //     {
        //         *(lim + 0) = (byte)(*lb);
        //         *(lim + 1) = (byte)(*lg);
        //         *(lim + 2) = (byte)(*lr);
        //     }
        // });
    }
}