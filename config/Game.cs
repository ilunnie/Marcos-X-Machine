using System;
using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    bool running = false;
    public override void Open()
    {
        var marcos = new Marcos(new PointF(-100, -100));
        var marcosMob = new Mob();
        marcosMob.Life = 10;
        marcosMob.Entity = marcos;

        // for (int i = 0; i < 1000; i++)
        // {
        //     var sla = new Marcos(new PointF(Random.Shared.Next(0, 1921), Random.Shared.Next(0, 1081)));
        //     Memory.Entities.Add(sla);
        // }
        // Camera.MoveTo(1050, 600);
    }
    public override void OnFrame()
    {
        // if (!running)
        // {
        //     running = ;
        //     return;
        // }
    }
}