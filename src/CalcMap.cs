using System.Drawing;
using System.Windows.Forms.VisualStyles;

public class CalcMap {
    public SubImage Image { get; set; }
    public PointF Position { get; protected set; }
    public SizeF Size { get; set; }
    public Hitbox Hitbox { get; set; }
    public int Layer { get; set; }
    
    public CalcMap(SubImage image, PointF position, SizeF size, Hitbox hitbox, int layer) {
        this.Image = image;
        this.Position = position;
        this.Size = size;
        this.Hitbox = hitbox;
        this.Layer = layer;
    }

    public CalcMap Clone()
        => new CalcMap(this.Image, this.Position, this.Size, this.Hitbox, this.Layer);

    public void Draw() {
        SizeF relativeSize = Functions.ProportionalSize(Image.Width, Image.Height, Size);
        PointF camPosition = Position.PositionOnCam();

        Sprite sprite = new Sprite(Image, Hitbox, camPosition, relativeSize, 0, Layer);
        Screen.Queue.Add(sprite);
    }
    
}