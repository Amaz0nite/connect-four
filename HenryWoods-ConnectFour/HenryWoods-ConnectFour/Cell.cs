using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class represents a space in the Connect4 game.
// It will have the location in the array, location on form and which player occupies the space.
// PlayerOccupied: 0= Nobody 1= Player1 2= Player2
namespace HenryWoods_ConnectFour
{
    class Cell
    {
        
            private int ColumnIndex;
            private int RowIndex;
            private int ColumnCoord;
            private int RowCoord;
            byte PlayerOccupied = 0;

            public Cell(int ColumnIndex, int RowIndex, int RowCoord, int ColumnCoord)
            {
                this.ColumnIndex = ColumnIndex;
                this.RowIndex = RowIndex;
                this.RowCoord = RowCoord;
                this.ColumnCoord = ColumnCoord;
            }

            public override string ToString()
            {
                return ("ColumnIndex:" + this.ColumnIndex + " RowIndex:" +
                      this.RowIndex + " X Coord: " +
                      this.ColumnCoord + " Y Coord: " + this.RowCoord);
            }

        public int getCoordX()
        {
            return this.ColumnCoord;
        }
        public int getCoordY()
        {
            return this.RowCoord;
        }
        public void setPlayerOccupied(byte Player)
        {
            this.PlayerOccupied = Player;
        }
        public byte getPlayerOccupied()
        {
            return this.PlayerOccupied;
        }
    }
    }


