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
        Memory.Sounds.Add(newSound);
        currentPlayingSound = newSound;
    }

    public static void PlayLoopedSound(this SoundType soundType, string file)
    {
        Sound sound = SoundBuffer.Current.Get(file);
        WaveFileReader reader = new WaveFileReader(file);
        LoopedAudio loopStream = new LoopedAudio(reader);

        sound.PlayLoop(loopStream);
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