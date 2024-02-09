using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class EtsLevel : ILevel
{
    private IEvent nowEvent = new Introduction2();
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InitialPosition => new PointF(640, 430);
    private PointF playerPosition = PointF.Empty;
    public PointF PlayerPosition { get => playerPosition; set => playerPosition = value; }
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InitialPosition) };
    public bool IsLoaded { get; set; } = false;

    public Loader Loader => new EtsLoad();
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
                new PointF(5 * TileSets.spriteMapSize.Width, 3 * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width, TileSets.spriteMapSize.Height / 3),
                new PointF(13 * TileSets.spriteMapSize.Width, 6.5f * TileSets.spriteMapSize.Height),
                new SalaDigitalLevel()
            );
            new Teleport(
                new PointF((1 + 21 * 3) * (TileSets.spriteMapSize.Width / 3), 3 * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width, TileSets.spriteMapSize.Height / 3),
                new PointF((2 + 8 * 3) * (TileSets.spriteMapSize.Width / 3), 5 * TileSets.spriteMapSize.Height),
                new FrenteEtsLevel()
            );
            new Teleport(
                new PointF(14 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, 3 * TileSets.spriteMapSize.Height ),
                new SizeF(20 , 20),
                new PointF(1680, 930),
                new SubterraneoLevel()
            );
            player.Destiny = new PointF(
                (2f + 21 * 3) * (TileSets.spriteMapSize.Width / 3),
                3 * TileSets.spriteMapSize.Height
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
