using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PI_Game
{
    public partial class GameWindow : Form
    {
        Konfiguracja konfig;
        Plansza plansza;
        Gracz gracz;
        Ruchy ruchy;

        bool zablokujPlansze = false;
        bool ruchGracza = false;
        bool vsOwca = false;
        bool vsWilk = false;
        int wynik_Wilk = 0;
        int wynik_Owca = 0;
        int wyg = 0;

        public GameWindow()
        {
            InitializeComponent();

            konfig = new Konfiguracja();
            plansza = new Plansza();
            gracz = new Gracz();
            ruchy = new Ruchy();
        }

        public struct para
        {
            public int pion_X;
            public int pion_Y;
            public int ruch_X;
            public int ruch_Y;
            public int Score;
        }

        //Sztuczna inteligencja Owciec
        public void SI_Owca(Plansza p, Gracz g, Ruchy r)
        {
            List<para> ruchy_owiec = new List<para>();
            List<para> best = new List<para>();
            List<para> worst = new List<para>();
            para pom;

            //wyszuknie polozenia Wilka
            int Wilk_X = 0;
            int Wilk_Y = 0;

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p.pole[i, j] == 2)
                    {
                        Wilk_X = i;
                        Wilk_Y = j;
                    }
                }
            }


            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p.pole[i, j] == 3)
                    {
                        r.klikniecie(i, j, p, g);
                        for (int n = 0; n < 8; n++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if (r.pole[m, n] == 1)
                                {
                                    for (int y = 0; y < 8; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                            if (r.pole[x, y] == 2 && p.pole[x, y] == 1)
                                            {
                                                //wspolrzedne aktualnego polozenia pionka
                                                pom.pion_X = m;
                                                pom.pion_Y = n;
                                                //wspolrzedne kolejnego ruchu
                                                pom.ruch_X = x;
                                                pom.ruch_Y = y;
                                                //bonusy
                                                int bonus_sasiad = 0;
                                                int bonus_za_wilka = 0;
                                                int bonus_ostatnia_owca = 0;
                                                //punkty ujemne za odejscie od wilka
                                                int kara = 0;

                                                //jesli wygrana owcy
                                                if (Wilk_Y - pom.ruch_Y == 1 || Wilk_Y - pom.ruch_Y == 0 || Wilk_Y - pom.ruch_Y == -1)
                                                {
                                                    if ((Wilk_X > 0 && Wilk_X < 7 && Wilk_Y > 0 && Wilk_Y < 7)
                                                    && p.pole[Wilk_X - 1, Wilk_Y - 1] != 1
                                                    && p.pole[Wilk_X + 1, Wilk_Y - 1] != 1
                                                    && p.pole[Wilk_X - 1, Wilk_Y + 1] != 1
                                                    && p.pole[Wilk_X + 1, Wilk_Y + 1] != 1)
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X == 0 && Wilk_Y == 7) && (Wilk_X + 1 == pom.ruch_X && Wilk_Y - 1 == pom.ruch_Y))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }                         
                                                    if ((Wilk_X == 0 && Wilk_Y < 7 && Wilk_Y > 0) && (p.pole[Wilk_X + 1, Wilk_Y - 1] == 3
                                                        && (Wilk_X + 1 == pom.ruch_X && Wilk_Y + 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X == 0 && Wilk_Y < 7 && Wilk_Y > 0) && (p.pole[Wilk_X + 1, Wilk_Y + 1] == 3
                                                        && (Wilk_X + 1 == pom.ruch_X && Wilk_Y - 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X == 7 && Wilk_Y < 7 && Wilk_Y > 0) && (p.pole[Wilk_X - 1, Wilk_Y - 1] == 3 
                                                        && (Wilk_X - 1 == pom.ruch_X && Wilk_Y + 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X == 7 && Wilk_Y < 7 && Wilk_Y > 0) && (p.pole[Wilk_X - 1, Wilk_Y + 1] == 3 
                                                        && (Wilk_X - 1 == pom.ruch_X && Wilk_Y - 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X > 0 && Wilk_X < 7 && Wilk_Y == 7) && (p.pole[Wilk_X - 1, Wilk_Y - 1] == 3
                                                        && (Wilk_X + 1 == pom.ruch_X && Wilk_Y - 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                    if ((Wilk_X > 0 && Wilk_X < 7 && Wilk_Y == 7) && (p.pole[Wilk_X + 1, Wilk_Y - 1] == 3
                                                        && (Wilk_X - 1 == pom.ruch_X && Wilk_Y - 1 == pom.ruch_Y)))
                                                    {
                                                        Console.WriteLine("Wygraly Owce!");
                                                        pom.Score = 100;
                                                        ruchy_owiec.Add(pom);
                                                    }
                                                }

                                                //jesli przegrana owcy
                                                int temp = 0;

                                                int Owca_X = 0;
                                                int Owca_Y = 0;
                                                if (Wilk_Y > 7)
                                                {
                                                    for (int d = 0; d < 8; d++)
                                                    {
                                                        for (int f = 0; f < 8; f++)
                                                        {
                                                            //wygrana Wilka
                                                            if (p.pole[f, d] == 3)
                                                            {
                                                                Owca_X = f;
                                                                Owca_Y = d;
                                                                temp++;
                                                            }
                                                            if (temp == 1) break;
                                                        }
                                                        if (temp == 1) break;
                                                    }
                                                }

                                                //wygrana Wilka
                                                else if (Wilk_Y <= pom.ruch_Y)
                                                {
                                                    Console.WriteLine("Wilk wygral!");
                                                    pom.Score = -100;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                /* else if (plansza.wygrana(g, p) == 2)
                                                 {
                                                     pom.Score = -100;
                                                     ruchy_owiec.Add(pom);
                                                 }*/
                                                //jesli jest sasiad po prawej i zblokowany wilk
                                                else if (pom.ruch_X + 2 <= 7 && (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3
                                                    && p.pole[pom.ruch_X + 1, pom.pion_Y + 1] == 2))
                                                {
                                                    pom.Score = 75;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                //jesli jest sasiad po lewej i zblokowany wilk
                                                else if (pom.ruch_X - 2 >= 0 && (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3
                                                    && p.pole[pom.ruch_X - 1, pom.pion_Y + 1] == 2))
                                                {
                                                    pom.Score = 75;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y <= 1 && Math.Abs(Wilk_X - pom.ruch_X) < 2)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;

                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 10 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 2 && Math.Abs(Wilk_X - pom.ruch_X) < 2)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;

                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 8 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y > 3 && Math.Abs(Wilk_X - pom.ruch_X) > 2)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;

                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 15 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 3 && Math.Abs(Wilk_X - pom.ruch_X) < 3)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;


                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 6 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 4 && Math.Abs(Wilk_X - pom.ruch_X) < 4)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;


                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 4 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 5 && Math.Abs(Wilk_X - pom.ruch_X) < 5)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;


                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 3 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 6 && Math.Abs(Wilk_X - pom.ruch_X) < 6)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;


                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 2 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else if (Wilk_Y - pom.ruch_Y < 7 && Math.Abs(Wilk_X - pom.ruch_X) < 7)
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;


                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;

                                                    pom.Score = 1 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                                else
                                                {
                                                    //bonus za sasiada
                                                    if (pom.ruch_X + 2 <= 7)
                                                        if (p.pole[pom.ruch_X + 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    if (pom.ruch_X - 2 >= 0)
                                                        if (p.pole[pom.ruch_X - 2, pom.ruch_Y] == 3)
                                                            bonus_sasiad = 5;
                                                    //bonus za wilka po skosie przed soba
                                                    if (pom.ruch_X + 1 <= 7 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X + 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    if (pom.ruch_X - 1 >= 0 && pom.ruch_Y + 1 <= 7)
                                                        if (p.pole[pom.ruch_X - 1, pom.ruch_Y + 1] == 2)
                                                            bonus_za_wilka = 20;
                                                    //bonus za bycie ostatnia owca
                                                    for (int q = 0; q <= 7; q++)
                                                    {
                                                        int owiec_w_wierszu = 0;
                                                        int temp_x = 0;
                                                        int temp_y = 0;

                                                        for (int w = 0; w <= 7; w++)
                                                        {
                                                            if (p.pole[w, q] == 3)
                                                            {
                                                                owiec_w_wierszu++;
                                                                temp_x = w;
                                                                temp_y = q;
                                                            }
                                                            if (owiec_w_wierszu > 1) break;

                                                            if (owiec_w_wierszu == 1 && w == 7 && temp_x == pom.pion_X && temp_y == pom.pion_Y)
                                                                bonus_ostatnia_owca = 15;
                                                        }
                                                        if (owiec_w_wierszu > 1) break;
                                                    }
                                                    //kara
                                                    if (Wilk_Y <= pom.ruch_Y)
                                                        kara = -50;


                                                    pom.Score = 0 + bonus_sasiad + bonus_za_wilka + bonus_ostatnia_owca + kara;
                                                    ruchy_owiec.Add(pom);
                                                }
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            pom.pion_X = 0;
            pom.pion_Y = 0;

            //sortujemy dobre i zle ruchy
            if (ruchy_owiec.Count > 0)
            {
                //sortowanie listy
                ruchy_owiec = ruchy_owiec.OrderBy(x => x.Score).ToList();

                pom = ruchy_owiec[0];

                for (int i = 0; i < ruchy_owiec.Count; i++)
                {
                    int temp_count = 1;
                
                    if (ruchy_owiec.Count >= 3) temp_count = 3;
                    if (ruchy_owiec.Count == 2) temp_count = 2;
                    if (ruchy_owiec.Count == 1) temp_count = 1;

                    System.Console.WriteLine("ruch od: " + "["+ i + "] to" + ruchy_owiec[i].Score);
                    if (ruchy_owiec[i].Score >= ruchy_owiec[ruchy_owiec.Count - temp_count].Score && ruchy_owiec[i].Score > 0)
                    {
                        pom = ruchy_owiec[i];
                        best.Add(pom);
                    }
                    else
                    {
                        pom = ruchy_owiec[i];
                        worst.Add(pom);
                    }
                }

                //sortowanie list
                best = best.OrderBy(x => x.Score).ToList();
                worst = worst.OrderBy(x => x.Score).ToList();

                //wybieramy ruch
                do
                {
                    if (best.Count > 0)
                    {
                        Random rnd = new Random();
                        pom = best[rnd.Next(0, best.Count)];
                    }
                    else
                    {
                        Random rnd = new Random();
                        pom = worst[rnd.Next(0, worst.Count)];
                    }
                } while (p.pole[pom.ruch_X, pom.ruch_Y] != 1);

                p.pole[pom.pion_X, pom.pion_Y] = 1; //pusty
                p.pole[pom.ruch_X, pom.ruch_Y] = 3; //owca
                gracz.zmiana_gracza();
                Refresh();
            }
            else
            {
                System.Console.WriteLine("Nie wykonano ruchu dla Owiec");
            }
            System.Console.WriteLine("-----------------");
            p.wyswietlanie();
            System.Console.WriteLine("wilk: " + pom.pion_X + ", " + pom.pion_Y);
            System.Console.WriteLine("-----------------");
        }

        //Sztuczna inteligencja Wilka
        public void SI_Wilk(Plansza p, Gracz g, Ruchy r)
        {
            List<para> ruchy_wilka = new List<para>();
            List<para> best = new List<para>();
            List<para> worst = new List<para>();
            para pom;

            //wyszuknie polozenia Wilka
            int Wilk_X = 0;
            int Wilk_Y = 0;

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p.pole[i, j] == 2)
                    {
                        Wilk_X = i;
                        Wilk_Y = j;
                    }
                }
            }

            //wyszuknie polozenia ostaniej Owcy
            int Owca_X = 0;
            int Owca_Y = 0;

            for (int j = 0; j < Wilk_Y; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p.pole[i, j] == 3)
                    {
                        Owca_X = i;
                        Owca_Y = j;
                    }
                }
            }


            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p.pole[i, j] == 2)
                    {
                        r.klikniecie(i, j, p, g);
                        for (int n = 0; n < 8; n++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if (r.pole[m, n] == 1)
                                {
                                    for (int y = 0; y < 8; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                            if (r.pole[x, y] == 2 && p.pole[x, y] == 1)
                                            {
                                                pom.ruch_X = x;
                                                pom.ruch_Y = y;
                                                pom.pion_X = m;
                                                pom.pion_Y = n;
                                                if (plansza.wygrana(g, p) == 2)
                                                {
                                                    pom.Score = 15;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                 
                                                else if (x >= 2 && p.pole[x - 2, y] == 3)
                                                {
                                                    pom.Score = 12;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if (x <= 5 && p.pole[x + 2, y] == 3)
                                                {
                                                    pom.Score = 12;
                                                    ruchy_wilka.Add(pom);
                                                }
 
                                                else if ( Math.Abs(Owca_X - pom.ruch_X) < 2)
                                                {
                                                    pom.Score = 10;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if ( Math.Abs(Owca_Y - pom.ruch_Y) < 1)
                                                {
                                                    pom.Score = 9;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if ( Math.Abs(Owca_Y - pom.ruch_Y) < 2)
                                                {
                                                    pom.Score = 8;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if ( Math.Abs(Owca_Y - pom.ruch_Y) < 3)
                                                {
                                                    pom.Score = 6;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if ( Math.Abs(Owca_Y - pom.ruch_Y) < 4)
                                                {
                                                    pom.Score = 4;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if ( Math.Abs(Owca_Y - pom.ruch_Y) < 5)
                                                {
                                                    pom.Score = 2;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else if (plansza.wygrana(g, p) == 3)
                                                {
                                                    pom.Score = -10;
                                                    ruchy_wilka.Add(pom);
                                                }
                                                else
                                                {
                                                    pom.Score = 0;
                                                    ruchy_wilka.Add(pom);
                                                }
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            pom.pion_X = 0;
            pom.pion_Y = 0;

            //sortujemy dobre i zle ruchy
            if (ruchy_wilka.Count > 0)
            {
                //sortowanie listy
                ruchy_wilka = ruchy_wilka.OrderBy(x => x.Score).ToList();

                pom = ruchy_wilka[0];

                for (int i = 0; i < ruchy_wilka.Count; i++)
                {
                    System.Console.WriteLine("Score wilka = " + ruchy_wilka[i].Score);
                    if (ruchy_wilka[i].Score > 8)
                    {
                        pom = ruchy_wilka[i];
                        best.Add(pom);
                    }
                    else
                    {
                        pom = ruchy_wilka[i];
                        worst.Add(pom);
                    }
                }
                System.Console.WriteLine("best = " + best.Count + "   worst = " + worst.Count);

                //sortowanie list
                best = best.OrderBy(x => x.Score).ToList();
                worst = worst.OrderBy(x => x.Score).ToList();

                //wybieramy ruch
                do
                {
                    //jeśli jeden ruch
                    if (best.Count == 1)
                    {
                        //przypisanie ruchu do pom
                        pom = best[best.Count - 1];
                    }
                    //jeśli więcej niż jeden ruch
                    else if (best.Count > 1)
                    {
                      //pomocnicza lista
                      List<para> temp = new List<para>();

                      pom = best[best.Count - 1];

                      System.Console.WriteLine("pom: " + pom);

                      for (int i = 1; i < best.Count; i++)
                      {
                          if (pom.Score == best[best.Count - i].Score) 
                          {
                              temp.Add(best[best.Count - i]);
                          }
                      }
                      Random rnd = new Random();
                      pom = temp[rnd.Next(0, temp.Count)];

                    }
                    else
                    {
                        Random rnd = new Random();
                        pom = worst[rnd.Next(0, worst.Count)];
                    }
                } while (p.pole[pom.ruch_X, pom.ruch_Y] != 1);

                p.pole[pom.pion_X, pom.pion_Y] = 1; //pusty
                p.pole[pom.ruch_X, pom.ruch_Y] = 2; //wilk
                gracz.zmiana_gracza();
                Refresh();
            }
            else
            {
                System.Console.WriteLine("Nie wykonano ruchu dla Wilka");
            }
            System.Console.WriteLine("-----------------");
            p.wyswietlanie();
            System.Console.WriteLine("wilk: " + pom.pion_X + ", " + pom.pion_Y);
            System.Console.WriteLine("-----------------");
        }

        //komunikat o wygranej
        public void wygral(int w)
        {
            if (w == 2)
            {
                plansza.zerowanie();
                MessageBox.Show("Wygrał Wilk");
                Refresh();
                wynik_Wilk++;
                licznik_W.Text = Convert.ToString(wynik_Wilk);
                zablokujPlansze = true;
            }

            else if (w == 3)
            {
                plansza.zerowanie();
                MessageBox.Show("Wygrały Owce");
                Refresh();
                wynik_Owca++;
                licznik_O.Text = Convert.ToString(wynik_Owca);
                zablokujPlansze = true;
            }
        }

        //wywolanie metody rysujaca plansze 
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            RysujPlansze(g);
        }

        //rysowanie planszy
        protected void RysujPlansze(Graphics g)
        {
            Bitmap bitmap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            Graphics g2 = Graphics.FromImage(bitmap);
            g2.SmoothingMode = SmoothingMode.HighQuality;

            //tlo
            g2.FillRectangle(new SolidBrush(konfig.kolory[4]), 18, 38, 322, 322);

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    //pola
                    if (plansza.pole[i, j] == 0)
                        g2.FillRectangle(new SolidBrush(konfig.kolory[0]), 20 + 40 * i, 40 + 40 * j, 38, 38);
                    else
                        g2.FillRectangle(new SolidBrush(konfig.kolory[1]), 20 + 40 * i, 40 + 40 * j, 38, 38);

                    //zacznaczenie
                    if (ruchy.pole[i, j] == 1)
                        g2.FillRectangle(new SolidBrush(konfig.kolory[5]), 20 + 40 * i, 40 + 40 * j, 38, 38);
                    if (ruchy.pole[i, j] == 2 || ruchy.pole[i, j] == 3)
                        g2.FillRectangle(new SolidBrush(konfig.kolory[6]), 20 + 40 * i, 40 + 40 * j, 38, 38);

                    //pionki
                    if (plansza.pole[i, j] > 1)
                    {
                        g2.FillEllipse(new SolidBrush(konfig.kolory[4]), 21 + 40 * i, 41 + 40 * j, 36, 36);

                        if (plansza.pole[i, j] == 2)
                            g2.FillEllipse(new SolidBrush(konfig.kolory[2]), 23 + 40 * i, 43 + 40 * j, 32, 32);

                        if (plansza.pole[i, j] == 3)
                            g2.FillEllipse(new SolidBrush(konfig.kolory[3]), 23 + 40 * i, 43 + 40 * j, 32, 32);
                    }
                }
            }
            g.DrawImageUnscaled(bitmap, 0, 0, bitmap.Width, bitmap.Height);
        }

        //nowa gra w pasku menu
        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plansza.rozpoczecie();
            ruchy.zerowanie();
            gracz.set_gracz(1);
            Refresh();
        }

        //koniec programu w pasku menu
        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //obsluga myszki na polu
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (x >= 20 && x < 340 && y >= 40 && y < 360
                && (x + 22) % 40 != 0 && (x + 21) % 40 != 0
                && (y + 2) % 40 != 0 && (y + 1) % 40 != 0)
            {
                int _x, _y;
                _x = (x - 20) / 40;
                _y = (y - 40) / 40;
                ruchy.klikniecie(_x, _y, plansza, gracz);
                Refresh();
            }

            if (ruchy.getRuch())
            {
                ruchGracza = true;
                ruchy.setRuch(false);
                wykonajRuchAI();
            }

            if (!zablokujPlansze) SprawdzWygrana();
        }

        private void wykonajRuchAI() {
            if (vsWilk && ruchGracza)
            {
                SI_Wilk(plansza, gracz, ruchy);
                ruchGracza = false;
            }
            if (vsOwca && ruchGracza)
            {
                SI_Owca(plansza, gracz, ruchy);
                ruchGracza = false;
            }
        }

        private void SprawdzWygrana() {
            if (vsWilk)
                if (gracz.get_gracz() == 1)
                {
                    wyg = plansza.wygrana(gracz, plansza);
                    wygral(wyg);
                    if (wyg > 0)
                    {
                        Refresh();
                        wyg = 0;
                    }
                }
                else
                {
                    wyg = plansza.wygrana(gracz, plansza);
                    wygral(wyg);
                    if (wyg > 0)
                    {
                        Refresh();
                        wyg = 0;
                    }
                }

            if (vsOwca)
                if (gracz.get_gracz() == 2)
                {
                    SI_Owca(plansza, gracz, ruchy);
                    wyg = plansza.wygrana(gracz, plansza);
                    wygral(wyg);
                    if (wyg > 0)
                    {
                        Refresh();
                        wyg = 0;
                    } 
                }
                else
                {
                    wyg = plansza.wygrana(gracz, plansza);
                    wygral(wyg);
                    if (wyg > 0)
                    {
                        Refresh();
                        wyg = 0;
                    }
                }
            if (!vsOwca && !vsWilk)
                    if (gracz.get_gracz() == 1)
                    {
                        wyg = plansza.wygrana(gracz, plansza);
                        wygral(wyg);
                        if (wyg > 0)
                        {
                            Refresh();
                            wyg = 0;
                        }
                    }
                else if (gracz.get_gracz() == 2)
                {
                    wyg = plansza.wygrana(gracz, plansza);
                    wygral(wyg);
                    if (wyg > 0)
                    {
                        Refresh();
                        wyg = 0;
                    }
                }
        }

        //przycisk - gra multiplayer
        private void button1_Click(object sender, EventArgs e)
        {
            zablokujPlansze = false;
            vsOwca = false;
            vsWilk = false;
            plansza.rozpoczecie();
            ruchy.zerowanie();
            gracz.set_gracz(1);
            Refresh();
        }
        //przycisk - resetowanie wyniku gry
        private void reset_Click(object sender, EventArgs e)
        {
            wynik_Wilk = 0;
            wynik_Owca = 0;
            licznik_W.Text = Convert.ToString(wynik_Wilk);
            licznik_O.Text = Convert.ToString(wynik_Owca);
        }

        //przycisk - gracz vs owca
        private void GRACZvsSI_Click(object sender, EventArgs e)
        {
            zablokujPlansze = false;
            vsOwca = true;
            vsWilk = false;
            plansza.rozpoczecie();
            ruchy.zerowanie();
            gracz.set_gracz(1);
            Refresh();
        }

        //przycisk - gracz vs wilk
        private void button1_Click_1(object sender, EventArgs e)
        {
            zablokujPlansze = false;
            vsOwca = false;
            vsWilk = true;
            plansza.rozpoczecie();
            ruchy.zerowanie();
            gracz.set_gracz(1);
            Refresh();
            Thread.Sleep(1000);
            if (vsWilk) SI_Wilk(plansza, gracz, ruchy);
        }

        //ekran powitalny
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;
            pictureBox5.Refresh();
        }

    }
}