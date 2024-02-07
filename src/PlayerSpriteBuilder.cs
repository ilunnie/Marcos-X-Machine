using System.Drawing;

public static class PlayerSpriteBuilder
{
    public static Player DrawLife(this Player player, PointF position, float scale, float gap = 2)
    {
        Image image = SpriteBuffer.Current.Get("src/sprites/hearts/hearts-sprite.png");
        var maxLife = player.MaxLife;
        var life = player.Life;
        var point = position;

        SubImage sprite;
        SizeF size = SizeF.Empty;
        for (int i = 0; i < maxLife; i += 2)
        {
            if (life > 1)
                sprite = image.Cut(0, 0, 3);
            else if (life == 1)
                sprite = image.Cut(1, 0, 3);
            else
                sprite = image.Cut(2, 0, 3);

            if (size.IsEmpty) size = sprite.ProportionalSize(scale);

            Screen.GUI.Add(new Sprite(
                sprite, null, point, size, layer: 10
            ));

            point.X += gap + size.Width;
            life -= 2;
        }

        return player;
    }
}