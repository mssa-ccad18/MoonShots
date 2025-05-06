public class TicTacToe
{
    public enum Piece
    {
        X,
        O,
        Empty
    }

    public enum GameState
    {
        XWin,
        OWin,
        Draw,
        InProgress
    }

    public delegate void PlayerMoveEventHanlder(Player player, int row, int col);

    public class Player
    {
        public Piece PlayerPiece { get; }

        public event PlayerMoveEventHanlder OnPlayerMove;

        public Player(Piece piece)
        {
            PlayerPiece = piece;
        }

        public void MakeMove(int row, int col)
        {
            // Trigger the event when the player makes a move
            OnPlayerMove?.Invoke(this, row, col);
        }
    }

    //Created a 9 grid board
    private Piece[,] board = new Piece[3, 3];

    // initializes the game board and makes each square empty
    private void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = Piece.Empty; // Initialize all cells as empty
            }
        }
    }

    // checks if the game is over
    private GameState CheckGameState()
    {
        // Check rows and columns for a win
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != Piece.Empty)
            {
                return board[i, 0] == Piece.X ? GameState.XWin : GameState.OWin;
            }
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != Piece.Empty)
            {
                return board[0, i] == Piece.X ? GameState.XWin : GameState.OWin;
            }
        }
    }
}
