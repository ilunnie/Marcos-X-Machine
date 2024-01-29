public class MusicEffect : Sound
{
    public MusicEffect(Sound sound) : base(sound)
        => this.WaveOut.Volume = 0.5f;
}