using NAudio.Wave;

public static class SoundBuilder
{
    public static Sound currentPlayingSound;

    public static void Play(this SoundType soundType, string file)
    {
        var sound = SoundBuffer.Current.Get(file, soundType);

        sound.Play();
        Memory.Sounds.Add(sound);
        currentPlayingSound = sound;
    }

    public static void PlayLoopedSound(this SoundType soundType, string file, long position)
    {
        Sound sound = SoundBuffer.Current.Get(file, soundType);
        WaveFileReader reader = new WaveFileReader(file);
        LoopedAudio loopStream = new LoopedAudio(reader);

        sound.PlayLoop(loopStream, 0);
    }

    public static void PlayBackGroundMusic(this SoundType soundType, string file, long position)
    {
        Sound sound = SoundBuffer.Current.Get(file, soundType);
        WaveFileReader reader = new WaveFileReader(file);
        LoopedAudio loopStream = new LoopedAudio(reader);

        sound.PlayLoop(loopStream, position);
        // sound.SetMusicVolume(10);
    }

    public static void StopSound()
    {
        if (currentPlayingSound != null)
        {
            currentPlayingSound.Stop();
            currentPlayingSound.Dispose();
            currentPlayingSound = null;
        }
    }
}