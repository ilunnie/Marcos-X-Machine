using System.Collections.Generic;
using System.Drawing;

public sealed class Screen : IScreen
{
    private static Screen queue = null;
    private static readonly object block = new object();
    
    public static List<Sprite> Sprites { get;  private set; }

    Screen() { Sprites = new List<Sprite>(); }
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
        Sprite temp;
        int index;
        for (int i = 0; i < Sprites.Count; i++)
        {
            index = i;
            for (int j = i + 1; j < Sprites.Count; j++)
            {
                if (Sprites[i] > Sprites[j] && Sprites[index] > Sprites[j])
                    index = j;
            }
            if(index != i)
            {
                temp = Sprites[i];
                Sprites[i] = Sprites[index];
                Sprites[index] = temp;
            }
        }
    }

    public void Update(Graphics g)
    {
        this.Sort();
        foreach (var sprite in Sprites)
        {
            g.DrawImage(sprite);
        }
        Sprites.Clear();
    }
}