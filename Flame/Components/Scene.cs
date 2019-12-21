using Flame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Components
{

    public class Layer
    {

        public string Name = "";
        public bool Fixed = true;
        public int[,] Tiles;

        public Layer(Scene Scene, string Name)
        {
            this.Name = Name;
            Tiles = new int[Scene.Dimensions.X, Scene.Dimensions.Y];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {

                    Tiles[x, y] = -1;

                }
            }
        }

        public void SetTile(Tile Tile, int X, int Y)
        {
            SetTile(Tile.ID, X, Y);
        }

        public void SetTile(int ID, int X, int Y)
        {
            if (Tiles.GetLength(0) > X && X >= 0)
            {
                if (Tiles.GetLength(1) > Y && Y >= 0)
                {
                    Tiles[X, Y] = ID;
                }
            }
        }

        public void SetTile(Tile Tile, Point Point)
        {
            SetTile(Tile.ID, Point);
        }

        public void SetTile(int ID, Point Point)
        {
            SetTile(ID, Point.X, Point.Y);
        }

        public void SetTile(Tile Tile, Vector2 Point)
        {
            SetTile(Tile.ID, Point.ToPoint());
        }

        public void SetTile(int ID, Vector2 Point)
        {
            SetTile(ID, Point.ToPoint());
        }

        public void SetTile(Tile Tile, MouseState MouseState)
        {
            SetTile(Tile, MouseState.Position);
        }

        public void SetTile(int ID, MouseState MouseState)
        {
            SetTile(ID, MouseState.Position);
        }

    }

    public class Scene
    {

        public Tileset Tileset { get; set; }
        public Point Dimensions { get; private set; }
        public List<Layer> Layers = new List<Layer>();
        public Color BackgroundColor = Color.Gray;
        public int MaxLayers = 10;

        public Scene(int Width, int Height)
        {
            Dimensions = new Point(Width, Height);

            Layer Background = new Layer(this, "BG");
           // Layer Midground = new Layer(this, "Mid");
            //Layer Foreground = new Layer(this, "FG");

            Layers.Add(Background);
           // Layers.Add(Midground);
           // Layers.Add(Foreground);
        }

        public Layer GetLayer(string Name)
        {
            foreach (Layer Layer in Layers)
            {
                if (Layer.Name == Name)
                {
                    return Layer;
                }
            }
            return null;
        }

        public void AddLayer()
        {

        }

    }
}
