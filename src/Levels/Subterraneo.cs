using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class SubterraneoLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }
    private PointF InitialPosition => new PointF(940, 480);
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
    private bool isClear = false;
    public bool IsClear { get => isClear; set => isClear = value; }

    public void OnFrame()
    {
        Player.Entity.FocusCam();
        if (!IsClear && Memory.AllEnemiesDead)
        {
            new Teleport(
                new PointF(15 * TileSets.spriteMapSize.Width, 3 * TileSets.spriteMapSize.Height + TileSets.spriteMapSize.Height / 3),
                new SizeF(20 , 20),
                new PointF(1680, 600),
                new EtsLevel()
            );
        }

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