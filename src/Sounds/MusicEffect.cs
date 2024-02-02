using NAudio.Wave;

public class MusicEffect : Sound
{
    public MusicEffect(AudioFileReader audioFileReader)
        : base(MusicWaveOut, audioFileReader) { } 
}