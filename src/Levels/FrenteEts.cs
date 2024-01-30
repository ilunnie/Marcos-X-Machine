using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class FrenteEtsLevel : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    private PointF InicialPosition => new PointF(640, 430);
    private Player player = null;
    public Player Player
        => player ??= Memory.Entities
                            .Select(entity => entity.Mob)
                            .OfType<Player>()
                            .FirstOrDefault() ?? new Player() { Entity = new Marcos(InicialPosition) };

    public decimal percent = 0;
    public decimal LoadPercent => percent;
    public Image LoadBackground = SpriteBuffer.Current.Get("src/Levels/LoadBackground.png");
    Sprite background;
    public FrenteEtsLoad Loader = new FrenteEtsLoad();
    int totalToLoad = -1;
    public FrenteEtsLevel()
    {
        background = new Sprite(
            LoadBackground, null, new PointF(0, 0), new SizeF(Camera.Size.Width, Camera.Size.Height), 0, 10
        );
        var gap = 10;
        var size = 30;
        background.Text.Add(
            new TextImage("",
            new Font("Arial", size),
            new SolidBrush(Color.White),
            new PointF(gap, Camera.Size.Height - size * 1.5f - gap))
        );
    }
    public void Load(Graphics g, PictureBox pb)
    {
        totalToLoad = Loader.Init(Player);
        while (LoadPercent != 100)
        {
            background.Text[0].Text = percent.ToString("0") + "%";

            Loader.Queue.Dequeue().Invoke();

            percent = (decimal)(totalToLoad - Loader.Queue.Count) / totalToLoad * 100;
            if (background.Text[0].Text != percent.ToString("0") + "%")
            {
                background.Draw(g);
                pb.Refresh();
            }
        }
        Memory.Frame = 0;
    }

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