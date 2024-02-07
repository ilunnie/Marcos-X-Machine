using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class DTALevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InitialPosition => new PointF(600, 200);

    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InitialPosition) };
                            
    public bool IsLoaded { get; set; } = false;
    public Loader Loader => new DTALoad();
    private Image backgroundLoad = null;
    public Image BackgroundLoad { get => backgroundLoad; set => backgroundLoad = value; }
    public bool IsClear { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnFrame()
    {
        if (Event is not null) { Event = Event.OnFrame(); return; }
        Player.Entity.FocusCam();
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnFrame();
        }
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        if (Event is not null) { Event.OnKeyDown(o, e); return; }
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyDown(o, e);
        }
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        if (Event is not null) { Event.OnKeyUp(o, e); return; }
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyUp(o, e);
        }
    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        if (Event is not null) { Event.OnMouseMove(o, e); return; }
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnMouseMove(o, e);
        }
    }
}