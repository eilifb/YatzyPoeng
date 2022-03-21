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

            if (terninger.Where(t => t > 6 || t < 1).Any() || terninger.Count() != 5)
                throw new ArgumentException("Terningene må være gyldige! (5 heltall mellom 1 og 6!)",terningerInput);

            Dictionary<int, List<int>> terningerSamlet = new Dictionary<int, List<int>>();
            for (int i = 1; i <= 6; i++)
                terningerSamlet.Add(i, terninger.Where(t => t == i).ToList());


            int poeng = 0;
            List<int> terningerSum;


            switch (kategori)
            {
                case "Enere":
                    poeng = terningerSamlet[1].Sum();
                    break;
                case "Toere":
                    poeng = terningerSamlet[2].Sum();
                    break;
                case "Treere":
                    poeng = terningerSamlet[3].Sum();
                    break;
                case "Firere":
                    poeng = terningerSamlet[4].Sum();
                    break;
                case "Femere":
                    poeng = terningerSamlet[5].Sum();
                    break;
                case "Seksere":
                    poeng = terningerSamlet[6].Sum();
                    break;

                case "Par":
                    terningerSum = terningerSamlet
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First()*2) //Grunnen til at jeg har Value.First()*2 isteden for Value.Sum() er i tilfelle hvor det er mer en to terninger som har samme øye.
                                    .ToList();

                    poeng = terningerSum.Count() != 0 ? terningerSum.Max() : 0;
                    break;

                case "ToPar":
                    if (terningerSamlet.Where(t => t.Value.Count > 4).Any()) //Spesialtilfelle: de to parene har samme øye.
                        //Det fungerer fint å bare ta det første øyet i terningerSamlet som har 2 par, da det bare er et øye som kan ha to par, gitt 5 terninger. 
                        poeng = terningerSamlet.Where(t => t.Value.Count > 4).First().Value.First() * 4; 
                    else
                    {
                        terningerSum = terningerSamlet
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First() * 2)
                                    .ToList();

                        poeng = terningerSum.Count() != 0 ? terningerSum.Count() != 2 ? 0 : terningerSum[^1] + terningerSum[^2] : 0;
                    }
                    break;
            }

            return poeng;
        }

        public int Sorter(List<int> terninger, string intp)
        {
            return terninger.Where(t => t == 1).Count();
        }
    }
}