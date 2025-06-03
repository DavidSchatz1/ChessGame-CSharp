using chess_divided_windows;

namespace DSChessApp.model
{
    public class Queen : ChessPiece
    {
        public override Queen getCopy()
        {
            return new Queen(isWhite);
        }
        public Queen(bool isWhite)
        {
            this.isWhite = isWhite;
        }
        public override string ToString()
        {
            return isWhite ? "WQ" : "BQ";
        }
        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return QueenMovement(currentX, currentY, targetX, targetY, board, game);
        }

        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            // בדיקה כללית של פונקציית האב
            if (!base.IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game))
                return false;
            return QueenMovement(currentX, currentY, targetX, targetY, board, game);
        }

        public bool QueenMovement(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            // יצירת אובייקטים זמניים של רץ וצריח לשימוש חוזר בלוגיקה שלהם
            Bishop bishop = new Bishop(isWhite);
            Rook rook = new Rook(isWhite);

            // בדיקה אם התנועה חוקית כרץ או כצריח
            if (bishop.IsLegalMove(currentX, currentY, targetX, targetY, board, false, game) ||
                rook.IsLegalMove(currentX, currentY, targetX, targetY, board, false, game))
            {
                return true;
            }
            return false;
        }
    }
}
