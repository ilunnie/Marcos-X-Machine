using System.Drawing;
using System.Windows.Forms;

public class Player : Mob
{
    public Direction Direction = Direction.BottomLeft;
    private bool isMovingLeft { get; set; } = false;
    private bool isMovingRight { get; set; } = false;
    private bool isMovingUp { get; set; } = false;
    private bool isMovingDown { get; set; } = false;
    public Walk WalkXLeft = Walk.Stop;
    public Walk WalkXRight = Walk.Stop;
    public Walk WalkYUp = Walk.Stop;
    public Walk WalkYDown = Walk.Stop;

    public override void OnFrame()
    {
        WalkXLeft = isMovingLeft == true ? Walk.Back : Walk.Stop;
        WalkXRight = isMovingRight == true ? Walk.Front : Walk.Stop;
        WalkYUp = isMovingUp == true ? Walk.Back : Walk.Stop;
        WalkYDown = isMovingDown == true ? Walk.Front : Walk.Stop;

        this.Move();
    }

    public override void OnDestroy()
        => this.Entity.AddAnimation(new MarcosDying());

    public override void OnMouseMove(object o, MouseEventArgs e)
    {
        
    }

    public override void OnKeyDown(object o, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.A:
                isMovingLeft = true;
                break;
            case Keys.D:
                isMovingRight = true;
                break;
            case Keys.W:
                isMovingUp = true;
                break;
            case Keys.S:
                isMovingDown = true;
                break;
        }
    }

    public override void OnKeyUp(object o, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.A:
                isMovingLeft = false;
                break;
            case Keys.D:
                isMovingRight = false;
                break;
            case Keys.W:
                isMovingUp = false;
                break;
            case Keys.S:
                isMovingDown = false;
                break;
        }
    }

    private void Move()
        => Entity.Move(new PointF(Entity.Position.X + Speed *  ((int)WalkXRight + (int)WalkXLeft), Entity.Position.Y + Speed * ((int)WalkYUp + (int)WalkYDown)));
}