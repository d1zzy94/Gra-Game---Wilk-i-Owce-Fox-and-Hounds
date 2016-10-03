using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_Game
{

    public class Plansza : Tablica
    {
        public Plansza() { }

        //zerownie planszy
        public override void zerowanie()
        {
            int k = 1;
            for (int j = 0; j < 8; j++)
            {
                k = (k + 1) % 2;
                for (int i = 0; i < 8; i++)
                {
                    pole[i, j] = k;
                    k = (k + 1) % 2;
                }
            }
        }

        public void setVal(int x, int y, int gracz) {
            if (gracz == 2)
            {
                //jesli Wilk to Owca
                pole[x, y] = 3;
            }
            else
            {
                //jesli Owca to Wilk
                pole[x, y] = 2;
            }
        }

        public int getVal(int x, int y) {
            return pole[x, y];
        }

        //rozmieszczenie pionkow na planszy
        public void rozpoczecie()
        {
            zerowanie();
            //	gracz 2 - Owca
            for (int j = 0; j < 1; j++)
                for (int i = 0; i < 8; i++)
                    if (pole[i, j] == 1) pole[i, j] = 3;

            //	gracz 1 - Wilk		
            for (int j = 7; j < 8; j++)
            {
                Random r = new Random();
                int i = r.Next(8);
                //pole[1, 2] = 2;
                if (i % 2 == 0)
                {
                    if (pole[i, j] == 1) pole[i, j] = 2;
                }
                else
                {
                    i = ((i * 2) - 2) / 2;
                    if (pole[i, j] == 1) pole[i, j] = 2;
                }
            }
        }

        //sprawdzenie wygranej graczy
        public int wygrana(Gracz g, Plansza p)
        {
                int Wilk_X = 0;
                int Wilk_Y = 0;
                int Owca_X = 0;
                int Owca_Y = 0;

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

                int temp = 0;
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        //wygrana Wilka
                        if (p.pole[i, j] == 3)
                        {
                            Owca_X = i;
                            Owca_Y = j;
                            temp++;
                        }
                        if (temp == 1) break;
                    }
                    if (temp == 1) break;
                }

                //wygrana Wilka
                if (Wilk_Y <= Owca_Y)
                {
                    Console.WriteLine("Wilk wygral!");
                    return 2;
                }

                int x = 0, y = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (p.pole[i, j] == 2)
                        {
                            x = i;
                            y = j;
                            break;
                        }
                    }
                    if (x != 0 || y != 0)
                        break;
                }

                if (p.pole[x, y] == 2)
                {
                    if ((x > 0 && x < 7 && y > 0 && y < 7) 
                    && p.pole[x - 1, y - 1] != 1 
                    && p.pole[x + 1, y - 1] != 1 
                    && p.pole[x - 1, y + 1] != 1 
                    && p.pole[x + 1, y + 1] != 1)
                    {
                        Console.WriteLine("Wygraly Owce!");
                        return 3;
                    }
                    if ((x == 0 && y == 7) && p.pole[x + 1, y - 1] != 1)
                    {
                        Console.WriteLine("Wygraly Owce!");
                        return 3;
                    }
                    if ((x == 0 && y < 7) && p.pole[x + 1, y - 1] != 1 && p.pole[x + 1, y + 1] != 1)
                    {
                        Console.WriteLine("Wygraly Owce!");
                        return 3;
                    }
                    if ((x == 7 && y < 7 && y > 0) && p.pole[x - 1, y - 1] != 1 && p.pole[x - 1, y + 1] != 1)
                    {
                        Console.WriteLine("Wygraly Owce!");
                        return 3;
                    }
                    if ((x > 0 && y == 7) && p.pole[x - 1, y - 1] != 1 && p.pole[x + 1, y - 1] != 1)
                    {
                        Console.WriteLine("Wygraly Owce!");
                        return 3;
                    }
                    else return 0;
                }
                return 0;
            }
            
    }
}