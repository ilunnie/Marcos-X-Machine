using System;
using System.Drawing;
using System.Windows.Forms;

public static class Debug
{ 
    static App app = null;
    static Player marquitos = new Player();

    public static void Open(App app)
    {   
        Debug.app = app;
        marquitos.Life = 10;
        marquitos.MaxLife = 10;

        Marcos marcolas = new Marcos(new PointF(300, 100));
        marquitos.Entity = marcolas;

        TileSets.tileSets();

        
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