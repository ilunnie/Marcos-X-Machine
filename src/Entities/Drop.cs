using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Drop : Entity
{
    public static List<(Entity Entity, float Opportunity)> drops { get; set; } = new List<(Entity, float)>
    {
        (new DubstepGun(), 1),
        (new ElectricRoboticGuitar(), 1),
        // (new GunBasicBotEntity(), 1),
        // (new RevolverEntity(), 1)
    };

    public static Entity Random()
    {
        float totalOpportunity = drops.Sum(drop => drop.Opportunity);
        float randomValue = (float)new Random().NextDouble() * totalOpportunity;

        float cumulativeOpportunity = 0;
        foreach (var (entity, opportunity) in drops)
        {
            cumulativeOpportunity += opportunity;
            if (randomValue <= cumulativeOpportunity)
                return entity;
        }

        return null;
    }

    public Drop(Entity entity, PointF position)
    {

    }
    public Drop(PointF position) : this(Drop.Random(), position) {}
}