using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class EtsLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InicialPosition => new PointF(540, 280);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InicialPosition) };

    public byte LoadPercent => 0;
    public Image LoadBackground = SpriteBuffer.Current.Get("Levels/LoadBackground");


    public void Load()
    {

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
}
