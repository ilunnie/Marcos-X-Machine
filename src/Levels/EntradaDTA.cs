using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class EntradaDTALevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InitialPosition => new PointF(2950, 300);
    private PointF playerPosition = PointF.Empty;
    public PointF PlayerPosition { get => playerPosition; set => playerPosition = value; }
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InitialPosition) };
    public bool IsLoaded { get; set; } = false;
    public Loader Loader => new EntradaDTAload();
    private Image backgroundLoad = null;
    public Image BackgroundLoad { get => backgroundLoad; set => backgroundLoad = value; }
    private bool isClear = false;
    public bool IsClear { get => isClear; set => isClear = value; }

    public void OnFrame()
    {
        if (Event is not null) { Event = Event.OnFrame(); return; }
        Player.Entity.FocusCam();
        if (!isClear || Memory.AllEnemiesDead)
        {
            new Teleport(
                new PointF(22 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, TileSets.spriteMapSize.Height),
                new SizeF((TileSets.spriteMapSize.Width ) * 5, TileSets.spriteMapSize.Height / 3),
                new PointF(TileSets.spriteMapSize.Width, 18 * TileSets.spriteMapSize.Height),
                new FrenteAlmoxarifadoLevel()
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