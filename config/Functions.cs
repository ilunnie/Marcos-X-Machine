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
            sprite.Anchor.Position.X + (!sprite.Anchor.ScreenReference ? sprite.Position.X : 0),
            sprite.Anchor.Position.Y + (!sprite.Anchor.ScreenReference ? sprite.Position.Y : 0)
        );

        g.RotateTransform(sprite.Angle);

        g.TranslateTransform(
            -(sprite.Anchor.Position.X + (!sprite.Anchor.ScreenReference ? sprite.Position.X : 0)),
            -(sprite.Anchor.Position.Y + (!sprite.Anchor.ScreenReference ? sprite.Position.Y : 0))
        );
        
        sprite.Draw(g);
        if (Memory.Mode == "debug" && sprite.Hitbox is not null)
            sprite.Hitbox.Draw(g, new PointF(
                sprite.Position.X,
                sprite.Position.Y
            ));

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
    /// <returns><c>PointF</c> com a posição calculada</returns>
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

    /// <summary>
    /// Calcula o tamanho da imagem com base na escala informada sem distorce-la
    /// </summary>
    /// <param name="originalSize">Tamanho original</param>
    /// <param name="scale">Escala desejada</param>
    /// <returns>O tamanho que deve ter para não distorcer</returns>
    public static SizeF ProportionalSize(float Width, float Height, float scale)
    {
        return new SizeF(
            Width * scale * Camera.Zoom,
            Height * scale * Camera.Zoom
        );
    }
    /// <summary>
    /// Calcula o tamanho da imagem com base na escala informada sem distorce-la
    /// </summary>
    /// <param name="originalSize">Tamanho original</param>
    /// <param name="scaledSize">Tamanho máximo</param>
    /// <returns>O tamanho que deve ter para não distorcer</returns>
    public static SizeF ProportionalSize(float Width, float Height, SizeF scaledSize)
        => ProportionalSize(Width, Height,
            Math.Min(scaledSize.Width / (float)Width, scaledSize.Height / (float)Height)
        );

    /// <summary>
    /// Calcula o Angulo que Ponto B está referente ao Ponto A
    /// </summary>
    /// <param name="A">Ponto de referencia</param>
    /// <param name="B">Ponto </param>
    /// <returns></returns>
    public static double AngleTo(this PointF A, PointF B)
        => Math.Atan2(B.Y - A.Y, B.X - A.X);

    public static PointF CoordinateRotation(float X, float Y, float Xref, float Yref, double angle)
    {
        float Xt = X - Xref;
        float Yt = Y - Yref;

        double Xrot = Xt * Math.Cos(angle) - Yt * Math.Sin(angle);
        double Yrot = Xt * Math.Sin(angle) + Yt * Math.Cos(angle);

        return new PointF(
            (float)(Xrot + Xref),
            (float)(Yrot + Yref)
        );
    }
    public static PointF CoordinateRotation(this PointF point, PointF anchor, double angle)
        => CoordinateRotation(point.X, point.Y, anchor.X, anchor.Y, angle);
}