using System;
using System.Collections.Generic;
using System.Drawing;

public class FrenteEtsLoad : Loader
{
    protected override void Init(Player player, LoadProcessBuilder builder)
        => builder
            .Then(() => {
                Memory.Entities.Clear();
                Memory.Map.Clear();
            })
            .Then(() => Memory.Entities.Add(player.Entity))
            .Then(() => TileSets.SetSprites("src/sprites/tileset/Tile.png"))
                .And(() => TileSets.ReadFile("src/Area/FrenteEts.csv"))
            .Then(() => {
                for (int i = 0; i < TileSets.Count; i++)
                {
                    var (value, column, row) = TileSets.Next().Value;
                    TileSets.Set(value, column, row);

                    if (column > Xmax) Xmax = column;
                    if (column < Xmin) Xmin = column;
                    if (row > Ymax) Ymax = row;
                    if (row < Ymin) Ymin = row;
                }
            })
            .Then(TileSets.CloseFile)
            .Then(() => {
                Camera.MaxLimitX = (Xmax + 1) * TileSets.spriteMapSize.Width;
                Camera.MaxLimitY = (Ymax + 1) * TileSets.spriteMapSize.Height;
                Camera.MinimumLimitX = Xmin * TileSets.spriteMapSize.Width;
                Camera.MinimumLimitY = Ymin * TileSets.spriteMapSize.Height;
            })
            .Then(() => player.Entity.FocusCam(false))
            .Then(() => new Teleport(
                new PointF(8 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, 3 * TileSets.spriteMapSize.Height),
                new SizeF(TileSets.spriteMapSize.Width, TileSets.spriteMapSize.Height ),
                new PointF(18 * TileSets.spriteMapSize.Width + TileSets.spriteMapSize.Width / 3, 4 * TileSets.spriteMapSize.Height),
                new EtsLevel()
            ));
}