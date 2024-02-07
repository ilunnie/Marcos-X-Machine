using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

public static class Debug
{ 
    static Player marquitos = new Player() {
        Life = 10,
        MaxLife = 10
    };
    // static DamagedBot damagedbot = new DamagedBot();

    public static void Open()
    {   

        // Marcos marcolas = new Marcos(new PointF(1000, 400));
        // marquitos.Entity = marcolas;

        // DamagedBotEntity damagedBot = new DamagedBotEntity(new PointF(540, 300));
        // damagedBot.damage = 1;
        // damagedbot.Entity = damagedBot;

        // TileSets.tileSets();
        // TileSets.ReadFile("src/Area/FrenteEts.csv");

        

        // var watch = new System.Diagnostics.Stopwatch();
        // watch.Start();

        // for (int i = 0; i < 500; i++)
        // {
        //     var marcos = new Marcos(new PointF(Random.Shared.Next(0, 1921), Random.Shared.Next(0, 1081)));
        //     Memory.Entities.Add(marcos);
        // }
        
        // watch.Stop();
        // MessageBox.Show($"Execution Time: {watch.ElapsedMilliseconds} ms");

        // Camera.MoveTo(1050, 600);
    }

    public static void OnFrame()
    {
        // marquitos.OnFrame();
        // marquitos.Entity.FocusCam();
        // damagedbot.OnFrame();
    }

    public static void OnKeyDown(object o, KeyEventArgs e)
    {
        // SoundBuilder.Play(SoundType.Effect, "src/Sounds/Marcos/andando.wav");
        // marquitos.OnKeyDown(o, e);
        if (e.KeyCode == Keys.Escape) Memory.App.Close();
    }

    public static void OnKeyUp(object o, KeyEventArgs e)
    {
        // marquitos.OnKeyUp(o, e);
        // SoundBuilder.StopSound();
    }

    public static void OnMouseMove(object o, MouseEventArgs e)
    {
        // marquitos.OnMouseMove(o, e);
        // if (e.Delta != 0) Camera.Zoom += (float)0.01 * e.Delta;
    }
}