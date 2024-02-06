using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public override void Open()
    {
        this.form.Cursor = new Cursor("src/sprites/cursor.cur");

<<<<<<< HEAD
        Memory.Level = new DTALevel();
        // SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Musics/introMusic.wav", 20);
=======
        Memory.Level = new SalaDigitalLevel();
        Sound.OpenFrom(SoundType.Music, "src/Sounds/Musics/introMusic.wav").Play();
>>>>>>> 321ff39214a8b46d49911e6b020bec88c9d348db

        Sound.SetEffectVolume(10);
        Sound.SetMusicVolume(0);
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