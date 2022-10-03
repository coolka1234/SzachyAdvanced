using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PionkiLib;

namespace SzachyAdvanced
{
    public partial class MainChessboard : Form
    {
        /// <summary>
        /// 1- to litery 2-cyfry 3-0 to biale 1 to czarne
        /// Białe:
        /// 1-Pionek 2-Wieza 3-Kon 4-Goniec 5-Qrolowa 6-Krol 7-Krol w szachu 
        /// Czarne:
        /// 8-Pionek 9-Wieża 10-Koń 11-Goniec 12-Qrolowa 13-Krol 14-Krol w szachu
        /// 
        /// 15-Punkcik
        /// </summary>
        public int[,] chessBoard = new int[8, 8];
        public int[,] chessBoard2 = new int[8, 8];
        public bool[,] chessBoardCheck = new bool[8, 8];
        public bool[,] possibleMoves = new bool[8, 8];
        /// <summary>
        /// [nr kolumny,0-bialy 1-czarny]
        /// </summary>
        public bool[,] enPassant = new bool[8, 2];
        public bool ifClicked=false;
        public bool isWhiteMove = true;
        public bool canCastleWhite = true;
        public bool canCastleWhiteShort = true;
        public bool canCastleBlack = true;
        public bool canCastleBlackShort = true;
        int[] xyPionkaMoved = new int[2];
        public MainChessboard()
        {
            InitializeComponent();
            
        }
        public PictureBox clickedOne = new PictureBox();
        public PictureBox clickedTwo = new PictureBox();
        /// <summary>
        /// 0-bialy 1-czarny
        /// </summary>
        public PictureBox[] kingPB = new PictureBox[2];
        
        public void clonePB(PictureBox pictureTo, PictureBox pictureFrom)
        {
            pictureTo.BackgroundImage = pictureFrom.BackgroundImage;
            pictureTo.Tag=pictureFrom.Tag;
            pictureTo.BackgroundImageLayout=pictureFrom.BackgroundImageLayout;
        }
        /// <summary>
        /// posible moves cale na false
        /// </summary>
        public void resetMoves()
        {
            for(int i=0;i<8;i++)
            {
                for(int k=0;k<8;k++)
                {
                    possibleMoves[i, k] = false;
                }
            }
        }  
        /// <summary>
        /// true jesli jest jakas wartosc true w possibleMoves, false jesli nie
        /// </summary>
        /// <returns></returns>
        public bool isThereMove()
        {
            for(int i=0;i<8;i++)
            {
                for(int k=0;k<8;k++)
                {
                    if(possibleMoves[i, k])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void resetEnPassant()
        {
            for(int i=0;i<8;i++)
            {
                enPassant[i, 0] = false;
                enPassant[i, 1] = false;
            }
        }

        // ***************************Sprawdzanie szachu**********************************//
        public void CheckfindForPawn(int x, int y)
        {
            if (chessBoard2[x,y]<8)
            {
                if(y<7&&x<7)
                {
                    chessBoardCheck[x + 1, y + 1] = true;
                }
                if(y<7&&x==7)
                {
                    chessBoardCheck[x - 1, y + 1] = true;
                }
                if(y<7&&x>0)
                {
                    chessBoardCheck[x - 1, y + 1]=true;
                }
            }
            else
            {
                if (y > 0 && x < 7&&x>0)
                {
                    chessBoardCheck[x - 1, y - 1] = true;
                    chessBoardCheck[x + 1, y - 1] = true;
                }
                if (y > 0 && x == 0)
                {
                    chessBoardCheck[x + 1, y - 1] = true;
                }
                if (y > 0 && x == 7)
                {
                    chessBoardCheck[x - 1, y - 1] = true;
                }
            }
        }
        public void CheckfindForRook(int x, int y)
        {
            int pawn = chessBoard2[x,y];
            //dla bialej wiezy
            if (pawn <8)
            {
                for (int i = x + 1; i < 8; i++)
                {
                    if (chessBoard2[i, y] == 0 || chessBoard2[i, y] == 15 )
                    {
                        chessBoard2[i, y] = 15;
                        chessBoardCheck[i, y] = true;
                    }
                    else if (chessBoard2[i, y] != 0  && chessBoard2[i, y] > 7)
                    {
                        chessBoardCheck[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    if (chessBoard2[i, y] == 0 || chessBoard2[i, y] == 15)
                    {
                        chessBoard2[i, y] = 15;
                        chessBoardCheck[i, y] = true;
                    }
                    else if (chessBoard2[i, y] != 0  && chessBoard2[i, y] > 7)
                    {
                        chessBoardCheck[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y + 1; i <= 7; i++)
                {
                    if (chessBoard2[x, i] == 0|| chessBoard2[x, i] == 15)
                    {
                        chessBoard2[x, i] = 15;
                        chessBoardCheck[x, i] = true;
                    }
                    else if (chessBoard2[x, i] != 0  && chessBoard2[x, i] > 7)
                    {
                        chessBoardCheck[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y - 1; i >= 0; i--)
                {
                    if (chessBoard2[x, i] == 0 || chessBoard2[x, i] == 15 )
                    {
                        chessBoard2[x, i] = 15;
                        chessBoardCheck[x, i] = true;
                    }
                    else if (chessBoard2[x, i] != 0  && chessBoard2[x, i] > 7)
                    {
                        chessBoardCheck[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else //czarna wieza
            {
                for (int i = x + 1; i < 8; i++)
                {
                    if (chessBoard2[i, y] == 0 || chessBoard2[i, y] == 15  )
                    {
                        chessBoard2[i, y] = 15;
                        chessBoardCheck[i, y] = true;
                    }
                    else if (chessBoard2[i, y] != 0  && chessBoard2[i, y] <= 7)
                    {
                        chessBoardCheck[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    if (chessBoard2[i, y] == 0 || chessBoard2[i, y] == 15 )
                    {
                        chessBoard2[i, y] = 15;
                        chessBoardCheck[i, y] = true;
                    }
                    else if (chessBoard2[i, y] != 0  && chessBoard2[i, y] <= 7)
                    {
                        chessBoardCheck[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y + 1; i <= 7; i++)
                {
                    if (chessBoard2[x, i] == 0 || chessBoard2[x, i] == 15 )
                    {
                        chessBoard2[x, i] = 15;
                        chessBoardCheck[x, i] = true;
                    }
                    else if (chessBoard2[x, i] != 0  && chessBoard2[x, i] <= 7)
                    {
                        chessBoardCheck[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y - 1; i >= 0; i--)
                {
                    if (chessBoard2[x, i] == 0 || chessBoard2[x, i] == 15 )
                    {
                        chessBoard2[x, i] = 15;
                        chessBoardCheck[x, i] = true;
                    }
                    else if (chessBoard2[x, i] != 0  && chessBoard2[x, i] <= 7)
                    {
                        chessBoardCheck[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

        } 

        public void CheckfindForHorse(int x, int y)
        {
            int[,] pola = new int[8, 2];
            pola[0, 0] = x + 1;
            pola[0, 1] = y + 2;
            if (pola[0, 0] < 8 && pola[0, 1] < 8)
            {
                if (true)
                {
                    chessBoardCheck[pola[0, 0], pola[0, 1]] = true;
                }
            }
            pola[1, 0] = x + 2;
            pola[1, 1] = y + 1;
            if (pola[1, 0] < 8 && pola[1, 1] < 8)
            {
                if (true)
                {
                    chessBoardCheck[pola[1, 0], pola[1, 1]] = true;
                }
            }
            pola[2, 0] = x + 2;
            pola[2, 1] = y - 1;
            if (pola[2, 0] < 8 && pola[2, 1] >= 0)
            {
                if (true)
                {
                    chessBoardCheck[pola[2, 0], pola[2, 1]] = true;
                }
            }
            pola[3, 0] = x + 1;
            pola[3, 1] = y - 2;
            if (pola[3, 0] < 8 && pola[3, 1] >= 0)
            {
                if (true)
                {
                    chessBoardCheck[pola[3, 0], pola[3, 1]] = true;
                }
            }
            pola[4, 0] = x - 1;
            pola[4, 1] = y - 2;
            if (pola[4, 0] >= 0 && pola[4, 1] >= 0)
            {
                if (true)
                {
                    chessBoardCheck[pola[4, 0], pola[4, 1]] = true;
                }
            }
            pola[5, 0] = x - 2;
            pola[5, 1] = y - 1;
            if (pola[5, 0] >= 0 && pola[5, 1] >= 0)
            {
                if (true)
                {
                    chessBoardCheck[pola[5, 0], pola[5, 1]] = true;

                }
            }
            pola[6, 0] = x - 2;
            pola[6, 1] = y + 1;
            if (pola[6, 0] >= 0 && pola[6, 1] < 8)
            {
                if (true)
                {
                    chessBoardCheck[pola[6, 0], pola[6, 1]] = true;
                }
            }
            pola[7, 0] = x - 1;
            pola[7, 1] = y + 2;
            if (pola[7, 0] >= 0 && pola[7, 1] < 8)
            {
                if (true)
                {
                    chessBoardCheck[pola[7, 0], pola[7, 1]] = true;
                }
            }
        }

        public void CheckfindForBishop(int x, int y)
        {
            int pawn = chessBoard2[x, y];
            int add = 1;
            if (pawn < 8)
            {
                while (x + add < 8 && y + add < 8)
                {
                    if (chessBoard2[x + add, y + add] == 0|| chessBoard2[x + add, y + add] == 15)
                    {
                        chessBoard2[x + add, y + add] = 15;
                        chessBoardCheck[x + add, y + add] = true;
                    }
                    else if (chessBoard2[x + add, y + add] > 7 )
                    {
                        chessBoardCheck[x + add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
                while (x + add < 8 && y - add >= 0)
                {
                    if (chessBoard2[x + add, y - add] == 0|| chessBoard2[x + add, y - add] == 15)
                    {
                        chessBoard2[x + add, y - add] = 15;
                        chessBoardCheck[x + add, y - add] = true;
                    }
                    else if (chessBoard2[x + add, y - add] > 7 )
                    {
                        chessBoardCheck[x + add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;

                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y - add >= 0)
                {
                    if (chessBoard2[x - add, y - add] == 0|| chessBoard2[x - add, y - add] == 15)
                    {
                        chessBoard2[x - add, y - add] = 15;
                        chessBoardCheck[x - add, y - add] = true;
                    }
                    else if (chessBoard2[x - add, y - add] > 7)
                    {
                        chessBoardCheck[x - add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y + add < 8)
                {
                    if (chessBoard2[x - add, y + add] == 0 || chessBoard2[x - add, y + add] == 15 )
                    {
                        chessBoard2[x - add, y + add] = 15;
                        chessBoardCheck[x - add, y + add] = true;
                    }
                    else if (chessBoard2[x - add, y + add] > 7 )
                    {
                        chessBoardCheck[x - add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
            }
            else
            {
                while (x + add < 8 && y + add < 8)
                {
                    if (chessBoard2[x + add, y + add] == 0 || chessBoard2[x + add, y + add] == 15)
                    {
                        chessBoard2[x + add, y + add] = 15;
                        chessBoardCheck[x + add, y + add] = true;
                    }
                    else if (chessBoard2[x + add, y + add] <= 7)
                    {
                        chessBoardCheck[x + add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
                while (x + add < 8 && y - add >= 0)
                {
                    if (chessBoard2[x + add, y - add] == 0 || chessBoard2[x + add, y - add] == 15)
                    {
                        chessBoard2[x + add, y - add] = 15;
                        chessBoardCheck[x + add, y - add] = true;
                    }
                    else if (chessBoard2[x + add, y - add] <= 7)
                    {
                        chessBoardCheck[x + add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;

                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y - add >= 0)
                {
                    if (chessBoard2[x - add, y - add] == 0 || chessBoard2[x - add, y - add] == 15)
                    {
                        chessBoard2[x - add, y - add] = 15;
                        chessBoardCheck[x - add, y - add] = true;
                    }
                    else if (chessBoard2[x - add, y - add] <= 7)
                    {
                        chessBoardCheck[x - add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y + add < 8)
                {
                    if (chessBoard2[x - add, y + add] == 0 || chessBoard2[x - add, y + add] == 15)
                    {
                        chessBoard2[x - add, y + add] = 15;
                        chessBoardCheck[x - add, y + add] = true;
                    }
                    else if (chessBoard2[x - add, y + add] <= 7)
                    {
                        chessBoardCheck[x - add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                
            }
            
        }

        public void CheckfindForQueen(int x, int y)
        {
            CheckfindForBishop(x, y);
            CheckfindForRook(x, y);
        }

        public void CheckfindForKing(int x, int y)
        {
            int pawn = chessBoard2[x, y];
            if (pawn < 8)
            {
                if (x < 7 && y < 7)
                {
                    if (chessBoard2[x + 1, y + 1] > 7)
                    {
                        chessBoardCheck[x + 1, y + 1] = true;
                    }
                    if (chessBoard2[x + 1, y + 1] == 0|| chessBoard2[x + 1, y + 1] == 15)
                    {
                        chessBoardCheck[x + 1, y + 1] = true;
                        chessBoard2[x + 1, y + 1] = 15;
                    }
                }
                if (x < 7)
                {
                    if ( chessBoard2[x + 1, y] > 7)
                    {
                        chessBoardCheck[x + 1, y] = true;
                    }
                    if (chessBoard2[x + 1, y] == 0|| chessBoard2[x + 1, y] == 15)
                    {
                        chessBoardCheck[x + 1, y] = true;
                        chessBoard2[x + 1, y] = 15;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (chessBoard2[x + 1, y - 1] > 7)
                    {
                        chessBoardCheck[x + 1, y - 1] = true;
                    }
                    if (chessBoard2[x + 1, y - 1] == 0 || chessBoard2[x + 1, y - 1] == 15)
                    {
                        chessBoardCheck[x + 1, y - 1] = true;
                        chessBoard2[x + 1, y - 1] = 15;
                    }
                }
                if (y > 0)
                {
                    if (chessBoard2[x, y - 1] > 7)
                    {
                        chessBoardCheck[x, y - 1] = true;
                    }
                    if (chessBoard2[x, y - 1] == 0|| chessBoard2[x, y - 1] == 15)
                    {
                        chessBoardCheck[x, y - 1] = true;
                        chessBoard2[x, y - 1] = 15;
                    }
                }
                if (x > 0 && y > 0)
                {
                    if (chessBoard2[x - 1, y - 1] > 7)
                    {
                        chessBoardCheck[x - 1, y - 1] = true;
                    }
                    if (chessBoard2[x - 1, y - 1] == 0|| chessBoard2[x - 1, y - 1] == 15)
                    {
                        chessBoardCheck[x - 1, y - 1] = true;
                        chessBoard2[x - 1, y - 1] = 15;
                    }
                }
                if (x > 0)
                {
                    if (chessBoard2[x - 1, y] > 7)
                    {
                        chessBoardCheck[x - 1, y] = true;
                    }
                    if (chessBoard2[x - 1, y] == 0|| chessBoard2[x - 1, y] == 15)
                    {
                        chessBoardCheck[x - 1, y] = true;
                        chessBoard2[x - 1, y] = 15;
                    }
                }
                if (x > 0 && y < 7)
                {
                    if (chessBoard2[x - 1, y + 1] > 7)
                    {
                        chessBoardCheck[x - 1, y + 1] = true;
                    }
                    if (chessBoard2[x - 1, y + 1] == 0 || chessBoard2[x - 1, y + 1] == 15)
                    {
                        chessBoardCheck[x - 1, y + 1] = true;
                        chessBoard2[x - 1, y + 1] = 15;
                    }
                }
                if (y < 7)
                {
                    if (chessBoard2[x, y + 1] > 7)
                    {
                        chessBoardCheck[x, y + 1] = true;
                    }
                    if (chessBoard2[x, y + 1] == 0 || chessBoard2[x, y + 1] == 15)
                    {
                        chessBoardCheck[x, y + 1] = true;
                        chessBoard2[x, y + 1] = 15;
                    }
                }
            }
            else
            {
                if (x < 7 && y < 7)
                {
                    if (chessBoard2[x + 1, y + 1] < 8)
                    {
                        chessBoardCheck[x + 1, y + 1] = true;
                    }
                    if (chessBoard2[x + 1, y + 1] == 0 || chessBoard2[x + 1, y + 1] == 15)
                    {
                        chessBoardCheck[x + 1, y + 1] = true;
                        chessBoard2[x + 1, y + 1] = 15;
                    }
                }
                if (x < 7)
                {
                    if (chessBoard2[x + 1, y] < 8)
                    {
                        chessBoardCheck[x + 1, y] = true;
                    }
                    if ( chessBoard2[x + 1, y] == 0 || chessBoard2[x + 1, y] == 15)
                    {
                        chessBoardCheck[x + 1, y] = true;
                        chessBoard2[x + 1, y] = 15;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (chessBoard2[x + 1, y - 1] < 8)
                    {
                        chessBoardCheck[x + 1, y - 1] = true;
                    }
                    if (chessBoard2[x + 1, y - 1] == 0 || chessBoard2[x + 1, y - 1] == 15)
                    {
                        chessBoardCheck[x + 1, y - 1] = true;
                        chessBoard2[x + 1, y - 1] = 15;
                    }
                }
                if (y > 0)
                {
                    if (chessBoard2[x, y - 1] <= 7)
                    {
                        chessBoardCheck[x, y - 1] = true;
                    }
                    if (chessBoard2[x, y - 1] == 0 || chessBoard2[x, y - 1] == 15)
                    {
                        chessBoardCheck[x, y - 1] = true;
                        chessBoard2[x, y - 1] = 15;
                    }
                }
                if (x > 0 && y > 0)
                {
                    if (chessBoard2[x - 1, y - 1] <= 7)
                    {
                        chessBoardCheck[x - 1, y - 1] = true;
                    }
                    if (chessBoard2[x - 1, y - 1] == 0 || chessBoard2[x - 1, y - 1] == 15)
                    {
                        chessBoardCheck[x - 1, y - 1] = true;
                        chessBoard2[x - 1, y - 1] = 15;
                    }
                }
                if (x > 0)
                {
                    if (chessBoard2[x - 1, y] <= 7)
                    {
                        chessBoardCheck[x - 1, y] = true;
                    }
                    if (chessBoard2[x - 1, y] == 0 || chessBoard2[x - 1, y] == 15)
                    {
                        chessBoardCheck[x - 1, y] = true;
                        chessBoard2[x - 1, y] = 15;
                    }
                }
                if (x > 0 && y < 7)
                {
                    if (chessBoard2[x - 1, y + 1] <= 7)
                    {
                        chessBoardCheck[x - 1, y + 1] = true;
                    }
                    if (chessBoard2[x - 1, y + 1] == 0 || chessBoard2[x - 1, y + 1] == 15)
                    {
                        chessBoardCheck[x - 1, y + 1] = true;
                        chessBoard2[x - 1, y + 1] = 15;
                    }
                }
                if (y < 7)
                {
                    if (chessBoard2[x, y + 1] <= 7)
                    {
                        chessBoardCheck[x, y + 1] = true;
                    }
                    if (chessBoard2[x, y + 1] == 0|| chessBoard2[x, y + 1] == 15)
                    {
                        chessBoardCheck[x, y + 1] = true;
                        chessBoard2[x, y + 1] = 15;
                    }
                }
            }
        }
        public bool ifCheckWhite(int x, int y, bool clone)
        {
            if (clone)
            {
                chessBoard2 = chessBoard.Clone() as int[,];
            }
            bool isCheck=false;
            for(int i=0;i<8;i++)
            {
                for(int k=0;k<8;k++)
                {
                    if (chessBoard2[i,k]==8)
                    {
                        CheckfindForPawn(i, k);
                    }
                    else if (chessBoard2[i,k]==9)
                    {
                        CheckfindForRook(i, k);
                    }
                    else if (chessBoard2[i, k] == 10)
                    {
                        CheckfindForHorse(i, k);
                    }
                    else if (chessBoard2[i, k] == 11)
                    {
                        CheckfindForBishop(i, k);
                    }
                    else if (chessBoard2[i, k] == 12)
                    {
                        CheckfindForQueen(i, k);
                    }
                    else if (chessBoard2[i, k] == 13)
                    {
                        CheckfindForKing(i, k);
                    }
                }
            }
            if (chessBoardCheck[x,y])
            {
                isCheck = true;
            }
            else
            {
                isCheck=false;
            }
            for(int i=0;i<8;i++)
            {
                for(int k=0;k<8;k++)
                {
                    chessBoardCheck[i,k]=false;
                }
            }
            return isCheck;
        }
        public bool ifCheckBlack(int x, int y, bool clone)
        {
            if (clone)
            {
                chessBoard2 = chessBoard.Clone() as int[,];
            }
            bool isCheck = false;
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard2[i, k] == 1)
                    {
                        CheckfindForPawn(i, k);
                    }
                    else if (chessBoard2[i, k] == 2)
                    {
                        CheckfindForRook(i, k);
                    }
                    else if (chessBoard2[i, k] == 3)
                    {
                        CheckfindForHorse(i, k);
                    }
                    else if (chessBoard2[i, k] == 4)
                    {
                        CheckfindForBishop(i, k);
                    }
                    else if (chessBoard2[i, k] == 5)
                    {
                        CheckfindForQueen(i, k);
                    }
                    else if (chessBoard2[i, k] == 6)
                    {
                        CheckfindForKing(i, k);
                    }
                }
            }
            if (chessBoardCheck[x, y])
            {
                isCheck = true;
            }
            else
            {
                isCheck = false;
            }
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    chessBoardCheck[i, k] = false;
                }
            }
            return isCheck;
        }
        // *********************Pozycje pionkow********************************************//
        public bool checkForConflict(int x, int y, int pawn, int oldx, int oldy)
        {
            for(int i=0;i<8;i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    chessBoard2[i, k] = 0;
                }
            }
            chessBoard2 = chessBoard.Clone() as int[,];
            chessBoard2[x, y] = pawn;
            chessBoard2[oldx, oldy] = 0;
            if(pawn<8)
            {
                return ifCheckWhite(returnXWhite(), returnYWhite(),false);
            }
            else
            {
                return ifCheckBlack(returnXBlack(), returnYBlack(),false);
            }
            
        }
        public int returnXWhite()
        {
            for(int i=0;i<8;i++)
            {
                for(int k=0;k<8;k++)
                {
                    if (chessBoard2[i,k]==6|| chessBoard2[i, k] == 7)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public int returnYWhite()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard2[i, k] == 6|| chessBoard2[i, k] == 7)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }
        public int returnXBlack()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard2[i, k] == 13|| chessBoard2[i, k] == 14)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public int returnYBlack()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard2[i, k] == 13||chessBoard2[i, k] == 14)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }

        public int returnXWhite2()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard[i, k] == 6 || chessBoard[i, k] == 7)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public int returnYWhite2()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard[i, k] == 6 || chessBoard[i, k] == 7)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }
        public int returnXBlack2()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard[i, k] == 13 || chessBoard[i, k] == 14)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public int returnYBlack2()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (chessBoard[i, k] == 13 || chessBoard[i, k] == 14)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }
        public void findForPawn(int x, int y)
        {
            if (y < 7 && chessBoard[x,y]==1)
            {
                if (chessBoard[x, y] == 1 && y == 1) //jesli to bialy i pierwszy ruch
                {
                    if (chessBoard[x, 2] == 0)
                    {
                        if (!checkForConflict(x, 2, chessBoard[x, y],x,y))
                        {
                            chessBoard[x, 2] = 15;
                            possibleMoves[x, 2] = true;
                        }
                        if (chessBoard[x, 3] == 0 && !checkForConflict(x, 3, chessBoard[x,y],x,y))
                        {
                            chessBoard[x, 3] = 15;
                            possibleMoves[x,3]=true;
                        }
                    }
                }
                else if (chessBoard[x, y] == 1 && y != 1) //jesli bialy i nie pierwszy
                {
                    if (chessBoard[x, y + 1] == 0&&!checkForConflict(x, y+1, chessBoard[x, y],x,y))
                    {
                        chessBoard[x, y + 1] = 15;
                        possibleMoves[x, y + 1] = true;
                    }
                }
                if (x < 7 && y < 7) //bicie
                {
                    if (chessBoard[x + 1, y + 1] != 0&&!checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x + 1, y + 1] = true;
                    }
                }
                if (x > 0 && y < 7)
                {
                    if (chessBoard[x - 1, y + 1] != 0&& !checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - 1, y + 1] = true;
                    }
                }
                //enPassant
                if(x>0&&y<7)
                {
                    if (chessBoard[x - 1, y] == 8 && !checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y) && enPassant[x-1,1])
                    {
                        possibleMoves[x - 1, y + 1] = true;
                        chessBoard[x - 1, y + 1] = 15;
                    }
                }
                if (x < 7&&y<7)
                {
                    if (chessBoard[x + 1, y] == 8 && !checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y) && enPassant[x + 1, 1])
                    {
                        possibleMoves[x + 1, y + 1] = true;
                        chessBoard[x + 1, y + 1] = 15;
                    }
                }
            }
            if (y > 0 && chessBoard[x,y]==8)
            {
                if (chessBoard[x, y] == 8 && y == 6) //jesli to czarny i pierwszy ruch
                {
                    if (chessBoard[x, 5] == 0 && !checkForConflict(x, 5, chessBoard[x, y],x,y))
                    {
                        chessBoard[x, 5] = 15;
                        possibleMoves[x,5]=true;
                        if (chessBoard[x, 4] == 0 && !checkForConflict(x, 4, chessBoard[x, y],x,y))
                        {
                            chessBoard[x, 4] = 15;
                            possibleMoves[x, 4] = true;
                        }
                    }
                }
                else if (chessBoard[x, y] == 8 && y != 6) //jesli czarny i nie pierwszy
                {
                    bool cos = !checkForConflict(x, y - 1, chessBoard[x, y],x,y);
                    if (chessBoard[x, y - 1] == 0 && !checkForConflict(x, y - 1, chessBoard[x, y],x,y))
                    {
                        chessBoard[x, y - 1] = 15;
                        possibleMoves[x, y - 1] = true;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (chessBoard[x + 1, y - 1] != 0 && !checkForConflict(x + 1, y - 1, chessBoard[x,y],x,y))
                    {
                        possibleMoves[x + 1, y - 1] = true;
                    }
                }
                if (x > 0 && y > 0)
                {
                    if (chessBoard[x - 1, y - 1] != 0&& !checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - 1, y - 1] = true;
                    }
                }
                //en Passant
                if (x > 0 && y > 0)
                {
                    if (chessBoard[x - 1, y] == 1 && !checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y) && enPassant[x - 1, 0])
                    {
                        possibleMoves[x - 1, y - 1] = true;
                        chessBoard[x - 1, y - 1] = 15;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (chessBoard[x + 1, y] == 1 && !checkForConflict(x + 1, y - 1, chessBoard[x, y], x, y) && enPassant[x + 1, 0])
                    {
                        possibleMoves[x + 1, y - 1] = true;
                        chessBoard[x + 1, y - 1] = 15;
                    }
                }
            }
            drawDots();
        }
        public void findForRook(int x, int y)
        {
            int pawn = chessBoard[x,y];
            //dla bialej wiezy
            if (pawn <8)
            {
                for (int i = x + 1; i < 8; i++)
                {
                    if (chessBoard[i, y] == 0 && !checkForConflict(i, y, pawn,x,y))
                    {
                        chessBoard[i, y] = 15;
                        possibleMoves[i, y] = true;
                    }
                    else if (chessBoard[i, y] != 0 && !checkForConflict(i, y, pawn,x,y) && chessBoard[i, y] > 7)
                    {
                        possibleMoves[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    if (chessBoard[i, y] == 0 && !checkForConflict(i, y, pawn,x,y))
                    {
                        chessBoard[i, y] = 15;
                        possibleMoves[i, y] = true;
                    }
                    else if (chessBoard[i, y] != 0 && !checkForConflict(i, y, pawn,x,y) && chessBoard[i, y] > 7)
                    {
                        possibleMoves[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y + 1; i <= 7; i++)
                {
                    if (chessBoard[x, i] == 0 && !checkForConflict(x, i, pawn,x,y))
                    {
                        chessBoard[x, i] = 15;
                        possibleMoves[x, i] = true;
                    }
                    else if (chessBoard[x, i] != 0 && !checkForConflict(x, i, pawn,x,y) && chessBoard[x, i] > 7)
                    {
                        possibleMoves[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y - 1; i >= 0; i--)
                {
                    if (chessBoard[x, i] == 0 && !checkForConflict(x, i, pawn,x,y))
                    {
                        chessBoard[x, i] = 15;
                        possibleMoves[x, i] = true;
                    }
                    else if (chessBoard[x, i] != 0 && !checkForConflict(x, i, pawn,x,y) && chessBoard[x, i] > 7)
                    {
                        possibleMoves[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else  //czarna wieza
            {
                for (int i = x + 1; i < 8; i++)
                {
                    if (chessBoard[i, y] == 0 && !checkForConflict(i, y, pawn,x,y))
                    {
                        chessBoard[i, y] = 15;
                        possibleMoves[i, y] = true;
                    }
                    else if (chessBoard[i, y] != 0 && !checkForConflict(i, y, pawn,x,y) && chessBoard[i, y] <= 7)
                    {
                        possibleMoves[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    if (chessBoard[i, y] == 0 && !checkForConflict(i, y, pawn,x,y))
                    {
                        chessBoard[i, y] = 15;
                        possibleMoves[i, y] = true;
                    }
                    else if (chessBoard[i, y] != 0 && !checkForConflict(i, y, pawn,x,y) && chessBoard[i, y] <= 7)
                    {
                        possibleMoves[i, y] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y + 1; i <= 7; i++)
                {
                    if (chessBoard[x, i] == 0 && !checkForConflict(x, i, pawn,x,y))
                    {
                        chessBoard[x, i] = 15;
                        possibleMoves[x, i] = true;
                    }
                    else if (chessBoard[x, i] != 0 && !checkForConflict(x, i, pawn,x,y) && chessBoard[x, i] <= 7)
                    {
                        possibleMoves[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = y - 1; i >= 0; i--)
                {
                    if (chessBoard[x, i] == 0 && !checkForConflict(x, i, pawn,x,y))
                    {
                        chessBoard[x, i] = 15;
                        possibleMoves[x, i] = true;
                    }
                    else if (chessBoard[x, i] != 0 && !checkForConflict(x, i, pawn,x,y) && chessBoard[x, i] <= 7)
                    {
                        possibleMoves[x, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            drawDots();
        }

        public void findForHorse(int x, int y)
        {
            int pawn = chessBoard[x, y];
            int[,] pola = new int[8, 2];
            pola[0,0] = x+1;
            pola[0,1] = y+2;
            if (pola[0, 0] < 8 && pola[0, 1] < 8)
            {
                if (!checkForConflict(pola[0, 0], pola[0, 1], pawn,x,y))
                {
                    possibleMoves[pola[0, 0], pola[0, 1]] = true;
                    if (chessBoard[pola[0, 0], pola[0,1]]==0)
                    {
                        chessBoard[pola[0, 0], pola[0, 1]] = 15;
                    }
                }
            }
            pola[1, 0] = x + 2;
            pola[1, 1] = y + 1;
            if (pola[1, 0] < 8 && pola[1, 1] < 8)
            {
                if (!checkForConflict(pola[1, 0], pola[1, 1], pawn,x,y))
                {
                    possibleMoves[pola[1, 0], pola[1, 1]] = true;
                    if (chessBoard[pola[1, 0], pola[1, 1]] == 0)
                    {
                        chessBoard[pola[1, 0], pola[1, 1]] = 15;
                    }
                }
            }
            pola[2, 0] = x + 2;
            pola[2, 1] = y - 1;
            if (pola[2, 0] < 8 && pola[2, 1] >= 0)
            {
                if (!checkForConflict(pola[2, 0], pola[2, 1], pawn,x,y))
                {
                    possibleMoves[pola[2, 0], pola[2, 1]] = true;
                    if (chessBoard[pola[2, 0], pola[2, 1]] == 0)
                    {
                        chessBoard[pola[2, 0], pola[2, 1]] = 15;
                    }
                }
            }
            pola[3, 0] = x + 1;
            pola[3, 1] = y - 2;
            if (pola[3, 0] < 8 && pola[3, 1] >= 0)
            {
                if (!checkForConflict(pola[3, 0], pola[3, 1], pawn,x,y))
                {
                    possibleMoves[pola[3, 0], pola[3, 1]] = true;
                    if (chessBoard[pola[3, 0], pola[3, 1]] == 0)
                    {
                        chessBoard[pola[3, 0], pola[3, 1]] = 15;
                    }
                }
            }
            pola[4, 0] = x - 1;
            pola[4, 1] = y - 2;
            if (pola[4, 0] >= 0 && pola[4, 1] >= 0)
            {
                if (!checkForConflict(pola[4, 0], pola[4, 1], pawn,x,y))
                {
                    possibleMoves[pola[4, 0], pola[4, 1]] = true;
                    if (chessBoard[pola[4, 0], pola[4, 1]] == 0)
                    {
                        chessBoard[pola[4, 0], pola[4, 1]] = 15;
                    }
                }
            }
            pola[5, 0] = x - 2;
            pola[5, 1] = y - 1;
            if (pola[5, 0] >= 0 && pola[5, 1] >= 0)
            {
                if (!checkForConflict(pola[5, 0], pola[5, 1], pawn,x,y))
                {
                    possibleMoves[pola[5, 0], pola[5, 1]] = true;
                    if (chessBoard[pola[5, 0], pola[5, 1]] == 0)
                    {
                        chessBoard[pola[5, 0], pola[5, 1]] = 15;
                    }
                }
            }
            pola[6, 0] = x - 2;
            pola[6, 1] = y + 1;
            if (pola[6, 0] >= 0 && pola[6, 1] < 8)
            {
                if (!checkForConflict(pola[6, 0], pola[6, 1], pawn,x,y))
                {
                    possibleMoves[pola[6, 0], pola[6, 1]] = true;
                    if (chessBoard[pola[6, 0], pola[6, 1]] == 0)
                    {
                        chessBoard[pola[6, 0], pola[6, 1]] = 15;
                    }
                }
            }
            pola[7, 0] = x - 1;
            pola[7, 1] = y + 2;
            if (pola[7, 0] >= 0 && pola[7, 1] < 8)
            {
                if (!checkForConflict(pola[7, 0], pola[7, 1], pawn,x,y))
                {
                    possibleMoves[pola[7, 0], pola[7, 1]] = true;
                    if (chessBoard[pola[7, 0], pola[7, 1]] == 0)
                    {
                        chessBoard[pola[7, 0], pola[7, 1]] = 15;
                    }
                }
            }
            drawDots();
        }

        public void findForBishop(int x, int y)
        {
            int pawn = chessBoard[x, y];
            int add = 1;
            if (pawn < 8)
            {
                while (x + add < 8 && y + add < 8)
                {
                    if (chessBoard[x + add, y + add] == 0 && !checkForConflict(x + add, y + add, chessBoard[x,y],x,y))
                    {
                        chessBoard[x + add, y + add] = 15;
                        possibleMoves[x + add, y + add] = true;
                    }
                    else if (chessBoard[x+add,y+add]>7 && !checkForConflict(x + add, y + add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x + add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
                while (x + add < 8 && y - add >= 0)
                {
                    if (chessBoard[x + add, y - add] == 0 && !checkForConflict(x + add, y - add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x + add, y - add] = 15;
                        possibleMoves[x + add, y - add] = true;
                    }
                    else if(chessBoard[x + add, y - add] > 7 && !checkForConflict(x + add, y - add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x + add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;
                        
                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y - add >= 0)
                {
                    if (chessBoard[x - add, y - add] == 0 && !checkForConflict(x - add, y - add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x - add, y - add] = 15;
                        possibleMoves[x - add, y - add] = true;
                    }
                    else if( chessBoard[x - add, y - add] > 7 && !checkForConflict(x - add, y - add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y + add < 8)
                {
                    if (chessBoard[x - add, y + add] == 0 && !checkForConflict(x - add, y + add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x - add, y + add] = 15;
                        possibleMoves[x - add, y + add] = true;
                    }
                    else if(chessBoard[x - add, y + add] > 7 && !checkForConflict(x - add, y + add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
            }
            else
            {
                while (x + add < 8 && y + add < 8)
                {
                    if (chessBoard[x + add, y + add] == 0 && !checkForConflict(x + add, y + add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x + add, y + add] = 15;
                        possibleMoves[x + add, y + add] = true;
                    }
                    else if (chessBoard[x + add, y + add] <= 7 && !checkForConflict(x + add, y + add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x + add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
                while (x + add < 8 && y - add >= 0)
                {
                    if (chessBoard[x + add, y - add] == 0 && !checkForConflict(x + add, y - add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x + add, y - add] = 15;
                        possibleMoves[x + add, y - add] = true;
                    }
                    else if (chessBoard[x + add, y - add] <= 7 && !checkForConflict(x + add, y - add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x + add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;

                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y - add >= 0)
                {
                    if (chessBoard[x - add, y - add] == 0 && !checkForConflict(x - add, y - add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x - add, y - add] = 15;
                        possibleMoves[x - add, y - add] = true;
                    }
                    else if (chessBoard[x - add, y - add] <= 7 && !checkForConflict(x - add, y - add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - add, y - add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                    add++;
                }
                add = 1;
                while (x - add >= 0 && y + add < 8)
                {
                    if (chessBoard[x - add, y + add] == 0 && !checkForConflict(x - add, y + add, chessBoard[x, y], x, y))
                    {
                        chessBoard[x - add, y + add] = 15;
                        possibleMoves[x - add, y + add] = true;
                    }
                    else if (chessBoard[x - add, y + add] <= 7 && !checkForConflict(x - add, y + add, chessBoard[x, y], x, y))
                    {
                        possibleMoves[x - add, y + add] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    add++;
                }
                add = 1;
            }
            drawDots();
        }

        public void findForQueen(int x, int y)
        {
            findForBishop(x, y);
            findForRook(x, y);
        }

        public void findForKing(int x, int y)
        {
            //musi byc zmiana pictureBoxa
            int pawn = chessBoard[x, y];
            if (pawn < 8)
            {
                if (x < 7 && y < 7)
                {
                    if (!checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x+1, y+1]>7)
                    {
                        possibleMoves[x + 1, y + 1] = true;
                    }
                    if (!checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x+1, y+1] == 0)
                    {
                        possibleMoves[x + 1, y + 1] = true;
                        chessBoard[x + 1, y + 1] = 15;
                    }
                }
                if (x < 7)
                {
                    if (!checkForConflict(x + 1, y, chessBoard[x, y], x, y) && chessBoard[x+1, y] > 7)
                    {
                        possibleMoves[x + 1,y] = true;
                    }
                    if (!checkForConflict(x + 1, y, chessBoard[x, y], x, y) && chessBoard[x+1, y] == 0)
                    {
                        possibleMoves[x + 1, y] = true;
                        chessBoard[x + 1, y] = 15;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (!checkForConflict(x + 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x+1, y-1] > 7)
                    {
                        possibleMoves[x + 1, y - 1] = true;
                    }
                    if (!checkForConflict(x + 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x+1, y-1] == 0)
                    {
                        possibleMoves[x + 1, y - 1] = true;
                        chessBoard[x + 1, y - 1] = 15;
                    }
                }
                if (y > 0)
                {
                    if (!checkForConflict(x, y - 1, chessBoard[x, y], x, y) && chessBoard[x, y-1] > 7)
                    {
                        possibleMoves[x, y - 1] = true;
                    }
                    if (!checkForConflict(x, y - 1, chessBoard[x, y], x, y) && chessBoard[x, y-1] == 0)
                    {
                        possibleMoves[x, y - 1] = true;
                        chessBoard[x, y - 1] = 15;
                    }
                }
                if (x >0 && y > 0)
                {
                    if (!checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x-1, y-1] > 7)
                    {
                        possibleMoves[x -1, y - 1] = true;
                    }
                    if (!checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x-1, y-1] == 0)
                    {
                        possibleMoves[x - 1, y - 1] = true;
                        chessBoard[x - 1, y - 1] = 15;
                    }
                }
                if (x >0)
                {
                    if (!checkForConflict(x - 1, y, chessBoard[x, y], x, y) && chessBoard[x-1, y] > 7)
                    {
                        possibleMoves[x - 1,y] = true;
                    }
                    if (!checkForConflict(x - 1, y, chessBoard[x, y], x, y) && chessBoard[x-1, y] == 0)
                    {
                        possibleMoves[x - 1, y] = true;
                        chessBoard[x - 1, y] = 15;
                    }
                }
                if (x > 0 && y < 7)
                {
                    if (!checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x-1, y+1] > 7)
                    {
                        possibleMoves[x - 1, y + 1] = true;
                    }
                    if (!checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x-1, y+1] == 0)
                    {
                        possibleMoves[x - 1, y + 1] = true;
                        chessBoard[x - 1, y + 1] = 15;
                    }
                }
                if (y < 7)
                {
                    if (!checkForConflict(x, y + 1, chessBoard[x, y], x, y) && chessBoard[x, y+1] > 7)
                    {
                        possibleMoves[x, y + 1] = true;
                    }
                    if (!checkForConflict(x, y + 1, chessBoard[x, y], x, y) && chessBoard[x, y+1] == 0)
                    {
                        possibleMoves[x, y + 1] = true;
                        chessBoard[x, y + 1] = 15;
                    }
                }
            }
            else
            {
                if (x < 7 && y < 7)
                {
                    if (!checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x+1, y+1] < 8)
                    {
                        possibleMoves[x + 1, y + 1] = true;
                    }
                    if (!checkForConflict(x + 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x+1, y+1] == 0)
                    {
                        possibleMoves[x + 1, y + 1] = true;
                        chessBoard[x + 1, y + 1] = 15;
                    }
                }
                if (x < 7)
                {
                    if (!checkForConflict(x + 1, y, chessBoard[x, y], x, y) && chessBoard[x+1, y] < 8)
                    {
                        possibleMoves[x + 1, y] = true;
                    }
                    if (!checkForConflict(x + 1, y, chessBoard[x, y], x, y) && chessBoard[x+1, y] == 0)
                    {
                        possibleMoves[x + 1, y] = true;
                        chessBoard[x + 1, y] = 15;
                    }
                }
                if (x < 7 && y > 0)
                {
                    if (!checkForConflict(x + 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x+1, y-1] < 8)
                    {
                        possibleMoves[x + 1, y - 1] = true;
                    }
                    if (!checkForConflict(x + 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x+1, y-1] == 0)
                    {
                        possibleMoves[x + 1, y - 1] = true;
                        chessBoard[x + 1, y - 1] = 15;
                    }
                }
                if (y > 0)
                {
                    if (!checkForConflict(x, y - 1, chessBoard[x, y], x, y) && chessBoard[x, y-1] <= 7)
                    {
                        possibleMoves[x, y - 1] = true;
                    }
                    if (!checkForConflict(x, y - 1, chessBoard[x, y], x, y) && chessBoard[x, y-1] == 0)
                    {
                        possibleMoves[x, y - 1] = true;
                        chessBoard[x, y - 1] = 15;
                    }
                }
                if (x > 0 && y > 0)
                {
                    if (!checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x-1, y-1] <= 7)
                    {
                        possibleMoves[x - 1, y - 1] = true;
                    }
                    if (!checkForConflict(x - 1, y - 1, chessBoard[x, y], x, y) && chessBoard[x-1, y-1] == 0)
                    {
                        possibleMoves[x - 1, y - 1] = true;
                        chessBoard[x - 1, y - 1] = 15;
                    }
                }
                if (x > 0)
                {
                    if (!checkForConflict(x - 1, y, chessBoard[x, y], x, y) && chessBoard[x-1, y] <= 7)
                    {
                        possibleMoves[x - 1, y] = true;
                    }
                    if (!checkForConflict(x - 1, y, chessBoard[x, y], x, y) && chessBoard[x-1, y] == 0)
                    {
                        possibleMoves[x - 1, y] = true;
                        chessBoard[x - 1, y] = 15;
                    }
                }
                if (x > 0 && y < 7)
                {
                    if (!checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x-1, y+1] <= 7)
                    {
                        possibleMoves[x - 1, y + 1] = true;
                    }
                    if (!checkForConflict(x - 1, y + 1, chessBoard[x, y], x, y) && chessBoard[x-1, y+1] == 0)
                    {
                        possibleMoves[x - 1, y + 1] = true;
                        chessBoard[x - 1, y + 1] = 15;
                    }
                }
                if (y < 7)
                {
                    if (!checkForConflict(x, y + 1, chessBoard[x, y], x, y) && chessBoard[x, y+1] <= 7)
                    {
                        possibleMoves[x, y + 1] = true;
                    }
                    if (!checkForConflict(x, y + 1, chessBoard[x, y], x, y) && chessBoard[x, y+1] == 0)
                    {
                        possibleMoves[x, y + 1] = true;
                        chessBoard[x, y + 1] = 15;
                    }
                }
            }
            drawDots();
        }

        public bool roszada(int x, int y)
        {
            bool puste = true;
            if (x== 0&&y==0) //dluga biala roszada
            {
                
                for(int i=1;i<4;i++)
                {
                    if (chessBoard[i,0]!=0)
                    {
                        puste = false;
                        return false;
                    }
                }
                if (ifCheckWhite(2, 0,true))
                {
                    puste = false;
                    return false;
                }
                if (ifCheckWhite(3, 0,true))
                {
                    puste = false;
                    return false;
                }
                if (puste&&canCastleWhite)
                {
                    chessBoard[0,0] = 0;
                    chessBoard[4, 0] = 0;
                    chessBoard[2,0] = 6;
                    chessBoard[3, 0] = 2;
                    pictureBoxA1.BackgroundImage = null;
                    pictureBoxE1.BackgroundImage = null;
                    pictureBoxA1.Tag = null;
                    pictureBoxE1.Tag = null;
                    pictureBoxC1.BackgroundImage = Properties.Resources.KrolB;
                    pictureBoxC1.Tag = "KB";
                    pictureBoxD1.BackgroundImage = Properties.Resources.WiezaB;
                    pictureBoxD1.Tag = "WB";
                    return true;
                }
            }
            else if(x==7&&y==0) //krotka biala roszada
            {
                for (int i = 6; i > 4; i--)
                {
                    if (chessBoard[i, 0] != 0)
                    {
                        puste = false;
                        return false;
                    }
                    if (ifCheckWhite(i, 0,true))
                    {
                        puste = false;
                        return false;
                    }
                }
                if (puste && canCastleWhite)
                {
                    chessBoard[7, 0] = 0;
                    chessBoard[6, 0] = 6;
                    chessBoard[5, 0] = 2;
                    chessBoard[4, 0] = 0;
                    pictureBoxH1.BackgroundImage = null;
                    pictureBoxE1.BackgroundImage = null;
                    pictureBoxH1.Tag = null;
                    pictureBoxE1.Tag = null;
                    pictureBoxG1.BackgroundImage = Properties.Resources.KrolB;
                    pictureBoxG1.Tag = "KB";
                    pictureBoxF1.BackgroundImage = Properties.Resources.WiezaB;
                    pictureBoxF1.Tag = "WB";
                    return true;
                }
            }
            else if(x==0&&y==7) //dluga czarna 
            {
                for (int i = 1; i < 4; i++)
                {
                    if (chessBoard[i, 7] != 0)
                    {
                        puste = false;
                        return false;
                    }
                }
                if (ifCheckBlack(2, 7,true))
                {
                    puste = false;
                    return false;
                }
                if (ifCheckBlack(3, 7,true))
                {
                    puste = false;
                    return false;
                }
                if (puste && canCastleBlack)
                {
                    chessBoard[0, 7] = 0;
                    chessBoard[4, 7] = 0;
                    chessBoard[2, 7] = 13;
                    chessBoard[3, 7] = 9;
                    pictureBoxA8.BackgroundImage = null;
                    pictureBoxE8.BackgroundImage = null;
                    pictureBoxA8.Tag = null;
                    pictureBoxE8.Tag = null;
                    pictureBoxC8.BackgroundImage = Properties.Resources.KrolC;
                    pictureBoxC8.Tag = "KC";
                    pictureBoxD8.BackgroundImage = Properties.Resources.WiezaC;
                    pictureBoxD8.Tag = "WC";
                    return true;
                }
            }
            else if(x==7&&y==7)
            {
                for (int i = 6; i > 4; i--)
                {
                    if (chessBoard[i, 7] != 0)
                    {
                        puste = false;
                        return false;
                    }
                    if (ifCheckBlack(i, 7,false))
                    {
                        puste = false;
                        return false;
                    }
                }
                if (puste && canCastleBlack)
                {
                    chessBoard[7, 7] = 0;
                    chessBoard[6, 7] = 13;
                    chessBoard[5, 7] = 9;
                    chessBoard[4, 7] = 0;
                    pictureBoxH8.BackgroundImage = null;
                    pictureBoxE8.BackgroundImage = null;
                    pictureBoxH8.Tag = null;
                    pictureBoxE8.Tag = null;
                    pictureBoxG8.BackgroundImage = Properties.Resources.KrolC;
                    pictureBoxG8.Tag = "KC";
                    pictureBoxF8.BackgroundImage = Properties.Resources.WiezaC;
                    pictureBoxF8.Tag = "WC";
                    return true;
                }
            }
            return false;
        }

        //**********************Koniec pozycji pionkow*************************************//
        public void updateChessboard(int x, int y, PictureBox picture)
        {
            if (chessBoard[x,y] == 1)
            {
                picture.BackgroundImage = Properties.Resources.PionekB;
            }
            else if(chessBoard[x,y] == 2)
            {
                picture.BackgroundImage = Properties.Resources.WiezaB;
            }
            else if(chessBoard[x,y] == 3)
            {
                picture.BackgroundImage = Properties.Resources.KonB;
            }
            else if (chessBoard[x,y]==4)
            {
                picture.BackgroundImage = Properties.Resources.GoniecB;
            }
            else if (chessBoard[x,y]==5)
            {
                picture.BackgroundImage = Properties.Resources.KrolowaB;
            }
            else if (chessBoard[x,y]==6)
            {
                picture.BackgroundImage= Properties.Resources.KrolB;
            }
            else if (chessBoard[x,y]==7)
            {
                picture.BackgroundImage = Properties.Resources.KrolBSzach;
            }
            else if(chessBoard[x,y]==8)
            {
                picture.BackgroundImage = Properties.Resources.PionekC;
            }
            else if (chessBoard[x,y] == 9)
            {
                picture.BackgroundImage = Properties.Resources.WiezaC;
            }
            else if (chessBoard[x,y] == 10)
            {
                picture.BackgroundImage = Properties.Resources.KonC;
            }
            else if (chessBoard[x,y] == 11)
            {
                picture.BackgroundImage = Properties.Resources.GoniecC;
            }
            else if (chessBoard[x,y] == 12)
            {
                picture.BackgroundImage = Properties.Resources.KrolowaC;
            }
            else if (chessBoard[x,y] == 13)
            {
                picture.BackgroundImage = Properties.Resources.KrolC;
            }
            else if (chessBoard[x,y] == 14)
            {
                picture.BackgroundImage = Properties.Resources.KrolCSzach;
            }
            else if (chessBoard[x,y] == 15)
            {
                picture.BackgroundImage = Properties.Resources.Dot;
            }
            else if(chessBoard[x,y] == 0)
            {
                picture.BackgroundImage = null;
            }

        }
        public void deleteDots()
        {
            if (chessBoard[0, 0] == 15)
            {
                pictureBoxA1.BackgroundImage = null;
                chessBoard[0, 0] = 0;
            }
            if (chessBoard[1, 0] == 15)
            {
                pictureBoxB1.BackgroundImage = null;
                chessBoard[1, 0] = 0;
            }
            if (chessBoard[2, 0] == 15)
            {
                pictureBoxC1.BackgroundImage = null;
                chessBoard[2, 0] = 0;
            }
            if (chessBoard[3, 0] == 15)
            {
                pictureBoxD1.BackgroundImage = null;
                chessBoard[3, 0] = 0;
            }
            if (chessBoard[4, 0] == 15)
            {
                pictureBoxE1.BackgroundImage = null;
                chessBoard[4, 0] = 0;
            }
            if (chessBoard[5, 0] == 15)
            {
                pictureBoxF1.BackgroundImage = null;
                chessBoard[5, 0] = 0;
            }
            if (chessBoard[6, 0] == 15)
            {
                pictureBoxG1.BackgroundImage = null;
                chessBoard[6, 0] = 0;
            }
            if (chessBoard[7, 0] == 15)
            {
                pictureBoxH1.BackgroundImage = null;
                chessBoard[7, 0] = 0;
            }
            if (chessBoard[0, 1] == 15)
            {
                pictureBoxA2.BackgroundImage = null;
                chessBoard[0, 1] = 0;
            }
            if (chessBoard[1, 1] == 15)
            {
                pictureBoxB2.BackgroundImage = null;
                chessBoard[1, 1] = 0;
            }
            if (chessBoard[2, 1] == 15)
            {
                pictureBoxC2.BackgroundImage = null;
                chessBoard[2, 1] = 0;
            }
            if (chessBoard[3, 1] == 15)
            {
                pictureBoxD2.BackgroundImage = null;
                chessBoard[3, 1] = 0;
            }
            if (chessBoard[4, 1] == 15)
            {
                pictureBoxE2.BackgroundImage = null;
                chessBoard[4, 1] = 0;
            }
            if (chessBoard[5, 1] == 15)
            {
                pictureBoxF2.BackgroundImage = null;
                chessBoard[5, 1] = 0;
            }
            if (chessBoard[6, 1] == 15)
            {
                pictureBoxG2.BackgroundImage = null;
                chessBoard[6, 1] = 0;
            }
            if (chessBoard[7, 1] == 15)
            {
                pictureBoxH2.BackgroundImage = null;
                chessBoard[7, 1] = 0;
            }
            if (chessBoard[0, 2] == 15)
            {
                pictureBoxA3.BackgroundImage = null;
                chessBoard[0, 2] = 0;
            }
            if (chessBoard[1, 2] == 15)
            {
                pictureBoxB3.BackgroundImage = null;
                chessBoard[1, 2] = 0;
            }
            if (chessBoard[2, 2] == 15)
            {
                pictureBoxC3.BackgroundImage = null;
                chessBoard[2, 2] = 0;
            }
            if (chessBoard[3, 2] == 15)
            {
                pictureBoxD3.BackgroundImage = null;
                chessBoard[3, 2] = 0;
            }
            if (chessBoard[4, 2] == 15)
            {
                pictureBoxE3.BackgroundImage = null;
                chessBoard[4, 2] = 0;
            }
            if (chessBoard[5, 2] == 15)
            {
                pictureBoxF3.BackgroundImage = null;
                chessBoard[5, 2] = 0;
            }
            if (chessBoard[6, 2] == 15)
            {
                pictureBoxG3.BackgroundImage = null;
                chessBoard[6, 2] = 0;
            }
            if (chessBoard[7, 2] == 15)
            {
                pictureBoxH3.BackgroundImage = null;
                chessBoard[7, 2] = 0;
            }
            if (chessBoard[0, 3] == 15)
            {
                pictureBoxA4.BackgroundImage = null;
                chessBoard[0, 3] = 0;
            }
            if (chessBoard[1, 3] == 15)
            {
                pictureBoxB4.BackgroundImage = null;
                chessBoard[1, 3] = 0;
            }
            if (chessBoard[2, 3] == 15)
            {
                pictureBoxC4.BackgroundImage = null;
                chessBoard[2, 3] = 0;
            }
            if (chessBoard[3, 3] == 15)
            {
                pictureBoxD4.BackgroundImage = null;
                chessBoard[3, 3] = 0;
            }
            if (chessBoard[4, 3] == 15)
            {
                pictureBoxE4.BackgroundImage = null;
                chessBoard[4, 3] = 0;
            }
            if (chessBoard[5, 3] == 15)
            {
                pictureBoxF4.BackgroundImage = null;
                chessBoard[5, 3] = 0;
            }
            if (chessBoard[6, 3] == 15)
            {
                pictureBoxG4.BackgroundImage = null;
                chessBoard[6, 3] = 0;
            }
            if (chessBoard[7, 3] == 15)
            {
                pictureBoxH4.BackgroundImage = null;
                chessBoard[7, 3] = 0;
            }
            if (chessBoard[0, 4] == 15)
            {
                pictureBoxA5.BackgroundImage = null;
                chessBoard[0, 4] = 0;
            }
            if (chessBoard[1, 4] == 15)
            {
                pictureBoxB5.BackgroundImage = null;
                chessBoard[1, 4] = 0;
            }
            if (chessBoard[2, 4] == 15)
            {
                pictureBoxC5.BackgroundImage = null;
                chessBoard[2, 4] = 0;
            }
            if (chessBoard[3, 4] == 15)
            {
                pictureBoxD5.BackgroundImage = null;
                chessBoard[3, 4] = 0;
            }
            if (chessBoard[4, 4] == 15)
            {
                pictureBoxE5.BackgroundImage = null;
                chessBoard[4, 4] = 0;
            }
            if (chessBoard[5, 4] == 15)
            {
                pictureBoxF5.BackgroundImage = null;
                chessBoard[5, 4] = 0;
            }
            if (chessBoard[6, 4] == 15)
            {
                pictureBoxG5.BackgroundImage = null;
                chessBoard[6, 4] = 0;
            }
            if (chessBoard[7, 4] == 15)
            {
                pictureBoxH5.BackgroundImage = null;
                chessBoard[7, 4] = 0;
            }
            if (chessBoard[0, 5] == 15)
            {
                pictureBoxA6.BackgroundImage = null;
                chessBoard[0, 5] = 0;
            }
            if (chessBoard[1, 5] == 15)
            {
                pictureBoxB6.BackgroundImage = null;
                chessBoard[1, 5] = 0;
            }
            if (chessBoard[2, 5] == 15)
            {
                pictureBoxC6.BackgroundImage = null;
                chessBoard[2, 5] = 0;
            }
            if (chessBoard[3, 5] == 15)
            {
                pictureBoxD6.BackgroundImage = null;
                chessBoard[3, 5] = 0;
            }
            if (chessBoard[4, 5] == 15)
            {
                pictureBoxE6.BackgroundImage = null;
                chessBoard[4, 5] = 0;
            }
            if (chessBoard[5, 5] == 15)
            {
                pictureBoxF6.BackgroundImage = null;
                chessBoard[5, 5] = 0;
            }
            if (chessBoard[6, 5] == 15)
            {
                pictureBoxG6.BackgroundImage = null;
                chessBoard[6, 5] = 0;
            }
            if (chessBoard[7, 5] == 15)
            {
                pictureBoxH6.BackgroundImage = null;
                chessBoard[7, 5] = 0;
            }
            if (chessBoard[0, 6] == 15)
            {
                pictureBoxA7.BackgroundImage = null;
                chessBoard[0, 6] = 0;
            }
            if (chessBoard[1, 6] == 15)
            {
                pictureBoxB7.BackgroundImage = null;
                chessBoard[1, 6] = 0;
            }
            if (chessBoard[2, 6] == 15)
            {
                pictureBoxC7.BackgroundImage = null;
                chessBoard[2, 6] = 0;
            }
            if (chessBoard[3, 6] == 15)
            {
                pictureBoxD7.BackgroundImage = null;
                chessBoard[3, 6] = 0;
            }
            if (chessBoard[4, 6] == 15)
            {
                pictureBoxE7.BackgroundImage = null;
                chessBoard[4, 6] = 0;
            }
            if (chessBoard[5, 6] == 15)
            {
                pictureBoxF7.BackgroundImage = null;
                chessBoard[5, 6] = 0;
            }
            if (chessBoard[6, 6] == 15)
            {
                pictureBoxG7.BackgroundImage = null;
                chessBoard[6, 6] = 0;
            }
            if (chessBoard[7, 6] == 15)
            {
                pictureBoxH7.BackgroundImage = null;
                chessBoard[7, 6] = 0;
            }
            if (chessBoard[0, 7] == 15)
            {
                pictureBoxA8.BackgroundImage = null;
                chessBoard[0, 7] = 0;
            }
            if (chessBoard[1, 7] == 15)
            {
                pictureBoxB8.BackgroundImage = null;
                chessBoard[1, 7] = 0;
            }
            if (chessBoard[2, 7] == 15)
            {
                pictureBoxC8.BackgroundImage = null;
                chessBoard[2, 7] = 0;
            }
            if (chessBoard[3, 7] == 15)
            {
                pictureBoxD8.BackgroundImage = null;
                chessBoard[3, 7] = 0;
            }
            if (chessBoard[4, 7] == 15)
            {
                pictureBoxE8.BackgroundImage = null;
                chessBoard[4, 7] = 0;
            }
            if (chessBoard[5, 7] == 15)
            {
                pictureBoxF8.BackgroundImage = null;
                chessBoard[5, 7] = 0;
            }
            if (chessBoard[6, 7] == 15)
            {
                pictureBoxG8.BackgroundImage = null;
                chessBoard[6, 7] = 0;
            }
            if (chessBoard[7, 7] == 15)
            {
                pictureBoxH8.BackgroundImage = null;
                chessBoard[7, 7] = 0;
            }

        }
        public void drawDots()
        {
            if (chessBoard[0, 0] == 15)
            {
                pictureBoxA1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 0] =15;
            }
            if (chessBoard[1, 0] == 15)
            {
                pictureBoxB1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 0] =15;
            }
            if (chessBoard[2, 0] == 15)
            {
                pictureBoxC1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 0] =15;
            }
            if (chessBoard[3, 0] == 15)
            {
                pictureBoxD1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 0] =15;
            }
            if (chessBoard[4, 0] == 15)
            {
                pictureBoxE1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 0] =15;
            }
            if (chessBoard[5, 0] == 15)
            {
                pictureBoxF1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 0] =15;
            }
            if (chessBoard[6, 0] == 15)
            {
                pictureBoxG1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 0] =15;
            }
            if (chessBoard[7, 0] == 15)
            {
                pictureBoxH1.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 0] =15;
            }
            if (chessBoard[0, 1] == 15)
            {
                pictureBoxA2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 1] =15;
            }
            if (chessBoard[1, 1] == 15)
            {
                pictureBoxB2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 1] =15;
            }
            if (chessBoard[2, 1] == 15)
            {
                pictureBoxC2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 1] =15;
            }
            if (chessBoard[3, 1] == 15)
            {
                pictureBoxD2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 1] =15;
            }
            if (chessBoard[4, 1] == 15)
            {
                pictureBoxE2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 1] =15;
            }
            if (chessBoard[5, 1] == 15)
            {
                pictureBoxF2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 1] =15;
            }
            if (chessBoard[6, 1] == 15)
            {
                pictureBoxG2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 1] =15;
            }
            if (chessBoard[7, 1] == 15)
            {
                pictureBoxH2.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 1] =15;
            }
            if (chessBoard[0, 2] == 15)
            {
                pictureBoxA3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 2] =15;
            }
            if (chessBoard[1, 2] == 15)
            {
                pictureBoxB3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 2] =15;
            }
            if (chessBoard[2, 2] == 15)
            {
                pictureBoxC3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 2] =15;
            }
            if (chessBoard[3, 2] == 15)
            {
                pictureBoxD3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 2] =15;
            }
            if (chessBoard[4, 2] == 15)
            {
                pictureBoxE3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 2] =15;
            }
            if (chessBoard[5, 2] == 15)
            {
                pictureBoxF3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 2] =15;
            }
            if (chessBoard[6, 2] == 15)
            {
                pictureBoxG3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 2] =15;
            }
            if (chessBoard[7, 2] == 15)
            {
                pictureBoxH3.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 2] =15;
            }
            if (chessBoard[0, 3] == 15)
            {
                pictureBoxA4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 3] =15;
            }
            if (chessBoard[1, 3] == 15)
            {
                pictureBoxB4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 3] =15;
            }
            if (chessBoard[2, 3] == 15)
            {
                pictureBoxC4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 3] =15;
            }
            if (chessBoard[3, 3] == 15)
            {
                pictureBoxD4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 3] =15;
            }
            if (chessBoard[4, 3] == 15)
            {
                pictureBoxE4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 3] =15;
            }
            if (chessBoard[5, 3] == 15)
            {
                pictureBoxF4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 3] =15;
            }
            if (chessBoard[6, 3] == 15)
            {
                pictureBoxG4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 3] =15;
            }
            if (chessBoard[7, 3] == 15)
            {
                pictureBoxH4.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 3] =15;
            }
            if (chessBoard[0, 4] == 15)
            {
                pictureBoxA5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 4] =15;
            }
            if (chessBoard[1, 4] == 15)
            {
                pictureBoxB5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 4] =15;
            }
            if (chessBoard[2, 4] == 15)
            {
                pictureBoxC5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 4] =15;
            }
            if (chessBoard[3, 4] == 15)
            {
                pictureBoxD5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 4] =15;
            }
            if (chessBoard[4, 4] == 15)
            {
                pictureBoxE5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 4] =15;
            }
            if (chessBoard[5, 4] == 15)
            {
                pictureBoxF5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 4] =15;
            }
            if (chessBoard[6, 4] == 15)
            {
                pictureBoxG5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 4] =15;
            }
            if (chessBoard[7, 4] == 15)
            {
                pictureBoxH5.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 4] =15;
            }
            if (chessBoard[0, 5] == 15)
            {
                pictureBoxA6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 5] =15;
            }
            if (chessBoard[1, 5] == 15)
            {
                pictureBoxB6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 5] =15;
            }
            if (chessBoard[2, 5] == 15)
            {
                pictureBoxC6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 5] =15;
            }
            if (chessBoard[3, 5] == 15)
            {
                pictureBoxD6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 5] =15;
            }
            if (chessBoard[4, 5] == 15)
            {
                pictureBoxE6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 5] =15;
            }
            if (chessBoard[5, 5] == 15)
            {
                pictureBoxF6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 5] =15;
            }
            if (chessBoard[6, 5] == 15)
            {
                pictureBoxG6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 5] =15;
            }
            if (chessBoard[7, 5] == 15)
            {
                pictureBoxH6.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 5] =15;
            }
            if (chessBoard[0, 6] == 15)
            {
                pictureBoxA7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 6] =15;
            }
            if (chessBoard[1, 6] == 15)
            {
                pictureBoxB7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 6] =15;
            }
            if (chessBoard[2, 6] == 15)
            {
                pictureBoxC7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 6] =15;
            }
            if (chessBoard[3, 6] == 15)
            {
                pictureBoxD7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 6] =15;
            }
            if (chessBoard[4, 6] == 15)
            {
                pictureBoxE7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 6] =15;
            }
            if (chessBoard[5, 6] == 15)
            {
                pictureBoxF7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 6] =15;
            }
            if (chessBoard[6, 6] == 15)
            {
                pictureBoxG7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 6] =15;
            }
            if (chessBoard[7, 6] == 15)
            {
                pictureBoxH7.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 6] =15;
            }
            if (chessBoard[0, 7] == 15)
            {
                pictureBoxA8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[0, 7] =15;
            }
            if (chessBoard[1, 7] == 15)
            {
                pictureBoxB8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[1, 7] =15;
            }
            if (chessBoard[2, 7] == 15)
            {
                pictureBoxC8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[2, 7] =15;
            }
            if (chessBoard[3, 7] == 15)
            {
                pictureBoxD8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[3, 7] =15;
            }
            if (chessBoard[4, 7] == 15)
            {
                pictureBoxE8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[4, 7] =15;
            }
            if (chessBoard[5, 7] == 15)
            {
                pictureBoxF8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[5, 7] =15;
            }
            if (chessBoard[6, 7] == 15)
            {
                pictureBoxG8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[6, 7] =15;
            }
            if (chessBoard[7, 7] == 15)
            {
                pictureBoxH8.BackgroundImage = Properties.Resources.Dot;
                chessBoard[7, 7] =15;
            }
        }

        public void checkCastle(int x, int y)
        {
            if(ifCheckWhite(returnXWhite2(),returnYWhite2(),true))
            {
                kingPB[0].BackgroundImage = Properties.Resources.KrolBSzach;
                canCastleWhite = false;
                canCastleWhiteShort = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (chessBoard[i, k] == 1)
                        {
                            findForPawn(i, k);
                        }
                        else if (chessBoard[i, k] == 2)
                        {
                            findForRook(i, k);
                        }
                        else if (chessBoard[i, k] == 3)
                        {
                            findForHorse(i, k);
                        }
                        else if (chessBoard[i, k] == 4)
                        {
                            findForBishop(i, k);
                        }
                        else if (chessBoard[i, k] == 5)
                        {
                            findForQueen(i, k);
                        }
                        else if (chessBoard[i, k] == 6)
                        {
                            findForKing(i, k);
                        }
                    }
                }
                if (!isThereMove())
                {

                    //koniec gry... koniec!
                    Pionki.result = 2;
                    GameOver gameOver = new GameOver();
                    gameOver.Show();
                    this.Enabled = false;
                }
                else
                {
                    resetMoves();
                    deleteDots();
                }
            }
            else
            {
                kingPB[0].BackgroundImage = Properties.Resources.KrolB;
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (chessBoard[i, k] == 1)
                        {
                            findForPawn(i, k);
                        }
                        else if (chessBoard[i, k] == 2)
                        {
                            findForRook(i, k);
                        }
                        else if (chessBoard[i, k] == 3)
                        {
                            findForHorse(i, k);
                        }
                        else if (chessBoard[i, k] == 4)
                        {
                            findForBishop(i, k);
                        }
                        else if (chessBoard[i, k] == 5)
                        {
                            findForQueen(i, k);
                        }
                        else if (chessBoard[i, k] == 6)
                        {
                            findForKing(i, k);
                        }
                    }
                }
                if (!isThereMove())
                {

                    //koniec gry... koniec!
                    Pionki.result = 0;
                    GameOver gameOver = new GameOver();
                    gameOver.Show();
                    this.Enabled = false;
                }
                else
                {
                    resetMoves();
                    deleteDots();
                }

            }
            if (ifCheckBlack(returnXBlack2(), returnYBlack2(),true))
            {
                kingPB[1].BackgroundImage = Properties.Resources.KrolCSzach;
                canCastleBlack = false;
                canCastleBlackShort = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (chessBoard[i, k] == 8)
                        {
                            findForPawn(i, k);
                        }
                        else if (chessBoard[i, k] == 9)
                        {
                            findForRook(i, k);
                        }
                        else if (chessBoard[i, k] == 10)
                        {
                            findForHorse(i, k);
                        }
                        else if (chessBoard[i, k] == 11)
                        {
                            findForBishop(i, k);
                        }
                        else if (chessBoard[i, k] == 12)
                        {
                            findForQueen(i, k);
                        }
                        else if (chessBoard[i, k] == 13)
                        {
                            findForKing(i, k);
                        }
                    }
                }
                if (!isThereMove())
                {
                    //koniec
                    Pionki.result = 1;
                    GameOver gameOver = new GameOver();
                    gameOver.Show();
                    this.Enabled = false;
                }
                else
                {
                    deleteDots();
                    resetMoves();
                }
            }
            else
            {
                kingPB[1].BackgroundImage = Properties.Resources.KrolC;
                for (int i = 0; i < 8; i++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (chessBoard[i, k] == 8)
                        {
                            findForPawn(i, k);
                        }
                        else if (chessBoard[i, k] == 9)
                        {
                            findForRook(i, k);
                        }
                        else if (chessBoard[i, k] == 10)
                        {
                            findForHorse(i, k);
                        }
                        else if (chessBoard[i, k] == 11)
                        {
                            findForBishop(i, k);
                        }
                        else if (chessBoard[i, k] == 12)
                        {
                            findForQueen(i, k);
                        }
                        else if (chessBoard[i, k] == 13)
                        {
                            findForKing(i, k);
                        }
                    }
                }
                if (!isThereMove())
                {
                    //koniec
                    Pionki.result = 1;
                    GameOver gameOver = new GameOver();
                    gameOver.Show();
                    this.Enabled = false;
                }
                else
                {
                    deleteDots();
                    resetMoves();
                }
            }
        }
        public void pressPicture(int x, int y, PictureBox picture)
        {
            if (picture.Tag.ToString().Length == 0 && ifClicked) //gracz rusza pionek na puste pole
            {
                if (possibleMoves[x,y])
                {
                    //promocja na puste biała
                    if(clickedOne.Tag.ToString()=="PB"&&y==7)
                    {
                        Pionki.whiteOrBlack = 0;
                        PromotionPanel panel = new PromotionPanel();
                        panel.ShowDialog();
                        chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                        chessBoard[x, y] = Pionki.promoted;
                        clickedTwo.Tag = "";
                        if(Pionki.promoted==2)
                        {
                            picture.BackgroundImage = Properties.Resources.WiezaB;
                            picture.Tag = "WB";
                        }
                        else if(Pionki.promoted==3)
                        {
                            picture.BackgroundImage = Properties.Resources.KonB;
                            picture.Tag = "SB";
                        }
                        else if(Pionki.promoted==4)
                        {
                            picture.BackgroundImage = Properties.Resources.GoniecB;
                            picture.Tag = "GB";
                        }
                        else if(Pionki.promoted==5)
                        {
                            picture.BackgroundImage = Properties.Resources.KrolowaB;
                            picture.Tag = "QB";
                        }
                        
                        clickedOne.BackgroundImage = null;
                        clickedOne.Tag = "";
                        //clickedOne = null;
                        resetMoves();
                        deleteDots();
                        if (isWhiteMove)
                        {
                            isWhiteMove = false;
                        }
                        else
                        {
                            isWhiteMove = true;
                        }
                        ifClicked = false;
                        updateChessboard(x, y, picture);
                        updateChessboard(xyPionkaMoved[0], xyPionkaMoved[1], clickedTwo);
                        checkCastle(x,y);
                        resetEnPassant();
                        return;
                    }
                    //promocja na puste czarna
                    if (clickedOne.Tag.ToString() == "PC" && y == 0)
                    {
                        Pionki.whiteOrBlack = 1;
                        PromotionPanel panel = new PromotionPanel();
                        panel.ShowDialog();
                        chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                        chessBoard[x, y] = Pionki.promoted;
                        clickedTwo.Tag = "";
                        clickedTwo.BackgroundImage = null;
                        if (Pionki.promoted == 9)
                        {
                            picture.BackgroundImage = Properties.Resources.WiezaC;
                            picture.Tag = "WC";
                        }
                        else if (Pionki.promoted == 10)
                        {
                            picture.BackgroundImage = Properties.Resources.KonC;
                            picture.Tag = "SC";
                        }
                        else if (Pionki.promoted == 11)
                        {
                            picture.BackgroundImage = Properties.Resources.GoniecC;
                            picture.Tag = "GC";
                        }
                        else if (Pionki.promoted == 12)
                        {
                            picture.BackgroundImage = Properties.Resources.KrolowaC;
                            picture.Tag = "QC";
                        }

                        clickedOne.BackgroundImage = null;
                        clickedOne.Tag = "";
                        //clickedOne = null;
                        resetMoves();
                        deleteDots();
                        if (isWhiteMove)
                        {
                            isWhiteMove = false;
                        }
                        else
                        {
                            isWhiteMove = true;
                        }
                        ifClicked = false;
                        updateChessboard(x, y, picture);
                        updateChessboard(xyPionkaMoved[0], xyPionkaMoved[1], clickedTwo);
                        checkCastle(x,y);
                        resetEnPassant();
                        return;
                    }
                    picture.BackgroundImage = clickedOne.BackgroundImage;
                    picture.Tag = clickedOne.Tag;
                    chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                    clickedTwo.Tag = "";
                    if (clickedOne.Tag.ToString()[1] !='P')
                    {
                        resetEnPassant();
                    }
                    if(clickedOne.Tag.ToString()=="PB")
                    {
                        chessBoard[x, y] = 1;
                        if (xyPionkaMoved[1]+2==y)
                        {
                            enPassant[x, 0] = true;
                        }
                        else
                        {
                            resetEnPassant();
                        }
                        if (xyPionkaMoved[0]+1==x)
                        {
                            if(x==1)
                            {
                                pictureBoxB5.BackgroundImage = null;
                                pictureBoxB5.Tag = "";
                                chessBoard[1, 4] = 0;
                            }
                            else if(x==2)
                            {
                                pictureBoxC5.BackgroundImage = null;
                                pictureBoxC5.Tag = "";
                                chessBoard[2, 4] = 0;
                            }
                            else if(x==3)
                            {
                                pictureBoxD5.BackgroundImage = null;
                                pictureBoxD5.Tag = "";
                                chessBoard[3, 4] = 0;
                            }
                            else if (x == 4)
                            {
                                pictureBoxE5.BackgroundImage = null;
                                pictureBoxE5.Tag = "";
                                chessBoard[4, 4] = 0;
                            }
                            else if (x == 5)
                            {
                                pictureBoxF5.BackgroundImage = null;
                                pictureBoxF5.Tag = "";
                                chessBoard[5, 4] = 0;
                            }
                            else if (x == 6)
                            {
                                pictureBoxG5.BackgroundImage = null;
                                pictureBoxG5.Tag = "";
                                chessBoard[6, 4] = 0;
                            }
                            else if (x == 7)
                            {
                                pictureBoxH5.BackgroundImage = null;
                                pictureBoxH5.Tag = "";
                                chessBoard[7, 4] = 0;
                            }
                        }
                        else if (xyPionkaMoved[0]-1==x)
                        {
                            if (x == 1)
                            {
                                pictureBoxB5.BackgroundImage = null;
                                pictureBoxB5.Tag = "";
                                chessBoard[1, 4] = 0;
                            }
                            else if(x==0)
                            {
                                pictureBoxA5.BackgroundImage = null;
                                pictureBoxA5.Tag = "";
                                chessBoard[0, 4] = 0;
                            }
                            else if (x == 2)
                            {
                                pictureBoxC5.BackgroundImage = null;
                                pictureBoxC5.Tag = "";
                                chessBoard[2, 4] = 0;
                            }
                            else if (x == 3)
                            {
                                pictureBoxD5.BackgroundImage = null;
                                pictureBoxD5.Tag = "";
                                chessBoard[3, 4] = 0;
                            }
                            else if (x == 4)
                            {
                                pictureBoxE5.BackgroundImage = null;
                                pictureBoxE5.Tag = "";
                                chessBoard[4, 4] = 0;
                            }
                            else if (x == 5)
                            {
                                pictureBoxF5.BackgroundImage = null;
                                pictureBoxF5.Tag = "";
                                chessBoard[5, 4] = 0;
                            }
                            else if (x == 6)
                            {
                                pictureBoxG5.BackgroundImage = null;
                                pictureBoxG5.Tag = "";
                                chessBoard[6, 4] = 0;
                            }
                        }
                    }
                    else if(clickedOne.Tag.ToString() == "WB")
                    {
                        chessBoard[x, y] = 2;
                        if (xyPionkaMoved[0]==0&& xyPionkaMoved[1]==0)
                        {
                            canCastleWhite = false;
                        }
                        else if (xyPionkaMoved[0] == 7 && xyPionkaMoved[1]==0)
                        {
                            canCastleWhiteShort = false;
                        }
                    }
                    else if (clickedOne.Tag.ToString() == "SB")
                    {
                        chessBoard[x, y] = 3;
                    }
                    else if (clickedOne.Tag.ToString() == "GB")
                    {
                        chessBoard[x, y] = 4;
                    }
                    else if (clickedOne.Tag.ToString() == "QB")
                    {
                        chessBoard[x, y] = 5;
                    }
                    else if (clickedOne.Tag.ToString() == "KB")
                    {
                        chessBoard[x, y] = 6;
                        kingPB[0] = picture;
                        canCastleWhite = false;
                        canCastleWhiteShort = false;
                    }
                    else if (clickedOne.Tag.ToString() == "PC")
                    {
                        chessBoard[x, y] = 8;
                        if (xyPionkaMoved[1]-2==y)
                        {
                            enPassant[x, 1] = true;
                        }
                        if (xyPionkaMoved[0] + 1 == x)
                        {
                            if (x == 1)
                            {
                                pictureBoxB4.BackgroundImage = null;
                                pictureBoxB4.Tag = "";
                                chessBoard[1, 3] = 0;
                            }
                            else if (x == 2)
                            {
                                pictureBoxC4.BackgroundImage = null;
                                pictureBoxC4.Tag = "";
                                chessBoard[2, 3] = 0;
                            }
                            else if (x == 3)
                            {
                                pictureBoxD4.BackgroundImage = null;
                                pictureBoxD4.Tag = "";
                                chessBoard[3, 3] = 0;
                            }
                            else if (x == 4)
                            {
                                pictureBoxE4.BackgroundImage = null;
                                pictureBoxE4.Tag = "";
                                chessBoard[4, 3] = 0;
                            }
                            else if (x == 5)
                            {
                                pictureBoxF4.BackgroundImage = null;
                                pictureBoxF4.Tag = "";
                                chessBoard[5, 3] = 0;
                            }
                            else if (x == 6)
                            {
                                pictureBoxG4.BackgroundImage = null;
                                pictureBoxG4.Tag = "";
                                chessBoard[6, 3] = 0;
                            }
                            else if (x == 7)
                            {
                                pictureBoxH4.BackgroundImage = null;
                                pictureBoxH4.Tag = "";
                                chessBoard[7, 3] = 0;
                            }
                        }
                        else if (xyPionkaMoved[0] -  1 == x)
                        {
                            if (x == 0)
                            {
                                pictureBoxA4.BackgroundImage = null;
                                pictureBoxA4.Tag = "";
                                chessBoard[0, 3] = 0;
                            }
                            else if (x == 1)
                            {
                                pictureBoxB4.BackgroundImage = null;
                                pictureBoxB4.Tag = "";
                                chessBoard[1, 3] = 0;
                            }
                            else if (x == 2)
                            {
                                pictureBoxC4.BackgroundImage = null;
                                pictureBoxC4.Tag = "";
                                chessBoard[2, 3] = 0;
                            }
                            else if (x == 3)
                            {
                                pictureBoxD4.BackgroundImage = null;
                                pictureBoxD4.Tag = "";
                                chessBoard[3, 3] = 0;
                            }
                            else if (x == 4)
                            {
                                pictureBoxE4.BackgroundImage = null;
                                pictureBoxE4.Tag = "";
                                chessBoard[4, 3] = 0;
                            }
                            else if (x == 5)
                            {
                                pictureBoxF4.BackgroundImage = null;
                                pictureBoxF4.Tag = "";
                                chessBoard[5, 3] = 0;
                            }
                            else if (x == 6)
                            {
                                pictureBoxG4.BackgroundImage = null;
                                pictureBoxG4.Tag = "";
                                chessBoard[6, 3] = 0;
                            }
                        }
                    }
                    else if (clickedOne.Tag.ToString() == "WC")
                    {
                        chessBoard[x, y] = 9;
                        if (xyPionkaMoved[0] == 0 && xyPionkaMoved[1] == 7)
                        {
                            canCastleBlack = false;
                        }
                        else if (xyPionkaMoved[0] == 7 && xyPionkaMoved[1] == 7)
                        {
                            canCastleBlackShort = false;
                        }
                    }
                    else if (clickedOne.Tag.ToString() == "SC")
                    {
                        chessBoard[x, y] = 10;
                    }
                    else if (clickedOne.Tag.ToString() == "GC")
                    {
                        chessBoard[x, y] = 11;
                    }
                    else if (clickedOne.Tag.ToString() == "QC")
                    {
                        chessBoard[x, y] = 12;
                    }
                    else if (clickedOne.Tag.ToString() == "KC")
                    {
                        chessBoard[x, y] = 13;
                        kingPB[1]=picture;
                        canCastleBlack = false;
                        canCastleBlackShort = false;
                    }
                    clickedOne.BackgroundImage=null;
                    clickedOne.Tag = "";
                    //clickedOne = null;
                    resetMoves();
                    deleteDots();
                    if (isWhiteMove)
                    {
                        isWhiteMove = false;
                    }
                    else
                    {
                        isWhiteMove = true;
                    }
                    ifClicked = false;
                    checkCastle(x, y);
                    //resetEnPassant();
                    
                }
            }
            else if (picture.Tag.ToString().Length == 0 && !ifClicked) //gracz klika puste pole po prostu
            {
                return;
            }
            else if (picture.Tag.ToString().Length != 0)
            {
                if (!ifClicked && isWhiteMove && picture.Tag.ToString()[1] == 'B') //wybiera bialy wlasny pionek
                {
                    clonePB(clickedOne, picture);
                    clickedTwo = picture;
                    xyPionkaMoved[0] = x;
                    xyPionkaMoved[1] = y;
                    if (picture.Tag.ToString()[0] == 'P')
                    {
                        findForPawn(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'W')
                    {
                        findForRook(x,y);
                        //pictureBoxA5.BackgroundImage=picture.BackgroundImage;
                    }
                    else if (picture.Tag.ToString()[0] == 'S')
                    {
                        findForHorse(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'G')
                    {
                        findForBishop(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'Q')
                    {
                        findForQueen(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'K')
                    {
                        findForKing(x,y);
                    }
                    ifClicked = true;
                }
                else if (!ifClicked && !isWhiteMove && picture.Tag.ToString()[1] == 'C') //czarny wybiera wlasny pionek
                {
                    clonePB(clickedOne, picture);
                    clickedTwo = picture;
                    xyPionkaMoved[0] = x;
                    xyPionkaMoved[1] = y;
                    if (picture.Tag.ToString()[0] == 'P')
                    {
                        findForPawn(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'W')
                    {
                        findForRook(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'S')
                    {
                        findForHorse(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'G')
                    {
                        findForBishop(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'Q')
                    {
                        findForQueen(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'K')
                    {
                        findForKing(x,y);
                    }
                    ifClicked = true;
                }
                else if (ifClicked && isWhiteMove && picture.Tag.ToString()[1] == 'B') //zmienia bialy wybrany pionek
                {
                    deleteDots();
                    resetMoves();
                    xyPionkaMoved[0] = x;
                    xyPionkaMoved[1] = y;
                    if (clickedOne.Tag.ToString() == "KB")
                    {
                        if (roszada(x, y))
                        {
                            isWhiteMove = false;
                            ifClicked = false;
                            canCastleWhiteShort = false;
                            canCastleWhite = false;
                            return;
                        }
                    }
                    clonePB(clickedOne, picture);
                    clickedTwo = picture;
                    if (picture.Tag.ToString()[0] == 'P')
                    {
                        findForPawn(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'W')
                    {
                        
                        
                            findForRook(x,y);
                        

                    }
                    else if (picture.Tag.ToString()[0] == 'S')
                    {
                        findForHorse(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'G')
                    {
                        findForBishop(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'Q')
                    {
                        findForQueen(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'K')
                    {
                        findForKing(x,y);
                    }
                }
                else if (ifClicked && !isWhiteMove && picture.Tag.ToString()[1] == 'C') //zmienia pionek czarny
                {
                    deleteDots();
                    resetMoves();
                    xyPionkaMoved[0] = x;
                    xyPionkaMoved[1] = y;
                    if (clickedOne.Tag.ToString() == "KC")
                    {
                        if (roszada(x, y))
                        {
                            isWhiteMove = true;
                            ifClicked = false;
                            canCastleBlack = false;
                            canCastleBlackShort = false;
                            return;
                        }
                    }
                    clonePB(clickedOne, picture);
                    clickedTwo = picture;
                    if (picture.Tag.ToString()[0] == 'P')
                    {
                        findForPawn(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'W')
                    {

                            findForRook(x, y);
                        
                    }
                    else if (picture.Tag.ToString()[0] == 'S')
                    {
                        findForHorse(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'G')
                    {
                        findForBishop(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'Q')
                    {
                        findForQueen(x,y);
                    }
                    else if (picture.Tag.ToString()[0] == 'K')
                    {
                        findForKing(x,y);
                    }
                }
                else if (ifClicked && isWhiteMove && picture.Tag.ToString()[1] == 'C') //bialy capturuje
                {
                    if (possibleMoves[x,y])
                    {
                        if (clickedOne.Tag.ToString() == "PB" && y == 7)
                        {
                            Pionki.whiteOrBlack = 0;
                            PromotionPanel panel = new PromotionPanel();
                            panel.ShowDialog();
                            chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                            chessBoard[x, y] = Pionki.promoted;
                            clickedTwo.Tag = "";
                            if (Pionki.promoted == 2)
                            {
                                picture.BackgroundImage = Properties.Resources.WiezaB;
                                picture.Tag = "WB";
                            }
                            else if (Pionki.promoted == 3)
                            {
                                picture.BackgroundImage = Properties.Resources.KonB;
                                picture.Tag = "SB";
                            }
                            else if (Pionki.promoted == 4)
                            {
                                picture.BackgroundImage = Properties.Resources.GoniecB;
                                picture.Tag = "GB";
                            }
                            else if (Pionki.promoted == 5)
                            {
                                picture.BackgroundImage = Properties.Resources.KrolowaB;
                                picture.Tag = "QB";
                            }

                            clickedOne.BackgroundImage = null;
                            clickedOne.Tag = "";
                            //clickedOne = null;
                            resetMoves();
                            deleteDots();
                            if (isWhiteMove)
                            {
                                isWhiteMove = false;
                            }
                            else
                            {
                                isWhiteMove = true;
                            }
                            ifClicked = false;
                            updateChessboard(x, y, picture);
                            updateChessboard(xyPionkaMoved[0], xyPionkaMoved[1], clickedTwo);
                            checkCastle(x, y);
                            resetEnPassant();
                            return;
                        }
                        picture.BackgroundImage = clickedOne.BackgroundImage;
                        picture.Tag = clickedOne.Tag;
                        clickedTwo.BackgroundImage = null;
                        clickedTwo.Tag = "";
                        chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                        if (clickedOne.Tag.ToString() == "PB")
                        {
                            chessBoard[x, y] = 1;
                        }
                        else if (clickedOne.Tag.ToString() == "WB")
                        {
                            chessBoard[x, y] = 2;
                            if (xyPionkaMoved[0] == 0 && xyPionkaMoved[1] == 0)
                            {
                                canCastleWhite = false;
                            }
                            else if (xyPionkaMoved[0] == 7 && xyPionkaMoved[1] == 0)
                            {
                                canCastleWhiteShort = false;
                            }
                        }
                        else if (clickedOne.Tag.ToString() == "SB")
                        {
                            chessBoard[x, y] = 3;
                        }
                        else if (clickedOne.Tag.ToString() == "GB")
                        {
                            chessBoard[x, y] = 4;
                        }
                        else if (clickedOne.Tag.ToString() == "QB")
                        {
                            chessBoard[x, y] = 5;
                        }
                        else if (clickedOne.Tag.ToString() == "KB")
                        {
                            chessBoard[x, y] = 6;
                            kingPB[0] = picture;
                            canCastleWhite=false;
                            canCastleWhiteShort = false;
                        }
                        else if (clickedOne.Tag.ToString() == "PC")
                        {
                            chessBoard[x, y] = 8;
                        }
                        else if (clickedOne.Tag.ToString() == "WC")
                        {
                            chessBoard[x, y] = 9;
                            if (xyPionkaMoved[0] == 0 && xyPionkaMoved[1] == 7)
                            {
                                canCastleBlack = false;
                            }
                            else if (xyPionkaMoved[0] == 7 && xyPionkaMoved[1] == 7)
                            {
                                canCastleBlackShort = false;
                            }
                        }
                        else if (clickedOne.Tag.ToString() == "SC")
                        {
                            chessBoard[x, y] = 10;
                        }
                        else if (clickedOne.Tag.ToString() == "GC")
                        {
                            chessBoard[x, y] = 11;
                        }
                        else if (clickedOne.Tag.ToString() == "QC")
                        {
                            chessBoard[x, y] = 12;
                        }
                        else if (clickedOne.Tag.ToString() == "KC")
                        {
                            chessBoard[x, y] = 13;
                            kingPB[1] = picture;
                            canCastleBlack = false;
                            canCastleBlackShort = false;
                        }
                        clickedOne.BackgroundImage = null;
                        clickedOne.Tag = "";
                        resetMoves();
                        deleteDots();
                        isWhiteMove = false;
                        checkCastle(x, y);
                        resetEnPassant();
                    }

                }
                else if (ifClicked && !isWhiteMove && picture.Tag.ToString()[1] == 'B') //czarny capturuje
                {
                    if (possibleMoves[x,y])
                    {
                        if (clickedOne.Tag.ToString() == "PC" && y == 0)
                        {
                            Pionki.whiteOrBlack = 1;
                            PromotionPanel panel = new PromotionPanel();
                            panel.ShowDialog();
                            chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                            chessBoard[x, y] = Pionki.promoted;
                            clickedTwo.Tag = "";
                            clickedTwo.BackgroundImage = null;
                            if (Pionki.promoted == 9)
                            {
                                picture.BackgroundImage = Properties.Resources.WiezaC;
                                picture.Tag = "WC";
                            }
                            else if (Pionki.promoted == 10)
                            {
                                picture.BackgroundImage = Properties.Resources.KonC;
                                picture.Tag = "SC";
                            }
                            else if (Pionki.promoted == 11)
                            {
                                picture.BackgroundImage = Properties.Resources.GoniecC;
                                picture.Tag = "GC";
                            }
                            else if (Pionki.promoted == 12)
                            {
                                picture.BackgroundImage = Properties.Resources.KrolowaC;
                                picture.Tag = "QC";
                            }

                            clickedOne.BackgroundImage = null;
                            clickedOne.Tag = "";
                            //clickedOne = null;
                            resetMoves();
                            deleteDots();
                            if (isWhiteMove)
                            {
                                isWhiteMove = false;
                            }
                            else
                            {
                                isWhiteMove = true;
                            }
                            ifClicked = false;
                            updateChessboard(x, y, picture);
                            updateChessboard(xyPionkaMoved[0], xyPionkaMoved[1], clickedTwo);
                            checkCastle(x, y);
                            resetEnPassant();
                            return;
                        }
                        picture.BackgroundImage = clickedOne.BackgroundImage;
                        picture.Tag = clickedOne.Tag;
                        clickedTwo.BackgroundImage = null;
                        clickedTwo.Tag = "";
                        chessBoard[xyPionkaMoved[0], xyPionkaMoved[1]] = 0;
                        if (clickedOne.Tag.ToString() == "PB")
                        {
                            chessBoard[x, y] = 1;
                        }
                        else if (clickedOne.Tag.ToString() == "WB")
                        {
                            chessBoard[x, y] = 2;
                        }
                        else if (clickedOne.Tag.ToString() == "SB")
                        {
                            chessBoard[x, y] = 3;
                        }
                        else if (clickedOne.Tag.ToString() == "GB")
                        {
                            chessBoard[x, y] = 4;
                        }
                        else if (clickedOne.Tag.ToString() == "QB")
                        {
                            chessBoard[x, y] = 5;
                        }
                        else if (clickedOne.Tag.ToString() == "KB")
                        {
                            chessBoard[x, y] = 6;
                            kingPB[0] = picture;
                        }
                        else if (clickedOne.Tag.ToString() == "PC")
                        {
                            chessBoard[x, y] = 8;
                        }
                        else if (clickedOne.Tag.ToString() == "WC")
                        {
                            chessBoard[x, y] = 9;
                        }
                        else if (clickedOne.Tag.ToString() == "SC")
                        {
                            chessBoard[x, y] = 10;
                        }
                        else if (clickedOne.Tag.ToString() == "GC")
                        {
                            chessBoard[x, y] = 11;
                        }
                        else if (clickedOne.Tag.ToString() == "QC")
                        {
                            chessBoard[x, y] = 12;
                        }
                        else if (clickedOne.Tag.ToString() == "KC")
                        {
                            chessBoard[x, y] = 13;
                            kingPB[1] = picture;
                        }
                        deleteDots();
                        resetMoves();
                        clickedOne.BackgroundImage = null;
                        clickedOne.Tag = "";
                        isWhiteMove = true;
                        checkCastle(x, y);
                        resetEnPassant();
                    }

                }
            }
            updateChessboard(x, y, picture);
            updateChessboard(xyPionkaMoved[0], xyPionkaMoved[1], clickedTwo);
            
        }
        private void pictureBoxA1_Click(object sender, EventArgs e)
        {
            pressPicture(0, 0, pictureBoxA1);
        }

        private void pictureBoxA2_Click(object sender, EventArgs e)
        {
            pressPicture(0, 1, pictureBoxA2);
        }

        private void MainChessboard_Load(object sender, EventArgs e)
        {
            chessBoard[0, 0] = 2;
            chessBoard[1, 0] = 3;
            chessBoard[2, 0]= 4;
            chessBoard[3, 0] = 5;
            chessBoard[4, 0] = 6;
            chessBoard[7, 0] = 2;
            chessBoard[6, 0] = 3;
            chessBoard[5, 0] = 4;
            for(int i=0;i<8;i++)
            {
                chessBoard[i, 1] = 1;
                chessBoard[i, 6] = 8;
            }
            chessBoard[0, 7] = 9;
            chessBoard[1, 7] = 10;
            chessBoard[2, 7] = 11;
            chessBoard[3, 7] = 12;
            chessBoard[4, 7] = 13;
            chessBoard[7, 7] = 9;
            chessBoard[6, 7] = 10;
            chessBoard[5, 7] = 11;
            kingPB[0] = pictureBoxE1;
            kingPB[1] = pictureBoxE8;
            resetMoves();
            deleteDots();

        }

        private void pictureBoxA3_Click(object sender, EventArgs e)
        {
            pressPicture(0, 2, pictureBoxA3);
        }

        private void pictureBoxA4_Click(object sender, EventArgs e)
        {
            pressPicture(0, 3, pictureBoxA4);
        }

        private void pictureBoxB3_Click(object sender, EventArgs e)
        {
            pressPicture(1, 2, pictureBoxB3);
        }

        private void pictureBoxB7_Click(object sender, EventArgs e)
        {
            pressPicture(1, 6, pictureBoxB7);
        }

        private void pictureBoxB5_Click(object sender, EventArgs e)
        {
            pressPicture(1, 4, pictureBoxB5);
        }

        private void pictureBoxA7_Click(object sender, EventArgs e)
        {
            pressPicture(0, 6, pictureBoxA7);
        }

        private void pictureBoxB2_Click(object sender, EventArgs e)
        {
            pressPicture(1, 1, pictureBoxB2);
        }

        private void pictureBoxA5_Click(object sender, EventArgs e)
        {
            pressPicture(0, 4, pictureBoxA5);
        }

        private void pictureBoxA8_Click(object sender, EventArgs e)
        {
            pressPicture(0, 7, pictureBoxA8);
        }

        private void pictureBoxB4_Click(object sender, EventArgs e)
        {
            pressPicture(1, 3, pictureBoxB4);
        }

        private void pictureBoxA6_Click(object sender, EventArgs e)
        {
            pressPicture(0, 5, pictureBoxA6);
        }

        private void pictureBoxB6_Click(object sender, EventArgs e)
        {
            pressPicture(1, 5, pictureBoxB6);
        }

        private void pictureBoxB8_Click(object sender, EventArgs e)
        {
            pressPicture(1, 7, pictureBoxB8);
        }

        private void pictureBoxB1_Click(object sender, EventArgs e)
        {
            pressPicture(1, 0, pictureBoxB1);
        }

        private void pictureBoxC6_Click(object sender, EventArgs e)
        {
            pressPicture(2, 5, pictureBoxC6);
        }

        private void pictureBoxC3_Click(object sender, EventArgs e)
        {
            pressPicture(2, 2, pictureBoxC3);
        }

        private void pictureBoxD3_Click(object sender, EventArgs e)
        {
            pressPicture(3, 2, pictureBoxD3);
        }

        private void pictureBoxD6_Click(object sender, EventArgs e)
        {
            pressPicture(3, 5, pictureBoxD6);
        }

        private void pictureBoxE7_Click(object sender, EventArgs e)
        {
            pressPicture(4, 6, pictureBoxE7);
        }

        private void pictureBoxD2_Click(object sender, EventArgs e)
        {
            pressPicture(3, 1, pictureBoxD2);
        }

        private void pictureBoxD4_Click(object sender, EventArgs e)
        {
            pressPicture(3, 3, pictureBoxD4);
        }

        private void pictureBoxD5_Click(object sender, EventArgs e)
        {
            pressPicture(3,4, pictureBoxD5);
        }

        private void pictureBoxC4_Click(object sender, EventArgs e)
        {
            pressPicture(2, 3, pictureBoxC4);
        }

        private void pictureBoxC2_Click(object sender, EventArgs e)
        {
            pressPicture(2, 1, pictureBoxC2);
        }

        private void pictureBoxC5_Click(object sender, EventArgs e)
        {
            pressPicture(2, 4, pictureBoxC5);
        }

        private void pictureBoxD7_Click(object sender, EventArgs e)
        {
            pressPicture(3, 6, pictureBoxD7);
        }

        private void pictureBoxC1_Click(object sender, EventArgs e)
        {
            pressPicture(2,0,pictureBoxC1);
        }

        private void pictureBoxC7_Click(object sender, EventArgs e)
        {
            pressPicture(2, 6, pictureBoxC7);
        }

        private void pictureBoxE2_Click(object sender, EventArgs e)
        {
            pressPicture(4, 1, pictureBoxE2);
        }

        private void pictureBoxF1_Click(object sender, EventArgs e)
        {
            pressPicture(5, 0, pictureBoxF1);
        }

        private void pictureBoxE4_Click(object sender, EventArgs e)
        {
            pressPicture(4, 3, pictureBoxE4);
        }

        private void pictureBoxD1_Click(object sender, EventArgs e)
        {
            pressPicture(3, 0, pictureBoxD1);
        }

        private void pictureBoxE1_Click(object sender, EventArgs e)
        {
            pressPicture(4, 0, pictureBoxE1);
        }

        private void pictureBoxF2_Click(object sender, EventArgs e)
        {
            pressPicture(5, 1, pictureBoxF2);
        }

        private void pictureBoxF4_Click(object sender, EventArgs e)
        {
            pressPicture(5, 3, pictureBoxF4);
        }

        private void pictureBoxG1_Click(object sender, EventArgs e)
        {
            pressPicture(6, 0, pictureBoxG1);
        }

        private void pictureBoxH1_Click(object sender, EventArgs e)
        {
            pressPicture(7, 0, pictureBoxH1);
        }

        private void pictureBoxH2_Click(object sender, EventArgs e)
        {
            pressPicture(7, 1, pictureBoxH2);
        }

        private void pictureBoxG2_Click(object sender, EventArgs e)
        {
            pressPicture(6, 1, pictureBoxG2);
        }

        private void pictureBoxH3_Click(object sender, EventArgs e)
        {
            pressPicture(7, 2, pictureBoxH3);
        }

        private void pictureBoxG3_Click(object sender, EventArgs e)
        {
            pressPicture(6, 2, pictureBoxG3);
        }

        private void pictureBoxF3_Click(object sender, EventArgs e)
        {
            pressPicture(5, 2, pictureBoxF3);
        }

        private void pictureBoxE3_Click(object sender, EventArgs e)
        {
            pressPicture(4, 2, pictureBoxE3);
        }

        private void pictureBoxG4_Click(object sender, EventArgs e)
        {
            pressPicture(6, 3, pictureBoxG4);
        }

        private void pictureBoxH4_Click(object sender, EventArgs e)
        {
            pressPicture(7, 3, pictureBoxH4);
        }

        private void pictureBoxH5_Click(object sender, EventArgs e)
        {
            pressPicture(7, 4, pictureBoxH5);
        }

        private void pictureBoxG5_Click(object sender, EventArgs e)
        {
            pressPicture(6, 4, pictureBoxG5);
        }

        private void pictureBoxF5_Click(object sender, EventArgs e)
        {
            pressPicture(5, 4, pictureBoxF5);
        }

        private void pictureBoxE5_Click(object sender, EventArgs e)
        {
            pressPicture(4, 4, pictureBoxE5);
        }

        private void pictureBoxE6_Click(object sender, EventArgs e)
        {
            pressPicture(4, 5, pictureBoxE6);
        }

        private void pictureBoxF6_Click(object sender, EventArgs e)
        {
            pressPicture(5, 5, pictureBoxF6);
        }

        private void pictureBoxG6_Click(object sender, EventArgs e)
        {
            pressPicture(6, 5, pictureBoxG6);
        }

        private void pictureBoxH6_Click(object sender, EventArgs e)
        {
            pressPicture(7, 5, pictureBoxH6);
        }

        private void pictureBoxH7_Click(object sender, EventArgs e)
        {
            pressPicture(7, 6, pictureBoxH7);
        }

        private void pictureBoxG7_Click(object sender, EventArgs e)
        {
            pressPicture(6, 6, pictureBoxG7);
        }

        private void pictureBoxF7_Click(object sender, EventArgs e)
        {
            pressPicture(5, 6, pictureBoxF7);
        }

        private void pictureBoxH8_Click(object sender, EventArgs e)
        {
            pressPicture(7, 7, pictureBoxH8);
        }

        private void pictureBoxG8_Click(object sender, EventArgs e)
        {
            pressPicture(6, 7, pictureBoxG8);
        }

        private void pictureBoxF8_Click(object sender, EventArgs e)
        {
            pressPicture(5, 7, pictureBoxF8);
        }

        private void pictureBoxE8_Click(object sender, EventArgs e)
        {
            pressPicture(4, 7, pictureBoxE8);
        }

        private void pictureBoxD8_Click(object sender, EventArgs e)
        {
            pressPicture(3, 7, pictureBoxD8);
        }

        private void pictureBoxC8_Click(object sender, EventArgs e)
        {
            pressPicture(2, 7, pictureBoxC8);
        }
    }
}
