using System;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public ILevel Level { get; set; }
    public override void Open()
    {
    

        // for (int i = 0; i < 1000; i++)
        // {
        //     var sla = new Marcos(new PointF(Random.Shared.Next(0, 1921), Random.Shared.Next(0, 1081)));
        //     Memory.Entities.Add(sla);
        // }
        // Camera.MoveTo(1050, 600);
    }
    public override void OnFrame()
    {
        // if (Level.LoadPercent != 100)
        // {
        //     Level.Load();
        //     return;
        // }
    }
}