using Ed_Skullhead.Collectibles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ed_Skullhead.Factories
{
    public class CollectibleFactory
    {
        public CollectibleFactory()
        {
        }
        public Coin CreateCoin(Texture2D coinTexture, Rectangle position)
        {
            return new Coin(coinTexture, position);
        }
        public Bone CreateBone(Texture2D boneTexture, Rectangle position)
        {
            return new Bone(boneTexture, position);
        }
    }
}
