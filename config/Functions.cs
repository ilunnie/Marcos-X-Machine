using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;

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

    public static float EuclidianDistance(PointF current, PointF goal) 
        => MathF.Sqrt(
            (current.X - goal.X) * (current.X - goal.X) + 
            (current.Y - goal.Y) * (current.Y - goal.Y)
        );
    
    public static List<int> GetNextMove(PointF playerPointF, PointF enemyPointF, byte[] map, int width)
    {
        var path = new List<int>();

        var costMap = new Dictionary<int, float>();
        var cameMap = new Dictionary<int, int>();
        var queue = new PriorityQueue<int, float>();
        int player = (int)(playerPointF.Y * width + playerPointF.X);
        int enemy = (int)(enemyPointF.Y * width + enemyPointF.X);

        int[] neighbors = new int[8];
        int CurSubWid, SubFromSub, PlusFromSub;
        int CurPlusWid, SubFromPlus, PlusFromPlus;
        float newCost;

        queue.Enqueue(enemy, 0);
        cameMap[enemy] = enemy;
        costMap[enemy] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == player)
                break;

            CurSubWid = current - width;
            CurPlusWid = current + width;
            SubFromSub = CurSubWid - 1;
            PlusFromSub = CurSubWid + 1;
            SubFromPlus = CurPlusWid - 1;
            PlusFromPlus = CurSubWid + 1;

            neighbors[0] = SubFromSub > 0 || SubFromSub < width || map[SubFromSub] != 1 ? SubFromSub : -1;
            neighbors[1] = CurSubWid > 0 || CurSubWid < width || map[CurSubWid] != 1 ? CurSubWid : -1;
            neighbors[2] = PlusFromSub > 0 || PlusFromSub < width || map[PlusFromSub] != 1 ? PlusFromSub : -1;

            neighbors[3] = current - 1 > 0 || current - 1 < width || map[current - 1] != 1 ? current - 1 : -1;
            neighbors[4] = current + 1 > 0 || current + 1 < width || map[current + 1] != 1 ? current + 1 : -1;

            neighbors[5] = SubFromPlus > 0 || SubFromPlus < width || map[SubFromPlus] != 1 ? SubFromPlus : -1;
            neighbors[6] = CurPlusWid > 0 || CurPlusWid < width || map[CurPlusWid] != 1 ? CurPlusWid : -1;
            neighbors[7] = PlusFromPlus > 0 || PlusFromPlus < width || map[PlusFromPlus] != 1? PlusFromPlus : -1;

            foreach (var next in neighbors)
            {
                if (next == -1)
                    continue;

                newCost = costMap[current] + 1;
                if(!costMap.ContainsKey(next) || newCost < costMap[next])
                {
                    costMap[next] = newCost;
                    float priority = newCost + EuclidianDistance(
                        new PointF(player / width, player % width),
                        new PointF(next / width, next % width)
                    );
                    queue.Enqueue(next, -priority);
                    cameMap[next] = current;
                }
            }
        }

        for (int i = 0; i < cameMap.Count; i++)
        {
            path.Add(cameMap[i]);
            
            if (cameMap[i] == player)
                break;
        }

        return path;
    }
}