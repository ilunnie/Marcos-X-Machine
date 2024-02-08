using System;
using System.Collections.Generic;
using System.Drawing;

public class FrenteAlmoxarifadoLoad : Loader
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
                .And(() => TileSets.ReadFile("src/Area/FrenteAlmoxarifado.csv"))
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
                new BasicBot () {Entity = new BasicBotEntity(new PointF(TileSets.spriteMapSize.Width * 5.5f, TileSets.spriteMapSize.Height * 13))};
            })
            .Then(() => {
                new BasicBot () {Entity = new BasicBotEntity(new PointF(TileSets.spriteMapSize.Width * 17.5f, TileSets.spriteMapSize.Height * 2))};
            })
            .Then(() => {
                new MissingHeadBot () {Entity = new MissingHeadBotEntity(new PointF(TileSets.spriteMapSize.Width * 13.5f, TileSets.spriteMapSize.Height * 13.5f))};
            })
            .Then(() => {
                new Kirby () {Entity = new KirbyEntity(new PointF(TileSets.spriteMapSize.Width * 5.5f, TileSets.spriteMapSize.Height * 5.5f))};
            })
            ;
}