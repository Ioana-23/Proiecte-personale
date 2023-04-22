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
    public partial class FormGame : Form
    {
        double tileToBoardRatio = 8.333 / 100;
        double factoryToBoardRatio = 26.666 / 100;
        double boardToTableRatio = 300.0 / 1140.0;
        int nrJucatori = 4;
        int marimePiese = 0;
        int jucatorCurent = 0;
        int numberOfTilesInHand = 0;
        int numberOfTilesInLine = 0;
        int indexOfLine = -1;
        int jucatorCuPiesaDeMarcaj = 0;
        int x = 0;
        bool mutaRandul = false;
        bool mutareaTablelor = false;
        bool inceput = true;
        int[] punctaj = { 0, 0, 0, 0 };
        int[] coordonateLinii = { 0, 0, 0, 0, 0 };
        int coordonateLiniaPodelei = 0;
        int[] subtractTiles = { 1, 1, 2, 2, 2, 3, 3 };
        string[] nume = new string[4];
        bool finalulRundei = false;
        bool final = false;
        bool primul = false;
        string[] numeJucatori = new string[4];
        int factorySize = 0;
        Point[] rememberMove = new Point[]{
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
        };
        List<PictureBox> bag = new List<PictureBox>();
        List<TextBox> names = new List<TextBox>();
        List<PictureBox> box = new List<PictureBox>();
        List<List<PictureBox>> factoriesPieces = new List<List<PictureBox>>();
        List<PictureBox> factories = new List<PictureBox>();
        List<PictureBox> groupMovingTiles = new List<PictureBox>();
        List<List<List<PictureBox>>> onTheBoardPieces = new List<List<List<PictureBox>>>();
        List<List<PictureBox>> liniaPodelei = new List<List<PictureBox>>();
        List<PictureBox> middleOfTheFactories = new List<PictureBox>();
        PictureBox piesaDeMarcaj = new PictureBox();
        List<List<List<PictureBox>>> rightSideMatrix = new List<List<List<PictureBox>>>();
        List<GroupBox> boards = new List<GroupBox>();
        TextBox castigator = new TextBox();
        TableLayoutPanel scorFinal = new TableLayoutPanel();
        PictureBox fireworks = new PictureBox();
        PictureBox congratulations = new PictureBox();
        Button setari = new Button();
        Button cancel = new Button();
        Button meniu = new Button();
        Button restart = new Button();
        GroupBox meniuSetari = new GroupBox();
        Panel fabrici = new Panel();
        int[,] culori = new int[4, 5]{
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}};

        //public FormGame(int nrdeJucatori, string[] nume)
        public FormGame(int nrdeJucatori, string[] nume)
        {
            nrJucatori = nrdeJucatori;
            numeJucatori = nume;
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
            this.MinimumSize = new Size(1024, 1000);
            this.Size = new Size(1140, 1096);
            CreateBoards();
            CreateFactoriesPieces();
            CreateTiles();
            CreateFactory();
            CreatePictureBoxes();
            CreateLiniaPodelei();
            CreateRightSideMatrix();
            AddNames();
            setari.Text = "SETARI";
            setari.BackColor = Color.AntiqueWhite;
            setari.ForeColor = Color.Black;
           // iesireDinJoc.
            setari.Font = new Font(setari.Font.FontFamily, 16);
            setari.Size = new Size(100, 80);
            setari.Location = new Point(20, this.Size.Height - setari.Size.Height - 50);
            //setari.Anchor = AnchorStyles.None;
            //setari.Anchor = AnchorStyles.Top + Bottom + Left + Right;
            setari.Anchor = AnchorStyles.Top + Left;
            this.Controls.Add(setari);
            this.setari.Click += new System.EventHandler(this.Setari_Click);
            StartJoc();
            /*  2 jucatori -> 5
                3 jucatori -> 7
                4 jucatori -> 9
            */
        }

        private void AddNames()
        {
            for(int i = 0; i < nrJucatori; i++)
            {
                TextBox addName = new TextBox();
                addName.Font = new Font(addName.Font.FontFamily, 16);
                addName.Size = new Size(boards[0].Width, 65);
                addName.BackColor = Color.AntiqueWhite;
                addName.Multiline = true;
                addName.ForeColor = Color.Black;
                addName.TextAlign = HorizontalAlignment.Center;
                addName.Anchor = AnchorStyles.None;
                //addName.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                names.Add(addName);
            }
            names[0].Location = new Point(5, 5);
            boards[0].Controls.Add(names[0]);
            names[0].Text = numeJucatori[0] +"\r\n" +" scor : " + punctaj[0];
            names[0].Enabled = false;
            switch(nrJucatori)
            {
                case 2: Add2Names(); break;
                case 3: Add3Names(); break;
                case 4: Add4Names(); break;
            }
            //this.Controls.Add(names[0]);
        }

        private void Add2Names()
        {
            names[1].Location = new Point(5, 5);
            boards[1].Controls.Add(names[1]);
            names[1].Text = numeJucatori[1] + "\r\n" + "scor : " + punctaj[1];
            names[1].Enabled = false;
        }

        private void Add3Names()
        {
            Add2Names();
            names[2].Location = new Point(5, 5);
            boards[2].Controls.Add(names[2]);
            names[2].Text = numeJucatori[2] + "\r\n" + "scor : " + punctaj[2];
            names[2].Enabled = false;
        }

        private void Add4Names()
        {
            Add3Names();
            names[3].Location = new Point(5, 5);
            boards[3].Controls.Add(names[3]);
            names[3].Text = numeJucatori[3] + "\r\n" + "scor : " + punctaj[3];
            names[3].Enabled = false;
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
                        rightSide.Location = new Point(boards[0].Location.X + boards[0].Width / 2 + 6 + k * (marimePiese + 2), boards[0].Location.Y + 1 * boards[0].Height / 3 + 5 + j * (marimePiese + 2));
                        rightSide.Size = new Size(marimePiese, marimePiese);
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
                addBoard.Anchor = AnchorStyles.Bottom;
                //addBoard.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                //addBoard.MinimumSize = new Size(200, 200);
                addBoard.MaximumSize = new Size(300, 300);
                //boards.Add(board1);
                boards.Add(addBoard);
            }
            boards[0].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 50);
            rememberMove[0] = boards[0].Location;
            boards[0].Anchor = AnchorStyles.Bottom;
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
            rememberMove[1] = boards[1].Location;
            this.Controls.Add(boards[1]);
        }

        private void Add3Boards()
        {
            boards[1].Location = new Point(this.Width - 4 * boards[0].Width / 3 + 20, 2 * boards[0].Width / 3);
            rememberMove[1] = boards[1].Location;
            this.Controls.Add(boards[1]);
            boards[2].Location = new Point(boards[0].Width / 3 - 20, 2 * boards[0].Width / 3);
            rememberMove[2] = boards[2].Location;
            this.Controls.Add(boards[2]);
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
                    marimePiese = tileSize;
                    currentTile.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentTile.Anchor = AnchorStyles.None;
                    //currentTile.Anchor = AnchorStyles.Top + Bottom + Left + Right;
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
                factorySize = (int)factorySizeDouble;
                currentFactory.BackColor = Color.Transparent;
                currentFactory.Size = new Size(factorySize, factorySize);
                currentFactory.Location = new Point(this.Width / 2 - currentFactory.Width / 2, 350);
                currentFactory.BackgroundImageLayout = ImageLayout.Stretch;
                currentFactory.Anchor = AnchorStyles.None;
                //currentFactory.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                factories.Add(currentFactory);
                //fabrici.Add(currentFactory);
            }
            piesaDeMarcaj.Size = new Size(marimePiese, marimePiese);
            piesaDeMarcaj.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\1.png";
            piesaDeMarcaj.Anchor = AnchorStyles.None;
            //piesaDeMarcaj.Anchor = AnchorStyles.Top + Bottom + Left + Right;
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
                    onTheBoardPieces[j][i][0].Size = new Size(marimePiese, marimePiese);
                    onTheBoardPieces[j][i][0].Anchor = AnchorStyles.None;
                    //onTheBoardPieces[j][i][0].Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    coordonateLinii[i] = boards[0].Location.Y + 1 * boards[0].Height / 3 + 5 + i * (marimePiese + 2);
                    onTheBoardPieces[j][i][0].Location = new Point(boards[0].Width / 2 - marimePiese - 8 - i * (marimePiese + 2), 1 * boards[0].Height / 3 + 5 + i * (marimePiese + 2));
                    //onTheBoardPieces[j][i][0].BackColor = Color.Red;
                    //this.Controls.Add(onTheBoardPieces[j][i][0]);
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
            coordonateLiniaPodelei = boards[0].Location.Y + boards[0].Height - 15 - marimePiese;
            for (int i = 0; i < nrJucatori; i++)
            {
                PictureBox currentTileOnFloorLine = new PictureBox();
                currentTileOnFloorLine.Size = new Size(marimePiese, marimePiese);
                boards[i].Controls.Add(currentTileOnFloorLine);
                currentTileOnFloorLine.Location = new Point(10, boards[0].Height - 15 - marimePiese);
                currentTileOnFloorLine.Anchor = AnchorStyles.None;
                //currentTileOnFloorLine.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                liniaPodelei[i].Add(currentTileOnFloorLine);
                liniaPodelei[i][0].Visible = false;
            }
        }

        private void ShuffleTiles()
        {
            bag = bag.OrderBy(piece => Guid.NewGuid()).ToList();
            box = box.OrderBy(piece => Guid.NewGuid()).ToList();
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
                    if (bag.Count < 4)
                    {
                        switch(bag.Count)
                        {
                            case 0: break;
                            case 1: bag[0].Location = new Point(factories[i].Location.X + marimePiese / 2, factories[i].Location.Y + 5); 
                                    factoriesPieces[i].Add(bag[0]);
                                    bag[0].Visible = true;
                                    this.Controls.Add(factoriesPieces[i][0]); 
                                    factoriesPieces[i][0].BringToFront(); bag.Clear(); break;
                            case 2: bag[0].Location = new Point(factories[i].Location.X + marimePiese / 2, factories[i].Location.Y + 5);
                                    bag[1].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * marimePiese / 2, factories[i].Location.Y + 5);
                                    factoriesPieces[i].Add(bag[0]);
                                    factoriesPieces[i].Add(bag[1]);
                                    bag[1].Visible = true;
                                    bag[0].Visible = true;
                                    this.Controls.Add(factoriesPieces[i][0]);
                                    this.Controls.Add(factoriesPieces[i][1]);
                                    factoriesPieces[i][0].BringToFront();
                                    factoriesPieces[i][1].BringToFront(); bag.Clear(); break;

                            case 3: bag[0].Location = new Point(factories[i].Location.X + marimePiese / 2, factories[i].Location.Y + 5);
                                    bag[1].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * marimePiese / 2, factories[i].Location.Y + 5);
                                    bag[2].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * marimePiese / 2, factories[i].Location.Y + factories[i].Width / 2);
                                    factoriesPieces[i].Add(bag[0]);
                                    factoriesPieces[i].Add(bag[1]);
                                    factoriesPieces[i].Add(bag[2]);
                                    bag[1].Visible = true;
                                    bag[0].Visible = true;
                                    bag[2].Visible = true;
                                    this.Controls.Add(factoriesPieces[i][0]);
                                    this.Controls.Add(factoriesPieces[i][1]);
                                    this.Controls.Add(factoriesPieces[i][2]);
                                    factoriesPieces[i][0].BringToFront();
                                    factoriesPieces[i][1].BringToFront();
                                    factoriesPieces[i][2].BringToFront(); bag.Clear(); break;
                        }
                        break;
                    }
                }
                bag[0].Location = new Point(factories[i].Location.X + marimePiese / 2, factories[i].Location.Y + 5);
                bag[1].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * marimePiese / 2, factories[i].Location.Y + 5);
                bag[2].Location = new Point(factories[i].Location.X + factories[i].Width - 3 * marimePiese / 2, factories[i].Location.Y + factories[i].Width / 2);
                bag[3].Location = new Point(factories[i].Location.X + marimePiese / 2, factories[i].Location.Y + factories[i].Width / 2);
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
            factories[0].Location = new Point(this.Width / 2 - factories[0].Width / 2, boards[1].Location.Y + boards[0].Height + 25);
            factories[1].Location = new Point(boards[0].Location.X, 2 * factories[1].Height + factories[0].Location.Y - 50);
            factories[2].Location = new Point(boards[0].Location.X + boards[0].Width - factories[2].Width, 2 * factories[2].Height + factories[0].Location.Y - 50);
            factories[3].Location = new Point(boards[0].Location.X + factories[3].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 25);
            factories[4].Location = new Point(boards[0].Location.X + boards[0].Width - 3 * factories[4].Width / 2, 2 * factories[3].Height + factories[1].Location.Y - 25);
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - marimePiese / 2, factories[3].Location.Y + factories[0].Height / 2 - marimePiese / 2);
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
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - marimePiese / 2, factories[1].Location.Y + factories[0].Height / 2 - marimePiese / 2);
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
            factories[0].Location = new Point(this.Width / 2 - factories[0].Width / 2, boards[2].Location.Y + boards[0].Height + 25);
            factories[1].Location = new Point(boards[0].Location.X, 2 * factories[1].Height + factories[0].Location.Y - 85);
            factories[2].Location = new Point(boards[0].Location.X + boards[0].Width - factories[2].Width, 2 * factories[2].Height + factories[0].Location.Y - 85);
            factories[3].Location = new Point(factories[1].Location.X - factories[1].Width, 2 * factories[3].Height + factories[1].Location.Y - 65);
            factories[4].Location = new Point(factories[2].Location.X + factories[4].Width, 2 * factories[3].Height + factories[1].Location.Y - 65);
            factories[5].Location = new Point(boards[0].Location.X - factories[5].Width / 2, 2 * factories[3].Height + factories[3].Location.Y - 65);
            factories[6].Location = new Point(factories[2].Location.X + factories[6].Width / 2, 2 * factories[3].Height + factories[3].Location.Y - 65);
            factories[7].Location = new Point(boards[0].Location.X + factories[3].Width / 2, 2 * factories[5].Height + factories[5].Location.Y - 70);
            factories[8].Location = new Point(boards[0].Location.X + boards[0].Width - 3 * factories[3].Width / 2, 2 * factories[5].Height + factories[5].Location.Y - 70);
            piesaDeMarcaj.Location = new Point(factories[0].Location.X + factories[0].Width / 2 - marimePiese / 2, factories[1].Location.Y + factories[0].Height / 2 - marimePiese / 2);
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
            if (!mutareaTablelor)
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
                        
                        if (FirstPersonToDrawMarker())
                        {
                            if (liniaPodelei[jucatorCurent][0].ImageLocation != piesaDeMarcaj.ImageLocation && liniaPodelei[jucatorCurent][0].ImageLocation != null)
                            {
                                PictureBox adauga = new PictureBox();
                                adauga.Size = liniaPodelei[jucatorCurent][0].Size;
                                adauga.ImageLocation = liniaPodelei[jucatorCurent][0].ImageLocation;
                                adauga.SizeMode = liniaPodelei[jucatorCurent][0].SizeMode;
                                box.Add(adauga);
                            }
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
                    if (sender.Location.Y >= onTheBoardPieces[jucatorCurent][i][0].Location.Y - marimePiese / 2 && sender.Location.Y <= onTheBoardPieces[jucatorCurent][i][0].Location.Y + marimePiese / 2)
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
                groupMovingTiles[0].Location = moveBoards[jucatorCurent].PointToClient(new Point(Cursor.Position.X - marimePiese / 2, Cursor.Position.Y - marimePiese / 2));
                DrawGroupInLine(groupMovingTiles[0]);

            }
            else
            {*/
            if (groupMovingTiles.Count != 0 && e.Button == MouseButtons.Left)
            {
                groupMovingTiles[0].Location = this.PointToClient(new Point(Cursor.Position.X - marimePiese / 2, Cursor.Position.Y - marimePiese / 2));
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
                            if (groupMovingTiles[0].Location.Y > coordonateLiniaPodelei - marimePiese / 2 && groupMovingTiles[0].Location.Y < coordonateLiniaPodelei + marimePiese / 2)
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
                    AdunaPunctajeleFinale();
                    ClearEverything();
                    int punctajmaxim = 0, indicemaxim = 0;
                    for (int i = 0; i < nrJucatori; i++)
                    {
                        if (punctaj[i] > punctajmaxim)
                        {
                            punctajmaxim = punctaj[i];
                            indicemaxim = i;
                        }

                    }
                    
                    castigator.Size = new Size(600, 100);
                    castigator.Location = new Point(this.Size.Width / 2 - castigator.Size.Width / 2, this.Size.Height / 2 - (castigator.Size.Height + 30 + nrJucatori * 100) / 2); ;
                    castigator.Font = new Font(castigator.Font.FontFamily, 22);
                    castigator.BackColor = Color.AntiqueWhite;
                    castigator.Enabled = false;
                    castigator.Multiline = true;
                    castigator.Anchor = AnchorStyles.None;
                    //castigator.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    castigator.Text = "Castigatorul este " + numeJucatori[indicemaxim] + ", cu " + punctaj[indicemaxim] + " de puncte \r\n Felicitari tuturor jucatorilor!";
                    this.Controls.Add(castigator);
                    Rearrenge();
                    scorFinal.ColumnCount = 2;
                    scorFinal.RowCount = 1;
                    scorFinal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    scorFinal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    scorFinal.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
                    scorFinal.Controls.Add(new Label() { Text = numeJucatori[0] }, 0, 0);
                    scorFinal.Controls.Add(new Label() { Text = punctaj[0].ToString() }, 1, 0);
                    scorFinal.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
                    scorFinal.Anchor = AnchorStyles.None;
                   // scorFinal.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    scorFinal.Size = new Size(350, nrJucatori * 100);
                    scorFinal.Location = new Point(castigator.Location.X + (castigator.Width - scorFinal.Width) / 2, castigator.Location.Y + castigator.Height + 30);
                    scorFinal.BackColor = Color.AntiqueWhite;
                    scorFinal.ForeColor = Color.Black;
                    scorFinal.Enabled = false;
                    scorFinal.Font = new Font(scorFinal.Font.FontFamily, 14);
                    for (int i = 1; i < nrJucatori; i++)
                    {
                        scorFinal.RowCount++;
                        scorFinal.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
                        scorFinal.Controls.Add(new Label() { Text = numeJucatori[i] }, 0, i);
                        scorFinal.Controls.Add(new Label() { Text = punctaj[i].ToString() }, 1, i);
                    }
                    this.Controls.Add(scorFinal);
                    fireworks.Size = new Size(300, 300);
                    fireworks.Location = new Point(this.Width / 2 - fireworks.Width / 2, scorFinal.Location.Y + scorFinal.Height + 30);
                    fireworks.SizeMode = PictureBoxSizeMode.StretchImage;
                    fireworks.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\fireworks_PNG15647.png";
                    fireworks.BackColor = Color.Transparent;
                    fireworks.Anchor = AnchorStyles.None;
                    fireworks.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    congratulations.Size = new Size(300, 300);
                    congratulations.Location = new Point(this.Width / 2 - fireworks.Width / 2, castigator.Location.Y - congratulations.Height - 30);
                    congratulations.SizeMode = PictureBoxSizeMode.StretchImage;
                    congratulations.ImageLocation = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\congrats1.png";
                    congratulations.BackColor = Color.Transparent;
                    congratulations.Anchor = AnchorStyles.None;
                    //congratulations.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    this.Controls.Add(fireworks);
                    this.Controls.Add(congratulations);
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
            else
            {
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
            }
        }

        private void Rearrenge()
        {
            int aux1 = 0;
            string aux2 = null;
            for(int i = 0; i < nrJucatori - 1; i++)
            {
                for(int j = i + 1; j < nrJucatori; j++)
                {
                    if(punctaj[i]<punctaj[j])
                    {
                        aux1 = punctaj[i];
                        punctaj[i] = punctaj[j];
                        punctaj[j] = aux1;
                        aux2 = numeJucatori[i];
                        numeJucatori[i] = numeJucatori[j];
                        numeJucatori[j] = aux2;
                    }
                }
            }
        }

        private void ClearEverything()
        {
            for(int j = 0; j < nrJucatori; j++)
            {
                this.Controls.Remove(boards[j]);
            }
            for(int j = 0; j < 2 * nrJucatori + 1; j++)
            {
                this.Controls.Remove(factories[j]);
            }
            this.Controls.Remove(piesaDeMarcaj);
        }

        private void AdunaPunctajeleFinale()
        {
            for (int i = 0; i < nrJucatori; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int numarlinie = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        if (rightSideMatrix[i][j][k] != null)
                        {
                            numarlinie++;
                        }
                    }
                    if(numarlinie == 5)
                    {
                        punctaj[i] += 2;
                    }
                    int numarcoloana = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        if (rightSideMatrix[i][k][j] != null)
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
                names[i].Text = numeJucatori[i] + "\r\n" + " scor : " + punctaj[i];
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
            for (int i = 0; i < nrJucatori; i++)
            {
                rememberMove[i] = boards[i].Location;
            }

        }

        private void Repozitionare2()
        {
            boards[jucatorCurent % 2].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            boards[(jucatorCurent + 1) % 2].Location = new Point(this.Size.Width / 2 - boards[0].Size.Width / 2, 0);
        }
        private void Repozitionare3()
        {
            boards[jucatorCurent % 3].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            boards[(jucatorCurent + 1) % 3].Location = new Point(this.Width - 4 * boards[0].Width / 3 + 20, 2 * boards[0].Width / 3);
            boards[(jucatorCurent + 2) % 3].Location = new Point(boards[0].Width / 3 - 20, 2 * boards[0].Width / 3);
        }
        private void Repozitionare4()
        {
            
            boards[jucatorCurent % 4].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            boards[(jucatorCurent + 1)% 4].Location = new Point(this.Size.Width - boards[0].Size.Width - 30, this.Size.Width / 2 - boards[0].Size.Width / 2);
            boards[(jucatorCurent + 2) % 4].Location = new Point(this.Size.Width / 2 - boards[0].Size.Width / 2, 0);
            boards[(jucatorCurent + 3) % 4].Location = new Point(0, this.Size.Width / 2 - boards[0].Size.Width / 2);
            
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
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Size = new Size(marimePiese, marimePiese);
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Anchor = AnchorStyles.None;
                        //rightSideMatrix[jucatorCurent][i][indexPiesa].Anchor = AnchorStyles.Top + Bottom + Left + Right;
                        rightSideMatrix[jucatorCurent][i][indexPiesa].Location = new Point(boards[0].Width / 2 + 6 + indexPiesa * (marimePiese + 2), 1 * boards[0].Height / 3 + 5 + i * (marimePiese + 2));
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
            for (int i = 0; i < nrJucatori; i++)
            {
                names[i].Text = numeJucatori[i] + "\r\n" + " scor : " + punctaj[i];
            }
            piesaDeMarcaj.Visible = true;
            int numarLiniaPodelei = liniaPodelei[jucatorCurent].Count;
            for(int j = 1; j < numarLiniaPodelei; j++)
            {
                box.Add(liniaPodelei[jucatorCurent][1]);
                boards[jucatorCurent].Controls.Remove(liniaPodelei[jucatorCurent][1]);
                liniaPodelei[jucatorCurent].Remove(liniaPodelei[jucatorCurent][1]);
            }
            if(liniaPodelei[jucatorCurent][0].ImageLocation != null && liniaPodelei[jucatorCurent][0].ImageLocation != piesaDeMarcaj.ImageLocation)
            {
                PictureBox adauga = new PictureBox();
                adauga.Size = liniaPodelei[jucatorCurent][0].Size;
                adauga.ImageLocation = liniaPodelei[jucatorCurent][0].ImageLocation;
                adauga.SizeMode = liniaPodelei[jucatorCurent][0].SizeMode;
                box.Add(adauga);
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
                groupMovingTiles[0].Anchor = AnchorStyles.None;
                //groupMovingTiles[0].Anchor = AnchorStyles.Top + Bottom + Left + Right;
                if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                {
                    middleOfTheFactories.Remove(groupMovingTiles[0]);
                }
                groupMovingTiles[0].Location = new Point(onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.X + i * (marimePiese + 2), onTheBoardPieces[jucatorCurent][indexOfLine][0].Location.Y);
                onTheBoardPieces[jucatorCurent][indexOfLine].Add(groupMovingTiles[0]);
                boards[jucatorCurent].Controls.Add(onTheBoardPieces[jucatorCurent][indexOfLine][i]);
               // onTheBoardPieces[jucatorCurent][indexOfLine][0].Enabled = false;
                groupMovingTiles.Remove(groupMovingTiles[0]);
            }
            if (exces != 0)
            {
                AddToFloorLine(exces);
            }
            int suma2 = 0, suma1 = 0;
            for(int j = 0; j < 5; j++)
            {
                if(rightSideMatrix[jucatorCurent][indexOfLine][j] != null)
                {
                    suma2++;
                }
                
            }
            for(int i = 0; i <= indexOfLine; i++)
            {
                if (onTheBoardPieces[jucatorCurent][indexOfLine][i].ImageLocation != null)
                {
                    suma1++;
                }
            }
            if(suma2 == 4 && suma1==indexOfLine+1 && !primul)
            {
                MessageBox.Show(numeJucatori[jucatorCurent] + " va incheia jocul! Aceasta este ultima runda!");
                final = true;
                primul = true;
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
                    piecesOnFloorLine.Size = new Size(marimePiese, marimePiese);
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
                    groupMovingTiles[0].Anchor = AnchorStyles.None;
                    //groupMovingTiles[0].Anchor = AnchorStyles.Top + Bottom + Left + Right;
                    if (middleOfTheFactories.Contains(groupMovingTiles[0]))
                    {
                        middleOfTheFactories.Remove(groupMovingTiles[0]);
                    }
                    groupMovingTiles[0].Location = new Point(liniaPodelei[jucatorCurent][index - 1].Location.X + marimePiese + 4, liniaPodelei[jucatorCurent][0].Location.Y);
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
                inTheMiddle.Location = new Point(factories[1].Location.X + marimePiese / 2 + factories[0].Width + 3 + i * (marimePiese + 3), factories[1].Location.Y + j * (marimePiese + 3) + 3);
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
                inTheMiddle.Location = new Point(factories[3].Location.X + 4 * factories[0].Width / 3 + 3 + i * (marimePiese + 3), factories[3].Location.Y - 3 * marimePiese / 4 + j * (marimePiese + 3) + 3);
                i++;
                if (i == 6)
                {
                    j++;
                    i = 0;
                }
                foreach (List<PictureBox> pieces in factoriesPieces)
                {
                    if (pieces.Contains(inTheMiddle))
                    {
                        pieces.Remove(inTheMiddle);
                    }
                }
                //this.Controls.Add(inTheMiddle);
                inTheMiddle.BringToFront();
            }
        }

        private void AddToTheMiddleOf9()
        {
            int i = 0, j = 0;
            foreach (PictureBox inTheMiddle in middleOfTheFactories)
            {
                inTheMiddle.Location = new Point(factories[3].Location.X + 4 * factories[0].Width / 3 + 3 + i * (marimePiese + 3), factories[3].Location.Y + j * (marimePiese + 3) + 3);
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
            //jucatorCurent++;
        }

        private void FormGame_Resize(object sender, EventArgs e)
        {
            if (this.Height < 1096 || this.Width < 1140)
            {
                switch (nrJucatori)
                {
                    case 2: Modify2Players(); break;
                    case 3: Modify3Players(); break;
                    case 4: Modify4Players(); break;
                }
            }
            double tileSizeDouble = Math.Ceiling(this.tileToBoardRatio * boards[0].Size.Width);
            marimePiese = (int)tileSizeDouble;
            for (int j = 0; j < nrJucatori; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    onTheBoardPieces[j][i][0].Size = new Size(marimePiese, marimePiese);
                }
            }
            for (int i = 0; i < nrJucatori; i++)
            {
                boards[i].Size = new Size((int)(boardToTableRatio * this.Width), (int)(boardToTableRatio * this.Width));
                rememberMove[i] = boards[i].Location;
            }
            Console.WriteLine(this.Width + " " + this.Height);
            setari.Location = new Point(20, this.Size.Height - setari.Size.Height - 50);
            for (int i = 0; i < 5; i++)
            {
                coordonateLinii[i] = boards[jucatorCurent].Location.Y + 1 * boards[jucatorCurent].Height / 3 + 5 + i * (marimePiese + 2);
            }
            coordonateLiniaPodelei = boards[jucatorCurent].Location.Y + boards[jucatorCurent].Height - 15 - marimePiese;
            
            
                    /*Console.WriteLine((int)(this.Width / boardToTableRatio));
                    Console.WriteLine(boardToTableRatio);
                    Console.WriteLine(this.Width);
                    boards[0].Size = new Size((int)(this.Width * boardToTableRatio), (int)(this.Width * boardToTableRatio));
                    boards[0].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height - 60);
            }*/
        }

        private void Modify2Players()
        {
            boards[jucatorCurent].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height);
            boards[(jucatorCurent + 1) % 2].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, 0);

        }

        private void Modify3Players()
        {
            boards[jucatorCurent].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height);
            boards[(jucatorCurent + 1) % 3].Location = new Point(this.Width - 4 * boards[0].Width / 3, 2 * boards[0].Width / 3);
            boards[(jucatorCurent + 2) % 3].Location = new Point(boards[0].Width / 3, 2 * boards[0].Width / 3);

        }

        private void Modify4Players()
        {
            boards[jucatorCurent].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, this.Size.Height - boards[0].Height);
            boards[(jucatorCurent + 1) % 4].Location = new Point(this.Size.Width - boards[0].Width, this.Width / 2 - boards[0].Width / 2);
            boards[(jucatorCurent + 2) % 4].Location = new Point(this.Size.Width / 2 - boards[0].Width / 2, 0);
            boards[(jucatorCurent + 3) % 4].Location = new Point(0, this.Size.Width / 2 - boards[0].Width / 2);

        }

        private void Setari_Click(object sender, EventArgs e)
        {
            if (final)
            {
                this.Controls.Remove(congratulations);
                this.Controls.Remove(fireworks);
                this.Controls.Remove(scorFinal);
                this.Controls.Remove(castigator);
            }
            else
            {
                ClearEverything();
                int nr = 0;
                for (int i = 0; i < nrJucatori * 2 + 1; i++)
                {
                    nr = factoriesPieces[i].Count;
                    for (int j = 0; j < nr; j++)
                    {
                        this.Controls.Remove(factoriesPieces[i][j]);

                    }
                }
                nr = middleOfTheFactories.Count;
                for (int i = 0; i < nr; i++)
                {
                    this.Controls.Remove(middleOfTheFactories[i]);
                }
            }
            meniuSetari.Size = new Size(300, 200);
            meniuSetari.Location = new Point(this.Width / 2 - meniuSetari.Width / 2, this.Height / 2 - meniuSetari.Height / 2);
            meniuSetari.BackColor = Color.Transparent;
            meniuSetari.Anchor = AnchorStyles.None;
           // meniuSetari.Anchor = AnchorStyles.Top + Bottom + Left + Right;
            this.Controls.Add(meniuSetari);
            if(!final)
            {
                cancel.Size = new Size(125, 70);
                cancel.Location = new Point(meniuSetari.Width / 2 - cancel.Width / 2, meniuSetari.Height / 2 + 5);
                cancel.BackColor = Color.AntiqueWhite;
                cancel.ForeColor = Color.Black;
                cancel.Anchor = AnchorStyles.None;
                //cancel.Anchor = AnchorStyles.Top + Bottom + Left + Right;
                cancel.Text = "Revenire";
                this.cancel.Click += new System.EventHandler(this.Cancel_Click);
                meniuSetari.Controls.Add(cancel);
            }

            meniu.Size = new Size(125, 70);
            meniu.Location = new Point(5, meniuSetari.Height / 2 - 5 - meniu.Height);
            meniu.BackColor = Color.AntiqueWhite;
            meniu.ForeColor = Color.Black;
            meniu.Anchor = AnchorStyles.None;
            //meniu.Anchor = AnchorStyles.Top + Bottom + Left + Right;
            meniu.Text = "Meniu";
            this.meniu.Click += new System.EventHandler(this.Meniu_Click);
            meniuSetari.Controls.Add(meniu);
            restart.Size = new Size(125, 70);
            restart.Location = new Point(meniuSetari.Width - 5 - restart.Width, meniuSetari.Height / 2 - 5 - meniu.Height);
            restart.BackColor = Color.AntiqueWhite;
            restart.Anchor = AnchorStyles.None;
            //restart.Anchor = AnchorStyles.Top + Bottom + Left + Right;
            restart.ForeColor = Color.Black;
            restart.Text = "Restart";
            this.restart.Click += new System.EventHandler(this.Restart_Click);
            meniuSetari.Controls.Add(restart);
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            this.Close();
            Form joc = new FormGame(nrJucatori, numeJucatori);
            joc.Show();

        }

        private void Meniu_Click(object sender, EventArgs e)
        {
            this.Close();
            Form meniuStart = new FormMeniu();
            meniuStart.Show();
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(meniuSetari);
            for (int j = 0; j < nrJucatori; j++)
            {
                this.Controls.Add(boards[j]);
            }
            for (int j = 0; j < 2 * nrJucatori + 1; j++)
            {
                this.Controls.Add(factories[j]);
            }
            this.Controls.Add(piesaDeMarcaj);
            int nr = 0;
            for (int i = 0; i < nrJucatori * 2 + 1; i++)
            {
                nr = factoriesPieces[i].Count;
                for (int j = 0; j < nr; j++)
                {
                    this.Controls.Add(factoriesPieces[i][j]);
                    factoriesPieces[i][j].BringToFront();

                }
            }
            nr = middleOfTheFactories.Count;
            for (int i = 0; i < nr; i++)
            {
                this.Controls.Add(middleOfTheFactories[i]);
                middleOfTheFactories[i].BringToFront();
            }
        }
    }
}
