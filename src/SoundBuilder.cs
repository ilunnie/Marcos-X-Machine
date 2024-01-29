public static class SoundBuilder
{
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
    }
}