using System;
using System.Collections.Generic;

public class EtsLoad
{
    public Queue<Action> Queue { get; private set; }

    public int Init()
    {
        Queue.Enqueue(() => {});
        return int.MaxValue;
    }
}