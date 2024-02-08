using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NAudio.Wave;

public class Sound
{
    public static WaveOut MusicWaveOut = new WaveOut();
    public static WaveOutEvent EffectWaveOut = new WaveOutEvent();
    private static List<Sound> effects = new();
    private static List<Sound> musics = new();
    private static int effectVolume = 100;
    private static int musicVolume = 100;
    public static DateTime startTime;
    public static void SetEffectVolume(int value)
    {
        effectVolume = value;
        foreach (var effect in effects)
        {
            if (value > 100)
                effect.Audio.Volume = 1.0f;
            else if (value < 0)
                effect.Audio.Volume = 0f;
            else effect.Audio.Volume = value / 100f;
        }
    }
    public static void SetMusicVolume(int value)
    {
        musicVolume = value;
        foreach (var music in musics)
        {
            if (value > 100)
                music.Audio.Volume = 1.0f;
            else if (value < 0)
                music.Audio.Volume = 0f;
            else music.Audio.Volume = value / 100f;
        }
    }
    public static void StopMusics()
    {
        MusicWaveOut.Stop();
        MusicWaveOut.Dispose();
        var newWaveOut = new WaveOut();
        MusicWaveOut = newWaveOut;
        musics.Clear();
    }
    
    public static Sound OpenFrom(SoundType type, string file)
        => SoundBuffer.Current.Get(file, type);
    
    public AudioFileReader Audio { get; protected set; }
    public IWavePlayer waveOut { get; protected set; }

    public Sound(IWavePlayer waveOut, AudioFileReader audio)
    {
        if (waveOut == MusicWaveOut)
        {
            musics.Add(this);
            audio.Volume = musicVolume / 100f;
        }
        else if (waveOut == EffectWaveOut)
        {
            effects.Add(this);
            audio.Volume = effectVolume / 100f;
        }
        
        this.waveOut = waveOut;
        this.Audio = audio;
    }
    public virtual void Play()
    {
        if (waveOut.PlaybackState == PlaybackState.Playing)
        {
            // waveOut.Stop();
            // waveOut.Dispose();
            // Audio.Position = 0;
            waveOut = new WaveOutEvent();
        }
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
        startTime = DateTime.Now;
    }
    public virtual void PlayOnce()
    {
        if (waveOut.PlaybackState == PlaybackState.Playing)
        {
            waveOut.Stop();
            waveOut.Dispose();
            Audio.Position = 0;
        }
        
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
    }

    public virtual void PlayAt(long position)
    {
        if (waveOut.PlaybackState == PlaybackState.Playing)
        {
            waveOut.Stop();
            waveOut.Dispose();
            Audio.Position = 0;
        }
        waveOut.Init(Audio);
        long audioStart = (long)(position * waveOut.OutputWaveFormat.AverageBytesPerSecond);
        Audio.Position = audioStart;
        waveOut.Play();
    }
    
    public void Wait(Action action)
    {
        EventHandler<StoppedEventArgs> stopedEvent = null;
        stopedEvent = (o, e) =>
        {
            action();   
            waveOut.PlaybackStopped -= stopedEvent;
        };
        waveOut.PlaybackStopped += stopedEvent;
    }

    public virtual void Stop()
    {
        waveOut.Stop();
        waveOut.Dispose();
        waveOut = null;
    }

    public virtual void Restart()
    {
        waveOut.Stop();
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
    }
}