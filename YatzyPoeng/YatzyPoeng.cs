using System;
using System.Collections.Generic;
using System.Linq;

namespace YatzyPoengNS
{
    public class YatzyPoeng
    {
        /// <summary>
        /// Gitt et sett med terninger og en yatzy kategori beregnes poeng
        /// </summary>
        /// <param name="terningerInput"> 5 terninger separert med komma</param>
        /// <param name="kategori">Kategorien den ønskes å beregnes for</param>
        /// <returns></returns>
        public int BeregnPoeng(string terningerInput, string kategori)
        {
            List<int> terninger = terningerInput.Split(",").Select(t => { return System.Convert.ToInt32(t); }).ToList();

            if (terninger.Where(t => t > 6 || t < 1).Any())
                throw new ArgumentException("Terningene må være heltall mellom 1 og 6.",terningerInput);
            if (terninger.Count() != 5)
                throw new ArgumentException("Det må være akuratt 5 terninger.", terningerInput);

            //terningerGruppert er et sett med lister hvor listene inneholder terninger gruppert etter verdier, altså alle 1enere sammen, toerene sammen, osv.
            Dictionary<int, List<int>> terningerGruppert = new Dictionary<int, List<int>>(); 
            for (int i = 1; i <= 6; i++)
                terningerGruppert.Add(i, terninger.Where(terning => terning == i).ToList());

            int poeng = 0;
            List<int> terningerSum;
            
            switch (kategori)
            {
                case "Enere":
                    poeng = terningerGruppert[1].Sum();
                    break;
                case "Toere":
                    poeng = terningerGruppert[2].Sum();
                    break;
                case "Treere":
                    poeng = terningerGruppert[3].Sum();
                    break;
                case "Firere":
                    poeng = terningerGruppert[4].Sum();
                    break;
                case "Femere":
                    poeng = terningerGruppert[5].Sum();
                    break;
                case "Seksere":
                    poeng = terningerGruppert[6].Sum();
                    break;

                case "Par":
                    terningerSum = terningerGruppert
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First()*2) //Grunnen til at jeg har Value.First()*2 isteden for Value.Sum() er i tilfelle hvor det er mer en to terninger som har samme verdi.
                                    .ToList();
                    poeng = terningerSum.Count() != 0 ? terningerSum.Max() : 0;
                    break;

                case "ToPar":
                    if (terningerGruppert.Where(t => t.Value.Count > 4).Any()) //Spesialtilfelle: de to parene har samme verdi.
                        //Det fungerer fint å bare ta den første verdien i terningerGruppert som har 2 par, da det bare er en verdi som kan ha to par, gitt 5 terninger. 
                        poeng = terningerGruppert.Where(t => t.Value.Count > 4).First().Value.First() * 4; 
                    else
                    {
                        terningerSum = terningerGruppert
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First() * 2)
                                    .ToList();
                        poeng = terningerSum.Count() != 0 ? terningerSum.Count() != 2 ? 0 : terningerSum[^1] + terningerSum[^2] : 0;
                    }
                    break;

                case "TreLike":
                    int? treLike = terningerGruppert
                                        .Where(t => t.Value.Count >= 3)
                                        .Select(t => t.Value.First() * 3) //Se kommentaren på "Par"
                                        .FirstOrDefault(); //Kan bare velge "en tilfeldig" gitt at det bare kan være tre like blant fem terninger.
                    poeng = treLike != null ? treLike.Value : 0; //Må alikevel sjekke om det er tre like i det hele tatt.
                    break;

                case "FireLike":
                    int? fireLike = terningerGruppert
                                        .Where(t => t.Value.Count >= 4)
                                        .Select(t => t.Value.First() * 4)
                                        .FirstOrDefault();
                    poeng = fireLike != null ? fireLike.Value : 0;
                    break;

                case "LitenStraight":
                    terninger.Sort();
                    for(int i  = 1; i < 6; i++)
                        if (terninger[i] != i)
                            return poeng = 0;
                    poeng = 15;
                    break;

                case "StorStraight":
                    terninger.Sort();
                    for (int i = 1; i < 6; i++)
                        if (terninger[i] != i+1)
                            return poeng = 0;
                    poeng = 20;
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