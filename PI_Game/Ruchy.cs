using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PI_Game
{
    public class Ruchy : Tablica
    {
        private int old_x;
        private int old_y;
        bool wykonanoRuch = false;

        public Ruchy() { }

        public bool getRuch()
        {
            return wykonanoRuch;
        }

        public void setRuch(bool a)
        {
            wykonanoRuch = a;
        }

        public void klikniecie(int x, int y, Plansza p, Gracz gracz)
        {
            //pole klasy "Ruchy"
            if (this.pole[x, y] == 0)   
            {
                this.zerowanie();
                //jezeli w polu znajduje sie pionek
                if (p.pole[x, y] > 1)   
                {
                    //obecnie zaznaczony pionek
                    this.pole[x, y] = 1; 

                    //jezeli jest to Wilk
                    if (p.pole[x, y] == 2 && gracz.get_gracz() == 1)
                    {
                        //prawo gora
                        if (x > 0 && y > 0 && p.pole[x - 1, y - 1] == 1)
                            this.pole[x - 1, y - 1] = 2;
                        //prawo dol
                        if (x < 7 && y < 7 && p.pole[x + 1, y + 1] == 1)
                            this.pole[x + 1, y + 1] = 2;         
                        //lewo gora
                        if (x < 7 && y > 0 && p.pole[x + 1, y - 1] == 1)
                            this.pole[x + 1, y - 1] = 2;
                        //lewo dol
                        if (x > 0 && y < 7 && p.pole[x - 1, y + 1] == 1)
                            this.pole[x - 1, y + 1] = 2;
                    }

                    //jezeli jest to Owca
                    else if (p.pole[x, y] == 3 && gracz.get_gracz() == 2)
                    {
                        //lewo dol
                        if (x > 0 && y < 7 && p.pole[x - 1, y + 1] == 1)
                            this.pole[x - 1, y + 1] = 2;
                        //prawo dol
                        if (x < 7 && y < 7 && p.pole[x + 1, y + 1] == 1)
                            this.pole[x + 1, y + 1] = 2;
                    }

                    this.wyswietlanie();
                }
                //przypisanie nowego polozenia pionka
                old_x = x;
                old_y = y;
            }
            else if (this.pole[x, y] == 2)
            {
                p.pole[x, y] = p.pole[old_x, old_y];
                p.pole[old_x, old_y] = 1;
                wykonanoRuch = true;
                this.zerowanie();
                gracz.zmiana_gracza();
            }

        }
    }
}
