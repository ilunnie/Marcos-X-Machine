using System;
using System.Drawing;

public static class AnimationBuilder
{
    public static SubImage Cut(this Image sprite, int x = 0, int y = 0, int spritesQuantX = 4, int spritesQuantY = 1)
    {
        Rectangle rect = new Rectangle(
            sprite.Width / spritesQuantX * x, 
            sprite.Height / spritesQuantY * y, 
            sprite.Width / spritesQuantX, 
            sprite.Height / spritesQuantY);

        return new SubImage(sprite, rect);;
    }

    public static void AddAnimation(this Entity entity, IAnimation animation)
    {
        if (entity.Animation is null)
            entity.Animation = animation;
        else
            entity.Animation.Next = animation;
    }

    public static void AddStaticAnimation(this Entity entity, string local, int x, int y = 0, int spritesQuantX = 4, int spritesQuantY = 1)
    {
        Image sprite = SpriteBuffer.Current.Get("src/sprites/" + local);
        entity.AddAnimation(new StaticAnimation() {
            Image = sprite.Cut(x, y, spritesQuantX, spritesQuantY)
        });
    }
    public static void AddStaticAnimation(this Entity entity, string local, Direction direction = Direction.BottomLeft, int spritesQuantX = 4)
        => entity.AddStaticAnimation(local, (int)direction, spritesQuantX: spritesQuantX);

    public static void AddWalkingAnimation(this Entity entity, string local, int x, int y = 0, int spritesQuantX = 4, int spritesQuantY = 1)
    {
        Image sprite = SpriteBuffer.Current.Get("src/sprites/" + local);
        entity.AddAnimation(new WalkingAnimation() {
            Image = sprite.Cut(x, y, spritesQuantX, spritesQuantY)
        });
    }
    public static void AddWalkingAnimation(this Entity entity, string local, Direction direction = Direction.BottomLeft, int spritesQuantX = 4)
        => entity.AddWalkingAnimation(local, (int)direction, spritesQuantX: spritesQuantX);
}