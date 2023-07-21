using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Ed_Skullhead.Collectibles
{
    public class Coin : Collectible
    {
        public Coin(Texture2D texture, Rectangle collectibleRect) : base(texture, collectibleRect)
        {
            value = 1;
        }
    }
}
