public class SoundEffect : Sound
{
    public SoundEffect(Sound sound) : base(sound)
        => this.WaveOut.Volume = 0.9f;
}