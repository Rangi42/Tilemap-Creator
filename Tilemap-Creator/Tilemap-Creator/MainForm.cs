﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    public partial class MainForm : Form
    {
        Tilemap tilemap;
        Tileset tileset;

        Sprite tilesetImage;
        Bitmap tilemapImage;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tilesetImage?.Dispose();
            tileset?.Dispose();

            tilemapImage?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                tilesetSprite?.Dispose();
                tileset?.Dispose();

                // test making a tileset
                using (var bmp = new Bitmap("remilia.jpg"))
                using (var spr = new Sprite(bmp))
                {
                    // info about the loaded sprite
                    Console.WriteLine("sprite: size=({0}x{1}), colors={2}", spr.Width, spr.Height, spr.Palette.Length);

                    // create a new tileset
                    tileset = new Tileset(spr);
                    Console.WriteLine("tileset:", tileset.Size);
                    foreach (var size in tileset.PerfectSizes)
                    {
                        Console.WriteLine("> ({0} x {1})", size.Width, size.Height);
                    }

                    // smoosh into fixed size for now
                    tilesetSprite = tileset.Smoosh(640);

                    pTileset.Size = new Size(tilesetSprite.Width * 4, tilesetSprite.Height * 4);
                    pTileset.Image = tilesetSprite;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }*/


            // test creating a Tilemap/Tileset
            try
            {
                using (var bmp = new Bitmap("psychicf.bmp"))
                using (var spr = new Sprite(bmp))
                {
                    // create tileset from image
                    Tileset.Create(spr, false, out tileset, out tilemap);
                    Console.WriteLine("tileset size: {0}, original size: {1}", tileset.Size, tilemap.Width * tilemap.Height);

                    // render tileset
                    tilesetImage = tileset.Smoosh(8);

                    pTileset.Size = new Size(tilesetImage.Width * 2, tilesetImage.Height * 2);
                    pTileset.Image = tilesetImage;

                    // render tilemap
                    tilemapImage = new Bitmap(tilemap.Width * Tileset.TileSize, tilemap.Height * Tileset.TileSize);
                    using (var g = Graphics.FromImage(tilemapImage))
                    {
                        for (int y = 0; y < tilemap.Height; y++)
                        {
                            for (int x = 0; x < tilemap.Width; x++)
                            {
                                g.DrawImage(tileset[tilemap[x, y].TilesetIndex], x * Tileset.TileSize, y * Tileset.TileSize);
                            }
                        }
                    }

                    pTilemap.Size = new Size(tilemapImage.Width * 2, tilemapImage.Height * 2);
                    pTilemap.Image = tilemapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tilesetImage?.Save("test.bmp", SpriteFormat.BMP);
        }
    }
}
