using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azul_atestat
{
    public partial class Form1 : Form
    {
        double tileToBoardRatio = 8.333 / 100;
        double factoryToBoardRatio = 26.666 / 100;
        double boardToTableRatio = 300.0 / 1140.0;
        int nrJucatori = 4;
        int jucatorCurent = 0;
        int numberOfTilesInHand = 0;
        int numberOfTilesInLine = 0;
        int indexOfLine = -1;
        int jucatorCuPiesaDeMarcaj = -1;
        int x = 0;
        bool mutaRandul = false;
        bool mutareaTablelor = false;
        bool inceput = true;
        int[] punctaj = { 0, 0, 0, 0 };
        int[] coordonateLinii = { 0, 0, 0, 0, 0 };
        int coordonateLiniaPodelei = 0;
        int[] subtractTiles = { 1, 1, 2, 2, 2, 3, 3 };
        bool finalulRundei = false;
        bool final = false;
        Point[] rememberMove = new Point[]{
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
        };
        List<PictureBox> bag = new List<PictureBox>();
        List<PictureBox> box = new List<PictureBox>();
        List<List<PictureBox>> factoriesPieces = new List<List<PictureBox>>();
        List<PictureBox> factories = new List<PictureBox>();
        List<PictureBox> groupMovingTiles = new List<PictureBox>();
        List<List<List<PictureBox>>> onTheBoardPieces = new List<List<List<PictureBox>>>();
        List<List<PictureBox>> liniaPodelei = new List<List<PictureBox>>();
        List<PictureBox> middleOfTheFactories = new List<PictureBox>();
        PictureBox piesaDeMarcaj = new PictureBox();
        List<List<List<PictureBox>>> rightSideMatrix = new List<List<List<PictureBox>>>();
        List<PictureBox> pointMark = new List<PictureBox>();
        List<GroupBox> boards = new List<GroupBox>();
        Button iesireDinJoc = new Button();
        int[,] culori = new int[4, 5]{
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}};

        public Form1()
        {
            string[,] wallConfiguration = new string[1, 5] {
                {  "blue", "yellow", "red", "black", "white"}
            };

            InitializeComponent();
            //this.BackgroundImage = Azul_atestat.Properties.Resources.tabletop1;
            //this.BackgroundImage = Azul_atestat.Properties.Resources.tabletop2;
            //this.BackgroundImage = Azul_atestat.Properties.Resources.tabletop3;
            this.BackgroundImage = Azul_atestat.Properties.Resources.tabletop4;
            Console.WriteLine(boardToTableRatio);
            //this.BackgroundImage = Azul_atestat.Properties.Resources.tabletop5;
            this.BackgroundImageLayout = ImageLayout.Tile;
            CreateBoards();
            CreateFactoriesPieces();
            CreateTiles();
            CreateFactory();
            CreatePointMarker();
            CreatePictureBoxes();
            CreateLiniaPodelei();
            CreateRightSideMatrix();
            iesireDinJoc.BackColor = Color.AntiqueWhite;
            iesireDinJoc.ForeColor = Color.Black;
            iesireDinJoc.Text = "EXIT";
            iesireDinJoc.Font = new Font(iesireDinJoc.Font.FontFamily, 16);
            iesireDinJoc.Size = new Size(150, 90);
            iesireDinJoc.Location = new Point(20, this.Size.Height - iesireDinJoc.Size.Height - 50);
            this.Controls.Add(iesireDinJoc);
            StartJoc();
            /*  2 jucatori -> 5
                3 jucatori -> 7
                4 jucatori -> 9
            */
        }

        private void CreatePointMarker()
        {
            for (int i = 0; i < nrJucatori; i++)
            {
                PictureBox pointMarker = new PictureBox();
                pointMarker.BackColor = Color.Black;
                pointMarker.Size = new Size(13, 13);
                pointMarker.Location = new Point(13, 0);
                boards[i].Controls.Add(pointMarker);
                //this.Controls.Add(pointMarker);
                pointMark.Add(pointMarker);
                pointMarker.BringToFront();
            }
        }

        private void CreateRightSideMatrix()
        {

            for (int i = 0; i < nrJucatori; i++)
            {
                List<List<PictureBox>> addAPlayer = new List<List<PictureBox>>();
                rightSideMatrix.Add(addAPlayer);
                for (int j = 0; j < 5; j++)
                {
                    List<PictureBox> addALine = new List<PictureBox>();
                    rightSideMatrix[i].Add(addALine);
                    for (int k = 0; k < 5; k++)
                    {
                        /*PictureBox rightSide = new PictureBox();
                        rightSide.Location = new Point(boards[0].Location.X + boards[0].Width / 2 + 6 + k * (bag[0].Width + 2), boards[0].Location.Y + 1 * boards[0].Height / 3 + 5 + j * (bag[0].Width + 2));
                        rightSide.Size = new Size(bag[0].Width, bag[0].Height);
                       // rightSide.BackColor = Color.Red;
                        rightSideMatrix[i][j].Add(rightSide);
                        //this.Controls.Add(rightSideMatrix[i][j][k]);
                        //rightSideMatrix[i][j][k].BringToFront();*/
                        rightSideMatrix[i][j].Add(null);
                    }

                }
            }
        }
        private void CreateBoards()
        {
            double boardSizeDouble = Math.Ceiling(boardToTableRatio * this.Width);
            int boardSize = (int)boardSizeDouble;
            for (int i = 0; i < nrJucatori; i++)
            {
                //PictureBox board1 = new PictureBox();
                GroupBox addBoard = new GroupBox();
                addBoard.BackgroundImage = Azul_atestat.Properties.Resources.board_front;
                addBoard.Size = new Size((int)(boardToTableRatio * this.Width), (int)(boardToTableRatio * this.Width));
                addBoard.BackgroundImageLayout = ImageLayout.Stretch;
                //boards.Add(board1);
                boards.Add(addBoard);
            }
            boards[0].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            rememberMove[0] = boards[0].Location;

            //boards[0].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            //moveBoards[0].Controls.Add(moveBoards[0]);
            this.Controls.Add(boards[0]);
            switch (nrJucatori)
            {
                case 2: Add2Boards(); break;
                case 3: Add3Boards(); break;
                case 4: Add4Boards(); break;
            }
        }
        private void Add2Boards()
        {
            boards[1].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, 40);
            this.Controls.Add(boards[1]);
        }

        private void Add3Boards()
        {
            boards[1].Location = new Point(this.Width - 4 * boards[0].Width / 3, 2 * boards[0].Width / 3);
            this.Controls.Add(boards[1]);
        }

        private void Add4Boards()
        {
            boards[1].Location = new Point(this.Size.Width - boards[0].Width - 30, this.Width / 2 - boards[0].Width / 2);
            rememberMove[1] = boards[1].Location;
            this.Controls.Add(boards[1]);
            boards[2].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, 0);
            rememberMove[2] = boards[2].Location;
            this.Controls.Add(boards[2]);
            boards[3].Location = new Point(0, this.Size.Width / 2 - boards[0].Width / 2);
            rememberMove[3] = boards[3].Location;
            this.Controls.Add(boards[3]);
        }
        private void StartJoc()
        {
            inceput = true;
            ShuffleTiles();
            AddToFactory();
        }
        private void CreateFactoriesPieces()
        {
            for (int i = 0; i < 2 * nrJucatori + 1; i++)
            {
                factoriesPieces.Add(new List<PictureBox>());
            }
        }

        private void CreateTiles()
        {

            string[] tileColors = { "blue", "yellow", "red", "black", "white" };
            Bitmap[] tiles =
            {
                Azul_atestat.Properties.Resources.blue,
                Azul_atestat.Properties.Resources.yellow,
                Azul_atestat.Properties.Resources.red,
                Azul_atestat.Properties.Resources.black,
                Azul_atestat.Properties.Resources.white
            };

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    PictureBox currentTile = new PictureBox();
                    double tileSizeDouble = Math.Ceiling(this.tileToBoardRatio * boards[0].Size.Width);
                    int tileSize = (int)tileSizeDouble;
                    currentTile.Size = new Size(tileSize, tileSize);
                    currentTile.SizeMode = PictureBoxSizeMode.StretchImage;
                    //currentTile.BackColor = Color.Transparent;
                    //currentTile.Image = tiles[j];
                    //Console.WriteLine(tiles[j].PropertyItems);
                    currentTile.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\" + tileColors[j] + ".png";
                    //currentTile.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\" + tile + ".png";

                    currentTile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileDrag_MouseDown);
                    currentTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TileDrag_MouseMove);
                    currentTile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TileDrag_MouseUp);
                    bag.Add(currentTile);
                }
            }
            //Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\");

        }

        private void CreateFactory()
        {
            for (int i = 0; i < 2 * nrJucatori + 1; i++)
            {
                PictureBox currentFactory = new PictureBox();
                currentFactory.BackgroundImage = Azul_atestat.Properties.Resources.fabrica;
                double factorySizeDouble = Math.Ceiling(this.factoryToBoardRatio * boards[0].Width);
                int factorySize = (int)factorySizeDouble;
                currentFactory.BackColor = Color.Transparent;
                currentFactory.Size = new Size(factorySize, factorySize);
                currentFactory.Location = new Point(this.Width / 2 - currentFactory.Width / 2, 350);
                currentFactory.BackgroundImageLayout = ImageLayout.Stretch;
                factories.Add(currentFactory);
            }
            piesaDeMarcaj.Size = new Size(bag[0].Width, bag[0].Height);
            piesaDeMarcaj.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\1.png";
            piesaDeMarcaj.SizeMode = PictureBoxSizeMode.StretchImage;
            switch (nrJucatori)
            {
                case 2: Add5Factories(); break;
                case 3: Add7Factories(); break;
                case 4: Add9Factories(); break;
            }
        }

        private void CreatePictureBoxes()
        {
            for (int j = 0; j < nrJucatori; j++)
            {
                onTheBoardPieces.Add(new List<List<PictureBox>>());
                for (int i = 0; i < 5; i++)
                {
                    onTheBoardPieces[j].Add(new List<PictureBox>());
                    foreach (PictureBox emptytile in onTheBoardPieces[j][i])
                    {
                        emptytile.Image = null;
                    }
                }
            }
            for (int j = 0; j < nrJucatori; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    onTheBoardPieces[j].Add(new List<PictureBox>());
                    foreach (PictureBox emptytile in onTheBoardPieces[j][i])
                    {
                        emptytile.Image = null;
                    }
                }
            }
            for (int j = 0; j < nrJucatori; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    PictureBox currentPictureBox = new PictureBox();
                    onTheBoardPieces[j][i].Add(currentPictureBox);
                    onTheBoardPieces[j][i][0].Size = new Size(bag[0].Width, bag[0].Height);
                    coordonateLinii[i] = boards[0].Location.Y + 1 * boards[0].Height / 3 + 5 + i * (bag[0].Width + 2);
                    onTheBoardPieces[j][i][0].Location = new Point(boards[0].Width / 2 - bag[0].Width - 8 - i * (bag[0].Width + 2), 1 * boards[0].Height / 3 + 5 + i * (bag[0].Width + 2));
                    /*onTheBoardPieces[j][i][0].BackColor = Color.Red;
                    this.Controls.Add(onTheBoardPieces[j][i][0]);*/
                    onTheBoardPieces[j][i][0].BringToFront();
                    onTheBoardPieces[j][i][0].Visible = false;
                    boards[j].Controls.Add(onTheBoardPieces[j][i][0]);
                }
            }


        }

        private void CreateLiniaPodelei()
        {
            for (int i = 0; i < nrJucatori; i++)
            {
                liniaPodelei.Add(new List<PictureBox>());
            }
            coordonateLiniaPodelei = boards[0].Location.Y + boards[0].Height - 15 - bag[0].Height;
            for (int i = 0; i < nrJucatori; i++)
            {
                PictureBox currentTileOnFloorLine = new PictureBox();
                currentTileOnFloorLine.Size = new Size(bag[0].Width, bag[0].Height);
                boards[i].Controls.Add(currentTileOnFloorLine);
                currentTileOnFloorLine.Location = new Point(10, boards[0].Height - 15 - bag[0].Height);
                liniaPodelei[i].Add(currentTileOnFloorLine);
                liniaPodelei[i][0].Visible = false;
            }
        }

        private void ShuffleTiles()
        {
            bag = bag.OrderBy(piece => Guid.NewGuid()).ToList();
        }

        private void AddToFactory()
        {
            for (int i = 0; i < nrJucatori * 2 + 1; i++)
            {
                //Console.WriteLine("in cutie sunt " + box.Count);
                if (bag.Count < 4)
                {
                    foreach (PictureBox add in box)
                    {
                        bag.Add(add);
                    }
                    box.Clear();
                }
                bag[0].Location = new Point(factories[i].Location.X + bag[0].Width / 2, factories[i].Location.Y + 5);
                bag[1].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * bag[0].Width / 2, factories[i].Location.Y + 5);
                bag[2].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * bag[0].Width / 2, factories[i].Location.Y + factories[i].Width / 2);
                bag[3].Location = new Point(factories[i].Location.X + bag[0].Width / 2, factories[i].Location.Y + factories[i].Width / 2);
                factoriesPieces[i].Add(bag[0]);
                factoriesPieces[i].Add(bag[1]);
                factoriesPieces[i].Add(bag[2]);
                factoriesPieces[i].Add(bag[3]);
                bag[0].Visible = true;
                bag[1].Visible = true;
                bag[2].Visible = true;
                bag[3].Visible = true;
                bag.Remove(bag[0]);
                bag.Remove(bag[0]);
                bag.Remove(bag[0]);
                bag.Remove(bag[0]);
                this.Controls.Add(factoriesPieces[i][0]);
                this.Controls.Add(factoriesPieces[i][1]);
                this.Controls.Add(factoriesPieces[i][2]);
                this.Controls.Add(factoriesPieces[i][3]);
                factoriesPieces[i][0].BringToFront();
                factoriesPieces[i][1].BringToFront();
                factoriesPieces[i][2].BringToFront();
                factoriesPieces[i][3].BringToFront();
            }
        }

        private void Add5Factories()
        {
            factories[0].Location = new Point(this.Width / 2 - factories[0].Width / 2, 350);
            factories[1].Location = new Point(boards[0].Location.X, 2 * factories[1].Height + factories[0].Location.Y - 50);
            factories[2].Location = new Point(boards[0].Location.X + boards[0].Width - factories[2].Width, 2 * factories[2].Height + factories[0].Location.Y - 50);
            factories[3].Location = new Point(boards[0].Location.X + factories[3].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 25);
            factories[4].Location = new Point(boards[0].Location.X + boards[0].Width - 3 * factories[4].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 25);
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - bag[0].Width / 2, factories[3].Location.Y + factories[0].Height / 2 - bag[0].Height / 2);
            this.Controls.Add(factories[0]);
            this.Controls.Add(factories[1]);
            this.Controls.Add(factories[2]);
            this.Controls.Add(factories[3]);
            this.Controls.Add(factories[4]);
            this.Controls.Add(piesaDeMarcaj);
            piesaDeMarcaj.BringToFront();
        }

        private void Add7Factories()
        {
            factories[0].Location = new Point(this.Width / 2 - factories[0].Width / 2, 300);
            factories[1].Location = new Point(boards[0].Location.X, 2 * factories[1].Height + factories[0].Location.Y - 75);
            factories[2].Location = new Point(boards[0].Location.X + boards[0].Width - factories[2].Width, 2 * factories[2].Height + factories[0].Location.Y - 75);
            factories[3].Location = new Point(factories[1].Location.X - factories[1].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 50);
            factories[4].Location = new Point(factories[2].Location.X + factories[4].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 50);
            factories[5].Location = new Point(boards[0].Location.X + factories[3].Width / 2, 2 * factories[5].Height + factories[3].Location.Y - 50);
            factories[6].Location = new Point(boards[0].Location.X + boards[0].Width - 3 * factories[3].Width / 2, 2 * factories[5].Height + factories[3].Location.Y - 50);
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - bag[0].Width / 2, factories[1].Location.Y + factories[0].Height / 2 - bag[0].Height / 2);
            this.Controls.Add(factories[0]);
            this.Controls.Add(factories[1]);
            this.Controls.Add(factories[2]);
            this.Controls.Add(factories[3]);
            this.Controls.Add(factories[4]);
            this.Controls.Add(factories[5]);
            this.Controls.Add(factories[6]);
            this.Controls.Add(piesaDeMarcaj);
            piesaDeMarcaj.BringToFront();
        }

        private void Add9Factories()
        {
            factories[0].Location = new Point(this.Width / 2 - factories[0].Width / 2, 300);
            factories[1].Location = new Point(boards[0].Location.X, 2 * factories[1].Height + factories[0].Location.Y - 85);
            factories[2].Location = new Point(boards[0].Location.X + boards[0].Width - factories[2].Width, 2 * factories[2].Height + factories[0].Location.Y - 85);
            factories[3].Location = new Point(factories[1].Location.X - factories[1].Width, 2 * factories[3].Height + factories[1].Location.Y - 65);
            factories[4].Location = new Point(factories[2].Location.X + factories[4].Width, 2 * factories[3].Height + factories[1].Location.Y - 65);
            factories[5].Location = new Point(boards[0].Location.X - factories[5].Width / 2, 2 * factories[3].Height + factories[3].Location.Y - 65);
            factories[6].Location = new Point(factories[2].Location.X + factories[6].Width / 2, 2 * factories[3].Height + factories[3].Location.Y - 65);
            factories[7].Location = new Point(boards[0].Location.X + factories[3].Width / 2, 2 * factories[5].Height + factories[5].Location.Y - 70);
            factories[8].Location = new Point(boards[0].Location.X + boards[0].Width - 3 * factories[3].Width / 2, 2 * factories[5].Height + factories[5].Location.Y - 70);
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - bag[0].Width / 2, factories[1].Location.Y + factories[0].Height / 2 - bag[0].Height / 2);
            this.Controls.Add(factories[0]);
            this.Controls.Add(factories[1]);
            this.Controls.Add(factories[2]);
            this.Controls.Add(factories[3]);
            this.Controls.Add(factories[4]);
            this.Controls.Add(factories[5]);
            this.Controls.Add(factories[6]);
            this.Controls.Add(factories[7]);
            this.Controls.Add(factories[8]);
            this.Controls.Add(piesaDeMarcaj);
            piesaDeMarcaj.BringToFront();
        }

        private void TileDrag_MouseDown(object sender, MouseEventArgs e)
        {
            mutaRandul = false;
            if (!mutareaTablelor && !final)
            {
                if (TileBelongsToFactoriesPiece((PictureBox)sender))
                {
                    groupMovingTiles.Add((PictureBox)sender);
                    groupMovingTiles[0].BringToFront();
                    GetGroupTiles((PictureBox)sender);
                    DrawGroupInLine((PictureBox)sender);
                }
                else
                {
                    if (TileBelongsToMiddleOfTheTable((PictureBox)sender))
                    {
                        groupMovingTiles.Add((PictureBox)sender);
                        groupMovingTiles[0].BringToFront();
                        if (liniaPodelei[jucatorCurent].Count != 0 && liniaPodelei[jucatorCurent][0].ImageLocation != piesaDeMarcaj.ImageLocation && liniaPodelei[jucatorCurent][0].ImageLocation != null)
                        {
                            box.Add(liniaPodelei[jucatorCurent][0]);
                        }
                        if (FirstPersonToDrawMarker())
                        {
                            liniaPodelei[jucatorCurent][0].ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\1.png";
                            liniaPodelei[jucatorCurent][0].Size = piesaDeMarcaj.Size;
                            liniaPodelei[jucatorCurent][0].SizeMode = PictureBoxSizeMode.StretchImage;
                            jucatorCuPiesaDeMarcaj = jucatorCurent;
                            piesaDeMarcaj.Visible = false;
                            liniaPodelei[jucatorCurent][0].Visible = true;
                            //moveBoards[jucatorCurent].Controls.Add(liniaPodelei[jucatorCurent][0]);
                            //this.Controls.Add(liniaPodelei[jucatorCurent][0]);
                            liniaPodelei[jucatorCurent][0].BringToFront();
                        }
                        GetGroupTilesFromTheMiddle((PictureBox)sender);
                        DrawGroupInLineFromTheMiddle((PictureBox)sender);
                        groupMovingTiles[0].BringToFront();

                    }
                    /* else
                     {
                         if (TileBelongsToOnTheBoardPieces((PictureBox)sender) && finalulRundei)
                         {
                             groupMovingTiles.Add((PictureBox) sender);
                             groupMovingTiles[0].BringToFront();
                             //moveBoards[jucatorCurent].Controls.Add(groupMovingTiles[0]);
                             //indexOfLine = 0;
                         }
                     }*/
                }
            }


        }

        /*private bool TileBelongsToOnTheBoardPieces(PictureBox sender)
        {
            if (sender.Location.X >= 0 && sender.Location.X <= moveBoards[0].Width && sender.Location.Y > 0 && sender.Location.Y < moveBoards[0].Height)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sender.Location.Y >= onTheBoardPieces[jucatorCurent][i][0].Location.Y - bag[0].Width / 2 && sender.Location.Y <= onTheBoardPieces[jucatorCurent][i][0].Location.Y + bag[0].Width / 2)
                    {
                        indexOfLineRightSide = i;
                        if(onTheBoardPieces[jucatorCurent][i].Count == indexOfLineRightSide + 1)
                        {
                            return true;
                        }
                        //Console.WriteLine(indexOfLine);
                    }
                }
            }
            return false;
        }*/

        private void TileDrag_MouseMove(object sender, MouseEventArgs e)
        {
            /*if(finalulRundei && groupMovingTiles.Count != 0 && e.Button == MouseButtons.Left)
            {
                groupMovingTiles[0].Location = moveBoards[jucatorCurent].PointToClient(new Point(Cursor.Position.X - bag[0].Width / 2, Cursor.Position.Y - bag[0].Height / 2));
                DrawGroupInLine(groupMovingTiles[0]);

            }
            else
            {*/
            if (groupMovingTiles.Count != 0 && e.Button == MouseButtons.Left)
            {
                groupMovingTiles[0].Location = this.PointToClient(new Point(Cursor.Position.X - bag[0].Width / 2, Cursor.Position.Y - bag[0].Height / 2));
                DrawGroupInLine(groupMovingTiles[0]);
            }
            //}

        }

        private void TileDrag_MouseUp(object sender, MouseEventArgs e)
        {
            if (groupMovingTiles.Count != 0)
            {
                if (CheckIfPossible((PictureBox)sender))
                {
                    if (middleOfTheFactories.Contains(sender))
                    {
                        PutOnTheBoard((PictureBox)sender);
                        RemoveFromMiddle((PictureBox)sender);
                        AddToTheMiddle();
                    }
                    else
                    {
                        GetGroupTilesForMiddle((PictureBox)sender);
                        PutOnTheBoard((PictureBox)sender);
                        AddToTheMiddle();
                    }
                    mutaRandul = true;

                }
                else
                {
                    if (groupMovingTiles[0].Location.Y >= boards[jucatorCurent].Location.Y && groupMovingTiles[0].Location.Y <= boards[jucatorCurent].Location.Y + boards[jucatorCurent].Width)
                    {
                        if (groupMovingTiles[0].Location.X >= boards[jucatorCurent].Location.X && groupMovingTiles[0].Location.X <= boards[jucatorCurent].Location.X + boards[jucatorCurent].Width / 2)
                        {
                            if (groupMovingTiles[0].Location.Y > coordonateLiniaPodelei - bag[0].Width / 2 && groupMovingTiles[0].Location.Y < coordonateLiniaPodelei + bag[0].Height / 2)
                            {
                                GetGroupTilesForMiddle((PictureBox)sender);
                                AddToFloorLine(groupMovingTiles.Count);
                                int nrTiles = 4;
                                foreach (List<PictureBox> deleteFromFactory in factoriesPieces)
                                {
                                    if (deleteFromFactory.Contains(sender))
                                    {
                                        for (int i = 0; i < nrTiles; i++)
                                        {
                                            if (deleteFromFactory[i].ImageLocation == ((PictureBox)sender).ImageLocation)
                                            {
                                                deleteFromFactory.Remove(deleteFromFactory[i]);
                                                nrTiles--;
                                                i--;
                                            }
                                        }
                                    }
                                }
                                AddToTheMiddle();

                                mutaRandul = true;
                            }
                        }
                        /*else
                        if (groupMovingTiles[0].Location.X > moveBoards[0].Width / 2 && finalulRundei)
                        {
                            AddToRightSide();
                        }*/
                    }

                }

                groupMovingTiles.Clear();
                //jucatorCurent = 0;

            }
            if (mutaRandul)
            {
                if (jucatorCurent == nrJucatori - 1)
                {
                    jucatorCurent = -1;
                }
                mutareaTablelor = true;
                inceput = false;
                mutaRandul = false;
                jucatorCurent++;
                WaitForTimer();
            }
            
            else
            {
                if (SfarsitRunda())
                {
                    jucatorCurent = 0;
                    for (int i = 0; i < nrJucatori; i++)
                    {
                        AddToRightSide();
                        jucatorCurent++;
                    }
                    if (final)
                    {
                        jucatorCurent = 0;
                        for (int i = 0; i < nrJucatori; i++)
                        {
                            AddToRightSide();
                            jucatorCurent++;
                        }
                        AdunaPunctajeleFinale();
                        ClearEverything();
                        int punctajmaxim = 0, indicemaxim = 0;
                        for(int i = 0; i < nrJucatori; i++)
                        {
                            if(punctaj[i]> punctajmaxim)
                            {
                                punctajmaxim = punctaj[i];
                                indicemaxim = i;
                            }

                        }
                        TextBox castigator = new TextBox();
                        castigator.Size = new Size(100, 300);
                        castigator.Location = new Point(this.Size.Width / 2 - castigator.Size.Width / 2, this.Size.Height / 2 - castigator.Size.Height);
                        castigator.Text = "Castigatorul este, cu " + punctaj[indicemaxim] + ".Felicitari tuturor jucatorilor";
                    }
                    else
                    {
                        finalulRundei = false;
                        mutaRandul = false;
                        jucatorCurent = jucatorCuPiesaDeMarcaj;
                        Repozitionare();
                        inceput = true;
                        // moveBoards[jucatorCurent].Location = new Point(0, 0);

                        StartJoc();
                        // moveBoards[0].Location = new Point(0, 0);
                    }
                }
            }
        }

        private void ClearEverything()
        {
            for(int i = 0; i < nrJucatori; i++)
            {
                this.Controls.Remove(boards[0]);
            }
            for(int i = 0; i < 2 * nrJucatori + 1; i++)
            {
                this.Controls.Remove(factories[0]);
            }
        }

        private void AdunaPunctajeleFinale()
        {
            for (int i = 0; i < nrJucatori; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (rightSideMatrix[i][j].Count == j + 1)
                    {
                        punctaj[i] += 2;
                    }
                    int numarcoloana = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        if (rightSideMatrix[i][j][k] != null && rightSideMatrix[i][j][k].ImageLocation != null)
                        {
                            numarcoloana++;
                        }
                    }
                    if(numarcoloana == 5)
                    {
                        punctaj[i] += 7;
                    }
                    if(culori[i, j] == 5)
                    {
                        punctaj[i] += 10;
                    }
                }
            }
        }
            
        


        private void Repozitionare()
        {
            switch(nrJucatori)
            {
                case 2: Repozitionare2(); break;
                case 3: Repozitionare3(); break;
                case 4: Repozitionare4(); break;
            }
            
        }

        private void Repozitionare2()
        {

        }
        private void Repozitionare3()
        {

        }
        private void Repozitionare4()
        {
            if(jucatorCurent == 0)
            {
                boards[3].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);

            }
            else
            {
                boards[(jucatorCurent - 1) % 4].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            }
            boards[jucatorCurent % 4].Location = new Point(this.Size.Width - boards[0].Size.Width - 30, this.Size.Width / 2 - boards[0].Size.Width / 2);
            boards[(jucatorCurent + 1) % 4].Location = new Point(this.Size.Width / 2 - boards[0].Size.Width / 2, 0);
            boards[(jucatorCurent + 2) % 4].Location = new Point(0, this.Size.Width / 2 - boards[0].Size.Width / 2);
            for (int i = 0; i < nrJucatori; i++)
            {
                rememberMove[i] = boards[i].Location;

            }
        }

        private void AddToRightSide()
        {
            for (int i = 0; i < 5; i++)
            {
                if (onTheBoardPieces[jucatorCurent][i].Count == i + 1 && onTheBoardPieces[jucatorCurent][i][0].ImageLocation != null)
                {
                    int indiceCuloare = 0;
                    string getLocation = null;
                    getLocation = onTheBoardPieces[jucatorCurent][i][0].ImageLocation.Split('\\')[8].Split('.')[0];
                    if (getLocation == "yellow") indiceCuloare = 1;
                    else
                    {
                        if (getLocation == "red") indiceCuloare = 2;
                        else
                        {
                            if (getLocation == "black") indiceCuloare = 3;
                            else
                            {
                                if (getLocation == "white") indiceCuloare = 4;
                            }
                        }
                    }
                    int indexPiesa = (indiceCuloare + i) % 5;
                    bool maiExista = true;
                    foreach (PictureBox searchForTile in rightSideMatrix[jucatorCurent][i])
                    {
                        if (searchForTile != null)
                        {
                            if (searchForTile.ImageLocation == onTheBoardPieces[jucatorCurent][i][0].ImageLocation)
                            {
                                maiExista = false;
                            }
                        }
                    }
                    culori[jucatorCurent, indiceCuloare]++;
                    if (maiExista)
                    {
                        rightSideMatrix[jucatorCurent][i][indexPiesa] = new PictureBox();
                        rightSideMatrix[jucatorCurent][i][indexPiesa].BorderStyle = BorderStyle.Fixed3D;
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Visible = true;
                        rightSideMatrix[jucatorCurent][i][indexPiesa].ImageLocation = onTheBoardPieces[jucatorCurent][i][0].ImageLocation;
                        rightSideMatrix[jucatorCurent][i][indexPiesa].SizeMode = onTheBoardPieces[jucatorCurent][i][0].SizeMode;
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Size = new Size(bag[0].Width, bag[0].Height);
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Location = new Point(boards[0].Width / 2 + 6 + indexPiesa * (bag[0].Width + 2), 1 * boards[0].Height / 3 + 5 + i * (bag[0].Width + 2));
                        boards[jucatorCurent].Controls.Add(rightSideMatrix[jucatorCurent][i][indexPiesa]);
                        rightSideMatrix[jucatorCurent][i][indexPiesa].BringToFront();
                    }
                    else
                    {
                        //groupMovingTiles.Addd(onTheBoardPieces[jucatorCurent][i][0])
                        for (int j = 1; j <= i; j++)
                        {
                            groupMovingTiles.Add(onTheBoardPieces[jucatorCurent][i][1]);
                        }
                        AddToFloorLine(i + 1);
                    }
                    for (int j = 0; j < i; j++)
                    {
                        box.Add(onTheBoardPieces[jucatorCurent][i][1]);
                        boards[jucatorCurent].Controls.Remove(onTheBoardPieces[jucatorCurent][i][1]);
                        onTheBoardPieces[jucatorCurent][i].Remove(onTheBoardPieces[jucatorCurent][i][1]);
                    }
                    onTheBoardPieces[jucatorCurent][i][0].Visible = false;
                    onTheBoardPieces[jucatorCurent][i][0].ImageLocation = null;
                    Console.WriteLine("jucatorul " + jucatorCurent + " are " + onTheBoardPieces[jucatorCurent][i][0].ImageLocation);
                    AddPoints(indexPiesa, i);
                    
                    
                }
            }
            int nrpiese = 0;
            foreach (PictureBox subtract in liniaPodelei[jucatorCurent])
            {
                if (subtract.ImageLocation != null)
                {
                    punctaj[jucatorCurent] -= subtractTiles[nrpiese];
                    nrpiese++;
                }
            }
            if (punctaj[jucatorCurent] < 0) punctaj[jucatorCurent] = 0;
            Point pozitieMarker1 = new Point(13, pointMark[0].Width + 3);
            if (punctaj[jucatorCurent] == 1)
            {
                pointMark[jucatorCurent].Location = new Point(pointMark[jucatorCurent].Location.X, pointMark[jucatorCurent].Location.Y + pointMark[jucatorCurent].Width + 3);
            }
            else DrawMarkerToPoints(pozitieMarker1);
            piesaDeMarcaj.Visible = true;
            for(int j = 1; j < liniaPodelei[jucatorCurent].Count; j++)
            {
                box.Add(liniaPodelei[jucatorCurent][1]);
                boards[jucatorCurent].Controls.Remove(liniaPodelei[jucatorCurent][1]);
                liniaPodelei[jucatorCurent].Remove(liniaPodelei[jucatorCurent][1]);
            }
            if(liniaPodelei[jucatorCurent][0].ImageLocation != null)
            {
                box.Add(liniaPodelei[jucatorCurent][0]);
            }
            liniaPodelei[jucatorCurent][0].ImageLocation = null;
            liniaPodelei[jucatorCurent][0].Visible = false;

            //moveBoards[0].Location = new Point(0, 0);
            /*if(NuMaiExistaPieseDeMutat())
            {
                if (jucatorCurent == nrJucatori-1)
                {
                    finalulRundei = false;
                    inceput = true;
                    StartJoc();
                }
                else
                {
                    jucatorCurent++;
                    //mutareaTablelor = true;
                    //WaitForTimer();
                }
                
            }*/
            //Console.WriteLine(punctaj[0]);
        }

        private bool NuMaiExistaPieseDeMutat()
        {
            for(int i = 0; i < 5; i++)
            {
                if (onTheBoardPieces[jucatorCurent][i].Count == i+1 && onTheBoardPieces[jucatorCurent][i][0].ImageLocation != null)
                { 
                    return false;
                }
            }
            return true;
        }

        private void DrawMarkerToPoints(Point pozitieMarker1)
        {
            pointMark[jucatorCurent].Location = new Point(pozitieMarker1.X + (pointMark[jucatorCurent].Width + 1)* ((punctaj[jucatorCurent] - 1) % 20) - (1 + (punctaj[jucatorCurent] % 20) / 5), pozitieMarker1.Y + (pointMark[jucatorCurent].Width + 3)  * (punctaj[jucatorCurent] / 20));
        }
        private void AddPoints(int indexPiesa, int i)
        {
            int punctajVertical = 0;
            int punctajOrizontal = 0;
            int inainte = indexPiesa + 1, inapoi = indexPiesa - 1;
            bool inainteExista = true;
            bool inapoiExista = true;
            
            while((inainte < 5 || inapoi >= 0) && (inainteExista || inapoiExista))
            {
                if(inainte < 5 && inainteExista && rightSideMatrix[jucatorCurent][i][inainte] != null)
                {
                    punctajOrizontal++;
                }
                else
                {
                    inainteExista = false;
                }
                if(inapoi >= 0 && inapoiExista && rightSideMatrix[jucatorCurent][i][inapoi] != null)
                {
                    punctajOrizontal++;
                }
                else
                {
                    inapoiExista = false;
                }
                inainte++;
                inapoi--;
            }
            inainte = i + 1;
            inapoi = i - 1;
            inainteExista = true;
            inapoiExista = true;
            while ((inainte < 5 || inapoi >= 0) && (inainteExista || inapoiExista))
            {
                if (inainte < 5 && inainteExista && rightSideMatrix[jucatorCurent][inainte][indexPiesa] != null)
                {
                    punctajVertical++;
                }
                else
                {
                    inainteExista = false;
                }
                if (inapoi >= 0 && inapoiExista && rightSideMatrix[jucatorCurent][inapoi][indexPiesa] != null)
                {
                    punctajVertical++;
                }
                else
                {
                    inapoiExista = false;
                }
                inainte++;
                inapoi--;
            }
            if (punctajOrizontal == 0 && punctajVertical == 0)
            {
                punctaj[jucatorCurent]++;
            }
            else
            {
                if (punctajOrizontal != 0) punctajOrizontal++;
                if (punctajVertical != 0) punctajVertical++;
                punctaj[jucatorCurent] += punctajOrizontal;
                punctaj[jucatorCurent] += punctajVertical;
            }
            
        }
        
        private bool SfarsitRunda()
        {
            foreach(List<PictureBox> searchForRemainingTiles in factoriesPieces)
            {
                if(searchForRemainingTiles.Count !=0 )
                {
                    Console.WriteLine(searchForRemainingTiles.Count);
                    return false;
                }
            }
            if(middleOfTheFactories.Count != 0)
            {
                Console.WriteLine(middleOfTheFactories.Count);
                return false;
            }
            return true;
        }
        private void RemoveFromMiddle(PictureBox sender)
        {
            for (int i = 0; i < middleOfTheFactories.Count; i++)
            {
                if (middleOfTheFactories[i].ImageLocation == sender.ImageLocation)
                {
                    middleOfTheFactories.Remove(middleOfTheFactories[i]);
                    i--;
                }
            }
            
        }

        private bool TileBelongsToFactoriesPiece(PictureBox sender)
        {
            foreach (List<PictureBox> pieces in factoriesPieces)
            {
                if (pieces.Contains(sender))
                {
                    return true;
                }
            }
            return false;
        }

        private void GetGroupTiles(PictureBox sender)
        {
            foreach (List<PictureBox> pieces in factoriesPieces)
            {
                if (pieces.Contains(sender))
                {
                    for (int i = 0; i < pieces.Count; i++)
                    {
                        if (pieces[i].ImageLocation == sender.ImageLocation && pieces[i] != groupMovingTiles[0])
                        {
                            groupMovingTiles.Add(pieces[i]);
                            
                        }
                    }
                }
            }
        }

        private void DrawGroupInLine(PictureBox sender)
        {
            for (int i = 1; i < groupMovingTiles.Count; i++)
            {
                groupMovingTiles[i].Location = new Point(sender.Location.X + i * (sender.Width + 3), sender.Location.Y);
                groupMovingTiles[i].BringToFront();
            }
        }

        private bool TileBelongsToMiddleOfTheTable(PictureBox sender)
        {
            foreach (PictureBox searchForTileInMiddle in middleOfTheFactories)
            {
                if (searchForTileInMiddle == sender)
                {
                    return true;
                }
            }
            return false;
        }

        private bool FirstPersonToDrawMarker()
        {
            foreach (List<PictureBox> checkMarker in liniaPodelei)
            {
                if (checkMarker[0].ImageLocation == piesaDeMarcaj.ImageLocation)
                {
                    return false;
                }
            }
            return true;
        }

        private void GetGroupTilesFromTheMiddle(PictureBox sender)
        {
            foreach (PictureBox groupTiles in middleOfTheFactories)
            {
                if (groupTiles.ImageLocation == sender.ImageLocation && groupTiles != sender)
                {
                    groupMovingTiles.Add(groupTiles);

                }
            }
            
        }

        private void DrawGroupInLineFromTheMiddle(PictureBox sender)
        {
            for (int i = 1; i < groupMovingTiles.Count; i++)
            {
                groupMovingTiles[i].Location = new Point(sender.Location.X + i * (sender.Width + 3), sender.Location.Y);
                groupMovingTiles[i].BringToFront();
            }
        }

        private bool CheckIfPossible(PictureBox sender)
        {
            indexOfLine = -1;
            if(sender.Location.X >= boards[jucatorCurent].Location.X && sender.Location.X <= boards[jucatorCurent].Location.X + boards[jucatorCurent].Width / 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sender.Location.Y >= coordonateLinii[i] - onTheBoardPieces[jucatorCurent][i][0].Height / 2 && sender.Location.Y < coordonateLinii[i] + onTheBoardPieces[jucatorCurent][i][0].Height / 2)
                    {
                        indexOfLine = i;
                    }

                }
            }
            
            if (indexOfLine == -1)
            {
                return false;
            }
            foreach (PictureBox search in rightSideMatrix[jucatorCurent][indexOfLine])
            {
                if(search != null)
                {
                    if (search.ImageLocation == groupMovingTiles[0].ImageLocation)
                    {
                        return false;
                    }
                }
                    
            }
            if (onTheBoardPieces[jucatorCurent][indexOfLine][0].ImageLocation == groupMovingTiles[0].ImageLocation && onTheBoardPieces[jucatorCurent][indexOfLine].Count != indexOfLine + 1 || onTheBoardPieces[jucatorCurent][indexOfLine][0].ImageLocation == null)
            {
                return true;
            }
            return false;
        }

        private void GetGroupTilesForMiddle(PictureBox sender)
        {
            foreach (List<PictureBox> pieces in factoriesPieces)
            {
                if (pieces.Contains(sender))
                {
                    for (int i = 0; i < pieces.Count; i++)
                    {
                        if (pieces[i].ImageLocation != sender.ImageLocation)
                        {
                            middleOfTheFactories.Add(pieces[i]);
                        }
                    }
                }
            }
        }
        private void PutOnTheBoard(PictureBox sender)
        {
            numberOfTilesInLine = onTheBoardPieces[jucatorCurent][indexOfLine].Count;
            int exces = 0;
            numberOfTilesInHand = groupMovingTiles.Count;
            int nrTiles = 4;
            foreach(List<PictureBox> deleteFromFactory in factoriesPieces)
            {
                if(deleteFromFactory.Contains(sender))
                {
                    for(int i = 0; i < nrTiles; i++)
                    {
                        if(deleteFromFactory[i].ImageLocation == sender.ImageLocation)
                        {
                            deleteFromFactory.Remove(deleteFromFactory[i]);
                            nrTiles--;
                            i--;
                        }
                    }
                }

            }
            if (onTheBoardPieces[jucatorCurent][indexOfLine][0].ImageLocation == null)
            {
                boards[jucatorCurent].Controls.Add(groupMovingTiles[0]);
                groupMovingTiles[0].Location = new Point(onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.X, onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.Y);
                onTheBoardPieces[jucatorCurent][indexOfLine][0].ImageLocation = groupMovingTiles[0].ImageLocation;
                onTheBoardPieces[jucatorCurent][indexOfLine][0].SizeMode = PictureBoxSizeMode.StretchImage;
                numberOfTilesInHand--;
                boards[jucatorCurent].Controls.Add(onTheBoardPieces[jucatorCurent][indexOfLine][0]);
                groupMovingTiles[0].Visible = false;
                onTheBoardPieces[jucatorCurent][indexOfLine][0].Visible = true;
                if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                {
                    middleOfTheFactories.Remove(groupMovingTiles[0]);
                }
                //onTheBoardPieces[jucatorCurent][indexOfLine][0].Enabled = false;
                onTheBoardPieces[jucatorCurent][indexOfLine][0].BringToFront();
                groupMovingTiles.Remove(groupMovingTiles[0]);
            }
            if (numberOfTilesInLine + numberOfTilesInHand > indexOfLine + 1)
            {
                exces = numberOfTilesInHand + numberOfTilesInLine - indexOfLine - 1;
                numberOfTilesInHand = indexOfLine + 1 - numberOfTilesInLine;
            }
            for (int i = numberOfTilesInLine; i < numberOfTilesInLine + numberOfTilesInHand; i++)
            {
                boards[jucatorCurent].Controls.Add(groupMovingTiles[0]);
                if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                {
                    middleOfTheFactories.Remove(groupMovingTiles[0]);
                }
                groupMovingTiles[0].Location = new Point(onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.X + i * (bag[0].Width + 2), onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.Y);
                onTheBoardPieces[jucatorCurent][indexOfLine].Add(groupMovingTiles[0]);
                boards[jucatorCurent].Controls.Add(onTheBoardPieces[jucatorCurent][indexOfLine][i]);
               // onTheBoardPieces[jucatorCurent][indexOfLine][0].Enabled = false;
                groupMovingTiles.Remove(groupMovingTiles[0]);
            }
            if (exces != 0)
            {
                AddToFloorLine(exces);
            }
            if(rightSideMatrix[jucatorCurent][indexOfLine].Count == indexOfLine + 1)
            {
                MessageBox.Show("Jucatorul " + jucatorCurent + " va incheie jocul! Aceasta este ultima runda!");
                final = true;
            }
        }

        private void AddToFloorLine(int exces)
        {
            int numberOfTilesOnFLoorLine = liniaPodelei[jucatorCurent].Count;
            int numberOfTilesInHandFloorLine = exces;
            if (numberOfTilesOnFLoorLine + exces > 7)
            {
                exces = numberOfTilesOnFLoorLine + exces - 7;
                numberOfTilesInHandFloorLine = 7 - numberOfTilesOnFLoorLine;
                for (int i = 0; i < exces; i++)
                {
                    /*PictureBox piecesOnFloorLine = new PictureBox();
                    piecesOnFloorLine.ImageLocation = groupMovingTiles[0].ImageLocation;
                    piecesOnFloorLine.Size = new Size(bag[0].Width, bag[0].Height);
                    piecesOnFloorLine.SizeMode = PictureBoxSizeMode.StretchImage;*/
                    box.Add(groupMovingTiles[0]);
                    this.Controls.Remove(groupMovingTiles[0]);
                    groupMovingTiles.Remove(groupMovingTiles[0]);
                    i--;
                    //box.Add(piecesOnFloorLine);
                }
            }
            if (liniaPodelei[jucatorCurent][0].ImageLocation == null)
            {
                boards[jucatorCurent].Controls.Add(groupMovingTiles[0]);
                groupMovingTiles[0].Location = new Point(liniaPodelei[jucatorCurent][0].Location.X, liniaPodelei[jucatorCurent][0].Location.Y);
                liniaPodelei[jucatorCurent][0].ImageLocation = groupMovingTiles[0].ImageLocation;
                liniaPodelei[jucatorCurent][0].SizeMode = PictureBoxSizeMode.StretchImage;
                boards[jucatorCurent].Controls.Add(liniaPodelei[jucatorCurent][0]);
                groupMovingTiles[0].Visible = false;
                liniaPodelei[jucatorCurent][0].Visible = true;
                liniaPodelei[jucatorCurent][0].BringToFront();
                //liniaPodelei[jucatorCurent][0].Enabled = false;
                if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                {
                    middleOfTheFactories.Remove(groupMovingTiles[0]);
                }
                groupMovingTiles.Remove(groupMovingTiles[0]);
                numberOfTilesInHandFloorLine--;
            }
            if(numberOfTilesOnFLoorLine <= 7 && groupMovingTiles.Count!=0)
            {
                for (int index = numberOfTilesOnFLoorLine; index < numberOfTilesOnFLoorLine + numberOfTilesInHandFloorLine; index++)
                {
                    boards[jucatorCurent].Controls.Add(groupMovingTiles[0]);
                    if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                    {
                        middleOfTheFactories.Remove(groupMovingTiles[0]);
                    }
                    groupMovingTiles[0].Location = new Point(liniaPodelei[jucatorCurent][index - 1].Location.X + bag[0].Width + 4, liniaPodelei[jucatorCurent][0].Location.Y);
                    //groupMovingTiles[0].Enabled = false;
                    liniaPodelei[jucatorCurent].Add(groupMovingTiles[0]);
                    boards[jucatorCurent].Controls.Add(liniaPodelei[jucatorCurent][index]);
                    groupMovingTiles.Remove(groupMovingTiles[0]);
                }
            }
        }

        private void AddToTheMiddle()
        {
            switch (nrJucatori)
            {
                case 2: AddToTheMiddleOf5(); break;
                case 3: AddToTheMiddleOf7(); break;
                case 4: AddToTheMiddleOf9(); break;
            }
        }

        private void AddToTheMiddleOf5()
        {
            int i = 0, j = 0;
            foreach (PictureBox inTheMiddle in middleOfTheFactories)
            {
                inTheMiddle.Location = new Point(factories[1].Location.X + bag[0].Width / 2 + factories[0].Width + 3 + i * (bag[0].Width + 3), factories[1].Location.Y + j * (bag[0].Width + 3) + 3);
                i++;
                if (i == 4)
                {
                    j++;
                    i = 0;
                }
                foreach (List<PictureBox> pieces in factoriesPieces)
                {
                    if (pieces.Contains(inTheMiddle))
                    {
                        pieces.Clear();
                    }
                }
                this.Controls.Add(inTheMiddle);
                inTheMiddle.BringToFront();
            }
        }

        private void AddToTheMiddleOf7()
        {
            int i = 0, j = 0;
            foreach (PictureBox inTheMiddle in middleOfTheFactories)
            {
                inTheMiddle.Location = new Point(factories[3].Location.X + 4 * factories[0].Width / 3 + 3 + i * (bag[0].Width + 3), factories[3].Location.Y - 3 * bag[0].Width / 4 + j * (bag[0].Width + 3) + 3);
                i++;
                if (i == 6)
                {
                    j++;
                    i = 0;
                }
                /*foreach (List<PictureBox> pieces in factoriesPieces)
                {
                    if (pieces.Contains(inTheMiddle))
                    {
                        pieces.Remove(inTheMiddle);
                    }
                }*/
                this.Controls.Add(inTheMiddle);
                inTheMiddle.BringToFront();
            }
        }

        private void AddToTheMiddleOf9()
        {
            int i = 0, j = 0;
            foreach (PictureBox inTheMiddle in middleOfTheFactories)
            {
                inTheMiddle.Location = new Point(factories[3].Location.X + 4 * factories[0].Width / 3 + 3 + i * (bag[0].Width + 3), factories[3].Location.Y + j * (bag[0].Width + 3) + 3);
                i++;
                if (i == 9)
                {
                    j++;
                    i = 0;
                }
                foreach (List<PictureBox> pieces in factoriesPieces)
                {
                    if (pieces.Contains(inTheMiddle))
                    {
                        pieces.Remove(pieces[0]);
                    }
                }
                //this.Controls.Add(inTheMiddle);
                inTheMiddle.BringToFront();
            }
        }

        private async void WaitForTimer()
        {
            if(!inceput)
            {
                await Task.Delay(1000);
                for (int i = 1; i < nrJucatori; i++)
                {
                    boards[i].Location = rememberMove[(i - 1)];
                }
                boards[0].Location = rememberMove[nrJucatori - 1];
                for (int i = 0; i < nrJucatori; i++)
                {
                    rememberMove[i] = boards[i].Location;

                }
                mutareaTablelor = false;
            }
            //jucatorCurent++;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine((int)(this.Width / boardToTableRatio));
            Console.WriteLine(boardToTableRatio);
            Console.WriteLine(this.Width);
            boards[0].Size = new Size((int)(this.Width * boardToTableRatio), (int)(this.Width * boardToTableRatio));
            boards[0].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
        }

        private void iesireDinJoc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
/*
➼LUCREAZA LA RESIZE CA NU MERGE

➼SCRIE CODUL PENTRU SFARSITUL UNEI RUNDE(NU SUNT PIESE NICI PE FABRICI NICI IN MIJLOC)

➼CREEAZA PICTUREBOX-URILE PENTRU COMPLETAREA MATRICEI DIN DREAPTA

➼ADAUGA PIATRA CARE MARCHEAZA PUNCTELE SI ACTUALIZEAZ-O AUTOMAT LA FINALUL UNEI RUNDE

➼SCRIE CODUL IN CAZUL IN CARE NU MAI SUNT PIESE IN PUNGUTA -> SE ADAUGA DIN CUTIE -> SE MAI FACE SHUFFLE?? -> CELE DIN CUTIE SE 
ADAUGA IN PUNGA -> CUTIA RAMANE GOALA

➼SCRIE CODUL IN CAZUL IN CARE NU SUNT DESTULE PIESE NICI IN CUTIE NICI IN PUNGUTA PENTRU A PUNE 4 PIESE PE FIECARE 
FABRICA -> EVENTUAL SCHIMBA CODUL EXISTENT PENTRU ADAUGAREA PIESELOR PE FABRICI

➼ANIMATIE PENTRU ROTIREA TABLELOR??

➼UN TIMER PE ECRAN CARE ARATA TIMUPL PANA LA URMATORUL JUCATOR

➼SCRIE CODUL PENTRU A PERMITE JUCATORULUI SA FACA MUTE PIESELE ADAUGATE PE TABLA IN TURA RESPECTIVA PANA I SE TERMINA RANDUL

➼SCRIE CODUL PENTRU SFARSITUL JOCULUI(UN JUCATOR A COMPLETAT UN RAND) -> EVENTUAL ARATA PE ECRAN O AVERTIZARE IN CAZUL IN CARE 
CINEVA A COMPLETAT ULTIMUL RAND NECESAR -> ULTIMA TURA IN CARE VA PUTETI MAXA PUNCTELE

➼IMBINA CELE DOUA PROIECTE(CU INTRODUCERE IN JOC SI CU DESFASURAREA ACESTUIA, ADAUGA REGULAMENTUL

➼VARIANTA DOUA DE JOC, NIMIC NU SE SCHIMBA, CODUL PENTRU PICTUREBOX-URILE DIN DREAPTA SE COMPLICA PUTIN -> APARE NECESITATEA 
VERIFICARII POSIBILITATII PLASARI UNEI PIESE ASTFEL INCAT SA NU SE REPTE PE LINIE SAU COLOANA

➼PUNE BUTON DE EXIT JOC, START AL JOC, CU ACEIASI JUCATORI -> NU MAI APARE MENIUL DE INCEPUT, SI NICI REGULAMENTUL, SAU START 
JOC CU ALTI JUCATORI -> SE REIA PROGRAMUL

➼MODIFICA CODUL PENTRU ADAUGARE PE TABLA -> FARA COPY DE PICTUREBOX ->SCHIMBA LOCATIA PICTUREBOXULUI
*/

