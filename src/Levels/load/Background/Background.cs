using System;
using System.Drawing;

public static class Background
{
    public static Image Random()
    {
        Random rand = new Random();
        return SpriteBuffer.Current.Get(
            "src/Levels/Load/Background/Marcos (" +
            rand.Next(1, 7).ToString() + ").png"
        );
    }
}