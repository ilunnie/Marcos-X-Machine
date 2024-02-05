using System;
using System.Drawing;
using System.Text.RegularExpressions;

public static class Collision
{
    public static bool VerifyCollision(this Entity entity, Entity other)
    {
        RectangleF entityRect = new RectangleF();
        RectangleF comparRect = new RectangleF();

        bool result = false;
        // int tolerance = 100;
        // double distance = entity.Position.Distance(entity.OldPosition);

        // if (entity.Position.Distance(entity.OldPosition) > tolerance)
        // {
        //     for (int i = 0; i < distance; i++)
        //     {
        //         foreach (var thisRect in entity.Hitbox.rectangles)
        //         {
        //             foreach (var otherRect in other.Hitbox.rectangles)
        //             {
        //                 var entityPos = entity.OldPosition.LinearInterpolation(entity.Position, i);
        //                 entityRect.X = entityPos.X + thisRect.X;
        //                 entityRect.Y = entityPos.Y + thisRect.Y;
        //                 entityRect.Width = thisRect.Width;
        //                 entityRect.Height = thisRect.Height;

        //                 comparRect.X = other.Position.X + otherRect.X;
        //                 comparRect.Y = other.Position.Y + otherRect.Y;
        //                 comparRect.Width = otherRect.Width;
        //                 comparRect.Height = otherRect.Height;

        //                 if (entityRect.IntersectsWith(comparRect))
        //                 {
        //                     entity.OnHit(other);
        //                     result = true;
        //                 }
        //             }
        //         }
        //     }
        // }    

        foreach (var thisRect in entity.Hitbox.rectangles)
        {
            foreach (var otherRect in other.Hitbox.rectangles)
            {
                entityRect.X = entity.Position.X + thisRect.X;
                entityRect.Y = entity.Position.Y + thisRect.Y;
                entityRect.Width = thisRect.Width;
                entityRect.Height = thisRect.Height;

                comparRect.X = other.Position.X + otherRect.X;
                comparRect.Y = other.Position.Y + otherRect.Y;
                comparRect.Width = otherRect.Width;
                comparRect.Height = otherRect.Height;

                if (entityRect.IntersectsWith(comparRect))
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
        foreach (var entity in Memory.Colliders)
        {
            if (entity.Hitbox is null)
                continue;
            bool collision = false;
            foreach (var other in Memory.Colliders)
            {
                if (other.Hitbox is null)
                    continue;
                if (entity != other && entity.VerifyCollision(other))
                {
                    collision = true;
                    break;
                }
            }

            entity.Hitbox.Pen = collision ? Pens.Red : Pens.Blue;
        }
    }
}
