using System.Drawing;

public class DTALoad : Loader
{
    protected override void Init(Player player, LoadProcessBuilder builder)
    => builder
            .Then(() => {
                Memory.Entities.Clear();
                Memory.Map.Clear();
            })
            .Then(() => Memory.Entities.Add(player.Entity))
            .Then(() => TileSets.SetSprites("src/sprites/tileset/Tile.png"))
                .And(() => TileSets.ReadFile("src/Area/DTA.csv"))
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
            // .Then(() => new Teleport(
            //     new PointF(10 * TileSets.spriteMapSize.Width, 7 * TileSets.spriteMapSize.Height),
            //     new SizeF(120 / 3, 120),
            //     new PointF(640, 430),
            //     new EtsLevel()
            // ))
            .Then(() => new MolFaseDois() { Entity = new MolEntity(new PointF(300,300)) })
            .Then(() => Memory.Level.Event = new Spotlights())
            .Then(() => {
                foreach (var entity in Memory.Entities)
                {
                    if (entity.Mob is not null) entity.Mob.OnInit();
                }
            })
                ;
}