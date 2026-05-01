
using System;
using System.Text.RegularExpressions;

namespace BioApp
{
    internal class Program
    {
        private const string meny = "0 - Avsluta programmet\n" + 
                                    "1 - Boka. (Du får ange din ålder för ungdoms- eller pemsionärspris.)\n" +
                                    "2 - Boka sällskap.\n" +
                                    "3 - Recension (Skriv en kort text om vad du tyckte om filmen)\n" +
                                    "4 - Vad tycker du om BioAppen? (Skriv en rad. Minst tre ord)";
        
        static void Main(string[] args)
        {
            int menyVal = 0;
 // Här borde man ropa på en metod som skriver ut vilka filer som är på G just nu           
            Console.WriteLine("*** Välkommen till bokningssystemet ***");

            while (true)
            {
                Console.WriteLine(
                    "\nHuvudmeny:\n- Du navigerar i menyn genom att skriva in siffran för önskat menyval.");
                Console.WriteLine(meny);
                Console.Write("Val: ");

                bool inputOk = int.TryParse(Console.ReadLine(), out menyVal);
                if (!inputOk)
                {
                    InfoFelInmatning();
                    continue;
                }

                switch (menyVal)
                {
                    case 0:
                        Console.WriteLine("Tack och välkommen åter.");
                        System.Environment.Exit(0);
                        break;
                    case 1:
                        BokaBiljett();
                        break;
                    case 2:
                        BokaGrupp();
                        break;
// Här kanske ett case för att skriva ut biljett(er) eller få skickad på epost/sms
                    case 3:
                        Recension();
                        break;
                    case 4:
                        Omdöme();
                        break;
                    default:
                        InfoFelInmatning();
                        break;
                }
            }
        }

        private static void Omdöme()
        {
            Console.Write("Skriv vad du tycker om BioAppen. (Minst tre ord)\n> ");
            string? omdöme = Console.ReadLine();
            if (omdöme is null || omdöme.Equals("")) // säkerställ att vi har inmatning
            {
                Console.WriteLine("Skriv minst tre ord.");
                return;
            }
            // Ta bort multipla mellanslag
            omdöme = Regex.Replace(omdöme, @"\s+", " ");

            string[] omdömeArray = omdöme.Split(null);
            try {
                Console.WriteLine($"3e ordet är: {omdömeArray[2]}");
            }
            catch (IndexOutOfRangeException)  // fånga felfallet färre än tre ord
            {
                Console.WriteLine("Jag bad om tre ord.");
                return;
            }
        }

        private static void Recension()
        {
            string? recension = "";
            Console.Write("Skriv en mening om vad du tyckte om filmen.\n> ");
            recension = Console.ReadLine();

            Console.WriteLine("Du tycker alltså:");
            for (int i=0; i<10; i++)
            {
                Console.Write(recension);
                if (i<9) Console.Write(" ");
            }            
        }

        // hjälpfunktion för att skriva ut felmeddelande till användare.
        // Standardmeddelande och valfri extratext.
        private static void InfoFelInmatning(string extra = "")
        {
            Console.WriteLine($"Felaktig inmatning! Försök igen. {extra}");
        }
        
        //Bokar enstaka biljett. Returnarar pris beroende på biljettyp   
        private static int BokaBiljett(int n=0)
        {
            int ålder;

            if (n > 0)  // Hantera ledtext för gruppbokning
            {
                Console.Write($"Person {n}. ");
            }
            Console.Write("Ange besökarens ålder: ");
            if (!int.TryParse(Console.ReadLine(), out ålder))
            {
                InfoFelInmatning("Du anger ålder med siffror.");
                return -1;
            }

            (string biljett, int pris) = BiljettPris(ålder);
            Console.WriteLine($"{biljett}: {pris}kr");
            return pris;
        }

        // Bokar ett helt sällskap.
        private static void BokaGrupp()
        {
            int antal = 0, totalPris = 0;
            Console.WriteLine("Hur många ingår i sällskapet? ");
            if (!int.TryParse(Console.ReadLine(), out antal))
            {
                InfoFelInmatning("Ge mig antal biljetter du vill boka.");
                return; 
            }
            for (int i=0; i<antal; i++)
            {
                int pris = BokaBiljett(i+1);
                if (pris < 0)
                {
                    return;
                }
                totalPris += pris;
            }
            Console.WriteLine($"Ni är {antal} personer. Kostnad för hela sällskapet: {totalPris}.");
        }

        // Biljetttyp och pris baserat på ålder.
        private static (string, int) BiljettPris(int ålder)
        {
            if (ålder < 20)
            {
                return ("Ungdom", 80);
            }
            else if (ålder > 64)
            {
                return ("Pensionär", 90);
            }
            else
            {
                return ("Standardpris", 120);
            }
        }
    }
}
