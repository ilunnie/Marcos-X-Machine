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
        Memory.Level = new EntradaDTALevel();
        SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Musics/introMusic.wav", 20);

        // Sound.SetMusicVolume(0);
        // Sound.SetVolume(100);
        
=======
        Memory.Level = new EtsLevel();
        // SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Musics/introMusic.wav", 20);
>>>>>>> 60be84e6b77f650e076c14ebb185a7ae50422919
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