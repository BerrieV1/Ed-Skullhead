using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Ed_Skullhead
{
    public class TileMapManager
    {
        private SpriteBatch spriteBatch;
        Texture2D tileset;
        TmxMap map;
        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;

        public TileMapManager(Texture2D tileset, TmxMap map, int tileWidth, int tileHeight, int tilesetTilesWide)
        {
            this.tileset = tileset;
            this.map = map;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tilesetTilesWide = tilesetTilesWide;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < map.Layers.Count; i++)
            {
                for (var j = 0; j < map.Layers[i].Tiles.Count; j++)
                {
                    int gid = map.Layers[i].Tiles[j].Gid;
                    if (gid == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)System.Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)System.Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }
    }
}
