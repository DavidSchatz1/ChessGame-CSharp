using chess_divided_windows;

namespace DSChessApp.model
{
    public class ChessPiece
    {
        public bool isWhite;
        public virtual ChessPiece getCopy()
        {
            return new ChessPiece(this.isWhite);
        }
        public ChessPiece(bool isWhite)
        {
            this.isWhite = isWhite;
        }
        public ChessPiece() { }
        public virtual bool isThreatening (int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return false;
        }
        public virtual bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            if (currentX > 7 || currentX < 0 || currentY > 7 || currentY < 0 || targetX > 7 || targetX < 0 || targetY > 7 || targetY < 0)
            {
                if (withMessage)
                    Console.WriteLine("invalid input. origin or destanation out of board"); //ניגש למשבצת מחוץ ללוח
                return false;
            }
            
            if (board[currentX, currentY] == null) // אם ניגש למשבצת שאין בה כלום
            {
                if (withMessage)
                    Console.WriteLine("there is no peice in the starting point of the move");
                return false;
            }           

            if (currentX == targetX && currentY == targetY)    // אם הוא לא זז לשום מקום - לא חוקי
            {
                if (withMessage)
                    Console.WriteLine("you didnt move anywhere");
                return false;
            }

            if (board[targetX, targetY] != null)
            {
                if (board[currentX, currentY].isWhite == board[targetX, targetY].isWhite)
                {
                    if (withMessage)
                        Console.WriteLine("that spot is taken by a freindly peice");
                    return false;
                }
            }
            return true;
        }
    }
}
