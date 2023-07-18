using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Ed_Skullhead.Sound
{
    public static class SoundManager
    {
        private static ContentManager contentManager;

        public static void Initialize(ContentManager contentManager)
        {
            SoundManager.contentManager = contentManager;
        }
        public static void PlaySound(string soundName, bool isLooped = false)
        {
            SoundEffect soundEffect = contentManager.Load<SoundEffect>(soundName);
            SoundEffectInstance instance = soundEffect.CreateInstance();
            instance.IsLooped = isLooped;
            instance.Play();
        }
    }
}
