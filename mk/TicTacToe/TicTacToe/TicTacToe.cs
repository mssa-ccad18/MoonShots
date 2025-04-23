using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    //Game Class
    public class TicTacToe
    {
        /// 1 enum for x, o, and empty field
        public enum Piece
        {
            X,
            O,
            Empty
        }
        /// enum for results
        public enum GameState
        {
            XWin,
            OWin,
            Draw,
            Inprogress
        }

        public delegate void PlayerMoveEventHandler(Player player, int row, int col);

        public class Player
        {
            public Piece PlayerPiece { get;  }

            public event PlayerMoveEventHandler OnPlayerMove;

            public Player(Piece piece)
            {
                PlayerPiece = piece;
            }

            public void MakeMove(int row, int col)
            {
                //Trigger the event when the player make a move
                OnPlayerMove?.Invoke(this, row, col);
            }
        }

        //Creates board grid
        private Piece[,] board = new Piece[3, 3];

        //initializes board
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = Piece.Empty; //Initializes all cells to empty.
                }

            }
        }

    }
}
