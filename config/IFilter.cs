using System.Drawing;

public interface IFilter
{
    public void Add();

    public void Remove();

    public unsafe void Apply(ref Graphics g);
}