using System;
using System.Drawing;
using System.Linq;
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
    public bool isMoving = false;
    public PointF tp { get; set; } = PointF.Empty;

    public Player()
    {
        var acidgun = new AcidGun();
        this.Hands.Add(new Hand(this, acidgun, 30));

        var roboticguitar = new ElectricRoboticGuitar();
        this.Hands.Add(new Hand(this, roboticguitar, 30));

        var pogshark = new PogSharkGun();
        this.Hands.Add(new Hand(this, pogshark, 30));

        var cshark = new CSharkGun();
        this.Hands.Add(new Hand(this, cshark, 30));

        var bulletgun = new BulletGun();
        this.Hands.Add(new Hand(this, bulletgun, 30));

        var dubstepgun = new DubstepGun();
        this.Hands.Add(new Hand(this, dubstepgun, 30));

        var revolver = new Revolver();
        this.Hands.Add(new Hand(this, revolver, 20));

        var gun_basicbot = new GunBasicBot();
        this.Hands.Add(new Hand(this, gun_basicbot, 25));

        this.Life = 10;

        this.MaxLife = 10;
    }

    public override void OnFrame()
    {
        if (!this.tp.IsEmpty)
        {
            this.Entity.Move(tp);
            this.tp = PointF.Empty;
        }
        if (Memory.MouseButton == MouseButtons.Left)
            this.Hands[hand].Click();
        this.Hands[hand].Set(Memory.Cursor, true);
        this.Hands[hand].Draw();
        if (isMovingLeft || isMovingRight || isMovingUp || isMovingDown)
        {
            this.Entity.AddWalkingAnimation("marcos/marcos-sprites-old.png", Direction);
        }
        else
        {
            this.Entity.AddStaticAnimation("marcos/marcos-sprites-old.png", Direction);
            if(this.Life < 0)
                Sound.OpenFrom(SoundType.Music, "src/Sounds/Marcos/andando.wav");
        }
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

        if (e.Delta > 0)
        {
            this.hand += 1;
            if (this.hand >= this.Hands.Count) this.hand = this.Hands.Count - 1;
        }
        if (e.Delta < 0)
        {
            this.hand -= 1;
            if (this.hand < 0) this.hand = 0;
        }
    }

    public override void OnKeyDown(object o, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.A:
                isMovingLeft = true;
                isMoving = true;
                break;
            case Keys.D:
                isMovingRight = true;
                isMoving = true;
                break;
            case Keys.W:
                isMovingUp = true;
                isMoving = true;
                break;
            case Keys.S:
                isMovingDown = true;
                isMoving = true;
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

        if (!isMovingUp && !isMovingRight && !isMovingDown && !isMovingRight)
            isMoving = false;
    }

    private void Move()
        => Entity.Move(new PointF(Entity.Position.X + Speed * ((int)WalkXRight + (int)WalkXLeft) * Memory.Frame, Entity.Position.Y + Speed * ((int)WalkYUp + (int)WalkYDown) * Memory.Frame));
}
