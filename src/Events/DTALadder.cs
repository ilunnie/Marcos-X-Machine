using System.Drawing;
using System.Windows.Forms;

public class DTALayer : IEvent
{
    private IEvent nextEvent = null;
    public IEvent Next { set => nextEvent = value; }

    private Player player = null;

    private PointF ladder = new PointF((1 + 9 * 3) * (TileSets.spriteMapSize.Width / 3), (2 + 10 * 3) * (TileSets.spriteMapSize.Height / 3));
    private bool inLadder = false;
    private PointF door = new PointF((.7f + 4 * 3) * (TileSets.spriteMapSize.Width / 3), (1 + 8 * 3) * (TileSets.spriteMapSize.Height / 3));

    private Hitbox temp = null;

    private (object o, KeyEventArgs e) keydown = (new object(), new KeyEventArgs(Keys.None));
    private (object o, KeyEventArgs e) keyup = (new object(), new KeyEventArgs(Keys.None));
    private (object o, MouseEventArgs e) mousemove = (new object(), new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));

    public IEvent OnFrame()
    {
        if (player == null)
        {
            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    { player = (Player)entity.Mob; break; }
            }
            return this;
        }
        if (!inLadder) {
            var distance = player.Entity.Position.Distance(ladder);
            if (distance < player.Speed * Memory.Frame)
            {
                player.Entity.Move(ladder);
                inLadder = true;
                return this;
            }
            var r = player.Speed / distance;
            player.Entity.Move(player.Entity.Position.LinearInterpolation(ladder, r * Memory.Frame));
        } else {
            if (temp is null && player.Entity.Hitbox is not null)
            {
                temp = player.Entity.Hitbox;
                player.Entity.Hitbox = null;
            }
            var distance = player.Entity.Position.Distance(door);
            if (distance < player.Speed * Memory.Frame)
            {
                player.Entity.Move(door);
                player.Entity.Hitbox = temp;
                player.OnKeyDown(keydown.o, keydown.e);
                player.OnKeyUp(keyup.o, keyup.e);
                player.OnMouseMove(mousemove.o, mousemove.e);
                return nextEvent;
            }
            var r = player.Speed / distance;
            player.Entity.Move(player.Entity.Position.LinearInterpolation(door, r * Memory.Frame));
        }
        return this;
    }

    public void OnKeyDown(object o, KeyEventArgs e) => this.keydown = (o, e);

    public void OnKeyUp(object o, KeyEventArgs e) => this.keyup = (o, e);

    public void OnMouseMove(object o, MouseEventArgs e) => this.mousemove = (o, e);
}
