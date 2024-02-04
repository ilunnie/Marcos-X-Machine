using System;
using System.Collections.Generic;
using System.Drawing;

public class EntradaDTAload : Loader
{

    protected override void Init(Player player, LoadProcessBuilder builder)
    => builder
            .Then(() => {
                Memory.Entities.Clear();
                Memory.Entities.Add(player.Entity);
                Memory.Map.Clear();
            })
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
            .Then(() => SoundBuilder.PlayBackGroundMusic(SoundType.Music, "src/Sounds/Musics/introMusic.wav", 20))
            .Then(() => new Teleport(
                new PointF((4 * 3) * (TileSets.spriteMapSize.Width / 3), 9 * 3 * (TileSets.spriteMapSize.Height / 3)),
                new SizeF(TileSets.spriteMapSize.Width / 2, TileSets.spriteMapSize.Height / 3 * 2),
                PointF.Empty,
                new DTALevel()
            ))
                .And(() => new EventCaller(
                    new PointF((1 + 9 * 3) * (TileSets.spriteMapSize.Width / 3), (2 + 10 * 3) * (TileSets.spriteMapSize.Height / 3)),
                    new SizeF(new SizeF(TileSets.spriteMapSize.Width / 3, TileSets.spriteMapSize.Height / 3)),
                    new DTALayer()
                ))
            ;
}