using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HenryWoods_ConnectFour
{
    public partial class frmConnectFour : Form
    {
        Cell[,] CellArray = new Cell[7, 6];
        Boolean PlayerOnesTurn = true;

        public frmConnectFour()
        {
            InitializeComponent();
        }

        // When the user click the screen, The system should get their coordinates and
        // Check them against the CellArray.
        private void ScreenClick(object sender, MouseEventArgs e)
        {
            String X = e.X.ToString();
            String Y = e.Y.ToString();
            Console.WriteLine("THE COORDS ARE: " + X + " , " + Y);
            //MessageBox.Show("THE COORDS ARE: " + X + " , " + Y);
            // Check that the player clicked on the connect four game area.
            if (e.X < 700 && e.Y < 600)
            {
                //MessageBox.Show("INSIDE");
                int Coord = 100;
                int Column = 0;
                int Row = 0;
                // Get the Column Index
                for (int i = 0; i != 7; i++)
                {
                    if(e.X < Coord)
                    {
                        Column = i;
                        break;
                    }
                    else { Coord = Coord + 100; }
                }
                // Get the Row index
                Coord = 100;
                for (int i = 0; i != 6; i++)
                {
                    if (e.Y < Coord)
                    {
                        Row = i;
                        break;
                    }
                    else { Coord = Coord + 100; }
                }

                // Search the column for an empty cell, If Empty cell is found, Row will be re-assigned.
                bool ColumnHasAFreeSpace = false;
                for(int i=5; i > -1;i = i -1)
                {
                    if (CellArray[Column, i].getPlayerOccupied().Equals(0))
                    {
                        ColumnHasAFreeSpace = true;
                        Row = i;
                        break;
                    }
                }

                if (ColumnHasAFreeSpace == true)
                {
                    //MessageBox.Show("" + CellArray.GetValue(Column,Row));
                    Graphics g = CreateGraphics();
                    //Creating 2 separate colours for each player
                    SolidBrush PlayerColor;
                    SolidBrush Red = new SolidBrush(Color.Red);
                    SolidBrush Blue = new SolidBrush(Color.Blue);
                    if (PlayerOnesTurn == true) { PlayerColor = Red; } else { PlayerColor = Blue; }

                    //Draws circle representing the players move
                    g.FillEllipse(PlayerColor, new Rectangle(CellArray[Column, Row].getCoordX() - 94, CellArray[Column, Row].getCoordY() - 95, 88, 88));


                    // Set the occupied space to the designated player
                    if (PlayerOnesTurn == true) { CellArray[Column, Row].setPlayerOccupied(1); CheckWinner(1); } else { CellArray[Column, Row].setPlayerOccupied(2); CheckWinner(2); }

                    //Changes the players turn
                    if (PlayerOnesTurn == true) { PlayerOnesTurn = false; } else { PlayerOnesTurn = true; }
                } else { MessageBox.Show("Invalid Selection: Please choose again."); }

                

            }
            else
            {
                MessageBox.Show("OUTSIDE");
            }

        }
        
        public bool CheckWinner(byte Player)     
        {
            int WinCounter = 0;

            //Checks for winner Vertically 
            for (int x = 0; x != 7; x++)
            {
                WinCounter = 0;
                for (int y = 0; y != 6; y++)
                {
                    if (CellArray[x, y].getPlayerOccupied().Equals(Player))
                    {
                        WinCounter += 1;
                        if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); }
                    }
                    else if(! CellArray[x, y].getPlayerOccupied().Equals(Player)){ WinCounter = 0; }
                }
                WinCounter = 0;
            }
            //CHECK HORIZONTAL
            for (int x = 0; x != 6; x++)
            {
                WinCounter = 0;
                for (int y = 0; y != 7; y++)
                {
                    if (CellArray[y, x].getPlayerOccupied().Equals(Player))
                    {
                        WinCounter += 1;
                        if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); }
                    }
                    else if (!CellArray[y, x].getPlayerOccupied().Equals(Player)) { WinCounter = 0; }
                }
                WinCounter = 0;
            }


            // Detects Diagonal down right win
            // The first two loops will just make sure you check every cell
            // The third loop is the one that checks for the diagonal win
            int Row;
            WinCounter = 0;
            for (int StartingRow = 0; StartingRow != 6; StartingRow++)
            {
                Row = StartingRow;
                for (int StartingColumn = 0; StartingColumn != 7; StartingColumn++)
                {
                 
                    for (int Column = StartingColumn; Column != 7;)
                    {
                        if (CellArray[Column, Row].getPlayerOccupied().Equals(Player))
                        {
                            WinCounter += 1;
                            if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); }
                        }
                        else { WinCounter = 0; }
                        if (Column == 6 || Row == 5) { WinCounter = 0; break; } else { Row += 1;Column += 1; }
                    }
                    WinCounter = 0;
                    Row = StartingRow;
                    if (WinCounter == 4) { break; }
                }
                if (WinCounter == 4) { break; }
            }

            // Detects Diagonal Left win
            // Its a little modification of the previous diagonal loop     
            WinCounter = 0;
            for (int StartingRow = 0; StartingRow != 6; StartingRow++)
            {
                Row = StartingRow;
                for (int StartingColumn = 0; StartingColumn != 7; StartingColumn++)
                {

                    for (int Column = StartingColumn; Column != 7;)
                    {
                        if (CellArray[Column, Row].getPlayerOccupied().Equals(Player))
                        {
                            WinCounter += 1;
                            if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); }
                        }
                        else { WinCounter = 0; }
                        if (Column == 0 || Row == 5) { WinCounter = 0; break; } else { Row += 1; Column -= 1; }
                    }
                    WinCounter = 0;
                    Row = StartingRow;
                    if (WinCounter == 4) { break; }
                }
                if (WinCounter == 4) { break; }
            }



            return true;

            
        }

        private void frmConnectFour_Load(object sender, EventArgs e)
        {
            //initializing the starting cell position
            int RowCoord = 100;
            int ColumnCoord = 100;

            for (int y = 0; y != 7; y++)
            {
                for (int x = 0; x != 6; x++)
                {
                    CellArray[y, x] = new Cell(y, x, RowCoord, ColumnCoord);
                    //Adding a new Cell object at column,row position
                    //CellArray.Add(new Cell(y,x,RowCoord,ColumnCoord));
                    RowCoord = RowCoord + 100;
                    //System.Diagnostics.Debug.WriteLine(CellArray.IndexOf(2));

                }
                //incrementing the column coord
                ColumnCoord += 100;
                // Reset Row coord
                RowCoord = 100;
            }
            //Prints each objects ToString method to the console

            for (int y = 0; y != 7; y++)
            {
                for (int x = 0; x != 6; x++)
                {
                    System.Diagnostics.Debug.WriteLine(CellArray[y, x]);
                }

            }





        }


        //This method creates the game board by using the form as a canvas and painting rectangles directly to it.
        private void frmConnectFour_Paint(object sender, PaintEventArgs e)
        {
            int RowCoord = 0;
            int ColumnCoord = 0;

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    System.Drawing.Brush myBrush = new SolidBrush(Color.Black);
                    System.Drawing.Pen myPen = new System.Drawing.Pen(myBrush, 10);
                    System.Drawing.Graphics formGraphics;
                    formGraphics = this.CreateGraphics();
                    formGraphics.DrawRectangle(myPen, new Rectangle(RowCoord, ColumnCoord, 100, 100));
                    myPen.Dispose();
                    formGraphics.Dispose();
                    RowCoord = RowCoord + 100;


                }
                ColumnCoord = ColumnCoord + 100;
                RowCoord = 0;
            }
        }
    }
}
            
            
        
    


