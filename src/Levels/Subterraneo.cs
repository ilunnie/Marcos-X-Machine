using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class SubterraneoLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }
    private PointF InitialPosition => new PointF(540, 280);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InitialPosition) };
                            
    public bool IsLoaded { get; set; } = false;
    public Loader Loader => new SubterraneoLoad();
    private Image backgroundLoad = null;
    public Image BackgroundLoad { get => backgroundLoad; set => backgroundLoad = value; }
    public bool IsClear { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnFrame()
    {
        Player.Entity.FocusCam();
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnFrame();
        }
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyDown(o, e);
        }
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnKeyUp(o, e);
        }
    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Mob != null)
                entity.Mob.OnMouseMove(o, e);
        }
    }
}