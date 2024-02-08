using System.Collections.Generic;
using System.Drawing;

public class Bot : Mob
{
    protected bool isMoving = false;
    protected float distanceFromPlayer = 500;

    protected Rectangle rectangle = Rectangle.Empty;
    protected PointF nextPosition = PointF.Empty;
    protected PointF lastPlayerPosition = PointF.Empty;
    protected Stack<int> nextMoves;
    protected Player player = null;
}