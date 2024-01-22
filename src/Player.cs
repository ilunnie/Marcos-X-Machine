public class Player : Mob
{
    public Direction Direction = Direction.BottomLeft;
    public Walk WalkX = Walk.Stop;
    public Walk WalkY = Walk.Stop;

    public override void OnDestroy()
    {
        this.Entity.AddAnimation(new MarcosDying());
    }
}