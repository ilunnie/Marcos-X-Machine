using System.Drawing;
using System.Windows.Forms;

public class Game : App
{
    public override void Open()
    {
        var marcos = new Marcos(new PointF(300, 100));
        Memory.Entities.Add(marcos);
        marcos.FocusCam(false);
    }
    public override void OnFrame()
    {
        foreach (var entity in Memory.Entities)
        {
            entity.Draw();
        }
    }
}