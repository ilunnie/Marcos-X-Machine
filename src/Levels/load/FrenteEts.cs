using System;
using System.Collections.Generic;
using System.Drawing;

public class FrenteEtsLoad
{
    public Queue<Action> Queue { get; private set; } = new Queue<Action>();
    public int Xmax = int.MinValue;
    public int Ymax = int.MinValue;
    public int Xmin = int.MaxValue;
    public int Ymin = int.MaxValue;

    public int Init(Player player)
    {
        Queue.Enqueue(() => {
            Memory.Entities.Clear();
            Memory.Entities.Add(player.Entity);
        });
        Queue.Enqueue(() => Memory.Map.Clear());
        Queue.Enqueue(() => TileSets.SetSprites("src/sprites/tileset/Tile.png"));
        TileSets.ReadFile("src/Area/FrenteEts.csv");
        for (int i = 0; i < TileSets.Count; i++)
        {
            Queue.Enqueue(() =>
            {
                var (value, column, row) = TileSets.Next().Value;
                TileSets.Set(value, column, row);

                if (column > Xmax) Xmax = column;
                if (column < Xmin) Xmin = column;
                if (row > Ymax) Ymax = row;
                if (row < Ymin) Ymin = row;
            });
        }
        Queue.Enqueue(() => TileSets.CloseFile());

        Queue.Enqueue(() => 
        {
            Camera.MaxLimitX = (Xmax + 1) * TileSets.spriteMapSize.Width;
            Camera.MaxLimitY = (Ymax + 1) * TileSets.spriteMapSize.Height;
            Camera.MinimumLimitX = Xmin * TileSets.spriteMapSize.Width;
            Camera.MinimumLimitY = Ymin * TileSets.spriteMapSize.Height;
        });

        Queue.Enqueue(() => {
            player.Entity.FocusCam(false);
        });

        Queue.Enqueue(() =>
        {
            new Teleport(
                new PointF(8 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, 3 * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width, TileSets.spriteMapSize.Height ),
                new PointF(18 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, 4 * TileSets.spriteMapSize.Height),
                new EtsLevel()
            );
        });

        return Queue.Count;
    }
}