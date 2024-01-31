public class SoundEffect : Sound
{
    public SoundEffect(Sound sound) : base(sound)
        => this.waveOut.Volume = 0.9f;
}