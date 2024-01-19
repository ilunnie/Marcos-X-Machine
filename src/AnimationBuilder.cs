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
}