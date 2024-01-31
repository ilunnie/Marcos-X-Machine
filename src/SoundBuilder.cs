using NAudio.Wave;

public static class SoundBuilder
{
    private static Sound currentPlayingSound;
    private static bool IsPlaying = false;

    public static void Play(this SoundType soundType, string file)
    {
        Sound sound = SoundBuffer.Current.Get(file);

        if (currentPlayingSound != null)
            return;

        else
        {
            Sound newSound;

            if (soundType == SoundType.Effect)
                newSound = new SoundEffect(sound);
            else
                newSound = new MusicEffect(sound);

            newSound.Play();
            Memory.Sounds.Add(newSound);
        }
    }

    public static void PlayLoopedSound(this SoundType soundType, string file)
    {
        Sound sound = SoundBuffer.Current.Get(file);

        if (IsPlaying == true)
            return;

        else
        {
            Sound newSound;

            WaveStream sourceStream = sound.Audio;

            if (soundType == SoundType.Effect)
                newSound = new SoundEffect(sound);
            else
                newSound = new MusicEffect(sound);

            newSound.PlayLoop(sourceStream);
            Memory.Sounds.Add(newSound);

            IsPlaying = true;
        }
    }

    public static void StopSound()
    {
        currentPlayingSound.Stop();
        currentPlayingSound = null;
        IsPlaying = false;
    }
}