using System;
using System.Drawing;
using System.Windows.Forms;

public class Player : Mob
{
    private bool isMovingLeft { get; set; } = false;
    private bool isMovingRight { get; set; } = false;
    private bool isMovingUp { get; set; } = false;
    private bool isMovingDown { get; set; } = false;
    public Walk WalkXLeft = Walk.Stop;
    public Walk WalkXRight = Walk.Stop;
    public Walk WalkYUp = Walk.Stop;
    public Walk WalkYDown = Walk.Stop;

    public Player()
    {
        var revolver = new RevolverEntity();
        this.Hands.Add(new Hand(this, revolver, 25));

        this.Life = 10;
        this.MaxLife = 10;
    }

    public override void OnFrame()
    {
        if(Memory.MouseButton == MouseButtons.Left)
            this.Hands[hand].Click();
        this.Hands[hand].Set(Memory.Cursor);
        this.Hands[hand].Draw();
        if(isMovingLeft || isMovingRight || isMovingUp || isMovingDown)
            this.Entity.AddWalkingAnimation("marcos/marcos-sprites-old.png", Direction);
        else
            this.Entity.AddStaticAnimation("marcos/marcos-sprites-old.png", Direction);
        this.Entity.Animation = this.Entity.Animation.Skip();

        WalkXLeft = isMovingLeft == true ? Walk.Back : Walk.Stop;
        WalkXRight = isMovingRight == true ? Walk.Front : Walk.Stop;
        WalkYUp = isMovingUp == true ? Walk.Back : Walk.Stop;
        WalkYDown = isMovingDown == true ? Walk.Front : Walk.Stop;

        this.Move();
    }

    public override void OnMouseMove(object o, MouseEventArgs e)
    {
        PointF player = this.Entity.Position.PositionOnCam();
        PointF mouse = e.Location;

        VerifyPosition(player, mouse);
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
        => Entity.Move(new PointF(Entity.Position.X + Speed * ((int)WalkXRight + (int)WalkXLeft) * Memory.Frame, Entity.Position.Y + Speed * ((int)WalkYUp + (int)WalkYDown) * Memory.Frame));
}