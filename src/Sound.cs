using NAudio.Wave;

public class Sound
{
    public AudioFileReader Audio { get; protected set; }
    public WaveOutEvent WaveOut { get; protected set; }
    public WaveOut waveOutLoop { get; protected set; }

    public Sound(WaveOutEvent waveOut, AudioFileReader audio)
    {
        this.WaveOut = waveOut;
        this.Audio = audio;
    }
    public Sound(Sound sound) : this(sound.WaveOut, sound.Audio) { }
    public virtual void Play()
    {
        if (WaveOut.PlaybackState != PlaybackState.Playing)
        {
            WaveOut.Stop();
            WaveOut.Init(Audio);
            WaveOut.Play();
        }
        else
        {
            WaveOut.Stop();
            WaveOut.Init(Audio);
            WaveOut.Play();
        }
    }

    public virtual void PlayLoop(WaveStream sourceStream)
    {
        if (waveOutLoop == null)
            {
                LoopedAudio loop = new LoopedAudio(sourceStream);
                waveOutLoop = new WaveOut();
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
        if (WaveOut.PlaybackState == PlaybackState.Playing)
        {
            WaveOut.Stop();
        }
    }

    public virtual void Restart()
    {
        WaveOut.Stop();
        Audio.Position = 0;
        WaveOut.Init(Audio);
        WaveOut.Play();
    }

    public virtual void Dispose()
    {
        if (WaveOut != null)
        {
            WaveOut.Stop();
            WaveOut.Dispose();
        }
        if(Audio != null)
        {
            Audio.Dispose();
        }
    }
}