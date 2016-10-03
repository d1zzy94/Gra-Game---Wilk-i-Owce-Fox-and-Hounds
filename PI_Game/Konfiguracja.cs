using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PI_Game
{
    public class Konfiguracja
    {
        public Color[] kolory;

        public Konfiguracja()
        {
            kolory = new Color[7];
            kolory_standard();
        }

        public void kolory_standard()
        {
            //szachownica - jasne pola
            kolory[0] = Color.BurlyWood;
            //szchownica - ciemne pola
            kolory[1] = Color.SaddleBrown;
            //Wilk
            kolory[2] = Color.Gray;
            //Owca
            kolory[3] = Color.Ivory;
            //obramownie
            kolory[4] = Color.Black;
            //zaznaczenie
            kolory[5] = Color.Khaki;
            //ruch
            kolory[6] = Color.GreenYellow;  
        }
    }
}
