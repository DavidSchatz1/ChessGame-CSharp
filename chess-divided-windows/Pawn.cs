using chess_divided_windows;

namespace DSChessApp.model
{
    public class Pawn : ChessPiece
    {
        public override Pawn getCopy()
        {
            return new Pawn(this.isWhite);
        }
        
        public Pawn(bool isWight) : base()
        {
            this.isWhite = isWight;
        }

        public override string ToString()
        {
            return isWhite ? "WP" : "BP";
        }

        public override bool isThreatening(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, ChessGame game)
        {
            int PawnDirection = board[currentX, currentY].isWhite ? -1 : 1;

            if (currentX == targetX + PawnDirection && (currentY == targetY + 1 || currentY == targetY - 1)) //החיילים איימו לכיוון הלא נכון אז שיניתי מפלוס למינוס, וגם שתי שורות למטה להיפך. לוודא שעובד
                return true;

            return false;
        }
        public override bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, ChessPiece[,] board, bool withMessage, ChessGame game)
        {
            if (!base.IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game))
                return false;

            if (currentX == targetX)
                return false;
            if (Math.Abs(currentY - targetY) > 1)
                return false;

            if (board[targetX, targetY] != null && currentY == targetY) // לעולם לא אוכלים בהליכה קדימה לא משנה צבע החייל
                return false;

            if (board[currentX, currentY].isWhite == true)// תנועה עבור חייל לבן
            {
                if (currentX == 1) //אם נמצא בשורה הראשונה ורוצה לנוע קדימה
                {
                    if (targetX != 2 && targetX != 3)
                        return false;
                }
                if (currentX != 1)
                {
                    if (targetX != currentX + 1)
                        return false;
                }

                //דרך הילוכו עבור חייל לבן
                if (targetX == game.EnnPasatXCoordinate && targetY == game.EnnPasatYCoordinate)
                {
                    if (currentX == 4 && (currentY == targetY + 1 || currentY == targetY - 1))
                    {
                        game.wasWhitePasatDemonstarted = true;
                        return true;           //אם דרך הילוכו מתקיים, אפשר לעצור, ולא לבדוק תנאים של אכילה
                    }

                }
                if (currentY != targetY && board[targetX, targetY] != null) //בדיקה שאין הליכה באלכסון למשבצת ריקה. קורה דווקא אחרי בדיקת דרך הילוכו
                    if (!((currentY == targetY + 1) || (currentY == targetY - 1) && board[targetX, targetY].isWhite == false))
                        return false;
                if (currentY != targetY && board[targetX, targetY] == null)
                    return false;

            }
            if (board[currentX, currentY].isWhite == false) // תנועה עבור חייל שחור
            {
                if (currentX == 6) //אם נמצא בשורה הראשונה ורוצה לנוע קדימה
                {
                    if (targetX != 4 && targetX != 5)
                        return false;
                }
                if (currentX != 6)
                {
                    if (targetX != currentX - 1)
                        return false;
                }

                if (targetX == game.EnnPasatXCoordinate && targetY == game.EnnPasatYCoordinate) //אם יש אפשרות לבצע דרך הילוכו - כאן הוא יוגדר כחוקי
                {
                    if (currentX == 3 && (currentY == targetY + 1 || currentY == targetY - 1))
                    {
                        game.wasBlackPasatDemonstrated = true;
                        return true;
                    }

                }
                if (currentY != targetY && board[targetX, targetY] != null) //בדיקה שאין הליכה באלכסון למשבצת ריקה. קורה דווקא אחרי בדיקת דרך הילוכו
                    if (!((currentY == targetY + 1) || (currentY == targetY - 1) && board[targetX, targetY].isWhite == true))
                        return false;
                if (currentY != targetY && board[targetX, targetY] == null)
                    return false;
            }

            return true;
        }
    }
}
