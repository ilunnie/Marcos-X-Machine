using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class LoadProcessBuilder
{
    int maxLoad = -1;
    int currentLoad = 0;
    Queue<ParallelLoadProcessBuild> queue = new();

    public int LoadNext()
    {
        if (queue.Count == 0)
            return 100;

        var item = queue.Dequeue();
        item.Load();

        currentLoad = queue.Sum(i => i.Size);
        if (maxLoad == -1)
            maxLoad = currentLoad;
        return 100 * (maxLoad - currentLoad) / maxLoad;
    }
    public ParallelLoadProcessBuild Then(Action action)
    {
        var parallel = new ParallelLoadProcessBuild(this);
        parallel.And(action);
        queue.Enqueue(parallel);
        return parallel;
    }
    public class ParallelLoadProcessBuild
    {
        public int Size => processList.Count;
        public ParallelLoadProcessBuild(LoadProcessBuilder parent)
            => this.parent = parent;
        LoadProcessBuilder parent = null;
        List<Action> processList = new();

        public void Load()
        {
            if (processList.Count == 1)
                processList[0]();
            else Parallel.ForEach(processList, process => process());
        }

        public ParallelLoadProcessBuild And(Action action)
        {
            processList.Add(action);
            return this;
        }

        public ParallelLoadProcessBuild Then(Action action)
            => parent.Then(action);
    }
}