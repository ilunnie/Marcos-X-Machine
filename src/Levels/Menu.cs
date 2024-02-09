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

    public Image background = SpriteBuffer.Current.Get("src/sprites/logo-game.png");
    public Image BackgroundLoad { get => background; set => background = value; }

    public Sprite backgroundSprite;
    public List<Button> Buttons { get; set; } = new List<Button>();
    private bool isClear = false;
    public bool IsClear { get => isClear; set => isClear = value; }

    public Menu()
    {
        backgroundSprite = new Sprite(background, PointF.Empty, Camera.Size, layer: 0);
        Buttons.Add(new Button(
            new PointF(Camera.Size.Width * .1f, Camera.Size.Height * .1f),
            new SizeF(Camera.Size.Width * .15f, Camera.Size.Height * .05f),
            SpriteBuffer.Current.Get("src/sprites/start-game.png"),
            () => {
                Memory.Level = new SalaDigitalLevel();
                Sound.StopMusics();
            }
        ));

        Buttons.Add(new Button(
            new PointF(Camera.Size.Width * .1f, Camera.Size.Height * 0.2f),
            new SizeF(Camera.Size.Width * .08f, Camera.Size.Height * .05f),
            SpriteBuffer.Current.Get("src/sprites/quit-game.png"),
            () => {
                Memory.App.Close();
            }
        ));
    }

    public void OnFrame()
    {
        Screen.GUI.Add(backgroundSprite);
        foreach (var button in Buttons)
        {
            button.Draw(layer: 1);
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
