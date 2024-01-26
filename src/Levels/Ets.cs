using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class EtsLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InicialPosition => new PointF(0, 0);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InicialPosition) };

    public byte LoadPercent => throw new System.NotImplementedException();


    public bool Load()
    {
        return false;
    }

    public void OnFrame()
    {
        
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        
    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        
    }

    bool ILevel.Load()
    {
        throw new System.NotImplementedException();
    }

    void ILevel.OnFrame()
    {
        throw new System.NotImplementedException();
    }

    void ILevel.OnMouseMove(object o, MouseEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    void ILevel.OnKeyDown(object o, KeyEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    void ILevel.OnKeyUp(object o, KeyEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}
