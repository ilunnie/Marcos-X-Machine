using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

public static class Debug
{ 
    static App app = null;
    static Player marquitos = new Player() {
        Life = 10,
        MaxLife = 10
    };
    static DamagedBot damagedbot = new DamagedBot();

    public static void Open(App app)
    {   
        Debug.app = app;

        Marcos marcolas = new Marcos(new PointF(-100, -100));
        marquitos.Entity = marcolas;

        DamagedBotEntity damagedBot = new DamagedBotEntity(new PointF(500, -100));
        damagedBot.damage = 1;
        damagedbot.Entity = damagedBot;
        
        TileSets.tileSets();
        TileSets.DrawFromFile();

        var marcos = new Marcos(new PointF(-100, -100));
        var marcosMob = new Mob();
        marcosMob.Life = 10;
        marcosMob.Entity = marcos;

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
        marquitos.OnFrame();
        marquitos.Entity.FocusCam();
        damagedbot.OnFrame();
        // foreach (var item in Memory.Tileset)
        // {
        //     Screen.Queue.Add(item);
        // }
    }

    public static void OnKeyDown(object o, KeyEventArgs e)
    {
        marquitos.OnKeyDown(o, e);
        if (e.KeyCode == Keys.Escape) app.Close();
    }

    public static void OnKeyUp(object o, KeyEventArgs e)
    {
        marquitos.OnKeyUp(o, e);
    }

    public static void OnMouseMove(object o, MouseEventArgs e)
    {
        marquitos.OnMouseMove(o, e);
        if (e.Delta != 0) Camera.Zoom += (float)0.01 * e.Delta;
    }
}