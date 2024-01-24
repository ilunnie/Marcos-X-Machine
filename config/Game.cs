using System;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public override void Open()
    {
        var marcos = new Marcos(new PointF(300, 100));

        // for (int i = 0; i < 1000; i++)
        // {
        //     var sla = new Marcos(new PointF(Random.Shared.Next(0, 1921), Random.Shared.Next(0, 1081)));
        //     Memory.Entities.Add(sla);
        // }
        // Camera.MoveTo(1050, 600);
    }
    public override void OnFrame()
    {
        
    }
}