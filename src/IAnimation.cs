public interface IAnimation
{
    public IAnimation NextFrame();
    public IAnimation Draw();
    public IAnimation Next();
    public IAnimation Clone();
    public IAnimation Skip()
    {
        if (this.Next() is null)
            return this.Clone();

        return this.Next();
    }
}