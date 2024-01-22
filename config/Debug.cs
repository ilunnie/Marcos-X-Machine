using System;
using System.Drawing;
using System.Windows.Forms;

public static class Debug
{
    public static void Open()
    {
        for (int i = 0; i < 1000; i++)
        {
            var marcos = new Marcos(new PointF(Random.Shared.Next(0, 1921), Random.Shared.Next(0, 1081)));
            Memory.Entities.Add(marcos);
        }
        Camera.MoveTo(1050, 600);
    }

    public static void OnFrame()
    {

    }

    public static void OnKeyDown(object o, KeyEventArgs e)
    {

    }

    public static void OnKeyUp(object o, KeyEventArgs e)
    {

    }

    public static void OnMouseMove(object o, MouseEventArgs e)
    {

    }
}