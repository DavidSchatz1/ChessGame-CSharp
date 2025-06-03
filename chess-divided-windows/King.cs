using chess_divided_windows;

namespace DSChessApp.model
{
    public class King : ChessPiece
    {
        public override King getCopy()
        {
            return new King(isWhite);
        }
        public King GetKing() { return this; }

        public King(bool isWight)
        {
            this.isWhite = isWight;
        }
        public override string ToString()
        {
            return isWhite ? "WK" : "BK";
        }
                
        public bool isCastlingLegal (int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            //>תנאים לביצוע הצרחה: לא בשח. לא זז מעולם (פלאג). המשבצות באמצע ריקות. משבצות שהמלך עובר בהן לא מאוימות.
            
            int castlingRow = board[currentX, currentY].isWhite ? 0 : 7; //קביעת צבע ההצרחה
            bool isWhiteCastling = board[currentX, currentY].isWhite ? true : false;

            if (currentX == castlingRow && currentY == 4)
            {
                if (ChessGame.isSpotAimed(castlingRow, 4, board, isWhiteCastling, game) == true) //בודק האם המלך שרוצה להצריח מאוים
                    return false;
                //לימין
                if (targetX == castlingRow && targetY == 6) //משבצת יעד נכונה
                {
                    if (castlingRow == 0 ? game.castlingWhiteRightPossible == true : game.castlingBlackRightPossible == true) //הפלאג שמשקף האם אי פעם הכלי זז - דלוק כראוי
                    {
                        if (board[castlingRow,5] == null && board[castlingRow, 6] == null)   //המשבצות באמצע ריקות
                        {
                            if (ChessGame.isSpotAimed(castlingRow, 5, board, isWhiteCastling, game) == false && ChessGame.isSpotAimed(castlingRow, 6, board, isWhiteCastling, game) == false) //המשבצות לא מאוימות
                                return true;
                        }
                    }
                }
                //לשמאל
                if (targetX == castlingRow && targetY == 2)
                {
                    if (castlingRow == 0 ? game.castlingWhiteLeftPossible == true : game.castlingBlackLeftPossible == true)
                    {
                        if (board[castlingRow, 3] == null && board[castlingRow, 2] == null && board[castlingRow, 1] == null)
                        {
                            if (ChessGame.isSpotAimed(castlingRow, 3, board, isWhiteCastling, game) == false && ChessGame.isSpotAimed(castlingRow, 2, board, isWhiteCastling, game) == false && ChessGame.isSpotAimed(castlingRow, 1, board, isWhiteCastling, game) == false) //המשבצות לא מאוימות
                                return true;
                        }
                    }
                }
            }
            
            return false;
        }

        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game) 
        {
            if (KingMovement(currentX, currentY, targetX, targetY, board) == false)
                if (isCastlingLegal(currentX, currentY, targetX, targetY, board, game) == false)
                    return false;

            return true;
        }

        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            return KingMovement(currentX, currentY, targetX, targetY, board);
        } 
        public bool KingMovement (int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board)
        {
            int Xdelta = currentX - targetX;
            int Ydelta = currentY - targetY;
            if (Math.Abs(Xdelta) > 1 || Math.Abs(Ydelta) > 1)
                return false;

            return true;
        }
    }
}
