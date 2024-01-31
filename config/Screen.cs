using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public sealed class Screen : IScreen
{
    private static Screen queue = null;
    private static readonly object block = new object();
    
    public static List<Sprite> Sprites { get;  private set; }
    public static List<IFilter> Filters { get; private set; }

    Screen()
    {
        Sprites = new List<Sprite>();
        Filters = new List<IFilter>();
    }
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

    public void Update(Graphics g)
    {
        this.Sort();

        foreach (var sprite in Sprites)
            g.DrawImage(sprite);
            
        foreach (var filter in Filters)
            filter.Apply(g);

        Sprites.Clear();
        Filters.Clear();
    }
}