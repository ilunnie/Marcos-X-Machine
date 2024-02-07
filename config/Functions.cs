using System;
using System.CodeDom;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

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
            sprite.Anchor.X + (!sprite.Anchor.ScreenReference ? sprite.Position.X : 0),
            sprite.Anchor.Y + (!sprite.Anchor.ScreenReference ? sprite.Position.Y : 0)
        );

        g.RotateTransform(sprite.Angle);

        g.TranslateTransform(
            -(sprite.Anchor.X + (!sprite.Anchor.ScreenReference ? sprite.Position.X : 0)),
            -(sprite.Anchor.Y + (!sprite.Anchor.ScreenReference ? sprite.Position.Y : 0))
        );

        sprite.Draw(g);

        g.ResetTransform();

        if (Memory.Mode == "debug" && sprite.Hitbox is not null)
            sprite.Hitbox.Draw(g, sprite);
    }

    /// <summary>
    /// Distancia Euclidiana
    /// </summary>
    public static double Distance(float Dx, float Dy)
        => Math.Sqrt(Dx * Dx + Dy * Dy);

    /// <summary>
    /// Mede a distancia entre ponto A até ponto B
    /// </summary>
    public static double Distance(float Ax, float Ay, float Bx, float By)
    {
        float dx = Bx - Ax;
        float dy = By - Ay;
        return Distance(dx, dy);
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
    /// Mede a distancia entre retangulo A até retangulo B
    /// </summary>
    public static double Distance(float Atop, float Aright, float Abottom, float Aleft, float Btop, float Bright, float Bbottom, float Bleft)
    {
        float dx = Math.Max(0, Math.Max(Aleft, Bleft) - Math.Min(Aright, Bright));
        float dy = Math.Max(0, Math.Max(Atop, Btop) - Math.Min(Abottom, Bbottom));

        return Distance(dx, dy);
    }
    /// <summary>
    /// Mede a distancia entre retangulo A até retangulo B
    /// </summary>
    public static double Distance(this RectangleF A, RectangleF B)
        => Distance(A.Top, A.Right, A.Bottom, A.Left, B.Top, B.Right, B.Bottom, B.Left);

    /// <summary>
    /// Calcula a interpolação linear entre ponto A e B
    /// </summary>
    /// <param name="t">Número de 0 a 1 que representa uma distancia entre ambos os pontos</param>
    /// <returns><c>PointF</c> com a posição calculada</returns>
    public static PointF LinearInterpolation(float Ax, float Ay, float Bx, float By, double t)
    {
        // return new PointF(
        //     (float)((1 - t) * Ax + t * Bx),
        //     (float)((1 - t) * Ay + t * By)
        // );
        return new PointF(
            (float)(Ax + t * (Bx - Ax)),
            (float)(Ay + t * (By - Ay))
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
    public static SizeF ProportionalSize(this SubImage image, float scale)
        => ProportionalSize(image.Width, image.Height, scale);
    public static SizeF ProportionalSize(this SubImage image, SizeF scaledSize)
        => ProportionalSize(image, 
            Math.Min(scaledSize.Width / image.Width, scaledSize.Height / image.Height)
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

    public static float EuclidianDistance(PointF current, PointF goal)
        => MathF.Sqrt(
            (current.X - goal.X) * (current.X - goal.X) +
            (current.Y - goal.Y) * (current.Y - goal.Y)
        );

    public static Stack<int> GetNextMoves(PointF playerPointF, PointF enemyPointF, byte[] map, int width)
    {
        var path = new Stack<int>();

        var costMap = new Dictionary<int, float>();
        var cameMap = new Dictionary<int, int>();
        var queue = new PriorityQueue<int, float>();
        int player =
            width * ((int)(playerPointF.Y / TileSets.spriteMapSize.Height) - 1) +
            (int)(playerPointF.X / TileSets.spriteMapSize.Width) - 1;
        int enemy =
            width * ((int)(enemyPointF.Y / TileSets.spriteMapSize.Height) - 1) +
            (int)(enemyPointF.X / TileSets.spriteMapSize.Width) - 1;

        int[] neighbors = new int[8];
        float newCost;

        queue.Enqueue(enemy, 0);
        cameMap[enemy] = enemy;
        costMap[enemy] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            int top = current - width;
            int bot = current + width;

            neighbors[0] = top - 1 > 0 && top - 1 < map.Length && map[top - 1] != 1 ? top - 1 : -1;
            neighbors[1] = top > 0 && top < map.Length && map[top] != 1 ? top : -1;
            neighbors[2] = top + 1 > 0 && top + 1 < map.Length && map[top + 1] != 1 ? top + 1 : -1;

            neighbors[3] = current - 1 > 0 && current - 1 < map.Length && map[current - 1] != 1 ? current - 1 : -1;
            neighbors[4] = current + 1 > 0 && current + 1 < map.Length && map[current + 1] != 1 ? current + 1 : -1;

            neighbors[5] = bot - 1 > 0 && bot - 1 < map.Length && map[bot - 1] != 1 ? bot - 1 : -1;
            neighbors[6] = bot > 0 && bot < map.Length && map[bot] != 1 ? bot : -1;
            neighbors[7] = bot + 1 > 0 && bot + 1 < map.Length && map[bot + 1] != 1 ? bot + 1 : -1;

            foreach (var next in neighbors)
            {
                if (next == -1)
                    continue;

                if (!costMap.ContainsKey(current))
                    return path;
                newCost = costMap[current] + 1;

                if (!costMap.ContainsKey(next) || newCost < costMap[next])
                {
                    costMap[next] = newCost;
                    float priority = newCost + EuclidianDistance(
                        new PointF(player % width, player / width),
                        new PointF(next % width, next / width)
                    );
                    queue.Enqueue(next, priority);
                    cameMap[next] = current;
                }
            }

            if (current == player)
                break;
        }

        path.Push(player);
        var last = player;
        while (true)
        {
            if (!cameMap.ContainsKey(last))
                return path;

            last = cameMap[last];
            path.Push(last);

            if (last == enemy)
                break;
        }

        return path;
    }

    /// <summary>
    /// Converte um Graphics para um Image
    /// </summary>
    /// <param name="g">Graphics a ser convertido</param>
    /// <returns></returns>
    public static Bitmap ToImage(this Graphics g)
    {
        Bitmap bitmap = new Bitmap(
            (int)g.VisibleClipBounds.Width,
            (int)g.VisibleClipBounds.Height,
            g
        );
        return bitmap;
    }

    public static PointF[] ToPolygon(this RectangleF rect, PointF Anchor, double Angle)
    {
        PointF[] vertices = new PointF[4];

        float[] xCoords = { rect.Left, rect.Right, rect.Right, rect.Left };
        float[] yCoords = { rect.Top, rect.Top, rect.Bottom, rect.Bottom };

        for (int i = 0; i < 4; i++)
        {
            double deltaX = xCoords[i] - Anchor.X;
            double deltaY = yCoords[i] - Anchor.Y;

            double newX = Anchor.X + deltaX * Math.Cos(Angle * Math.PI / 180) - deltaY * Math.Sin(Angle * Math.PI / 180);
            double newY = Anchor.Y + deltaX * Math.Sin(Angle * Math.PI / 180) + deltaY * Math.Cos(Angle * Math.PI / 180);

            vertices[i] = new PointF((float)newX, (float)newY);
        }

        return vertices;
    }

    public static bool SAT(PointF axis, PointF[] A, PointF[] B)
    {
        float min1 = float.MaxValue, max1 = float.MinValue;
        float min2 = float.MaxValue, max2 = float.MinValue;

        foreach (PointF p in A)
        {
            float dotProduct = (p.X * axis.X) + (p.Y * axis.Y);
            min1 = Math.Min(min1, dotProduct);
            max1 = Math.Max(max1, dotProduct);
        }

        foreach (PointF p in B)
        {
            float dotProduct = (p.X * axis.X) + (p.Y * axis.Y);
            min2 = Math.Min(min2, dotProduct);
            max2 = Math.Max(max2, dotProduct);
        }

        if (max1 < min2 || max2 < min1)
            return true;

        return false;
    }

    public static bool IntersectsWith(this PointF[] A, PointF[] B)
    {
        for (int i = 0; i < A.Length; i++)
        {
            PointF p1 = A[i];
            PointF p2 = A[(i + 1) % A.Length];
            PointF axis = new PointF(p2.Y - p1.Y, p1.X - p2.X);

            if (SAT(axis, A, B))
                return false;
        }

        for (int i = 0; i < B.Length; i++)
        {
            PointF p1 = B[i];
            PointF p2 = B[(i + 1) % B.Length];
            PointF axis = new PointF(p2.Y - p1.Y, p1.X - p2.X);

            if (SAT(axis, A, B))
                return false;
        }

        return true;
    }
}