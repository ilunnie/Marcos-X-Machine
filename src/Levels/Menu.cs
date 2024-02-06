using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Menu : ILevel
{
    private IEvent nowEvent = null;
    public IEvent Event { get => nowEvent; set => nowEvent = value; }

    public Player Player => null;

    public Loader Loader => null;

    public bool IsLoaded { get; set; } = true;

    public Image background = SpriteBuffer.Current.Get("src/Sprites/logo-game.png");
    public Image BackgroundLoad { get => background; set => background = value; }

    public Sprite backgroundSprite;
    public List<Button> Buttons { get; set; } = new List<Button>();
    public Menu()
    {
        backgroundSprite = new Sprite(background, PointF.Empty, Camera.Size, layer: 10);
        Buttons.Add(new Button(
            new PointF(Camera.Size.Width * .5f, Camera.Size.Height * .7f),
            new SizeF(Camera.Size.Width * .2f, Camera.Size.Height * .2f),
            SpriteBuffer.Current.Get("src/Sprites/Koala.jpg"),
            () => {
                Memory.Level = new SalaDigitalLevel();
            }
        ));
    }

    public void OnFrame()
    {
        Screen.Queue.Add(backgroundSprite);
        foreach (var button in Buttons)
        {
            button.Draw(layer: 11);
        }
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {

    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {

    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        if (Memory.MouseButton == MouseButtons.Left)
            foreach (var button in Buttons)
            {
                RectangleF rect = new RectangleF(
                    button.Position.X,
                    button.Position.Y,
                    button.Size.Width,
                    button.Size.Height
                );
                if (rect.Contains(Memory.Cursor))
                    button.Interact();
            }
    }
}
