using chess_divided_windows;

namespace DSChessApp.model
{
    public class Rook : ChessPiece
    {
        public override Rook getCopy()
        {
            return new Rook(this.isWhite);
        }        
        public Rook(bool isWhite)
        {
            this.isWhite = isWhite;
        }
        public override string ToString()
        {
            return isWhite ? "WR" : "BR";
        }

        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return RookMovement(currentX, currentY, targetX, targetY, board, false);
        }

        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            if (!base.IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game))  //בדיקת הלוגיקה הכללית של פונקציית האב 
                return false;

            return RookMovement(currentX, currentY, targetX, targetY, board, withMessage);
        }
        public bool RookMovement(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage)
        {
            if (currentX != targetX && currentY != targetY) // אם התנועה לא אותה שורה וגם לא באותה הטור - לא חוקי
                return false;

            // קביעת התנועה בציר האיקס
            int stepX = 0;
            if (currentX != targetX)
                stepX = (currentX < targetX) ? 1 : -1;
            
            //קביעת התנועה בצור הוואי
            int stepY = 0;
            if (currentY != targetY)
                stepY = (currentY < targetY) ? 1 : -1;
            
            
            int X = currentX + stepX;
            int Y = currentY + stepY;

            while (Y != targetY || X != targetX) //רץ על כל המשבצעות הריקות בכיוון התנועה, ובודק שהן ריקות
                {
                    if (board[X, Y] != null) // אם יש כלי בדרך אז הלולאה נעצרת מיד, התנועה לא חוקית
                    {
                        return false;
                    }
                    Y += stepY;
                    X += stepX;
            }          
            return true;
        }
    }    
}
