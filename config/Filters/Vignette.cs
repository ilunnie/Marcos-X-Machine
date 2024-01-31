using System.Drawing;

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
        
    }
}