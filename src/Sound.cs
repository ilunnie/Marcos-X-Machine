using NAudio.Wave;

public class Sound
{
    public AudioFileReader Audio { get; protected set; }
    public WaveOutEvent waveOut { get; protected set; }
    public WaveOut waveOutLoop { get; protected set; }

    public Sound(WaveOutEvent waveOut, AudioFileReader audio)
    {
        this.waveOut = waveOut;
        this.Audio = audio;
    }
    public Sound(Sound sound) : this(sound.waveOut, sound.Audio) { }
    public virtual void Play()
    {
        waveOut.Stop();
        waveOut.Dispose();
        Audio.Position = 0;
        waveOut.Init(Audio);
        waveOut.Play();
    }
    public virtual void PlayLoop(LoopedAudio stream, long position)
    {
        if (waveOutLoop == null)
        {
            LoopedAudio loop = new LoopedAudio(stream);
            waveOutLoop = new WaveOut();
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