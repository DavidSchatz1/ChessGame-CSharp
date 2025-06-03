using chess_divided_windows;

namespace DSChessApp.model
{
    public class Bishop : ChessPiece
    {
        public override Bishop getCopy()
        {
            return new Bishop(this.isWhite);
        }
        
        public Bishop(bool isWight)
        {
            this.isWhite = (isWight);
        }
        public override string ToString()
        {
            return isWhite ? "WB" : "BB";
        }

        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return BishopMovement(currentX, currentY, targetX, targetY, board);
        }

        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            if (!base.IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game))
                return false;
            return BishopMovement(currentX, currentY, targetX, targetY, board);
        }
        public bool BishopMovement(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board)
        {
            // בדיקה אם התנועה היא באלכסון
            if (Math.Abs(currentX - targetX) != Math.Abs(currentY - targetY))
            {
                return false;
            }

            // קביעת כיוון התנועה
            int stepX = (targetX > currentX) ? 1 : -1;
            int stepY = (targetY > currentY) ? 1 : -1;

            // בדיקת המשבצות לאורך הדרך
            int x = currentX + stepX;
            int y = currentY + stepY;
            while (x != targetX && y != targetY)
            {
                if (board[x, y] != null) // אם יש כלי בדרך
                {
                    return false;
                }
                x += stepX;
                y += stepY;
            }
            return true;
        }
    }
}
