using System;
using System.Threading.Tasks;

public class WhiteFilter : AdvancedFilter
{
    public float Intensity { get; set; } = .5f;
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
                var B = *lb + lintensity;
                var G = *lg + lintensity;
                var R = *lr + lintensity;

                *(lim + 0) = (byte)Math.Max(0, Math.Min(255, B));
                *(lim + 1) = (byte)Math.Max(0, Math.Min(255, G));
                *(lim + 2) = (byte)Math.Max(0, Math.Min(255, R));
            }
        });
    }
}