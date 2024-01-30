using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public override void Open()
    {
        this.form.Cursor = new Cursor("src/sprites/cursor.cur");
        Memory.Level = new SalaDigitalLevel();
    }
    public override void OnFrame()
    {
        if (Memory.Level.LoadPercent != 100)
            Memory.Level.Load(g, pb);

        Memory.Level.OnFrame();
    }
    public override void OnMouseMove(object o, MouseEventArgs e)
        => Memory.Level.OnMouseMove(o, e);

    public override void OnKeyDown(object o, KeyEventArgs e)
        => Memory.Level.OnKeyDown(o, e);

    public override void OnKeyUp(object o, KeyEventArgs e)
        => Memory.Level.OnKeyUp(o, e);
}