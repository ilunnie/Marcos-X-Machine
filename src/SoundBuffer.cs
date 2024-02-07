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

    public Sound Get(string file, SoundType type)
    {
        // var key = $"{file}::{type}";
        // if (map.ContainsKey(key))
        //     return map[key];

        Sound newSound = type switch
        {
            SoundType.Effect => new SoundEffect(new AudioFileReader(file)),
            SoundType.Music => new MusicEffect(new AudioFileReader(file)),
            _ => null
        };
        // map.Add(key, newSound);
        return newSound;
    }
}