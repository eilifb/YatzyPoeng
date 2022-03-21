using System;
using System.Collections.Generic;
using System.Linq;

namespace YatzyPoengNS
{
    public class YatzyPoeng
    {
        public int BeregnPoeng(string terningerInput, string kategori)
        {
            List<int> terninger = terningerInput.Split(",").Select(t => { return System.Convert.ToInt32(t); }).ToList();

            if (terninger.Where(t => t >= 6 || t < 1).Any() || terninger.Count() != 5)
                throw new ArgumentException("Terningene må være gyldige! (5 heltall mellom 1 og 6!)",terningerInput);

            int poeng = 0;
            int? sorter = null;

            switch (kategori)
            {
                case "Enere":
                    sorter = 1;
                    break;
                case "Toere":
                    sorter = 2;
                    break;
                case "Treere":
                    sorter = 3;
                    break;
                case "Firere":
                    sorter = 4;
                    break;
                case "Femere":
                    sorter = 5;
                    break;
                case "Seksere":
                    sorter = 6;
                    break;
            }

            if (sorter != null)
                poeng = terninger.Where(t => t == sorter).Count() * sorter.Value;

            return poeng;
        }

        public int Sorter(List<int> terninger, string intp)
        {
            return terninger.Where(t => t == 1).Count();
        }
    }
}