using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class DTALevel : ILevel
{
    private IEvent nowEvent = new Spotlights();
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InitialPosition => new PointF(TileSets.spriteMapSize.Width * 7.2f, TileSets.spriteMapSize.Height * 5);

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
    private bool isClear = false;
    public bool IsClear { get => isClear; set => isClear = value; }

    public void OnFrame()
    {
        if (Event is not null) { Event = Event.OnFrame(); return; }
        Player.Entity.FocusCam();
        if (!IsClear && Memory.AllEnemiesDead)
        {
            IsClear = true;
            new Teleport(
                new PointF(7 * TileSets.spriteMapSize.Width, 3.7f * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width, TileSets.spriteMapSize.Height / 3 * 2),
                new PointF((10 * 3) * (TileSets.spriteMapSize.Width / 3), (2 + 10 * 3) * (TileSets.spriteMapSize.Height / 3)),
                new EntradaDTALevel()
            );
            player.Destiny = new PointF(
                TileSets.spriteMapSize.Width * 7.5f,
                TileSets.spriteMapSize.Height * 3);
        }
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