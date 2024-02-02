using System.Drawing;
using System.Windows.Forms.VisualStyles;

public class CalcMap : Entity {
    public SubImage Image { get; set; }
    public int Layer { get; set; }
    
    public CalcMap(SubImage image, PointF position, SizeF size, Hitbox hitbox, int layer) {
        this.Image = image;
        this.Size = size;
        this.Hitbox = hitbox;
        this.Layer = layer;
        this.Position = position;
        this.OldPosition = position;
    }

    public override void OnHit(Entity entity)
    {
    }

    public CalcMap Clone()
        => new CalcMap(this.Image, this.Position, this.Size, new Hitbox(), this.Layer);

    public override void Spawn() { }

    public void Draw() {
        SizeF relativeSize = Functions.ProportionalSize(Image.Width, Image.Height, Size);
        PointF camPosition = Position.PositionOnCam();

        Sprite sprite = new Sprite(Image, Hitbox, camPosition, relativeSize, 0, Layer);
        Screen.Queue.Add(sprite);
    }
    
}