using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Graphics
{

    public class Tile
    {
        public int ID;
        public Point Position;
        public Texture2D Texture;

        public Tile(int X, int Y, Texture2D Texture)
        {
            this.Texture = Texture;
            Position = new Point(X, Y);
        }
    }

    public class Tileset
    {

        private Texture2D Texture;
        private Tile[,] Tiles;
        public readonly Point TileSize;
        private readonly int PerRow, PerCol;

        public Tileset(Texture2D Texture, int TileWidth, int TileHeight)
        {
            this.Texture = Texture;
            TileSize = new Point(TileWidth, TileHeight);
            PerRow = Texture.Width / TileWidth;
            PerCol = Texture.Height / TileHeight;

            Tiles = new Tile[PerRow, PerCol];

            for (int x = 0; x < PerRow; x++)
            {
                for (int y = 0; y < PerCol; y++)
                {
                    Tiles[x, y] = new Tile(x * TileSize.X, y * TileSize.Y, GetTileTextureIn(x, y));
                }
            }
        }

        public Texture2D GetTileTextureAt(int X, int Y)
        {
            Texture2D cropTexture = new Texture2D(Flame.GraphicsDevice, TileSize.X, TileSize.Y);
            Color[] data = new Color[TileSize.X * TileSize.Y];
            Texture.GetData(0, new Rectangle(X, Y, TileSize.X, TileSize.Y), data, 0, data.Length);
            cropTexture.SetData(data);
            return cropTexture;
        }

        public Texture2D GetTileTextureIn(int Row, int Col)
        {
            Texture2D cropTexture = new Texture2D(Flame.GraphicsDevice, TileSize.X, TileSize.Y);
            Color[] data = new Color[TileSize.X * TileSize.Y];
            Texture.GetData(0, new Rectangle(TileSize.X * Row, TileSize.Y * Col, TileSize.X, TileSize.Y), data, 0, data.Length);
            cropTexture.SetData(data);
            return cropTexture;
        }

        public Texture2D GetTileTextureByID(int ID)
        {
            Texture2D cropTexture = new Texture2D(Flame.GraphicsDevice, TileSize.X, TileSize.Y);
            Color[] data = new Color[TileSize.X * TileSize.Y];
            Texture.GetData(0, new Rectangle(TileSize.X * (ID % PerRow), TileSize.Y * (ID / PerRow), TileSize.X, TileSize.Y), data, 0, data.Length);
            cropTexture.SetData(data);
            return cropTexture;
        }

        public Tile GetTileByID(int ID)
        {
            return Tiles[(ID % PerRow), (ID / PerRow)];
        }

    }
}
