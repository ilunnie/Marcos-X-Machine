using System.Drawing;

public static class Collision
{
    public static bool VerifyCollision(this Entity entity, Entity other)
    {
        RectangleF entityRect = new RectangleF();
        RectangleF comparRect = new RectangleF();

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
                    return true;
                }
            }
        }

        return false;
    }

    public static void VerifyCollision()
    {
        foreach (var entity in Memory.Entities)
        {
            if (entity.Hitbox is null)
                continue;
            bool collision = false;
            foreach (var other in Memory.Entities)
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
