using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class FrenteEtsLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InitialPosition => new PointF(1045, 600);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InitialPosition) };

    public bool IsLoaded { get; set; } = false;
    public Loader Loader => new FrenteEtsLoad();
    private Image backgroundLoad = null;
    public Image BackgroundLoad { get => backgroundLoad; set => backgroundLoad = value; }
    private bool isClear = false;
    public bool IsClear { get => isClear; set => isClear = value; }

    public void OnFrame()
    {
        Player.Entity.FocusCam();
        if (!IsClear && Memory.AllEnemiesDead)
        {
            IsClear = true;
            new Teleport(
                new PointF(8 * TileSets.spriteMapSize.Width, 4 * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width * 2, TileSets.spriteMapSize.Height / 3),
                new PointF((1 + 21 * 3) * (TileSets.spriteMapSize.Width / 3), 4 * TileSets.spriteMapSize.Height),
                new EtsLevel()
            );
            new Teleport(
                new PointF(TileSets.spriteMapSize.Width / 3, 6.5f * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width / 3, TileSets.spriteMapSize.Height),
                new PointF((22 * 3 + 1) * (TileSets.spriteMapSize.Width / 3), 6.5f * TileSets.spriteMapSize.Height),
                new FrenteAlmoxarifadoLevel()
            );
            player.Destiny = new PointF(
                0,
                6.5f * TileSets.spriteMapSize.Height
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