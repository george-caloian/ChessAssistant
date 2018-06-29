using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chess
{
    public partial class _Default : Page
    {
        public static List<string> tabla = new List<string>(65);

        //public static int score;

        public static List<int> atacAlb = new List<int>(65);
        public static List<int> atacNegru = new List<int>(65);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["enPassant"] = 0;
                ViewState["enPassantWhite"] = 0;
                ViewState["enPassantBlack"] = 0;

                Session["RightWhiteRook"] = 1;
                Session["LeftWhiteRook"] = 1;
                Session["WhiteKing"] = 1;

                Session["RightBlackRook"] = 1;
                Session["LeftBlackRook"] = 1;
                Session["BlackKing"] = 1;

                ViewState["player"] = 0;
                ViewState["Selected"] = 0;
                ViewState["imgURL"] = string.Empty;

                tabla.Add("gol");

                tabla.Add("br");
                tabla.Add("bn");
                tabla.Add("bb");
                tabla.Add("bq");
                tabla.Add("bk");
                tabla.Add("bb");
                tabla.Add("bn");
                tabla.Add("br");

                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");
                tabla.Add("bp");

                for (int i = 0; i < 32; i++)
                {
                    tabla.Add("gol");
                }

                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");
                tabla.Add("wp");

                tabla.Add("wr");
                tabla.Add("wn");
                tabla.Add("ww");
                tabla.Add("wq");
                tabla.Add("wk");
                tabla.Add("ww");
                tabla.Add("wn");
                tabla.Add("wr");

                for (int i = 0; i < 65; i++)
                {
                    atacAlb.Add(0);
                    atacNegru.Add(0);
                }
            }

        }

        public static ImageButton lastsender;

        public static void attackedFields()
        {

            for (int i = 0; i < 65; i++)
            {
                atacAlb[i] = 0;
                atacNegru[i] = 0;
            }

            for (int poz = 1; poz < 65; poz++)
            {
                if (tabla[poz].Contains('w'))
                {
                    if (tabla[poz] == "wp")
                    {
                        //captura dreapta
                        if (poz % 8 != 0 && (poz - 7 > 0))
                        {
                            atacAlb[poz - 7] = 1;
                        }

                        //captura stanga
                        if (poz % 8 != 1 && (poz - 9 > 0))
                        {
                            atacAlb[poz - 9] = 1;
                        }
                    }
                    if (tabla[poz] == "wn")
                    {
                        if (poz - 17 > 0)
                        {
                            atacAlb[poz - 17] = 1;
                        }
                        if (poz - 15 > 0)
                        {
                            atacAlb[poz - 15] = 1;
                        }
                        if (poz - 10 > 0)
                        {
                            atacAlb[poz - 10] = 1;
                        }
                        if (poz - 6 > 0)
                        {
                            atacAlb[poz - 6] = 1;
                        }
                        if (poz + 6 < 64)
                        {
                            atacAlb[poz + 6] = 1;
                        }
                        if (poz + 10 < 64)
                        {
                            atacAlb[poz + 10] = 1;
                        }
                        if (poz + 15 < 64)
                        {
                            atacAlb[poz + 15] = 1;
                        }
                        if (poz + 17 < 64)
                        {
                            atacAlb[poz + 17] = 1;
                        }
                    }
                    //nebun alb
                    if (tabla[poz] == "ww")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-7);
                        directii.Add(7);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -9)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }

                                }
                            }
                        }
                    }

                    //turn alb
                    if (tabla[poz] == "wr")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-8);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(8);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == -1)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //dama alba
                    if (tabla[poz] == "wq")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-8);
                        directii.Add(-7);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(7);
                        directii.Add(8);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -9)
                                {
                                    if(poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -1)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //rege alb
                    if (tabla[poz] == "wk")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-8);
                        directii.Add(-7);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(7);
                        directii.Add(8);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                            {
                                if (directie == -9)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('w'))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacAlb[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('b'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }

                if (tabla[poz].Contains('b'))
                {
                    if (tabla[poz] == "bp")
                    {
                        //captura dreapta
                        if (poz % 8 != 0 && (poz + 9 < 64))
                        {
                            atacNegru[poz + 9] = 1;
                        }

                        //captura stanga
                        if (poz % 8 != 1 && (poz + 7 < 64))
                        {
                            atacNegru[poz + 7] = 1;
                        }
                    }
                    if (tabla[poz] == "bn")
                    {
                        if (poz - 17 > 0)
                        {
                            atacNegru[poz - 17] = 1;
                        }
                        if (poz - 15 > 0)
                        {
                            atacNegru[poz - 15] = 1;
                        }
                        if (poz - 10 > 0)
                        {
                            atacNegru[poz - 10] = 1;
                        }
                        if (poz - 6 > 0)
                        {
                            atacNegru[poz - 6] = 1;
                        }
                        if (poz + 6 < 64)
                        {
                            atacNegru[poz + 6] = 1;
                        }
                        if (poz + 10 < 64)
                        {
                            atacNegru[poz + 10] = 1;
                        }
                        if (poz + 15 < 64)
                        {
                            atacNegru[poz + 15] = 1;
                        }
                        if (poz + 17 < 64)
                        {
                            atacNegru[poz + 17] = 1;
                        }
                    }
                    //nebun negru
                    if (tabla[poz] == "bb")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-7);
                        directii.Add(7);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -9)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }

                                }
                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //turn negru
                    if (tabla[poz] == "br")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-8);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(8);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == -1)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //dama neagra
                    if (tabla[poz] == "bq")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-8);
                        directii.Add(-7);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(7);
                        directii.Add(8);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 8; i++)
                            {
                                if (directie == -9)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -1)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //rege negru
                    if (tabla[poz] == "bk")
                    {
                        List<int> directii = new List<int>(4);
                        directii.Add(-9);
                        directii.Add(-8);
                        directii.Add(-7);
                        directii.Add(-1);
                        directii.Add(1);
                        directii.Add(7);
                        directii.Add(8);
                        directii.Add(9);

                        foreach (int directie in directii)
                        {
                            for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                            {
                                if (directie == -9)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -8)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz / 8 + 1 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -7)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - poz % 8) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == -1)
                                {
                                    if (poz + i * directie > 0 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie > 0)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                                else if (directie == 1)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 7)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i < poz % 8 || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 8)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }

                                else if (directie == 9)
                                {
                                    if (poz + i * directie < 65 && tabla[poz + i * directie].Contains('b'))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    if ((poz + i * directie < 65)
                                        && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                        && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                    {
                                        atacNegru[poz + i * directie] = 1;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                    if (tabla[poz + i * directie].Contains('w'))
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }

            }
        }

        public static string possibleMoves(Page page, int linie, int coloana, int WhiteEnPassantValue, int BlackEnPassantValue)
        {
            string x = string.Empty;
            int maxEval = -30000;
            int poz = (linie - 1) * 8 + coloana;

            int whiteking = (int)HttpContext.Current.Session["WhiteKing"];
            int whiteleftrook = (int)HttpContext.Current.Session["LeftWhiteRook"];
            int whiterightrook = (int)HttpContext.Current.Session["RightWhiteRook"];
            int blackking = (int)HttpContext.Current.Session["BlackKing"];
            int blackleftrook = (int)HttpContext.Current.Session["LeftBlackRook"];
            int blackrightrook = (int)HttpContext.Current.Session["RightBlackRook"];

            //pion alb
            if (tabla[poz] == "wp")
            {
                if (tabla[poz - 8] == "gol")
                {
                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 8] = "wp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);

                    int pozMutare = linie * 10 + coloana - 10;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    if(eval > maxEval)
                    {                        
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                    

                }
                if ((poz - 16) > 0 && tabla[poz - 16] == "gol" && tabla[poz - 8] == "gol" && linie == 7)
                {
                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 16] = "wp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);

                    int pozMutare = linie * 10 + coloana - 20;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    if (eval > maxEval)
                    {                        
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                }

                if (tabla[poz - 7].Contains('b'))
                {
                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 7] = "wp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);

                    int pozMutare = linie * 10 + coloana - 9;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (tabla[poz - 9].Contains('b'))
                {
                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 9] = "wp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);

                    int pozMutare = linie * 10 + coloana - 11;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //en Passant dreapta
                if (poz - 7 == BlackEnPassantValue - 8)
                {
                    int pozMutare = linie * 10 + coloana - 9;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 7] = "wp";
                    copie[poz + 1] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //en Passant stanga
                if (poz - 9 == BlackEnPassantValue - 8)
                {
                    int pozMutare = linie * 10 + coloana - 11;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 9] = "wp";
                    copie[poz - 1] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wp" + pozMutare + " Evaluare:" + eval;
                    }
                }               
            }
            //pion negru
            if (tabla[poz] == "bp")
            {
                if (tabla[poz + 8] == "gol")
                {
                    int pozMutare = linie * 10 + coloana + 10;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 8] = "bp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if ((poz + 16) < 65 && tabla[poz + 16] == "gol" && tabla[poz + 8] == "gol" && linie == 2)
                {
                    int pozMutare = linie * 10 + coloana + 20;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 16] = "bp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (tabla[poz + 7].Contains('w'))
                {
                    int pozMutare = linie * 10 + coloana + 9;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 7] = "bp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (tabla[poz + 9].Contains('w'))
                {
                    int pozMutare = linie * 10 + coloana + 11;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 9] = "bp";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //en Passant dreapta
                if (poz + 9 == WhiteEnPassantValue + 8)
                {
                    int pozMutare = linie * 10 + coloana + 11;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 9] = "bp";
                    copie[poz + 1] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //en Passant stanga
                if (poz + 7 == WhiteEnPassantValue + 8)
                {
                    int pozMutare = linie * 10 + coloana + 9;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 7] = "bp";
                    copie[poz - 1] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bp" + pozMutare + " Evaluare:" + eval;
                    }
                }

            }


            //cal alb
            if (tabla[poz] == "wn")
            {
                if (poz - 17 > 0 && (tabla[poz - 17] == "gol" || tabla[poz - 17].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana - 21;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 17] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 15 > 0 && (tabla[poz - 15] == "gol" || tabla[poz - 15].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana - 19;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 15] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 10 > 0 && (tabla[poz - 10] == "gol" || tabla[poz - 10].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana - 12;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 10] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 6 > 0 && (tabla[poz - 6] == "gol" || tabla[poz - 6].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana - 8;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 6] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }

                if (poz + 17 < 65 && (tabla[poz + 17] == "gol" || tabla[poz + 17].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana + 21;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 17] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 15 < 65 && (tabla[poz + 15] == "gol" || tabla[poz + 15].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana + 19;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 15] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 10 < 65 && (tabla[poz + 10] == "gol" || tabla[poz + 10].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana + 12;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 10] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 6 < 65 && (tabla[poz + 6] == "gol" || tabla[poz + 6].Contains('b')))
                {
                    int pozMutare = linie * 10 + coloana + 8;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 6] = "wn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wn" + pozMutare + " Evaluare:" + eval;
                    }
                }

            }

            //cal negru
            if (tabla[poz] == "bn")
            {
                if (poz - 17 > 0 && (tabla[poz - 17] == "gol" || tabla[poz - 17].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana - 21;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 17] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 15 > 0 && (tabla[poz - 15] == "gol" || tabla[poz - 15].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana - 19;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 15] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 10 > 0 && (tabla[poz - 10] == "gol" || tabla[poz - 10].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana - 12;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 10] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz - 6 > 0 && (tabla[poz - 6] == "gol" || tabla[poz - 6].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana - 8;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 6] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }

                if (poz + 17 < 65 && (tabla[poz + 17] == "gol" || tabla[poz + 17].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana + 21;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 17] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 15 < 65 && (tabla[poz + 15] == "gol" || tabla[poz + 15].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana + 19;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 15] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 10 < 65 && (tabla[poz + 10] == "gol" || tabla[poz + 10].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana + 12;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 10] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }
                if (poz + 6 < 65 && (tabla[poz + 6] == "gol" || tabla[poz + 6].Contains('w')))
                {
                    int pozMutare = linie * 10 + coloana + 8;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 6] = "bn";
                    copie[poz] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bn" + pozMutare + " Evaluare:" + eval;
                    }
                }

            }



            //nebun alb
            if (tabla[poz] == "ww")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-7);
                directii.Add(7);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "ww";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "ww" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "ww";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "ww" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "ww";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "ww" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "ww";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "ww" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //nebun negru
            if (tabla[poz] == "bb")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-7);
                directii.Add(7);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bb";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bb" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bb";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bb" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bb";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bb" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bb";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bb" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                    }
                }
            }



            //turn alb
            if (tabla[poz] == "wr")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-8);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(8);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < linie)
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wr";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wr" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wr";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wr" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wr";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wr" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - linie))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wr";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wr" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                    }
                }
            }



            //turn negru
            if (tabla[poz] == "br")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-8);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(8);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < linie)
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "br";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "br" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "br";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "br" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "br";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "br" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - linie))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "br";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "br" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                    }
                }
            }



            //dama alba
            if (tabla[poz] == "wq")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-8);
                directii.Add(-7);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(7);
                directii.Add(8);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < linie)
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - linie))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //dama neagra
            if (tabla[poz] == "bq")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-8);
                directii.Add(-7);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(7);
                directii.Add(8);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < linie)
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana)
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - linie))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bq";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 2, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bq" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //rege alb
            if (tabla[poz] == "wk")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-8);
                directii.Add(-7);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(7);
                directii.Add(8);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < linie
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana)
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana)
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i < coloana
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - linie)
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }

                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('b'))
                                && i <= (8 - coloana)
                                && (atacNegru[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "wk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "wk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('b'))
                            {
                                break;
                            }
                        }
                    }
                }

                //rocada mica
                if (whiteking == 1 && whiterightrook == 1
                    && tabla[62] == "gol"
                    && tabla[63] == "gol")
                {
                    int pozMutare = 87;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 2] = "wk";
                    copie[poz + 1] = "wr";
                    copie[poz] = "gol";
                    copie[poz + 3] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wk" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //rocada mare
                if (whiteking == 1 && whiteleftrook == 1
                    && tabla[58] == "gol"
                    && tabla[59] == "gol"
                    && tabla[60] == "gol")
                {
                    int pozMutare = 83;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 2] = "wk";
                    copie[poz - 1] = "wr";
                    copie[poz] = "gol";
                    copie[poz - 4] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 3, copie, 1);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "wk" + pozMutare + " Evaluare:" + eval;
                    }
                }
            }

            //rege negru
            if (tabla[poz] == "bk")
            {
                List<int> directii = new List<int>(4);
                directii.Add(-9);
                directii.Add(-8);
                directii.Add(-7);
                directii.Add(-1);
                directii.Add(1);
                directii.Add(7);
                directii.Add(8);
                directii.Add(9);

                foreach (int directie in directii)
                {
                    for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                    {
                        if (directie == -9)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -8)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < linie
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -7)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana)
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == -1)
                        {
                            if ((poz + i * directie > 0)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana - i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                        else if (directie == 1)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana)
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 7)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i < coloana
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 9;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 8)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - linie)
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 10;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }

                        else if (directie == 9)
                        {
                            if ((poz + i * directie < 65)
                                && (tabla[poz + i * directie] == "gol" || tabla[poz + i * directie].Contains('w'))
                                && i <= (8 - coloana)
                                && (atacAlb[poz + i * directie] == 0))
                            {
                                int pozMutare = linie * 10 + coloana + i * 11;
                                string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                                ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla);
                                copie[poz + i * directie] = "bk";
                                copie[poz] = "gol";

                                int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    x = "bk" + pozMutare + " Evaluare:" + eval;
                                }
                            }
                            else
                            {
                                break;
                            }

                            // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                            if (tabla[poz + i * directie].Contains('w'))
                            {
                                break;
                            }
                        }
                    }
                }
                //rocada mare
                if (blackking == 1 && blackleftrook == 1
                        && tabla[2] == "gol"
                        && tabla[3] == "gol"
                        && tabla[4] == "gol")
                {
                    int pozMutare = 13;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz + 2] = "bk";
                    copie[poz + 1] = "br";
                    copie[poz] = "gol";
                    copie[poz + 3] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bk" + pozMutare + " Evaluare:" + eval;
                    }
                }

                //rocada mica
                if (blackking == 1 && blackrightrook == 1
                        && tabla[6] == "gol"
                        && tabla[7] == "gol")
                {
                    int pozMutare = 17;
                    string divid = "possibleMoves('ImageButton" + pozMutare.ToString() + "d');";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), pozMutare.ToString(), divid, true);

                    List<string> copie = new List<string>(65);
                    copie.Clear();
                    copie.AddRange(tabla);
                    copie[poz - 2] = "bk";
                    copie[poz - 1] = "br";
                    copie[poz] = "gol";
                    copie[poz - 4] = "gol";

                    int eval = -alphaBetaMax(-100000, 100000, 4, copie, 0);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        x = "bk" + pozMutare + " Evaluare:" + eval;
                    }
                }

            }
            return x;
        }


        protected void selectSquare(object sender, EventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;

            //muta albu'
            if ((int)ViewState["Selected"] == 0 && imgbtn.ImageUrl != "images/blank.png" && (int)ViewState["player"] == 0
                && imgbtn.ImageUrl.Contains('w'))
            {

                ViewState["imgURL"] = imgbtn.ImageUrl;
                ViewState["Selected"] = 1;
                ViewState["enPassantWhite"] = 0;


                string divid1 = "changeBackgr('" + imgbtn.ID + "d');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mykey", divid1, true);

                lastsender = imgbtn;

                // possible moves
                string poz_lastSender = lastsender.ID;
                int lin_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 2]);
                int col_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 1]);

                attackedFields();

                string u = possibleMoves(this.Page, lin_lastSender, col_lastSender, (int)ViewState["enPassantWhite"], (int)ViewState["enPassantBlack"]);
                TextBox1.Text = u;
                            
            }

            //muta negru'
            else if ((int)ViewState["Selected"] == 0 && imgbtn.ImageUrl != "images/blank.png" && (int)ViewState["player"] == 1
                && imgbtn.ImageUrl.Contains('b') && imgbtn.ImageUrl != "images/wb.png")
            {

                ViewState["imgURL"] = imgbtn.ImageUrl;
                ViewState["Selected"] = 1;
                ViewState["enPassantBlack"] = 0;

                string divid1 = "changeBackgr('" + imgbtn.ID + "d');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mykey", divid1, true);

                lastsender = imgbtn;

                // possible moves
                string poz_lastSender = lastsender.ID;
                int lin_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 2]);
                int col_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 1]);

                attackedFields();
                possibleMoves(this.Page, lin_lastSender, col_lastSender, (int)ViewState["enPassantWhite"], (int)ViewState["enPassantBlack"]);

                string u = possibleMoves(this.Page, lin_lastSender, col_lastSender, (int)ViewState["enPassantWhite"], (int)ViewState["enPassantBlack"]);
                TextBox1.Text = u;

            }

            //piesa deja selectata
            else if ((int)ViewState["Selected"] == 1)
            {

                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("MainContent");
                ImageButton deBlankuit = (ImageButton)cph.FindControl(lastsender.ID);

                int enPass = (int)ViewState["enPassant"];

                string poz_lastSender = lastsender.ID;
                int lin_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 2]);
                int col_lastSender = (int)Char.GetNumericValue(poz_lastSender[poz_lastSender.Length - 1]);

                string poz_currentSender = imgbtn.ID;
                int lin_currentSender = (int)Char.GetNumericValue(poz_currentSender[poz_currentSender.Length - 2]);
                int col_currentSender = (int)Char.GetNumericValue(poz_currentSender[poz_currentSender.Length - 1]);

                int lastSenderPoz = (lin_lastSender - 1) * 8 + col_lastSender;
                int currentSenderPoz = (lin_currentSender - 1) * 8 + col_currentSender;

                // pion alb
                if (tabla[lastSenderPoz] == "wp")
                {
                    // muta o patratica
                    if (lastSenderPoz - 8 == currentSenderPoz
                        && tabla[currentSenderPoz] == "gol")
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz <= 8)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //muta 2 patratele
                    else if (lastSenderPoz - 16 == currentSenderPoz
                        && lin_lastSender == 7
                        && tabla[currentSenderPoz + 8] == "gol" // patratica din fata lui e libera
                        && tabla[currentSenderPoz] == "gol" // a doua patratica din fata e libera
                        )
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz <= 8)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";

                        ViewState["enPassantWhite"] = currentSenderPoz;
                    }

                    // captura pe diagonala dreapta                  
                    else if (lastSenderPoz - 7 == currentSenderPoz
                        && tabla[currentSenderPoz].Contains('b')
                        && lastSenderPoz % 8 != 0)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz <= 8)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";


                    }

                    //captura pe diagonala stanga
                    else if (lastSenderPoz - 9 == currentSenderPoz
                        && tabla[currentSenderPoz].Contains('b')
                        && lastSenderPoz % 8 != 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz <= 8)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    // en Passant dreapta (tre -8 ca sa ne duca pe linia buna, noi dam click in spatele pionului)
                    else if (currentSenderPoz == (int)ViewState["enPassantBlack"] - 8
                        && lastSenderPoz - 7 == currentSenderPoz)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";
                        tabla[currentSenderPoz + 8] = "gol"; // pozitia pionului capturat

                        string enPassID = deBlankuit.ID;
                        int enPassID_lin = (int)Char.GetNumericValue(enPassID[enPassID.Length - 2]);
                        int enPassID_col = (int)Char.GetNumericValue(enPassID[enPassID.Length - 1]);
                        enPassID_col++;

                        enPassID = "ImageButton" + enPassID_lin + enPassID_col;
                        ImageButton deBlankuitenPassant = (ImageButton)cph.FindControl(enPassID);
                        deBlankuitenPassant.ImageUrl = "images/blank.png";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //en Passant stanga (tre -8 ca sa ne duca pe linia buna, noi dam click in spatele pionului)
                    else if (currentSenderPoz == (int)ViewState["enPassantBlack"] - 8
                        && lastSenderPoz - 9 == currentSenderPoz)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";
                        tabla[currentSenderPoz + 8] = "gol"; // pozitia pionului capturat

                        string enPassID = deBlankuit.ID;
                        int enPassID_lin = (int)Char.GetNumericValue(enPassID[enPassID.Length - 2]);
                        int enPassID_col = (int)Char.GetNumericValue(enPassID[enPassID.Length - 1]);
                        enPassID_col--;

                        enPassID = "ImageButton" + enPassID_lin + enPassID_col;
                        ImageButton deBlankuitenPassant = (ImageButton)cph.FindControl(enPassID);
                        deBlankuitenPassant.ImageUrl = "images/blank.png";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;

                }

                // pion negru
                if (tabla[lastSenderPoz] == "bp")
                {
                    // muta o patratica
                    if (lastSenderPoz + 8 == currentSenderPoz
                        && tabla[currentSenderPoz] == "gol")
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz >= 57)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //muta 2 patratele
                    else if (lastSenderPoz + 16 == currentSenderPoz
                        && lin_lastSender == 2
                        && tabla[currentSenderPoz - 8] == "gol" // patratica din fata lui e libera
                        && tabla[currentSenderPoz] == "gol" // a doua patratica din fata e libera
                        )
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz >= 57)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }
                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";

                        ViewState["enPassantBlack"] = currentSenderPoz;
                    }

                    // captura pe diagonala dreapta
                    else if (lastSenderPoz + 9 == currentSenderPoz
                        && tabla[currentSenderPoz].Contains('w')
                        && lastSenderPoz % 8 != 0)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz >= 57)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }
                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //captura pe diagonala stanga
                    else if (lastSenderPoz + 7 == currentSenderPoz
                        && tabla[currentSenderPoz].Contains('w')
                        && lastSenderPoz % 8 != 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        if (currentSenderPoz >= 57)
                        {
                            imgbtn.ImageUrl = "images/wq.png";
                            tabla[currentSenderPoz] = "wq";
                        }
                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    // en Passant dreapta (tre +8 ca sa ne duca pe linia buna, noi dam click in spatele pionului)
                    else if (currentSenderPoz == (int)ViewState["enPassantWhite"] + 8
                        && lastSenderPoz + 9 == currentSenderPoz)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";
                        tabla[currentSenderPoz - 8] = "gol"; // pozitia pionului capturat

                        string enPassID = deBlankuit.ID;
                        int enPassID_lin = (int)Char.GetNumericValue(enPassID[enPassID.Length - 2]);
                        int enPassID_col = (int)Char.GetNumericValue(enPassID[enPassID.Length - 1]);
                        enPassID_col++;

                        enPassID = "ImageButton" + enPassID_lin + enPassID_col;
                        ImageButton deBlankuitenPassant = (ImageButton)cph.FindControl(enPassID);
                        deBlankuitenPassant.ImageUrl = "images/blank.png";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }

                    //en Passant stanga (tre +8 ca sa ne duca pe linia buna, noi dam click in spatele pionului)
                    else if (currentSenderPoz == (int)ViewState["enPassantWhite"] + 8
                        && lastSenderPoz + 7 == currentSenderPoz)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";
                        tabla[currentSenderPoz - 8] = "gol"; // pozitia pionului capturat

                        string enPassID = deBlankuit.ID;
                        int enPassID_lin = (int)Char.GetNumericValue(enPassID[enPassID.Length - 2]);
                        int enPassID_col = (int)Char.GetNumericValue(enPassID[enPassID.Length - 1]);
                        enPassID_col--;

                        enPassID = "ImageButton" + enPassID_lin + enPassID_col;
                        ImageButton deBlankuitenPassant = (ImageButton)cph.FindControl(enPassID);
                        deBlankuitenPassant.ImageUrl = "images/blank.png";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }


                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;

                }

                // cal alb
                if (tabla[lastSenderPoz] == "wn")
                {
                    if (((lastSenderPoz - 17 == currentSenderPoz && lastSenderPoz - 17 > 0 && lastSenderPoz % 8 > 1)
                        || (lastSenderPoz - 15 == currentSenderPoz && lastSenderPoz - 15 > 0 && lastSenderPoz % 8 < 8)
                        || (lastSenderPoz - 10 == currentSenderPoz && lastSenderPoz % 8 > 2 && lastSenderPoz - 10 > 0)
                        || (lastSenderPoz - 6 == currentSenderPoz && lastSenderPoz % 8 < 7 && lastSenderPoz - 6 > 0)
                        || (lastSenderPoz + 6 == currentSenderPoz && lastSenderPoz % 8 > 2 && lastSenderPoz + 6 < 65)
                        || (lastSenderPoz + 10 == currentSenderPoz && lastSenderPoz % 8 < 7 && lastSenderPoz + 10 < 65)
                        || (lastSenderPoz + 15 == currentSenderPoz && lastSenderPoz + 15 < 65 && lastSenderPoz % 8 > 1)
                        || (lastSenderPoz + 17 == currentSenderPoz && lastSenderPoz + 17 < 65) && lastSenderPoz % 8 < 8)

                        && (tabla[currentSenderPoz] == "gol"
                        || tabla[currentSenderPoz].Contains('b')))

                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                // cal negru
                if (tabla[lastSenderPoz] == "bn")
                {
                    if (((lastSenderPoz - 17 == currentSenderPoz && lastSenderPoz - 17 > 0 && lastSenderPoz % 8 > 1)
                        || (lastSenderPoz - 15 == currentSenderPoz && lastSenderPoz - 15 > 0 && lastSenderPoz % 8 < 8)
                        || (lastSenderPoz - 10 == currentSenderPoz && lastSenderPoz % 8 > 2 && lastSenderPoz - 10 > 0)
                        || (lastSenderPoz - 6 == currentSenderPoz && lastSenderPoz % 8 < 7 && lastSenderPoz - 6 > 0)
                        || (lastSenderPoz + 6 == currentSenderPoz && lastSenderPoz % 8 > 2 && lastSenderPoz + 6 < 65)
                        || (lastSenderPoz + 10 == currentSenderPoz && lastSenderPoz % 8 < 7 && lastSenderPoz + 10 < 65)
                        || (lastSenderPoz + 15 == currentSenderPoz && lastSenderPoz + 15 < 65 && lastSenderPoz % 8 > 1)
                        || (lastSenderPoz + 17 == currentSenderPoz && lastSenderPoz + 17 < 65) && lastSenderPoz % 8 < 8)

                        && (tabla[currentSenderPoz] == "gol"
                        || tabla[currentSenderPoz].Contains('w')))
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //nebun alb
                if (tabla[lastSenderPoz] == "ww")
                {
                    int diagonala = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 9 == currentSenderPoz)
                        {
                            diagonala = -9;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 7 == currentSenderPoz)
                        {
                            diagonala = -7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 7 == currentSenderPoz)
                        {
                            diagonala = 7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 9 == currentSenderPoz)
                        {
                            diagonala = 9;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * diagonala == currentSenderPoz && !tabla[currentSenderPoz].Contains('w'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * diagonala == currentSenderPoz && tabla[currentSenderPoz].Contains('w'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * diagonala] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //nebun negru
                if (tabla[lastSenderPoz] == "bb")
                {
                    int diagonala = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 9 == currentSenderPoz)
                        {
                            diagonala = -9;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 7 == currentSenderPoz)
                        {
                            diagonala = -7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 7 == currentSenderPoz)
                        {
                            diagonala = 7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 9 == currentSenderPoz)
                        {
                            diagonala = 9;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * diagonala == currentSenderPoz && !tabla[currentSenderPoz].Contains('b'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * diagonala == currentSenderPoz && tabla[currentSenderPoz].Contains('b'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * diagonala] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;

                }

                //turn alb
                if (tabla[lastSenderPoz] == "wr")
                {

                    int linie = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 8 == currentSenderPoz)
                        {
                            linie = -8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i == currentSenderPoz)
                        {
                            linie = -1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i == currentSenderPoz)
                        {
                            linie = 1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 8 == currentSenderPoz)
                        {
                            linie = 8;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && !tabla[currentSenderPoz].Contains('w'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && tabla[currentSenderPoz].Contains('w'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * linie] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";

                        if (lastSenderPoz == 64)
                        {
                            Session["RightWhiteRook"] = 0;
                        }
                        if (lastSenderPoz == 57)
                        {
                            Session["LeftWhiteRook"] = 0;
                        }
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //turn negru
                if (tabla[lastSenderPoz] == "br")
                {
                    int linie = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 8 == currentSenderPoz)
                        {
                            linie = -8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i == currentSenderPoz)
                        {
                            linie = -1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i == currentSenderPoz)
                        {
                            linie = 1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 8 == currentSenderPoz)
                        {
                            linie = 8;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && !tabla[currentSenderPoz].Contains('b'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && tabla[currentSenderPoz].Contains('b'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * linie] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";

                        if (lastSenderPoz == 8)
                        {
                            Session["RightBlackRook"] = 0;
                        }
                        if (lastSenderPoz == 1)
                        {
                            Session["LeftBlackRook"] = 0;
                        }
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //dama alba
                if (tabla[lastSenderPoz] == "wq")
                {
                    int linie = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 9 == currentSenderPoz)
                        {
                            linie = -9;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 8 == currentSenderPoz)
                        {
                            linie = -8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 7 == currentSenderPoz)
                        {
                            linie = -7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i == currentSenderPoz)
                        {
                            linie = -1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i == currentSenderPoz)
                        {
                            linie = 1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 7 == currentSenderPoz)
                        {
                            linie = 7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 8 == currentSenderPoz)
                        {
                            linie = 8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 9 == currentSenderPoz)
                        {
                            linie = 9;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && !tabla[currentSenderPoz].Contains('w'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && tabla[currentSenderPoz].Contains('w'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * linie] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //dama neagra
                if (tabla[lastSenderPoz] == "bq")
                {
                    int linie = 0, ok = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        if (lastSenderPoz - i * 9 == currentSenderPoz)
                        {
                            linie = -9;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 8 == currentSenderPoz)
                        {
                            linie = -8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i * 7 == currentSenderPoz)
                        {
                            linie = -7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz - i == currentSenderPoz)
                        {
                            linie = -1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i == currentSenderPoz)
                        {
                            linie = 1;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 7 == currentSenderPoz)
                        {
                            linie = 7;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 8 == currentSenderPoz)
                        {
                            linie = 8;
                            ok = 1;
                            break;
                        }
                        if (lastSenderPoz + i * 9 == currentSenderPoz)
                        {
                            linie = 9;
                            ok = 1;
                            break;
                        }
                    }

                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            // daca ajung pana pe destinatie si nu e piesa proprie, e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && !tabla[currentSenderPoz].Contains('b'))
                            {
                                break;
                            }

                            // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                            if (lastSenderPoz + i * linie == currentSenderPoz && tabla[currentSenderPoz].Contains('b'))
                            {
                                ok = 0;
                                break;
                            }

                            // gasesc o piesa in cale
                            if (tabla[lastSenderPoz + i * linie] != "gol")
                            {
                                ok = 0;
                                break;
                            }


                        }
                    }
                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //rege alb
                if (tabla[lastSenderPoz] == "wk")
                {
                    int linie = 0, ok = 0;

                    if (lastSenderPoz - 9 == currentSenderPoz)
                    {
                        linie = -9;
                        ok = 1;

                    }
                    if (lastSenderPoz - 8 == currentSenderPoz)
                    {
                        linie = -8;
                        ok = 1;

                    }
                    if (lastSenderPoz - 7 == currentSenderPoz)
                    {
                        linie = -7;
                        ok = 1;

                    }
                    if (lastSenderPoz - 1 == currentSenderPoz)
                    {
                        linie = -1;
                        ok = 1;

                    }
                    if (lastSenderPoz + 1 == currentSenderPoz)
                    {
                        linie = 1;
                        ok = 1;

                    }
                    if (lastSenderPoz + 7 == currentSenderPoz)
                    {
                        linie = 7;
                        ok = 1;

                    }
                    if (lastSenderPoz + 8 == currentSenderPoz)
                    {
                        linie = 8;
                        ok = 1;

                    }
                    if (lastSenderPoz + 9 == currentSenderPoz)
                    {
                        linie = 9;
                        ok = 1;
                    }

                    //rocada mica
                    if (lastSenderPoz + 2 == currentSenderPoz
                        && tabla[lastSenderPoz + 1] == "gol"
                        && tabla[lastSenderPoz + 2] == "gol"
                        && tabla[lastSenderPoz + 3] == "wr"
                        && (int)Session["RightWhiteRook"] == 1
                        && (int)Session["WhiteKing"] == 1)
                    {
                        ok = 1;
                        linie = 2;
                    }

                    //rocada mare
                    if (lastSenderPoz - 2 == currentSenderPoz
                        && tabla[lastSenderPoz - 1] == "gol"
                        && tabla[lastSenderPoz - 2] == "gol"
                        && tabla[lastSenderPoz - 3] == "gol"
                        && tabla[lastSenderPoz - 4] == "wr"
                        && (int)Session["LeftWhiteRook"] == 1
                        && (int)Session["WhiteKing"] == 1)
                    {
                        ok = 1;
                        linie = -2;
                    }


                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                        if (lastSenderPoz + linie == currentSenderPoz && tabla[currentSenderPoz].Contains('w'))
                        {
                            ok = 0;
                        }
                    }

                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 1;
                        deBlankuit.ImageUrl = "images/blank.png";

                        if (linie == 2)
                        {
                            ImageButton88.ImageUrl = "images/blank.png";
                            tabla[64] = "gol";
                            tabla[62] = "wr";
                            ImageButton86.ImageUrl = "images/wr.png";
                        }
                        if (linie == -2)
                        {
                            ImageButton81.ImageUrl = "images/blank.png";
                            tabla[57] = "gol";
                            tabla[60] = "wr";
                            ImageButton84.ImageUrl = "images/wr.png";
                        }
                        Session["WhiteKing"] = 0;

                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                //rege negru
                if (tabla[lastSenderPoz] == "bk")
                {
                    int linie = 0, ok = 0;

                    if (lastSenderPoz - 9 == currentSenderPoz)
                    {
                        linie = -9;
                        ok = 1;

                    }
                    if (lastSenderPoz - 8 == currentSenderPoz)
                    {
                        linie = -8;
                        ok = 1;

                    }
                    if (lastSenderPoz - 7 == currentSenderPoz)
                    {
                        linie = -7;
                        ok = 1;

                    }
                    if (lastSenderPoz - 1 == currentSenderPoz)
                    {
                        linie = -1;
                        ok = 1;

                    }
                    if (lastSenderPoz + 1 == currentSenderPoz)
                    {
                        linie = 1;
                        ok = 1;

                    }
                    if (lastSenderPoz + 7 == currentSenderPoz)
                    {
                        linie = 7;
                        ok = 1;

                    }
                    if (lastSenderPoz + 8 == currentSenderPoz)
                    {
                        linie = 8;
                        ok = 1;

                    }
                    if (lastSenderPoz + 9 == currentSenderPoz)
                    {
                        linie = 9;
                        ok = 1;
                    }

                    //rocada mica
                    if (lastSenderPoz + 2 == currentSenderPoz
                        && tabla[lastSenderPoz + 1] == "gol"
                        && tabla[lastSenderPoz + 2] == "gol"
                        && tabla[lastSenderPoz + 3] == "br"
                        && (int)Session["RightBlackRook"] == 1
                        && (int)Session["BlackKing"] == 1)
                    {
                        ok = 1;
                        linie = 2;
                    }

                    //rocada mare
                    if (lastSenderPoz - 2 == currentSenderPoz
                        && tabla[lastSenderPoz - 1] == "gol"
                        && tabla[lastSenderPoz - 2] == "gol"
                        && tabla[lastSenderPoz - 3] == "gol"
                        && tabla[lastSenderPoz - 4] == "br"
                        && (int)Session["LeftBlackRook"] == 1
                        && (int)Session["BlackKing"] == 1)
                    {
                        ok = 1;
                        linie = -2;
                    }


                    //verificare calea e libera ( nu sunt piese pe drum)
                    if (ok == 1)
                    {
                        // daca ajung pana pe destinatie si e piesa proprie,nu e ok
                        if (lastSenderPoz + linie == currentSenderPoz && tabla[currentSenderPoz].Contains('b'))
                        {
                            ok = 0;
                        }
                    }

                    if (ok == 1)
                    {
                        imgbtn.ImageUrl = (string)ViewState["imgURL"];

                        ViewState["Selected"] = 0;
                        ViewState["imgURL"] = string.Empty;

                        tabla[currentSenderPoz] = tabla[lastSenderPoz];
                        tabla[lastSenderPoz] = "gol";

                        ViewState["player"] = 0;
                        deBlankuit.ImageUrl = "images/blank.png";

                        if (linie == 2)
                        {
                            ImageButton18.ImageUrl = "images/blank.png";
                            tabla[8] = "gol";
                            tabla[6] = "br";
                            ImageButton16.ImageUrl = "images/br.png";
                        }
                        if (linie == -2)
                        {
                            ImageButton11.ImageUrl = "images/blank.png";
                            tabla[1] = "gol";
                            tabla[4] = "br";
                            ImageButton14.ImageUrl = "images/br.png";
                        }

                        Session["BlackKing"] = 0;
                    }
                    //ViewState["Selected"] = 0;
                    //ViewState["imgURL"] = string.Empty;
                }

                ViewState["Selected"] = 0;
                ViewState["imgURL"] = string.Empty;

            }
        }

        public static List<List<string>> nextTable(List<string> tabla2, int player)
        {

            List<List<string>> listaTableAlb = new List<List<string>>();
            List<List<string>> listaTableNegru = new List<List<string>>();

            if (player == 0)
            {
                listaTableAlb.Clear();
            }
            else
            {
                listaTableNegru.Clear();
            }

            for (int poz = 1; poz < 65; poz++)
            {
                if (player == 0)
                {
                    if (tabla2[poz].Contains('w'))
                    {
                        if (tabla2[poz] == "wp")
                        {
                            //muta in fata o patratica
                            if (tabla2[poz - 8] == "gol")
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";

                                if (poz - 8 > 8)
                                    copie[poz - 8] = "wp";
                                else if (poz - 8 < 9 && poz - 8 > 0)
                                    copie[poz - 8] = "wq";

                                listaTableAlb.Add(copie);
                            }

                            // muta in fata 2 patratele
                            if ((poz - 16) > 0 && tabla2[poz - 16] == "gol" && tabla2[poz - 8] == "gol" && (poz > 48 && poz < 57))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 16] = "wp";

                                listaTableAlb.Add(copie);
                            }


                            //captura dreapta
                            if (poz % 8 != 0 && (poz - 7 > 0) && tabla2[poz - 7].Contains('b'))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 7] = "wp";

                                listaTableAlb.Add(copie);

                            }

                            //captura stanga
                            if (poz % 8 != 1 && (poz - 9 > 0) && tabla2[poz - 9].Contains('b'))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 9] = "wp";

                                listaTableAlb.Add(copie);
                            }

                        }
                        if (tabla2[poz] == "wn")
                        {
                            if (poz - 17 > 0 && !tabla2[poz - 17].Contains('w') && poz % 8 > 1)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 17] = "wn";

                                listaTableAlb.Add(copie);

                            }
                            if (poz - 15 > 0 && !tabla2[poz - 15].Contains('w') && poz % 8 < 8)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 15] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz - 10 > 0 && !tabla2[poz - 10].Contains('w') && poz % 8 > 2)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 10] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz - 6 > 0 && !tabla2[poz - 6].Contains('w') && poz % 8 < 7)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 6] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz + 6 < 64 && !tabla2[poz + 6].Contains('w') && poz % 8 > 2)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 6] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz + 10 < 64 && !tabla2[poz + 10].Contains('w') && poz % 8 < 7)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 10] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz + 15 < 64 && !tabla2[poz + 15].Contains('w') && poz % 8 > 1)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 15] = "wn";

                                listaTableAlb.Add(copie);
                            }
                            if (poz + 17 < 64 && !tabla2[poz + 17].Contains('w') && poz % 8 < 8)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 15] = "wn";

                                listaTableAlb.Add(copie);
                            }
                        }
                        //nebun alb
                        if (tabla2[poz] == "ww")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-7);
                            directii.Add(7);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "ww";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "ww";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "ww";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "ww";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }

                                    }
                                }
                            }
                        }

                        //turn alb
                        if (tabla2[poz] == "wr")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-8);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(8);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wr";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wr";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wr";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wr";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        //dama alba
                        if (tabla2[poz] == "wq")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-8);
                            directii.Add(-7);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(7);
                            directii.Add(8);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wq";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //rege alb
                        if (tabla2[poz] == "wk")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-8);
                            directii.Add(-7);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(7);
                            directii.Add(8);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('b'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "wk";

                                            listaTableAlb.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa neagra, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('b'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    if (tabla2[poz].Contains('b'))
                    {
                        if (tabla2[poz] == "bp")
                        {
                            //muta in fata o patratica
                            if (tabla2[poz + 8] == "gol")
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";

                                if (poz + 8 < 57)
                                    copie[poz + 8] = "bp";
                                else if (poz + 8 < 65 && poz + 8 > 56)
                                    copie[poz + 8] = "bq";

                                listaTableNegru.Add(copie);
                            }

                            // muta in fata 2 patratele
                            if ((poz + 16) < 65 && tabla2[poz + 16] == "gol" && tabla2[poz + 8] == "gol" && (poz > 8 && poz < 17))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 16] = "bp";

                                listaTableNegru.Add(copie);
                            }

                            //captura dreapta
                            if (poz % 8 != 0 && (poz + 9 < 64) && tabla2[poz + 9].Contains('w'))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 9] = "bp";

                                listaTableNegru.Add(copie);
                            }

                            //captura stanga
                            if (poz % 8 != 1 && (poz + 7 < 64) && tabla2[poz + 7].Contains('w'))
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 7] = "bp";

                                listaTableNegru.Add(copie);
                            }
                        }
                        if (tabla2[poz] == "bn")
                        {
                            if (poz - 17 > 0 && !tabla2[poz - 17].Contains('b') && poz % 8 > 1)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 17] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz - 15 > 0 && !tabla2[poz - 15].Contains('b') && poz % 8 < 8)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 15] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz - 10 > 0 && !tabla2[poz - 10].Contains('b') && poz % 8 > 2)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 10] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz - 6 > 0 && !tabla2[poz - 6].Contains('b') && poz % 8 < 7)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz - 6] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz + 6 < 64 && !tabla2[poz + 6].Contains('b') && poz % 8 > 2)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 6] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz + 10 < 64 && !tabla2[poz + 10].Contains('b') && poz % 8 < 7)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 10] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz + 15 < 64 && !tabla2[poz + 15].Contains('b') && poz % 8 > 1)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 15] = "bn";

                                listaTableNegru.Add(copie);
                            }
                            if (poz + 17 < 64 && !tabla2[poz + 17].Contains('b') && poz % 8 < 8)
                            {
                                List<string> copie = new List<string>(65);
                                copie.Clear();
                                copie.AddRange(tabla2);
                                copie[poz] = "gol";
                                copie[poz + 17] = "bn";

                                listaTableNegru.Add(copie);
                            }
                        }
                        //nebun negru
                        if (tabla2[poz] == "bb")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-7);
                            directii.Add(7);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bb";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bb";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bb";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }

                                    }
                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bb";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        //turn negru
                        if (tabla2[poz] == "br")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-8);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(8);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "br";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "br";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "br";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "br";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        //dama neagra
                        if (tabla2[poz] == "bq")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-8);
                            directii.Add(-7);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(7);
                            directii.Add(8);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 8; i++)
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bq";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //rege negru
                        if (tabla2[poz] == "bk")
                        {
                            List<int> directii = new List<int>(4);
                            directii.Add(-9);
                            directii.Add(-8);
                            directii.Add(-7);
                            directii.Add(-1);
                            directii.Add(1);
                            directii.Add(7);
                            directii.Add(8);
                            directii.Add(9);

                            foreach (int directie in directii)
                            {
                                for (int i = 1; i < 2; i++) // la fel ca la dama, doar ca nu trebuie FOR, asa ca am pus <2 ca sa nu modific prea mult codu'
                                {
                                    if (directie == -9)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -8)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz / 8 + 1 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -7)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - poz % 8) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == -1)
                                    {
                                        if ((poz + i * directie > 0)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                    else if (directie == 1)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= 8 - (poz % 8) || poz * 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 7)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i < poz % 8 || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 8)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz / 8 + 1)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }

                                    else if (directie == 9)
                                    {
                                        if ((poz + i * directie < 65)
                                            && (tabla2[poz + i * directie] == "gol" || tabla2[poz + i * directie].Contains('w'))
                                            && (i <= (8 - (poz % 8)) || poz % 8 == 0))
                                        {
                                            List<string> copie = new List<string>(65);
                                            copie.Clear();
                                            copie.AddRange(tabla2);
                                            copie[poz] = "gol";
                                            copie[poz + i * directie] = "bk";

                                            listaTableNegru.Add(copie);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        // daca am dat peste o piesa alba, nu pot sa trec mai departe de ea
                                        if (tabla2[poz + i * directie].Contains('w'))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                
            }
            //if (player == 0)
            //{
            //    // Set a variable to the My Documents path.
            //    string mydocpath =
            //        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //    // Write the string array to a new file named "WriteLines.txt".
            //    using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "WriteLines.txt")))
            //    {
            //        foreach (List<string> line in listaTableAlb)
            //            foreach (string piese in line)
            //                outputFile.WriteLine(piese);
            //    }
            //}
            //if (player == 1)
            //{
            //    // Set a variable to the My Documents path.
            //    string mydocpath =
            //        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //    // Write the string array to a new file named "WriteLines.txt".
            //    using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "WriteLines.txt")))
            //    {
            //        foreach (List<string> line in listaTableNegru)
            //            foreach (string piese in line)
            //                outputFile.WriteLine(piese);
            //    }
            //}
            if (player == 0)
            {
                return listaTableAlb;
            }
            else
                return listaTableNegru;

        }

        public static int Evaluate(List<string> tabla)
        {
            int val = 0;

            int WP = 100; //valoare pion
            int WN = 320; //valoare cal
            int WW = 330; //valoare nebun
            int WR = 500; //val tur
            int WQ = 900; //val regina
            int WK = 20000; //val rege

            int[] WPT = new int[]{  0,  0,  0,  0,  0,  0,  0,  0,
                                   50, 50, 50, 50, 50, 50, 50, 50,
                                   10, 10, 20, 30, 30, 20, 10, 10,
                                    5,  5, 10, 25, 25, 10,  5,  5,
                                    0,  0,  0, 20, 20,  0,  0,  0,
                                    5, -5,-10,  0,  0,-10, -5,  5,
                                    5, 10, 10,-20,-20, 10, 10,  5,
                                    0,  0,  0,  0,  0,  0,  0,  0  };

            int[] WNT = new int[]{  -50,-40,-30,-30,-30,-30,-40,-50,
                                    -40,-20,  0,  0,  0,  0,-20,-40,
                                    -30,  0, 10, 15, 15, 10,  0,-30,
                                    -30,  5, 15, 20, 20, 15,  5,-30,
                                    -30,  0, 15, 20, 20, 15,  0,-30,
                                    -30,  5, 10, 15, 15, 10,  5,-30,
                                    -40,-20,  0,  5,  5,  0,-20,-40,
                                    -50,-40,-30,-30,-30,-30,-40,-50  };

            int[] WWT = new int[]{  -20,-10,-10,-10,-10,-10,-10,-20,
                                    -10,  0,  0,  0,  0,  0,  0,-10,
                                    -10,  0,  5, 10, 10,  5,  0,-10,
                                    -10,  5,  5, 10, 10,  5,  5,-10,
                                    -10,  0, 10, 10, 10, 10,  0,-10,
                                    -10, 10, 10, 10, 10, 10, 10,-10,
                                    -10,  5,  0,  0,  0,  0,  5,-10,
                                    -20,-10,-10,-10,-10,-10,-10,-20  };

            int[] WRT = new int[]{0,  0,  0,  0,  0,  0,  0,  0,
                                  5, 10, 10, 10, 10, 10, 10,  5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                  0,  0,  0,  5,  5,  0,  0,  0  };

            int[] WQT = new int[]{  -20,-10,-10, -5, -5,-10,-10,-20,
                                    -10,  0,  0,  0,  0,  0,  0,-10,
                                    -10,  0,  5,  5,  5,  5,  0,-10,
                                     -5,  0,  5,  5,  5,  5,  0, -5,
                                      0,  0,  5,  5,  5,  5,  0, -5,
                                    -10,  5,  5,  5,  5,  5,  0,-10,
                                    -10,  0,  5,  0,  0,  0,  0,-10,
                                    -20,-10,-10, -5, -5,-10,-10,-20  };

            int[] WKT = new int[]{  -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -20,-30,-30,-40,-40,-30,-30,-20,
                                    -10,-20,-20,-20,-20,-20,-20,-10,
                                     20, 20,  0,  0,  0,  0, 20, 20,
                                     20, 30, 10,  0,  0, 10, 30, 20  };

            int[] BPT = new int[]{0, 0, 0, 0, 0, 0, 0, 0,
                                 5, 10, 10, -20, -20, 10, 10, 5,
                                 5, -5, -10, 0, 0, -10, -5, 5,
                                 0,  0, 0, 20, 20, 0, 0, 0,
                                 5,  5, 10, 25, 25, 10, 5, 5,
                                10, 10, 20, 30, 30, 20, 10, 10,
                                50, 50, 50, 50, 50, 50, 50, 50,
                                 0, 0, 0, 0, 0, 0, 0, 0 };

            int[] BNT = new int[]{ -50,-40,-30,-30,-30,-30,-40,-50,
                                    -40,-20,  0,  5,  5,  0,-20,-40,
                                    -30,  5, 10, 15, 15, 10,  5,-30,
                                    -30,  0, 15, 20, 20, 15,  0,-30,
                                    -30,  5, 15, 20, 20, 15,  5,-30,
                                    -30,  0, 10, 15, 15, 10,  0,-30,
                                    -40,-20,  0,  0,  0,  0,-20,-40,
                                    -50,-40,-30,-30,-30,-30,-40,-50};

            int[] BBT = new int[]{  -20,-10,-10,-10,-10,-10,-10,-20,
                                    -10,  5,  0,  0,  0,  0,  5,-10,
                                    -10, 10, 10, 10, 10, 10, 10,-10,
                                    -10,  0, 10, 10, 10, 10,  0,-10,
                                    -10,  5,  5, 10, 10,  5,  5,-10,
                                    -10,  0,  5, 10, 10,  5,  0,-10,
                                    -10,  0,  0,  0,  0,  0,  0,-10,
                                    -20,-10,-10,-10,-10,-10,-10,-20};

            int[] BRT = new int[]{0,  0,  0,  5,  5,  0,  0,  0,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                 -5,  0,  0,  0,  0,  0,  0, -5,
                                  5, 10, 10, 10, 10, 10, 10,  5,
                                  0,  0,  0,  0,  0,  0,  0,  0};

            int[] BQT = new int[]{  -20,-10,-10, -5, -5,-10,-10,-20,
                                    -10,  0,  5,  0,  0,  0,  0,-10,
                                    -10,  5,  5,  5,  5,  5,  0,-10,
                                      0,  0,  5,  5,  5,  5,  0, -5,
                                     -5,  0,  5,  5,  5,  5,  0, -5,
                                    -10,  0,  5,  5,  5,  5,  0,-10,
                                    -10,  0,  0,  0,  0,  0,  0,-10,
                                    -20,-10,-10, -5, -5,-10,-10,-20};

            int[] BKT = new int[]{   20, 30, 10,  0,  0, 10, 30, 20,
                                     20, 20,  0,  0,  0,  0, 20, 20,
                                    -10,-20,-20,-20,-20,-20,-20,-10,
                                    -20,-30,-30,-40,-40,-30,-30,-20,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30,
                                    -30,-40,-40,-50,-50,-40,-40,-30};

            int scorW = 0;
            int scorB = 0;

            for (int poz = 1; poz < 65; poz++)
            {
                if (tabla[poz].Contains('w'))
                {
                    //pion alb
                    if (tabla[poz] == "wp")
                    {
                        scorW += WP + WPT[poz - 1];
                    }
                    //cal alb
                    if (tabla[poz] == "wn")
                    {
                        scorW += WN + WNT[poz - 1];
                    }
                    //nebun alb
                    if (tabla[poz] == "ww")
                    {
                        scorW += WW + WWT[poz - 1];
                    }

                    //turn alb
                    if (tabla[poz] == "wr")
                    {
                        scorW += WR + WRT[poz - 1];
                    }

                    //dama alba
                    if (tabla[poz] == "wq")
                    {
                        scorW += WQ + WQT[poz - 1];
                    }
                    //rege alb
                    if (tabla[poz] == "wk")
                    {
                        scorW += WK + WKT[poz - 1];

                    }
                }

                if (tabla[poz].Contains('b'))
                {
                    //pion negru
                    if (tabla[poz] == "bp")
                    {
                        scorB += WP + BPT[poz - 1];
                    }
                    //cal negru
                    if (tabla[poz] == "bn")
                    {
                        scorB += WN + BNT[poz - 1];
                    }
                    //nebun negru
                    if (tabla[poz] == "bb")
                    {
                        scorB += WW + BBT[poz - 1];
                    }

                    //turn negru
                    if (tabla[poz] == "br")
                    {
                        scorB += WR + BRT[poz - 1];
                    }

                    //dama negru
                    if (tabla[poz] == "bq")
                    {
                        scorB += WQ + BQT[poz - 1];
                    }
                    //rege negru
                    if (tabla[poz] == "bk")
                    {
                        scorB += WK + BKT[poz - 1];

                    }
                }
            }

            val = scorW - scorB;
            return val;
        }


        public static int alphaBetaMax(int alpha, int beta, int depthleft, List<string> tabla, int player)
        {
            if (depthleft == 0) return Evaluate(tabla);


            List<List<string>> listaTableAlb = new List<List<string>>();
            List<List<string>> listaTableNegru = new List<List<string>>();
                        
            if (player == 0)
            {
                listaTableAlb = nextTable(tabla, player);
                foreach (List<string> tablaUrm in listaTableAlb)
                {
                    int score = alphaBetaMin(alpha, beta, depthleft - 1, tablaUrm, 1);
                    if (score >= beta)
                        return beta;   // fail hard beta-cutoff
                    if (score > alpha)
                        alpha = score; // alpha acts like max in MiniMax
                }
                return alpha;
            }
            else
            {
                listaTableNegru = nextTable(tabla, player);
                foreach (List<string> tablaUrm in listaTableNegru)
                {
                    int score = alphaBetaMin(alpha, beta, depthleft - 1, tablaUrm, 0);
                    if (score >= beta)
                        return beta;   // fail hard beta-cutoff
                    if (score > alpha)
                        alpha = score; // alpha acts like max in MiniMax
                }
                return alpha;
            }
        }

        public static int alphaBetaMin(int alpha, int beta, int depthleft, List<string> tabla, int player)
        {
            if (depthleft == 0) return -Evaluate(tabla);

            List<List<string>> listaTableAlb = new List<List<string>>();
            List<List<string>> listaTableNegru = new List<List<string>>();

            if (player == 0)
            {
                listaTableAlb = nextTable(tabla, player);
                foreach (List<string> tablaUrm in listaTableAlb)
                {
                    int score = alphaBetaMax(alpha, beta, depthleft - 1, tablaUrm, 1);
                    if (score <= alpha)
                        return alpha; // fail hard alpha-cutoff
                    if (score < beta)
                        beta = score; // beta acts like min in MiniMax
                }
                return beta;
            }
            else
            {
                listaTableNegru = nextTable(tabla, player);
                foreach (List<string> tablaUrm in listaTableNegru)
                {
                    int score = alphaBetaMax(alpha, beta, depthleft - 1, tablaUrm, 0);
                    if (score <= alpha)
                        return alpha; // fail hard alpha-cutoff
                    if (score < beta)
                        beta = score; // beta acts like min in MiniMax
                }
                return beta;
            }

        }


    }
}