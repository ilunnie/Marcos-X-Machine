using System.Drawing;

public static class TalkBox
{
    public static string Text { get; set; }
    public static Font Font { get; set; } = new Font("Arial", 25);
    public static SolidBrush Brush { get; set; } = new SolidBrush(Color.White);
    public static PointF TextPosition { get; set; } = new PointF(Box.Size.Width * .4f, Box.Size.Height * .3f);
    private static TextImage TextImage {
        get => new TextImage(Text, Font, Brush, TextPosition);
    }

    public static string EntityName { get; set; }
    public static Font NameFont { get; set; } = new Font("Arial", 30);
    public static SolidBrush NameColor { get; set; } = new SolidBrush(Color.LightGray);
    public static PointF NamePosition { get; set; } = new PointF(Box.Size.Width * .41f, Box.Size.Height * .15f);
    private static TextImage Name {
        get => new TextImage(EntityName, NameFont, NameColor, NamePosition);
    }
    private static Sprite box = null;
    public static Sprite Box {
        get {
            if (box is null)
            {
                var imagebox = SpriteBuffer.Current.Get("src/sprites/chat-box.png");
                var size = new SizeF(
                    Camera.Size.Width * .75f,
                    Camera.Size.Height * .35f
                );
                var position = new PointF(
                    (Camera.Size.Width - size.Width) / 2, 
                    Camera.Size.Height * .99f - size.Height
                );
                box = new Sprite(imagebox, position, size, layer: 0);
            }
            box.Text.Clear();
            box.Text.Add(Name);
            box.Text.Add(TextImage);
            return box;
        }
    }
    public static void Talk(this Entity entity, string text)
    {
        EntityName = entity.Name;
        Text = text;
        Screen.GUI.Add(Box);

        var thumbnail = new Sprite(
            entity.Thumbnail,
            new PointF(box.Position.X + box.Size.Width * .1f, box.Position.Y + box.Size.Height * .2f),
            new SizeF(box.Size.Width * .25f, box.Size.Height * .5f)
        );
        Screen.GUI.Add(thumbnail);
    }
}