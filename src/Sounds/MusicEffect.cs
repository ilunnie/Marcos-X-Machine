public class MusicEffect : Sound
{
    public MusicEffect(Sound sound) : base(sound)
        => this.waveOut.Volume = 0.5f;
}