using System.Drawing;

public static class Functions
{
    /// <summary>
    /// Desenha um objeto Sprite na tela
    /// </summary>
    /// <param name="g">Tela onde o objeto será desenhado</param>
    /// <param name="sprite">Objeto que será desenhado</param>
    public static void DrawImage(this Graphics g, Sprite sprite)
    {
        g.TranslateTransform(
            sprite.Anchor.Position.X + (sprite.Anchor.ScreenReference ? sprite.Position.X : 0),
            sprite.Anchor.Position.Y + (sprite.Anchor.ScreenReference ? sprite.Position.Y : 0)
        );

        g.RotateTransform(sprite.Angle);

        g.TranslateTransform(
            -(sprite.Anchor.Position.X + (sprite.Anchor.ScreenReference ? sprite.Position.X : 0)),
            -(sprite.Anchor.Position.Y + (sprite.Anchor.ScreenReference ? sprite.Position.Y : 0))
        );

        g.DrawImage(
            sprite.Image,
            sprite.Position.X + sprite.Anchor.Position.X * (sprite.Anchor.ScreenReference ? 1 : -1),
            sprite.Position.Y + sprite.Anchor.Position.Y * (sprite.Anchor.ScreenReference ? 1 : -1),
            sprite.Size.Width, sprite.Size.Height
        );

        g.ResetTransform();
    }
}