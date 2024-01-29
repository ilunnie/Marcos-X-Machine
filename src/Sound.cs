using NAudio.Wave;

public class Sound
{
    public AudioFileReader Audio { get; protected set; }
    public WaveOutEvent WaveOut { get; protected set; }

    public Sound(WaveOutEvent waveOut, AudioFileReader audio)
    {
        this.WaveOut = waveOut;
        this.Audio = audio;
    }
    public Sound(Sound sound) : this(sound.WaveOut, sound.Audio) {}

    public virtual void Play()
    {
        WaveOut.Stop();
        WaveOut.Init(Audio);
        WaveOut.Play();
    }
    public virtual void Stop()
    {
        if (WaveOut.PlaybackState == PlaybackState.Playing)
        {
            WaveOut.Stop();
            WaveOut.Dispose();
            Audio.Dispose();
        }
    }
}