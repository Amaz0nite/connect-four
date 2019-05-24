using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HenryWoods_ConnectFour
{
    /// <summary>
    /// This class is the logic layer and will handle all of the logic of the game.
    /// </summary>
    class GameLogic
    {
        /// <summary>
        /// Checks the column to see whether or not there is a space available
        /// </summary>
        /// <param name="Column">Required to know which column to check</param>
        /// <param name="GameSpace">Represents the spaces and is required to check against</param>
        /// <returns>Returns a boolean if the row has a free space available</returns>
        public bool CheckColumnForAvailableSpace(int Column,int[,] GameSpace)
        {
            // Search the column for an empty cell, Returns Boolean
            bool FreeSpace = false;
            int Unoccupied = 0;
            for (int i = 5; i > -1; i = i - 1)
            {
                if (GameSpace[Column, i].Equals(Unoccupied))
                {
                    FreeSpace = true;
                    break;
                }
            }
            return FreeSpace;
        }

        
        /// <summary>
        /// Checks the row for an available space and returns an integer representing the row
        /// </summary>
        /// <param name="Column">Required to know which column to check</param>
        /// <param name="GameSpace">Represents the spaces and is required to check against</param>
        /// <returns>Returns an integer that represents a row in the connect four game</returns>
        public int ReturnRowNumber(int Column, int[,] GameSpace)
        {    
            int Unoccupied = 0;
            for (int i = 5; i > -1; i = i - 1)
            {
                if (GameSpace[Column, i].Equals(Unoccupied))
                {
                    return i;  
                }
            }
            return 0;
        }

        /// <summary>
        /// Goes through a series of checking algorithms to check if the player has won connect four
        /// </summary>
        /// <param name="Player">A byte number that represents either player one or two</param>
        /// <param name="GameSpace">Int Array that holds data of all the spaces and who they are occupied by</param>
        /// <returns>Returns a boolean if the player has won or not</returns>
        public bool CheckWinner(byte Player, int[,] GameSpace)
        {
            int WinCounter = 0;

            //Checks for winner Vertically 
            for (int x = 0; x != 7; x++)
            {
                WinCounter = 0;
                for (int y = 0; y != 6; y++)
                {
                    if (GameSpace[x, y].Equals(Player))
                    {
                        WinCounter += 1;
                        if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS");break; }
                    }
                    else if (!GameSpace[x, y].Equals(Player)) { WinCounter = 0; }
                }
                WinCounter = 0;
            }
            //CHECK HORIZONTAL
            for (int x = 0; x != 6; x++)
            {
                WinCounter = 0;
                for (int y = 0; y != 7; y++)
                {
                    if (GameSpace[y, x].Equals(Player))
                    {
                        WinCounter += 1;
                        if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS");break; }
                    }
                    else if (!GameSpace[y, x].Equals(Player)) { WinCounter = 0; }
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
                        if (GameSpace[Column, Row].Equals(Player))
                        {
                            WinCounter += 1;
                            if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); break; }
                        }
                        else { WinCounter = 0; }
                        if (Column == 6 || Row == 5) { WinCounter = 0; break; } else { Row += 1; Column += 1; }
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
                        if (GameSpace[Column, Row].Equals(Player))
                        {
                            WinCounter += 1;
                            if (WinCounter == 4) { MessageBox.Show("Player" + Player + " WINS"); break; }
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



            return true; //*************** NEED TO IMPLEMENT THIS ******************


        }
    }
}
