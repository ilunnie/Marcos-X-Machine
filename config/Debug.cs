using System;
using System.Drawing;
using System.Windows.Forms;

public static class Debug
{ 
    static Player marquitos = new Player();

    public static void Open()
    {   
        marquitos.Life = 10;
        marquitos.MaxLife = 10;

        Marcos marcolas = new Marcos(new PointF(300, 100));
        Memory.Entities.Add(marcolas);
        marquitos.Entity = marcolas;
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
    }

    public static void OnKeyDown(object o, KeyEventArgs e)
    {
        marquitos.OnKeyDown(o, e);
    }

    public static void OnKeyUp(object o, KeyEventArgs e)
    {
        marquitos.OnKeyUp(o, e);
    }

    public static void OnMouseMove(object o, MouseEventArgs e)
    {

    }
}