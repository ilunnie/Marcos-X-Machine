using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

public class SubterraneoLoad : Loader
{
    protected override void Init(Player player, LoadProcessBuilder builder)
    => builder
            .Then(() =>
            {
                Memory.Entities.Clear();
                Memory.Map.Clear();
            })
            .Then(() => Memory.Entities.Add(player.Entity))
            .Then(() => TileSets.SetSprites("src/sprites/tileset/Tile.png"))
                .And(() => TileSets.ReadFile("src/Area/Subterraneo.csv"))
            .Then(() =>
            {
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
            .Then(() =>
            {
                Camera.MaxLimitX = (Xmax + 1) * TileSets.spriteMapSize.Width;
                Camera.MaxLimitY = (Ymax + 1) * TileSets.spriteMapSize.Height;
                Camera.MinimumLimitX = Xmin * TileSets.spriteMapSize.Width;
                Camera.MinimumLimitY = Ymin * TileSets.spriteMapSize.Height;
            })
            .Then(() => player.Entity.FocusCam(false))
            .Then(() => new Trevis(){ Entity = new TrevisEntity(new PointF(900,900))})
            .Then(() => new Teleport(
                new PointF(15 * TileSets.spriteMapSize.Width, 3 * TileSets.spriteMapSize.Height + TileSets.spriteMapSize.Height / 3),
                new SizeF(20 , 20),
                new PointF(1680, 430),
                new EtsLevel()
            ));

}