using System;
using System.Drawing;

public class Button : Entity
{
    public SubImage Image { get; set; }
    public Action Action { get; set; }
    public Button(PointF position, SizeF size, SubImage image, Action action)
    {
        this.Position = position;
        this.Size = size;
        this.Image = image;
        this.Action = action;
    }

    public override void Spawn() { }
    public override void Interact()
        => this.Action.Invoke();

    public override void Draw(float angle = 0, int layer = 1)
        => Screen.GUI.Add(new Sprite(this.Image, null, this.Position, this.Size, angle, layer));
}
