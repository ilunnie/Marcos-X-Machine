using System;
using System.Threading.Tasks;

public class RedFilter : AdvancedFilter
{
    private float intensity = .5f;
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
        Parallel.For(0, height, j =>
        {
            var lintensity = Intensity;

            var lwidth = width;
            var lstride = stride;

            var lr = r;
            var lg = g;
            var lb = b;
            var lim = im;

            var jlwidth = j * lwidth;

            lr += jlwidth;
            lg += jlwidth;
            lb += jlwidth;
            lim += j * lstride;

            for (int i = 0; i < lwidth; i++, lim += 3, lr++, lg++, lb++)
            {
                var B = *lb * (1 - lintensity);
                var G = *lg * (1 - lintensity);
                var R = *lr + (*lr * lintensity);

                *(lim + 0) = (byte)Math.Max(0, Math.Min(255, B));
                *(lim + 1) = (byte)Math.Max(0, Math.Min(255, G));
                *(lim + 2) = (byte)Math.Max(0, Math.Min(255, R));
            }
        });
    }
}