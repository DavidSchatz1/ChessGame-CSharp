using DSChessApp;
using DSChessApp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_divided_windows
{
    public class ChessGame
    {
        public ChessGame() { }
        public void playTheGame ()
        {            
            ChessPiece[,] board = createBoard();
            SetDefaultBoard(board);
            printBoard(board);

            //אם רוצים לבצע בדיקה על לוח שאיננו הלוח ההתחלתי הרגיל, מפעילים את החלק קוד הזה ומבטלים את יצירת הלוח הדיפולטיבית. כדי להשפיע על תוכן הלוח יש לשנות את  התוכן בפונקציה NONDEFAULT
            //SetNonDefaultBoard(board);
            //printBoard(board);

            string[] saveBoards = new string[300];

            while (isGameOver == false)
            {
                demonstrateATurn(board, saveBoards, this);
            }
        }
        public bool isGameOver = false; //משתנה שישלוט בעצירת המשחק

        public int EnnPasatXCoordinate = -1;  //אתחול המשתנים שיאפשרו דרך הילוכו

        public int EnnPasatYCoordinate = -1;

        public bool wasWhitePasatDemonstarted = false;

        public bool wasBlackPasatDemonstrated = false;

        public int movesWithNoProgression = 0;

        //אתחול המשתנים שיאפשרו הצרחה
        public bool castlingBlackRightPossible = true;

        public bool castlingBlackLeftPossible = true;

        public bool castlingWhiteRightPossible = true;

        public bool castlingWhiteLeftPossible = true;

        public bool Player1turn = true;
        public ChessPiece[,] copyBoard(ChessPiece[,] board)
        {
            ChessPiece[,] copyBoard = new ChessPiece[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                        copyBoard[i, j] = board[i, j].getCopy();
                }
            }
            return copyBoard;
        }
        public void SetDefaultBoard(ChessPiece[,] board)
        {
            board[0, 0] = new Rook(true);
            board[0, 1] = new Knight(true);
            board[0, 2] = new Bishop(true);
            board[0, 3] = new Queen(true);
            board[0, 4] = new King(true);
            board[0, 5] = new Bishop(true);
            board[0, 6] = new Knight(true);
            board[0, 7] = new Rook(true);
            board[1, 0] = new Pawn(true);
            board[1, 1] = new Pawn(true);
            board[1, 2] = new Pawn(true);
            board[1, 3] = new Pawn(true);
            board[1, 4] = new Pawn(true);
            board[1, 5] = new Pawn(true);
            board[1, 6] = new Pawn(true);
            board[1, 7] = new Pawn(true);

            board[7, 0] = new Rook(false);
            board[7, 1] = new Knight(false);
            board[7, 2] = new Bishop(false);
            board[7, 3] = new Queen(false);
            board[7, 4] = new King(false);
            board[7, 5] = new Bishop(false);
            board[7, 6] = new Knight(false);
            board[7, 7] = new Rook(false);
            board[6, 0] = new Pawn(false);
            board[6, 1] = new Pawn(false);
            board[6, 2] = new Pawn(false);
            board[6, 3] = new Pawn(false);
            board[6, 4] = new Pawn(false);
            board[6, 5] = new Pawn(false);
            board[6, 6] = new Pawn(false);
            board[6, 7] = new Pawn(false);
        }
        public void SetNonDefaultBoard(ChessPiece[,] board)
        {

        }
        public int[] getPlayerInput(bool isOrigin)
        {
            bool inputWorked = false;
            int[] origin = new int[2];
            if (Player1turn)
                Console.WriteLine("Whites turn! please enter the {0} spot of your move", isOrigin ? "origin" : "destenation");
            else
                Console.WriteLine("Blacks turn! please enter the {0} spot of your move", isOrigin ? "origin" : "destenation");
            string input = Console.ReadLine();
            do
            {
                string trimed = "";
                if (input != null)
                {
                    trimed = input.Trim();
                    if (trimed.Length == 2)
                        if (isInputValid(trimed[0], trimed[1]))
                        {
                            origin[1] = convertInput(trimed[0]);
                            origin[0] = int.Parse(trimed[1] + "") - 1;
                            inputWorked = true;
                            return origin;
                        }
                }
                Console.WriteLine("invalid input, try again");
                input = Console.ReadLine();
            } while (input == null || inputWorked == false);
            return null;
        }
        public static int convertInput(char input)
        {
            switch (input)
            {
                case 'A': case 'a': return 0;
                case 'B': case 'b': return 1;
                case 'C': case 'c': return 2;
                case 'D': case 'd': return 3;
                case 'E': case 'e': return 4;
                case 'F': case 'f': return 5;
                case 'G': case 'g': return 6;
                case 'H': case 'h': return 7;

            }
            return -1;
        }
        public bool isInputValid(char first, char second)
        {
            switch (first)
            {
                case 'A':
                case 'a':
                case 'B':
                case 'b':
                case 'C':
                case 'c':
                case 'D':
                case 'd':
                case 'E':
                case 'e':
                case 'F':
                case 'f':
                case 'G':
                case 'g':
                case 'H':
                case 'h':
                    break;

                default: return false;
            }
            switch (second)
            {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                    break;
                default: return false;
            }
            return true;
        }
        public ChessPiece[,] createBoard()
        {
            ChessPiece[,] board = new ChessPiece[8, 8];
            return board;
        }
        public void printBoard(ChessPiece[,] board)
        {
            Console.WriteLine();
            Console.WriteLine("   A   B   C   D   E   F   G   H ");
            for (int row = 7; row >= 0; row--)   // row
            {
                Console.Write(row + 1 + "  ");

                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == null)
                        Console.Write("-   ");
                    else
                        Console.Write(board[row, col] + "  ");
                }
                Console.Write(row + 1 + "  ");
                Console.WriteLine();

            }
            Console.WriteLine("   A   B   C   D   E   F   G   H ");
            Console.WriteLine();
            Console.WriteLine();
        }
        public bool CanMovePeicesOnBoard(int row, int col, int row2, int col2, bool withMessage, ChessPiece[,] board, ChessGame game)
        {
            if (IsLegalMove(row, col, row2, col2, withMessage, board, game))
            {
                board[row2, col2] = board[row, col];
                board[row, col] = null;
                return true;
            }
            else
            {
                if (withMessage)
                    Console.WriteLine("your move has not been executed since it's invalid");
                return false;
            }
        }
        public bool isKingCheck(ChessPiece[,] board, bool isWhite, ChessGame game) // this function will find the king, and check every oppenent piece to see if its aiming it
        {
            //finding the king
            int KingX = -1;
            int KingY = -1;
            for (int i = 0; i < 8 && KingX == -1; i++)
            {
                for (int j = 0; j < 8 && KingX == -1; j++)
                {
                    if (board[i, j] is King && board[i, j].isWhite == isWhite)
                    {
                        KingX = i;
                        KingY = j;
                    }
                }
            }
            //finding oppenent piece

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null && KingX != -1 && KingY != -1)//הוספתי כאן את התנאי שמיקומי המלך אותרו, כי באיזשהו משחק התרחש באג והמלך לא אותר כראוי, מה שגרם לחריגה מגבולות המערך וקריסה. לכאורה לעולם לא אמור להיבדק האם המלך בשח אם הוא לא אותר ככה שתוספת זו לא אמורה לגרום לשגיאה
                    {
                        if (board[i, j].isWhite != board[KingX, KingY].isWhite)
                        {
                            if (isThreatening(i, j, KingX, KingY, false, board, game))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool AreYouStillInCheck(ChessPiece[,] board, int currentX, int currentY, int targetX, int targetY, ChessGame game)
        {
            bool currentTurn = Player1turn;

            ChessPiece[,] copy = copyBoard(board);

            copy[targetX, targetY] = copy[currentX, currentY];
            copy[currentX, currentY] = null;

            if (isKingCheck(copy, currentTurn, game))
            {
                Console.WriteLine("invalid move, your in check");
                return true;
            }

            return false;
        }
        public bool isCheckMate(ChessPiece[,] board, bool isWhite, ChessGame game)
        {          //finding the king
            int KingX = -1; int KingY = -1;
            for (int i = 0; i < 8 && KingX == -1; i++)
            {
                for (int j = 0; j < 8 && KingX == -1; j++)
                {
                    if (board[i, j] is King && board[i, j].isWhite == isWhite)
                    { KingX = i; KingY = j; }
                }
            }
            ChessPiece[,] copy = copyBoard(board);
            //finding freind piece

            for (int row1 = 0; row1 < 8; row1++)
            {
                for (int col1 = 0; col1 < 8; col1++)
                {
                    if (copy[row1, col1] != null)
                    {
                        if (copy[row1, col1].isWhite == copy[KingX, KingY].isWhite) //מצאתי את הכלי החבר
                        {
                            for (int row2 = 0; row2 < 8; row2++) //נעבור על כל הלוח ונבדוק האם הצעד שלו לשם חוקי
                            {
                                for (int col2 = 0; col2 < 8; col2++)
                                {
                                    if (IsLegalMove(row1, col1, row2, col2, false, copy, game)) //נבדוק עבור כל משבצת האם הצעד אליו חוקי, אם כן, נבדוק המלך נשאר בשח אחרי זה
                                    {
                                        if (copy[row2, col2] != null)    //כדי להימנע ממצב שבו אני מבצע העתקת אובייקט למשבצת ריקה  ומקבל שגיאה, אני מחלק את הקוד לשתי חלקים בנקודה הזו, כל חלק יבצע העתקה, בדיקת הצלה משח, והחזרת הלוח אבל החזרת הלוח תותאם בדיוק לסיטואציה של משבצת 2 והאם היא ריקה
                                        {
                                            ChessPiece savePeice2 = copy[row2, col2].getCopy();
                                            copy[row2, col2] = copy[row1, col1];
                                            copy[row1, col1] = null;

                                            if (isKingCheck(copy, isWhite, game) == false)
                                                return false;
                                            
                                            else
                                            {
                                                copy[row1, col1] = copy[row2, col2]; //במקום זה שיעתיק את המיקום מלוח המקור. כך אני חוסך התעסקות באובייקטים נוספים ואולי זה יפתור את הבאג
                                                copy[row2, col2] = savePeice2;
                                            }
                                        }
                                        else
                                        {
                                            copy[row2, col2] = copy[row1, col1];
                                            copy[row1, col1] = null;

                                            if (isKingCheck(copy, isWhite, game) == false)
                                                return false;
                                            else
                                            {
                                                copy[row1, col1] = copy[row2, col2];
                                                copy[row2, col2] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void demonstrateATurn(ChessPiece[,] board, string[] saveBoards, ChessGame game)
        {
            int[] origin = new int[2];
            origin = getPlayerInput(true);
            int[] destenation = new int[2];
            destenation = getPlayerInput(false);
            bool currentTurn = Player1turn;

            ChessPiece[,] copy = copyBoard(board);

            if (AreYouStillInCheck(copy, origin[0], origin[1], destenation[0], destenation[1], game))
                return;
            if (board[origin[0], origin[1]] != null)
                if (Player1turn != board[origin[0], origin[1]].isWhite) // בדיקה שניגש לכלי בצבע של השחקן. ניתן לבצע רק לאחר בדיקת התגוננות משח
                    return;

            ChessPiece[,] secondCopy = copyBoard(board);
            if (CanMovePeicesOnBoard(origin[0], origin[1], destenation[0], destenation[1], true, board, game))
            {
                //בדיקת הכתרה
                if (board[destenation[0], destenation[1]] is Pawn && (destenation[0] == 0 || destenation[0] == 7))  //בודק אם יש צורך בהכתרה
                {
                    promotePawn(destenation[0], destenation[1], board[destenation[0], destenation[1]].isWhite, board);
                }
                //

                //טיפול בדרך הילוכו
                if (wasWhitePasatDemonstarted == true)
                {
                    board[destenation[0] - 1, destenation[1]] = null;
                    wasWhitePasatDemonstarted = false;
                }

                if (wasBlackPasatDemonstrated == true)
                {
                    board[destenation[0] + 1, destenation[1]] = null;
                    wasBlackPasatDemonstrated = false;
                }
                EnnPasatXCoordinate = -1;
                EnnPasatYCoordinate = -1;
                SetEnnPasat(origin[0], origin[1], destenation[0], destenation[1], board); //פה אני מזהה האם הייתה תנועה שיצרה פוטנציאל להילוכו בתור הבא

                //טיפול בשח ומט
                if (isKingCheck(board, !currentTurn, game))
                {
                    if (isCheckMate(board, !currentTurn, game))
                    {
                        Console.WriteLine("mat! {0} player wins", Player1turn ? "white" : "black");
                        isGameOver = true;
                    }
                    else
                    {
                        Console.WriteLine("check!");
                        wasBlackPasatDemonstrated = false;  //פונקציית בדיקת מט עלולה להדליק את המשתנים האלה במהלך הריצה שלה. בכל מקרה, הם צריכים להיות כבויים בשלב הזה (כי אם לא בוצע הילוכו בתור האמיתי זה אמור להיות כבוי, ואם בוצע - היה קטע קוד לעיל שביצע את שנדרש וכיבה אותם)
                        wasWhitePasatDemonstarted = false;
                    }
                }
                //סוף שח ומט
                //פת - אין מהלך חוקי
                if (isGameOver == false)
                {
                    if (isCheckMate(board, !currentTurn, game)) //פונקציית בדיקת מט נבדקת שוב, הפעם בלי שח לפני כן. כי מט - ללא שח לפניו, זה פט
                    {
                        Console.WriteLine("Stalemate! there is no legal move. Tie!");
                        isGameOver = true;
                    }
                }
                //ביטול האפשרות להצרחה, אם המשבצות התרוקנו פעם אחת
                if (board[0, 0] == null)
                    castlingWhiteLeftPossible = false;

                if (board[0, 7] == null)
                    castlingWhiteRightPossible = false;

                if (board[0, 4] == null)
                {
                    castlingWhiteRightPossible = false;
                    castlingWhiteLeftPossible = false;
                }

                if (board[7, 0] == null)
                    castlingBlackLeftPossible = false;

                if (board[7, 7] == null)
                    castlingBlackRightPossible = false;

                if (board[7, 4] == null)
                {
                    castlingBlackRightPossible = false;
                    castlingBlackLeftPossible = false;
                }

                // העברת הצריח - אם המלך זז בתנועת הצרחה
                //ימין 
                int castlingRow = origin[0];
                if (origin[0] == destenation[0] && origin[1] == 4 && board[destenation[0], destenation[1]] is King && destenation[1] == 6)
                {
                    board[castlingRow, 5] = board[castlingRow, 7]; //הצריח זז
                    board[castlingRow, 7] = null;        //ואחרי זה המשבצת שלו מתרוקנת
                }
                //שמאל
                if (origin[0] == destenation[0] && origin[1] == 4 && board[destenation[0], destenation[1]] is King && destenation[1] == 2)
                {
                    board[castlingRow, 3] = board[castlingRow, 0]; //הצריח זז
                    board[castlingRow, 0] = null;        //ואחרי זה המשבצת שלו מתרוקנת
                }

                // בדיקת פת - ללא חלקים
                if (isDeadPosition(board) == true)
                {
                    Console.WriteLine("you have got to a dead position. Tie. game over");
                    isGameOver = true;
                }

                //פת - 50 מהלכים
                movesWithNoProgression++;
                if (secondCopy[origin[0], origin[1]] is Pawn || secondCopy[destenation[0], destenation[1]] != null)
                    movesWithNoProgression = 0;
                if (movesWithNoProgression >= 100)
                {
                    isGameOver = true;
                    Console.WriteLine("you have reached the Fifty-move rule. Tie. game over");
                }

                //פת - 3 מהלכים חוזרים
                if (isThreeFoldRepetition(board, saveBoards) == true)
                {
                    isGameOver = true;
                    Console.WriteLine("Three-fold repetition! Tie. Game over");
                }
                Player1turn = !Player1turn;   // רק אם המהלך תקין יבוצע מעבר תור
            }
            printBoard(board);
        }
        public bool isThreeFoldRepetition(ChessPiece[,] board, string[] saveBoard)
        {
            //ליצור מחרוזת של הלוח
            string boardString = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == null)
                        boardString += "$";
                    if (board[i, j] != null)
                        boardString += board[i, j].ToString();
                }
            }
            boardString += castlingBlackRightPossible;
            boardString += castlingBlackLeftPossible;
            boardString += castlingWhiteRightPossible;
            boardString += castlingWhiteLeftPossible;
            boardString += EnnPasatXCoordinate;
            boardString += EnnPasatYCoordinate;
            //לאתר מיקום פנוי במערך ולשמור בו
            bool emptySpaceYetFound = true;
            int openSpace = 0;
            for (openSpace = 0; openSpace < 300 && emptySpaceYetFound; openSpace++)
            {
                if (saveBoard[openSpace] == null)
                {
                    saveBoard[openSpace] = boardString;
                    emptySpaceYetFound = false;
                }
            }
            //לוקח את המחרוזת האחרונה ובודק האם יש 2 מחרוזות לפני כן שזהות לו
            int countSameBoards = 0;
            if (openSpace > 2)
            {
                for (int n = openSpace - 2; n >= 0; n--)
                {
                    if (saveBoard[openSpace - 1] == saveBoard[n])
                        countSameBoards++;
                    if (countSameBoards == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsLegalMove(int currentX, int currentY, int targetX, int targetY, bool withMessage, ChessPiece[,] board, ChessGame game)
        {            
            if (board[currentX, currentY] == null) //מוודא שלא הצליח להסתנן מהלך שמתחיל במשבצת ריקה
                return false;
            else
                return board[currentX, currentY].IsLegalMove(currentX, currentY, targetX, targetY, board, withMessage, game);
        }
        public static bool isThreatening(int currentX, int currentY, int targetX, int targetY, bool withMessage, ChessPiece[,] board, ChessGame game)
        {
            return board[currentX, currentY].isThreatening(currentX, currentY, targetX, targetY, board, game);
        }
        public static void promotePawn(int targetX, int targetY, bool isWhite, ChessPiece[,] board)
        {
            char choice = '0';
            while (true)
            {
                Console.WriteLine("Promotion! Choose promotion piece. use one letter: (Q)ueen, (R)ook, (B)ishop, (K)night:");
                string input = Console.ReadLine();
                if (input != null && input != "")
                {
                    input = input.Trim();
                    if (input.Length == 1)
                    {
                        if (input != "")
                            choice = char.ToUpper(input[0]);

                        if ("QRBK".Contains(choice))
                        {
                            break; // קלט חוקי - יציאה מהלולאה
                        }
                    }
                }
                Console.WriteLine("Invalid input. Please enter only one letter -  Q, R, B, or K.");
            }
            switch (choice)
            {
                case 'Q':
                    board[targetX, targetY] = new Queen(isWhite);
                    break;
                case 'R':
                    board[targetX, targetY] = new Rook(isWhite);
                    break;
                case 'B':
                    board[targetX, targetY] = new Bishop(isWhite);
                    break;
                case 'K':
                    board[targetX, targetY] = new Knight(isWhite);
                    break;
                default:
                    Console.WriteLine("Invalid choice, defaulting to Queen.");
                    board[targetX, targetY] = new Queen(isWhite);
                    break;
            }
        }
        public void SetEnnPasat(int row, int col, int row2, int col2, ChessPiece[,] board)
        {
            if (board[row2, col2] is Pawn && board[row2, col2].isWhite == false && row == 6 && row2 == 4) //עבור דרך הילוכו שקורה כתוצאה מתנועה של חייל שחר
            {
                if (col2 + 1 > -1 && col2 + 1 < 8)
                {
                    if (board[row2, col2 + 1] != null) //עבור חייל לבן מימינו
                    {
                        if (board[row2, col2 + 1] is Pawn && board[row2, col2 + 1].isWhite == true)
                        {
                            EnnPasatXCoordinate = 5;
                            EnnPasatYCoordinate = col2;
                        }
                    }
                }

                if (col2 - 1 > -1 && col2 - 1 < 8)
                {
                    if (board[row2, col2 - 1] != null) //עבור חייל לבן משמאלו
                    {
                        if (board[row2, col2 - 1] is Pawn && board[row2, col2 - 1].isWhite == true)
                        {
                            EnnPasatXCoordinate = 5;
                            EnnPasatYCoordinate = col2;
                        }
                    }
                }

            }

            if (board[row2, col2] is Pawn && board[row2, col2].isWhite == true && row == 1 && row2 == 3) //עבור לבן
            {
                if (col2 + 1 > -1 && col2 + 1 < 8)
                {
                    if (board[row2, col2 + 1] != null) //עבור חייל לבן מימינו
                    {
                        if (board[row2, col2 + 1] is Pawn && board[row2, col2 + 1].isWhite == false)
                        {
                            EnnPasatXCoordinate = 2;
                            EnnPasatYCoordinate = col2;
                        }
                    }
                }
                if (col2 - 1 > -1 && col2 - 1 < 8)
                {
                    if (board[row2, col2 - 1] != null) //עבור חייל לבן משמאלו
                    {
                        if (board[row2, col2 - 1] is Pawn && board[row2, col2 - 1].isWhite == false)
                        {
                            EnnPasatXCoordinate = 2;
                            EnnPasatYCoordinate = col2;
                        }
                    }
                }
            }
        }
        public static bool isSpotAimed(int spotX, int spotY, ChessPiece[,] board, bool isWhiteCastling, ChessGame game)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                    {
                        if (board[i, j].isWhite != isWhiteCastling)
                        {
                            if (isThreatening(i, j, spotX, spotY, false, board, game) == true)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool isDeadPosition(ChessPiece[,] board)
        {
            int whiteKnights = 0;
            int blackKnights = 0;
            int whiteBishop = 0;
            int blackBishop = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                    {
                        if (board[i, j] is Pawn || board[i, j] is Queen || board[i, j] is Rook)
                        {
                            return false;
                        }
                        if (board[i, j] is Bishop && board[i, j].isWhite == true)
                            whiteBishop++;
                        if (board[i, j] is Bishop && board[i, j].isWhite == false)
                            blackBishop++;
                        if (board[i, j] is Knight && board[i, j].isWhite == true)
                            whiteKnights++;
                        if (board[i, j] is Knight && board[i, j].isWhite == false)
                            blackKnights++;
                    }
                }
            }
            if (whiteBishop + whiteKnights >= 2 || blackBishop + blackKnights >= 2)
                return false;

            return true;
        }
    }
}