using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class FrenteEtsLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InicialPosition => new PointF(940, 730);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InicialPosition) };

    public bool IsLoaded { get; set; } = false;
    public Loader Loader => new FrenteEtsLoad();

    public void OnFrame()
    {
        Player.Entity.FocusCam();
        Player.OnFrame();
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        Player.OnKeyDown(o, e);
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        Player.OnKeyUp(o, e);
    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        Player.OnMouseMove(o, e);
    }
}