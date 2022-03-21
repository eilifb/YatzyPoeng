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
            
            
            switch (kategori)
            {
                case "Enere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,1);
                    break;
                case "Toere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,2);
                    break;
                case "Treere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,3);
                    break;
                case "Firere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,4);
                    break;
                case "Femere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,5);
                    break;
                case "Seksere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,6);
                    break;
                case "Par":
                    poeng = BeregnPoengPar(terningerGruppert);
                    break;
                case "ToPar":
                    poeng = BeregnPoengToPar(terningerGruppert);
                    break;
                case "TreLike":
                    poeng = BeregnPoengLike(terningerGruppert, 3);
                    break;
                case "FireLike":
                    poeng = BeregnPoengLike(terningerGruppert, 4);
                    break;
                case "LitenStraight":
                    poeng = BeregnPoengStraight(terninger, storStraight: false);
                    break;
                case "StorStraight":
                    poeng = BeregnPoengStraight(terninger, storStraight: true);
                    break;

            }

            return poeng;
        }

        private int BeregnPoengEnkeltverdier(Dictionary<int,List<int>> terningerGruppert, int indeks)
        {
            return terningerGruppert[indeks].Sum();
        }

        private int BeregnPoengPar(Dictionary<int, List<int>> terningerGruppert)
        {
            List<int> terningerSum = terningerGruppert
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First() * 2) //Grunnen til at jeg har Value.First()*2 isteden for Value.Sum() er i tilfelle hvor det er mer en to terninger som har samme verdi.
                                    .ToList();

            return terningerSum.Count() != 0 ? terningerSum.Max() : 0;
        }

        private int BeregnPoengToPar(Dictionary<int,List<int>> terningerGruppert)
        {
            if (terningerGruppert.Where(t => t.Value.Count > 4).Any()) //Spesialtilfelle: de to parene har samme verdi.
                //Det fungerer fint å bare ta den første verdien i terningerGruppert som har 2 par, da det bare er en verdi som kan ha to par, gitt 5 terninger. 
                return terningerGruppert.Where(t => t.Value.Count > 4).First().Value.First() * 4;
            else
            {
                List<int> terningerSum = terningerGruppert
                            .Where(t => t.Value.Count > 1)
                            .Select(t => t.Value.First() * 2)
                            .ToList();

                return terningerSum.Count() != 2 ? 0 : terningerSum[^1] + terningerSum[^2];
            }
        }

        private int BeregnPoengLike(Dictionary<int, List<int>> terningerGruppert, int antallLike)
        {
            int? likeVerdier = terningerGruppert
                                        .Where(t => t.Value.Count >= antallLike)
                                        .Select(t => t.Value.First() * antallLike) //Må igjen gange istedenfor å summe, se evt. kommentaren i BeregnPoengPar();
                                        .FirstOrDefault(); //Kan bare velge "en tilfeldig" gitt at det bare kan være tre like blant fem terninger.

            return likeVerdier != null ? likeVerdier.Value : 0; //Må alikevel sjekke om det er tre like i det hele tatt.
        }

        private int BeregnPoengStraight(List<int> terninger, bool storStraight = false)
        {
            //Måten den sjekker om terningene oppfyller kravene for å bli telt som en straight er at den først sorterer
            //terningene, så går den gjennom terningene en etter en og sammenligner med en straight, som her er straighSammenligner.
            //Om det er en terning som ikke matcher straighten så avsluttes algorytmen tidlig og poeng blir returnert som 0.           
            int straightSammenligner = storStraight ? 2 : 1;
            terninger.Sort();

            for (int i = 0; i < 5; i++,straightSammenligner++)
                if (terninger[i] != straightSammenligner)
                    return 0;

            return storStraight ? 20 : 15;
        }
    }
}