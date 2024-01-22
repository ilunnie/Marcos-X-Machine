using System.Drawing;

public static class AnimationBuilder
{
    public static Bitmap Cut(this Image sprite, int x = 0, int y = 0, int spritesQuantX = 4, int spritesQuantY = 1)
    {
        RectangleF rect = new RectangleF(
            sprite.Width / spritesQuantX * x, 
            sprite.Height / spritesQuantY * y, 
            sprite.Width / spritesQuantX, 
            sprite.Height / spritesQuantY);

        Bitmap source = new Bitmap(sprite);
        Bitmap frame = source.Clone(rect, source.PixelFormat);

        return frame;
    }

    public static void AddAnimation(this Entity entity, IAnimation animation)
    {
        if (entity.Animation is null)
            entity.Animation = animation;
        else
            entity.Animation.Next = animation;
    }

    public static void AddStaticAnimation(this Entity entity, string local, Direction direction = Direction.BottomLeft)
    {
        Image sprite = Bitmap.FromFile("src/Sprites/" + local);
        entity.AddAnimation(new StaticAnimation() {
            Image = sprite.Cut((int) direction),
        });
    }

    public static void AddWalkingAnimation(this Entity entity, string local, Direction direction = Direction.BottomLeft)
    {
        Image sprite = Bitmap.FromFile("src/Sprites/" + local);
        entity.AddAnimation(new WalkingAnimation() {
            Image = sprite.Cut((int) direction),
        });
    }
}