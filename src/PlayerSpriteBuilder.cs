using System;
using System.Drawing;

public static class PlayerSpriteBuilder
{
    public static long Frame = 0;
    public static Player DrawLife(this Player player, PointF position, float scale, float gap = 2)
    {
        Image image = SpriteBuffer.Current.Get("src/sprites/icons/hearts-sprite.png");
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

    public static Player DrawWeapons(this Player player, float scale, float subProportion, float gap = 20)
    {
        Image image = SpriteBuffer.Current.Get("src/sprites/icons/weapon-model.png");
        SizeF mainSize = ((SubImage)image).ProportionalSize(scale);
        SizeF subSize = mainSize * subProportion;

        var main = new PointF(
                mainSize.Width / 2 + subSize.Width / 2 + gap,
                Camera.Size.Height - mainSize.Height * 1.5f - subSize.Height / 2 - gap);

        var hand = player.hand;
        if (player.Hands.Count > hand)
        {
            Screen.GUI.Add(new Sprite(image, null, main, mainSize));
            var thumbnail = player.Hands[hand].Entity.Thumbnail;
            var thumbSize = thumbnail.ProportionalSize(mainSize) / Camera.Zoom;
            Screen.GUI.Add(new Sprite(thumbnail, null, main + (mainSize - thumbSize) / 2, thumbSize));
        }

        if (player.Hands.Count > hand + 1)
        {
            var post = new PointF(
                subSize.Width / 2 + gap,
                main.Y - subSize.Width / 2
            );
            Screen.GUI.Add(new Sprite(image, null, post, subSize));
            var thumbnail = player.Hands[hand + 1].Entity.Thumbnail;
            var thumbSize = thumbnail.ProportionalSize(subSize) / Camera.Zoom;
            Screen.GUI.Add(new Sprite(thumbnail, null, post + (subSize - thumbSize) / 2, thumbSize));
        }

        if (hand - 1 >= 0)
        {
            var pre = new PointF(
                gap + subSize.Width + mainSize.Width,
                Camera.Size.Height - (subSize.Height * 1.5f + gap)
            );
            Screen.GUI.Add(new Sprite(image, null, pre, subSize));
            var thumbnail = player.Hands[hand - 1].Entity.Thumbnail;
            var thumbSize = thumbnail.ProportionalSize(subSize) / Camera.Zoom;
            Screen.GUI.Add(new Sprite(thumbnail, null, pre + (subSize - thumbSize) / 2, thumbSize));
        }

        return player;
    }

    private static bool press = false;
    private static byte framecount = 0;
    public static Player DrawButtonF(this Player player, float scale, float gap = 30)
    {
        Image image = SpriteBuffer.Current.Get("src/sprites/icons/f-button.png");
        if (framecount > 10) { press = !press; framecount = 0; }
        SubImage sprite = image.Cut(Convert.ToInt32(press), 0, 2);
        framecount++;
        var size = sprite.ProportionalSize(scale);

        var position = player.Entity.Position + player.Entity.Size / 2;
        position = position.PositionOnCam();

        Screen.GUI.Add(new Sprite(
            sprite, null,
            new PointF(position.X - size.Width / 2, position.Y - size.Height / 2 - player.Entity.Size.Height / 2 - gap),
            size
        ));

        return player;
    }

    public static Player DrawDestiny(this Player player, float scale, float gap = 20)
    {
        if (player.Destiny.IsEmpty) return player;

        SubImage image = SpriteBuffer.Current.Get("src/sprites/icons/exclamation.png");
        SizeF size = image.ProportionalSize(scale);
        PointF destiny = (player.Destiny - size / 2).PositionOnCam();

        var camera = new RectangleF(PointF.Empty, Camera.Size);
        if (!camera.Contains(destiny))
        {
            destiny = new PointF(
                Math.Max(camera.Left + gap, Math.Min(destiny.X, camera.Right - size.Width - gap)),
                Math.Max(camera.Top + gap, Math.Min(destiny.Y, camera.Bottom - size.Height - gap))
            );
        }


        Screen.GUI.Add(new Sprite(
            image, null,
            destiny, size
        ));
        return player;
    }
}