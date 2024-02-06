using System;
using System.Threading;

public class MenuLoad : Loader
{
    protected override void Init(Player player, LoadProcessBuilder builder)
    {
        Random rand = new Random();
        for (int i = 0; i < rand.Next(100, 1000); i++)
        {
            builder.Then(() => Thread.Sleep(rand.Next(0, 1000)));
        }
    }
}
