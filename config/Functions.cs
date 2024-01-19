using System;
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

    /// <summary>
    /// Mede a distancia entre ponto A até ponto B
    /// </summary>
    public static double Distance(float Ax, float Ay, float Bx, float By)
    {
        float dx = Bx - Ax;
        float dy = By - Ay;
        return Math.Sqrt(
            dx * dx +
            dy * dy
        );
    }
    /// <summary>
    /// Mede a distancia entre ponto A até ponto B
    /// </summary>
    public static double Distance(this PointF A, PointF B)
        => Distance(A.X, A.Y, B.X, B.Y);
    /// <summary>
    /// Mede a distancia entre ponto A até ponto B
    /// </summary>
    public static double Distance(this PointF A, float Bx, float By)
        => Distance(A.X, A.Y, Bx, By);

    /// <summary>
    /// Calcula a interpolação linear entre ponto A e B
    /// </summary>
    /// <param name="t">Número de 0 a 1 que representa uma distancia entre ambos os pontos</param>
    /// <returns></returns>
    public static PointF LinearInterpolation(float Ax, float Ay, float Bx, float By, double t)
    {
        return new PointF(
            (float)((1 - t) * Ax + t * Bx),
            (float)((1 - t) * Ay + t * By)
        );
    }
    /// <summary>
    /// Calcula a interpolação linear entre ponto A e B
    /// </summary>
    /// <param name="t">Número de 0 a 1 que representa uma distancia entre ambos os pontos</param>
    /// <returns></returns>
    public static PointF LinearInterpolation(this PointF A, PointF B, double t)
        => LinearInterpolation(A.X, A.Y, B.X, B.Y, t);
    /// <summary>
    /// Calcula a interpolação linear entre ponto A e B
    /// </summary>
    /// <param name="t">Número de 0 a 1 que representa uma distancia entre ambos os pontos</param>
    /// <returns></returns>
    public static PointF LinearInterpolation(this PointF A, float Bx, float By, double t)
        => LinearInterpolation(A.X, A.Y, Bx, By, t);
}