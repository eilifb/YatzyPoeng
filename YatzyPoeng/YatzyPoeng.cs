using System;
using System.Collections.Generic;
using System.Linq;

namespace YatzyPoengNS
{
    public class YatzyPoeng
    {
        /// <summary>
        /// Gitt et sett med terninger og en yatzy poengkategori beregnes hvor mange poeng du tjener.
        /// </summary>
        /// <param name="terningerInput"> 5 terninger separert med komma</param>
        /// <param name="kategori">Kategorien den ønskes å beregnes for</param>
        /// <returns>Poengene for kategorien oppgitt</returns>
        /// <example>
        /// <code>
        ///     string terningerInput = "6,6,6,6,6";
        ///     string kategori = "Yatzy";
        ///     YatzyPoeng yp = new YatzyPoeng();
        ///     yp.BeregnPoeng(terningerInput, kategori);        ///     
        /// </code> 
        /// </example>
        public int BeregnPoeng(string terningerInput, string kategori)
        { 
            kategori = kategori.Replace(" ", "").ToLower(); //Litt vanskeligere å lese switch blokken, men mer motstandsdyktig mot forskjellige stavemåter.
            List<int> terninger;

            try
            {
                terninger = terningerInput.Split(",").Select(t => { return System.Convert.ToInt32(t); }).ToList();
            }
            catch(FormatException e)
            {
                throw new ArgumentException("Terningene må formateres som tall med komma mellom, f.eks '1,2,3,4,5'", terningerInput);
            }

            if (terninger.Where(t => t > 6 || t < 1).Any())
                throw new ArgumentException("Terningene må være heltall mellom 1 og 6.",terningerInput);
            if (terninger.Count() != 5)
                throw new ArgumentException("Det må være akuratt 5 terninger.", terningerInput);
                        
            Dictionary<int, List<int>> terningerGruppert = GrupperTerningerEtterVerdi(terninger);
            int poeng = 0;
            
            switch (kategori)
            {
                case "enere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,1);
                    break;
                case "toere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,2);
                    break;
                case "treere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,3);
                    break;
                case "firere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,4);
                    break;
                case "femere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,5);
                    break;
                case "seksere":
                    poeng = BeregnPoengEnkeltverdier(terningerGruppert,6);
                    break;
                case "par":
                    poeng = BeregnPoengPar(terningerGruppert);
                    break;
                case "topar":
                    poeng = BeregnPoengToPar(terningerGruppert);
                    break;
                case "trelike":
                    poeng = BeregnPoengLike(terningerGruppert, 3);
                    break;
                case "firelike":
                    poeng = BeregnPoengLike(terningerGruppert, 4);
                    break;
                case "litenstraight":
                    poeng = BeregnPoengStraight(terninger, storStraight: false);
                    break;
                case "storstraight":
                    poeng = BeregnPoengStraight(terninger, storStraight: true);
                    break;
                case "fulthus":
                    poeng = BeregnPoengFultHus(terningerGruppert);
                    break;
                case "sjanse":
                    poeng = BergenPoengSjanse(terninger);
                    break;
                case "yatzy":
                    poeng = BeregnPoengYatzy(terningerGruppert);
                    break;
                default:
                    throw new ArgumentException("Fant ikke poengkategorien du skrev inn. Kan du ha stavet feil?", kategori);
            }

            return poeng;
        }

        /// <summary>
        /// Samler opp terningene og grupperer dem etter verdi
        /// </summary>
        /// <param name="terninger">En liste med terningverdier</param>
        /// <returns>
        /// En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien.
        /// </returns>
        public Dictionary<int,List<int>> GrupperTerningerEtterVerdi(List<int> terninger)
        {
            Dictionary<int, List<int>> terningerGruppert = new Dictionary<int, List<int>>();
            for (int i = 1; i <= 6; i++)
                terningerGruppert.Add(i, terninger.Where(terning => terning == i).ToList());

            return terningerGruppert;
        }

        /// <summary>
        /// Summerer alle terningene i et kast som har en gitt terningverdi.
        /// </summary>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <param name="verdi">Hvilen terningverdi du har lyst til å summere</param>
        /// <returns>Summen av alle terningene med en gitt terningverdi.</returns>
        private int BeregnPoengEnkeltverdier(Dictionary<int,List<int>> terningerGruppert, int verdi)
        {
            return terningerGruppert[verdi].Sum();
        }

        /// <summary>
        /// Sjekker om det er et eller flere par i et kast og summerer terningverdien i det høyeste paret.
        /// </summary>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <returns>Summen av det paret som har høyest terningverdi, eller 0 om det ikke er noen par.</returns>
        private int BeregnPoengPar(Dictionary<int, List<int>> terningerGruppert)
        {
            List<int> terningerSum = terningerGruppert
                                    .Where(t => t.Value.Count > 1)
                                    .Select(t => t.Value.First() * 2) //Grunnen til at jeg har Value.First()*2 isteden for Value.Sum() er i tilfelle hvor det er mer en to terninger som har samme verdi.
                                    .ToList();

            return terningerSum.Count() != 0 ? terningerSum.Max() : 0;
        }

        /// <summary>
        /// Sjekker om det er et eller flere par i et kast, og summerer terningverdien til de to høyeste parene.
        /// </summary>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <returns>Summen av de to parene med høyest terningverdi, eller 0 om det ikke er to par.</returns>
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

        /// <summary>
        /// Sjekker om det er et antall like terningverdier i et kast og summerer de terningverdiene.       
        /// </summary>
        /// <remarks>
        /// Hvor mange like funksjonen ser etter tar den ikke hensyn til, utover det spesifisert i antallLike.
        /// Så man kan bryte vanlige Yatzyregler her hvis man setter antallLike til noe annet en 3 eller 4.
        /// </remarks>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <param name="antallLike">Avgjør hvor mange like funksjonen ser etter</param>
        /// <returns>Summen av terningene som har lik terningverdi, eller 0 om det ikke er nok antall like terningverdier.</returns>
        private int BeregnPoengLike(Dictionary<int, List<int>> terningerGruppert, int antallLike)
        {
            int? likeVerdier = terningerGruppert
                                        .Where(t => t.Value.Count >= antallLike)
                                        .Select(t => t.Value.First() * antallLike) //Må igjen gange istedenfor å summe, se evt. kommentaren i BeregnPoengPar();
                                        .FirstOrDefault(); //Kan bare velge "en tilfeldig" gitt at det bare kan være tre like blant fem terninger.

            return likeVerdier != null ? likeVerdier.Value : 0; //Må alikevel sjekke om det er tre like i det hele tatt.
        }


        /// <summary>
        /// Sjekker om terningene i et kast utgjør en stor eller liten straight, og summerer terningverdiene hvis den gjør det.
        /// </summary>
        /// <param name="terninger">En liste over 5 terninger i et kast</param>
        /// <param name="storStraight">Avgjør om funksjonen skal se etter en liten (false) eller stor (true) straight</param>
        /// <returns>15 poeng for liten straight, 20 poeng for stor straight, 0 for ingen straight.</returns>
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

        /// <summary>
        /// Sjekker om to terninger deler en terningverdi og tre terninger deler en annen terningverdi (eller alle 5 deler samme), og summerer terningverdiene om de gjør.
        /// </summary>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <returns>Summen av terningverdiene om de oppfyller kravene, 0 hvis de ikke gjør det.</returns>
        private int BeregnPoengFultHus(Dictionary<int, List<int>> terningerGruppert)
        {
            if(terningerGruppert.Where(t => t.Value.Count == 2).Any() && terningerGruppert.Where(t => t.Value.Count == 3).Any())
                return terningerGruppert.Select(t => t.Value.Sum()).Sum();

            else if(terningerGruppert.Where(t => t.Value.Count == 5).Any()) //Antar at to terninger som er f.eks "2,2" og tre terninger som er "2,2,2" er gyldige.
                return terningerGruppert.Select(t => t.Value.Sum()).Sum();

            else
                return 0;            
        }

        /// <summary>
        /// Summerer alle terningverdiene i kastet.
        /// </summary>
        /// <param name="terninger">En liste over 5 terninger i et kast</param>
        /// <returns>Summen av alle terningverdiene i et kast.</returns>
        private int BergenPoengSjanse(List<int> terninger)
        {
            return terninger.Sum();
        }

        /// <summary>
        /// Sjekker om alle terningene i et kast deler terningverdi, og summerer de hvis de gjør.
        /// </summary>
        /// <param name="terningerGruppert">En Dictionary hvor Key'ene er terningverdier, og Value'ene er alle terningene i kastet med den terningverdien</param>
        /// <returns>Summen av alle terningverdiene, eller 0 hvis ikke alle terningverdiene er like.</returns>
        private int BeregnPoengYatzy(Dictionary<int, List<int>> terningerGruppert)
        {
            if (terningerGruppert.Where(t => t.Value.Count == 5).Any())
                return terningerGruppert.Select(t => t.Value.Sum()).Sum();
            else
                return 0;
        }
    }
}