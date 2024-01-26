using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Mob
{
    public Direction Direction = Direction.BottomLeft;
    private Entity entity = null;
    public Entity Entity 
    {
        get => entity;
        set {
            this.entity = value;
            value.Mob = this;
        }
    }
    public List<Hand> Hands { get; set; } = new List<Hand>();
    public int hand = 0;
    public float MaxLife;
    public float Life;
    public float Speed = 0.6f;

    public virtual void OnDestroy() {}
    public virtual void OnInit() {}
    public virtual void OnFrame() {}
    public virtual void OnMouseMove(object o, MouseEventArgs e) {}
    public virtual void OnKeyDown(object o, KeyEventArgs e) {}
    public virtual void OnKeyUp(object o, KeyEventArgs e) {}
    public virtual void VerifyPosition(PointF entityPosition, PointF referencePoint) {
        var theta = Math.Atan2(entityPosition.Y - referencePoint.Y, entityPosition.X - referencePoint.X);
        double angle = theta * (180f / Math.PI);
        int spriteIndex = (int)Math.Floor(angle / 90f) % 4;

        Direction = (Direction)(spriteIndex + 2);
    }
}