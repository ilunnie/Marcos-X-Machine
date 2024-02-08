using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Drop : Entity
{
    public static List<(Type Entity, float Opportunity, double distance)> drops { get; set; } = new List<(Type, float, double)>
    {
        (typeof(BulletGun), 1, 30),
        (typeof(DubstepGun), 1, 30),
        (typeof(ElectricRoboticGuitar), 1, 30),
        (typeof(GunBasicBot), 1, 25),
        (typeof(AcidGun), 1, 30),
        (typeof(PogSharkGun), 1, 30),
        (typeof(CSharkGun), 1, 30),
        (typeof(VandalReaver), 1, 10),
    };
    private static Random rand = new Random();
    public static double Distance { get; set; }

    public static Entity Random()
    {
        float totalOpportunity = drops.Sum(drop => drop.Opportunity);
        float randomValue = (float)rand.NextDouble() * totalOpportunity;

        float cumulativeOpportunity = 0;
        foreach (var (entity, opportunity, distance) in drops)
        {
            cumulativeOpportunity += opportunity;
            if (randomValue <= cumulativeOpportunity)
            {
                Drop.Distance = distance;
                return (Entity)Activator.CreateInstance(entity);
            }
        }

        return null;
    }

    private static List<Drop> Discart = new();
    private static List<Drop> ToPlayer = new();
    public static bool PlayerInDrop => ToPlayer.Count > 0;

    public Entity Entity { get; set; }
    private double _distance;

    public Drop(Entity entity, PointF position)
    {
        this._distance = Distance;
        this.Entity = entity;
        this.Position = position;
        this.Size = entity.Size;
        this.Animation = entity.Animation;

        this.Hitbox.rectangles.Add(
            new RectangleF(
                0, 0,
                Size.Width, Size.Height
            )
        );
    }
    public Drop(PointF position) : this(Drop.Random(), position) { }

    public override void OnHit(Entity entity)
    {
        if (entity.Mob is not Player || entity is Projectile) return;

        Drop.Discart.Add(this);
    }

    public override void Draw(float angle = 0, int layer = 1)
    {
        Drop.ToPlayer.Clear();
        Drop.ToPlayer.AddRange(Drop.Discart);
        Drop.Discart.Clear();
        base.Draw(angle, layer);
    }

    public static void Get(Player player)
    {
        Drop closer = null;
        double closerDistance = double.MaxValue;
        foreach (var drop in Drop.ToPlayer)
        {
            PointF dropAnchor = drop.Entity.Anchor;
            PointF playerCenter = new PointF(
                player.Entity.Position.X + player.Entity.Size.Width,
                player.Entity.Position.Y + player.Entity.Size.Height
            );
            double distance = playerCenter.Distance(dropAnchor);

            if (distance < closerDistance)
            {
                closer = drop;
                closerDistance = distance;
            }
        }
        if (closer is null) return;
        Drop.ToPlayer.Clear();
        Drop.Discart.Clear();
        closer.Destroy();
        for (int i = 0; i < player.MaxHand && i < player.Hands.Count; i++)
        {
            if (player.Hands[i] == null)
            {
                player.Hands[i] = new Hand(player, closer.Entity, closer._distance);
                return;
            }
        }
        if (player.Hands.Count < player.MaxHand)
        {
            player.Hands.Add(new Hand(player, closer.Entity, closer._distance));
            return;
        }
        var old = player.Hands[player.hand];
        player.Hands[player.hand] = new Hand(player, closer.Entity, closer._distance);
        Distance = old.Distance;
        var playerEntity = player.Entity;

        Memory.PostProcessing.Enqueue(() => new Drop(old.Entity, new PointF(
            playerEntity.Position.X + playerEntity.Size.Width / 2,
            playerEntity.Position.Y + playerEntity.Size.Height * 0.8f)));
    }
}