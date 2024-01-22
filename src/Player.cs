using System.Drawing;
using System.Windows.Forms;

public class Player : Mob
{
    public Direction Direction = Direction.BottomLeft;
    private bool isMovingLeft { get; set; } = false;
    private bool isMovingRight { get; set; } = false;
    private bool isMovingUp { get; set; } = false;
    private bool isMovingDown { get; set; } = false;
    public Walk WalkX = Walk.Stop;
    public Walk WalkY = Walk.Stop;

    public override void OnFrame()
    {
        if(isMovingLeft)
            WalkX = Walk.Back;
        else if(isMovingRight)
            WalkX = Walk.Front;
        else if(isMovingUp)
            WalkY = Walk.Back;
        else if(isMovingDown)
            WalkY = Walk.Front;
        else
        {
            WalkX = Walk.Stop;
            WalkY = Walk.Stop;
        }

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
        => Entity.Move(new PointF(Entity.Position.X + Speed * (int)WalkX, Entity.Position.Y + Speed * (int)WalkY));
}