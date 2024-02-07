using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public sealed class Screen : IScreen
{
    private static Screen queue = null;
    private static readonly object block = new object();
    
    public static List<Sprite> Sprites { get;  private set; } = new List<Sprite>();
    public static List<IFilter> Filters { get; private set; } = new List<IFilter>();
    public static List<Sprite> GUI { get; private set; } = new List<Sprite>();

    public static Screen Queue
    {
        get
        {
            lock(block)
            {
                if (queue == null)
                    queue = new Screen();

                return queue;
            }
        }
    }

    public void Add(Sprite sprite)
    {
        Sprites.Add(sprite);
    }

    public void Delete(Sprite sprite)
    {
        Sprites.Remove(sprite);
    }

    public void Sort()
    {
        Sprites.Sort();
    }  

    public void Update(Graphics g, Bitmap bmp)
    {
        this.Sort();

        foreach (var sprite in Sprites)
            g.DrawImage(sprite);
            
        foreach (var filter in Filters)
            filter.Apply(g, bmp);

        foreach (var gui in GUI)
            g.DrawImage(gui);

        Sprites.Clear();
        Filters.Clear();
        GUI.Clear();
    }
}