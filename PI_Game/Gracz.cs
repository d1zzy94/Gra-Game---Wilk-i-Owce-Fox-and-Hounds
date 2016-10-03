using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Game
{
    public class Gracz
    {
        protected int gracz;

        public Gracz()
        {
            set_gracz(0);
        }

        public Gracz(int gracz)
        {
            set_gracz(gracz);
        }

        public void set_gracz(int gracz)
        {
            this.gracz = gracz;
            Console.WriteLine("Gracz " + this.gracz);
        }

        public int get_gracz()
        {
            return this.gracz;
        }

        public void zmiana_gracza()
        {
            if (this.gracz == 1)
            {
                this.set_gracz(2);
            }
            else if (this.gracz == 2)
            {
                this.set_gracz(1);
            }
        }
    }
}
