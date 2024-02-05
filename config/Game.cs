using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public override void Open()
    {
        this.form.Cursor = new Cursor("src/sprites/cursor.cur");

        Memory.Level = new EntradaDTALevel();
        // SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Musics/introMusic.wav", 20);

        // Sound.SetMusicVolume(0);
        // Sound.SetVolume(100);
    }
    public override void OnFrame()
    {
        if (!Memory.Level.IsLoaded)
        {
            Memory.Level.Load(g, pb);
            return;
        }

        Memory.Level.OnFrame();
    }
    public override void OnMouseMove(object o, MouseEventArgs e)
        => Memory.Level.OnMouseMove(o, e);

    public override void OnKeyDown(object o, KeyEventArgs e)
        => Memory.Level.OnKeyDown(o, e);

    public override void OnKeyUp(object o, KeyEventArgs e)
        => Memory.Level.OnKeyUp(o, e);
}