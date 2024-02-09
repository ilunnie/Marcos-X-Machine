using System;
using System.Collections.Generic;
using System.Drawing;

public class EntradaDTAload : Loader
{

    protected override void Init(Player player, LoadProcessBuilder builder)
    => builder
            .Then(() => {
                Memory.Entities.Clear();
                Memory.Projectiles.Clear();
                Memory.Map.Clear();
                player.Destiny = PointF.Empty;
            })
            .Then(() => Memory.Entities.Add(player.Entity))
            .Then(() => TileSets.SetSprites("src/sprites/tileset/Tile.png"))
                .And(() => TileSets.ReadFile("src/Area/EntradaDTA.csv"))
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
            .Then(() => {
                new ZagoBot () {Entity = new ZagoBotEntity(new PointF(TileSets.spriteMapSize.Width * 22.5f, TileSets.spriteMapSize.Height * 14.5f))};
            })
            .Then(() => {
                new Kirby () {Entity = new KirbyEntity(new PointF(TileSets.spriteMapSize.Width * 12.5f, TileSets.spriteMapSize.Height * 15))};
            })
            .Then(() => {
                new Kirby () {Entity = new KirbyEntity(new PointF(TileSets.spriteMapSize.Width * 25.5f, TileSets.spriteMapSize.Height * 18))};
            })
            .Then(() => {
                new Kirby () {Entity = new KirbyEntity(new PointF(TileSets.spriteMapSize.Width * 19.5f, TileSets.spriteMapSize.Height * 5))};
            })
            ;
}