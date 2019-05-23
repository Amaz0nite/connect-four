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
    /// <summary>
    /// This form is the view and displays the game to the player and takes the user's input and sends it to the logic layer.
    /// </summary>
    public partial class frmConnectFour : Form
    {
        
        enum OccupiedBy
        {
            Unoccupied = 0,
            PlayerOne = 1,
            PlayerTwo = 2

        }

        
        // GameSpace is an int array that holds the value of the OccupiedBy enum.
        int[,] GameSpace = new int[7,6];
        int BrushWidth = 5;
        Boolean PlayerOnesTurn = true;
        GameLogic gameLogic = new GameLogic();
        //Creating 2 separate colours for each player
        SolidBrush PlayerColor;
        SolidBrush Red = new SolidBrush(Color.Red);
        SolidBrush Blue = new SolidBrush(Color.Blue);

        public frmConnectFour()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the player clicks on the screen this method is called.
        /// </summary>
        /// <param name="sender">The player's mouse click</param>
        /// <param name="e">MouseEventArgs</param>
        private void ScreenClick(object sender, MouseEventArgs e)
        {
            // Check that the player clicked on the connect four game area.
            if (e.X < 700 && e.Y < 600)
            {                           
                int Column = (int)e.X / 100;
                int Row = 0;
              
                if (gameLogic.CheckColumnForAvailableSpace(Column,GameSpace))
                {
                    Row = gameLogic.ReturnRowNumber(Column, GameSpace);                                            
                    if (PlayerOnesTurn == true) { PlayerColor = Red; } else { PlayerColor = Blue; }
                    //Draws circle representing the players move
                    DrawCircle(Column, Row);               
                    // Set the occupied space to the designated player
                    if (PlayerOnesTurn == true) { GameSpace[Column,Row] = (int)OccupiedBy.PlayerOne ; gameLogic.CheckWinner(1, GameSpace); } else { GameSpace[Column, Row] = (int)OccupiedBy.PlayerTwo; gameLogic.CheckWinner(2, GameSpace); }                   
                    //Changes the players turn
                    if (PlayerOnesTurn == true) { PlayerOnesTurn = false; } else { PlayerOnesTurn = true; }
                } else { MessageBox.Show("Invalid Selection: Please choose again."); }      
            }
            else
            {
                MessageBox.Show("OUTSIDE");
            }
        }

        /// <summary>
        /// This method creates the game board by using the form as a canvas and painting rectangles directly to it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                   // System.Drawing.s formGraphics;
                  Graphics formGraphics = e.Graphics;
                    formGraphics.DrawRectangle(myPen, new Rectangle(RowCoord, ColumnCoord, 100, 100));
                   
                    RowCoord = RowCoord + 100;

                }
                ColumnCoord = ColumnCoord + 100;
                RowCoord = 0;
            }

            ReDrawGame();
        }

        /// <summary>
        /// This Method redraws all the game pieces so that when the window is resized or minimized the game pieces don't disappear.
        /// </summary>
        public void ReDrawGame()
        {
            Graphics g = this.CreateGraphics();
            for (int y = 0; y != 7; y++)
            {
                for (int x = 0; x != 6; x++)
                {
                    if (GameSpace[y, x].Equals((int)OccupiedBy.PlayerOne))
                    {
                        g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(y * 100 + 5, x * 100 + 5, 88, 88));
                    }
                    else if (GameSpace[y, x].Equals((int)OccupiedBy.PlayerTwo))
                    {
                        g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(y * 100 + 5, x * 100 + 5, 88, 88));
                    }
                }

            }

        }
        /// <summary>
        /// Draws a circle onto the form.
        /// </summary>
        /// <param name="Column">Represents the X Coordinate</param>
        /// <param name="Row">Represents the Y Coordinate</param>
        public void DrawCircle(int Column, int Row)
        {
            Graphics g = this.CreateGraphics();
            g.FillEllipse(PlayerColor, new Rectangle(Column * 100 + BrushWidth, Row * 100 + BrushWidth, 88, 88));
        }
    }
}
            
            
        
    


