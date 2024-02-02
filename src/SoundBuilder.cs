using NAudio.Wave;

public static class SoundBuilder
{
    public static Sound currentPlayingSound;

    public static void Play(this SoundType soundType, string file)
    {
        Sound sound = SoundBuffer.Current.Get(file);
        Sound newSound;

        if (soundType == SoundType.Effect)
            newSound = new SoundEffect(sound);
        else
            newSound = new MusicEffect(sound);

        newSound.Play();
        newSound.SetVolume(50);
        Memory.Sounds.Add(newSound);
        currentPlayingSound = newSound;
    }

    public static void PlayLoopedSound(this SoundType soundType, string file, long position)
    {
        Sound sound = SoundBuffer.Current.Get(file);
        WaveFileReader reader = new WaveFileReader(file);
        LoopedAudio loopStream = new LoopedAudio(reader);

        sound.PlayLoop(loopStream, 0);
    }

    public static void PlayBackGroundMusic(this SoundType soundType, string file, long position)
    {
        Sound sound = SoundBuffer.Current.Get(file);
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