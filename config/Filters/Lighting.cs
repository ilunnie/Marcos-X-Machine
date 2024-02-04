using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

public class Lighting : AdvancedFilter
{
    public List<PointF> Illuminators { get; set; } = new List<PointF>();
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
        var illuminators = Illuminators.ToArray();
        Parallel.For(0, height, j =>
        {
            var lilluminators = illuminators;
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
                double pixelIntensity = 0;
                foreach (var light in lilluminators)
                {
                    var deltaX = light.X - i;
                    var deltaY = light.Y - j;
                    var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    
                    var lightContribution = 255 * 2 * Math.Exp(-distance * (1 - lintensity) * 0.01);
                    pixelIntensity += lightContribution;
                }

                var B = *lb * pixelIntensity / 255;
                var G = *lg * pixelIntensity / 255;
                var R = *lr * pixelIntensity / 255;

                *(lim + 0) = (byte)Math.Max(0, Math.Min(255, B));
                *(lim + 1) = (byte)Math.Max(0, Math.Min(255, G));
                *(lim + 2) = (byte)Math.Max(0, Math.Min(255, R));
            }
        });
    }
}