using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PI_Game
{
    public class Tablica
    {
        public int[,] pole;

        public Tablica()
        {
            pole = new int[8, 8];
            this.zerowanie();
        }

        public virtual void zerowanie()
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    pole[i, j] = 0;
                }
            }
        }

        //metoda wyswietla obiekty "Tablica"
        public void wyswietlanie()
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Console.Write(pole[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
