using chess_divided_windows;

namespace DSChessApp.model
{
    public class Knight : ChessPiece
    {
        public override Knight getCopy()
        {
            return new Knight(this.isWhite);
        }

        public Knight(bool isWight) : base()
        {
            this.isWhite = isWight;
        }
        public override string ToString()
        {
            return isWhite ? "WN" : "BN";
        }

        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return KnightMovement(currentX, currentY, targetX, targetY, board);
        }

        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            if (!base.IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game))
                return false;            

            return KnightMovement (currentX, currentY, targetX, targetY, board);
        }

        public bool KnightMovement(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board)
        {
            int Xdelta = currentX - targetX;
            int Ydelta = currentY - targetY;

            if (Xdelta * Xdelta + Ydelta * Ydelta != 5)
                return false;

            return true;
        }
    }
}
