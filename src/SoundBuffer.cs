using System.Collections.Generic;
using NAudio.Wave;

public class SoundBuffer
{
    private SoundBuffer() {  }
    private static SoundBuffer crr = new();
    public static SoundBuffer Current => crr;
    public static void Reset()
        => crr = new();

    private Dictionary<string, Sound> map = new();

    public Sound Get(string file)
    {
        if (map.ContainsKey(file))
            return map[file];

        var newSound = new Sound(new WaveOutEvent(), new AudioFileReader(file));
        map.Add(file, newSound);
        return newSound;
    }
}