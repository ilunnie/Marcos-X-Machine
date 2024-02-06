using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class Collision
{
    public static bool VerifyCollision(this Entity entity, Entity other)
    {
        RectangleF entityRect = new RectangleF();
        RectangleF comparRect = new RectangleF();

        bool result = false;
        foreach (var thisRect in entity.Hitbox.rectangles)
        {
            entityRect.X = thisRect.X;
            entityRect.Y = thisRect.Y;
            entityRect.Width = thisRect.Width;
            entityRect.Height = thisRect.Height;

            PointF[] thisPoly = entityRect.ToPolygon(entity.Anchor, entity.Hitbox.Angle);
            thisPoly = thisPoly.Select(v => new PointF(v.X + entity.Position.X, v.Y + entity.Position.Y)).ToArray();
            foreach (var otherRect in other.Hitbox.rectangles)
            {
                comparRect.X = otherRect.X;
                comparRect.Y = otherRect.Y;
                comparRect.Width = otherRect.Width;
                comparRect.Height = otherRect.Height;

                PointF[] otherPoly = comparRect.ToPolygon(other.Anchor, other.Hitbox.Angle);
                otherPoly = otherPoly.Select(v => new PointF(v.X + other.Position.X, v.Y + other.Position.Y)).ToArray();
                if (thisPoly.IntersectsWith(otherPoly))
                {
                    entity.OnHit(other);
                    result = true;
                }
            }
        }

        return result;
    }

    public static void VerifyCollision()
    {
        object lockObject = new object(); // Objeto de bloqueio para garantir acesso seguro ao estado compartilhado
        Parallel.ForEach(Memory.Colliders, entity =>
        {
            if (entity.Hitbox is null)
                return;

            bool collision = false;
            Parallel.ForEach(Memory.Colliders, other =>
            {
                if (other.Hitbox is null || entity == other)
                    return;

                if (entity.VerifyCollision(other))
                {
                    lock (lockObject) // Bloqueia o acesso ao estado compartilhado durante a atualização
                    {
                        collision = true;
                    }
                }
            });

            lock (lockObject) // Bloqueia o acesso ao estado compartilhado durante a atualização
            {
                entity.Hitbox.Pen = collision ? Pens.Red : Pens.Blue;
            }
        });
    }

}
