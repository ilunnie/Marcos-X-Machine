using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public ILevel Level { get; set; }
    public override void Open()
    {
        this.form.Cursor = new Cursor("src/sprites/cursor.cur");
        Level = new EtsLevel();
    }
    public override void OnFrame()
    {
        if (Level.LoadPercent != 100)
        {
            Level.Load();
            return;
        }
        Level.OnFrame();
    }
    public override void OnMouseMove(object o, MouseEventArgs e)
        => Level.OnMouseMove(o, e);

    public override void OnKeyDown(object o, KeyEventArgs e)
        => Level.OnKeyDown(o, e);

    public override void OnKeyUp(object o, KeyEventArgs e)
        => Level.OnKeyUp(o, e);
}