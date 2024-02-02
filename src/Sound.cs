using System.Threading;
using NAudio.Wave;

public class Sound
{
    public static WaveOutEvent MusicWaveOut = new WaveOutEvent();
    public static WaveOutEvent EffectWaveOut = new WaveOutEvent();

    public AudioFileReader Audio { get; protected set; }
    public WaveOutEvent waveOut { get; protected set; }
    public WaveOutEvent waveOutLoop { get; protected set; }

    public Sound(WaveOutEvent waveOut, AudioFileReader audio)
    {
        this.waveOut = waveOut;
        this.Audio = audio;
    }
    public virtual void Play()
    {
        waveOut.Stop();
        waveOut.Dispose();
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
    }

    public static void SetVolume(int value)
    {
        if (value > 100)
            EffectWaveOut.Volume = 1.0f;
        else if (value < 0)
            EffectWaveOut.Volume = 0f;
        else EffectWaveOut.Volume = value / 100f;
    }
    public static void SetMusicVolume(int value)
    {
        if (MusicWaveOut != null)
        {
            if (value > 100)
                MusicWaveOut.Volume = 1.0f;
            else if (value < 0)
                MusicWaveOut.Volume = 0f;
            else
                MusicWaveOut.Volume = value / 100f;
        }
    }

    public virtual void PlayLoop(LoopedAudio stream, long position)
    {
        if (waveOutLoop == null)
        {
            LoopedAudio loop = new LoopedAudio(stream);
            waveOutLoop = new WaveOutEvent();
            long audioStart = (long)(position * stream.WaveFormat.AverageBytesPerSecond);
            loop.Position = audioStart;
            waveOutLoop.Init(loop);
            waveOutLoop.Play();
        }
        else
        {
            waveOutLoop.Stop();
            waveOutLoop.Dispose();
            waveOutLoop = null;
        }
    }

    public virtual void Stop()
    {
        if (waveOut.PlaybackState == PlaybackState.Playing)
        {
            waveOut.Stop();
            waveOut.Dispose();
        }
    }

    public virtual void Restart()
    {
        waveOut.Stop();
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
    }

    public virtual void Dispose()
    {
        if (waveOut != null)
        {
            waveOut.Stop();
            waveOut.Dispose();
        }
        if (Audio != null)
        {
            Audio.Dispose();
        }
    }
}