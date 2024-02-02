using NAudio.Wave;

public class SoundEffect : Sound
{
    public SoundEffect(AudioFileReader audioFileReader)
        : base(EffectWaveOut, audioFileReader) { } 
}