using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Ed_Skullhead.Sound
{
    public static class SoundManager
    {
        private static ContentManager contentManager;
        private static SoundEffectInstance backgroundMusicInstance;
        private static SoundEffectInstance currentSoundInstance;

        public static void Initialize(ContentManager contentManager)
        {
            SoundManager.contentManager = contentManager;
        }
        public static void PlaySound(string soundName, bool isLooped = false)
        {
            SoundEffect soundEffect = contentManager.Load<SoundEffect>(soundName);
            if (currentSoundInstance != null && !currentSoundInstance.IsDisposed)
            {
                currentSoundInstance.Stop();
            }
            currentSoundInstance = soundEffect.CreateInstance();
            currentSoundInstance.IsLooped = isLooped;
            currentSoundInstance.Play();
        }
        public static void PlayBackgroundMusic(string soundName, bool isLooped = true)
        {
            SoundEffect soundEffect = contentManager.Load<SoundEffect>(soundName);
            if (backgroundMusicInstance != null && !backgroundMusicInstance.IsDisposed)
            {
                backgroundMusicInstance.Stop();
            }
            backgroundMusicInstance = soundEffect.CreateInstance();
            backgroundMusicInstance.IsLooped = isLooped;
            backgroundMusicInstance.Play();
        }
        public static void StopBackgroundMusic()
        {
            if (backgroundMusicInstance != null && !backgroundMusicInstance.IsDisposed)
            {
                backgroundMusicInstance.Stop();
            }
        }
    }
}
