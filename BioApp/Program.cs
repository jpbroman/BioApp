// See https://aka.ms/new-console-template for more information

using System;

namespace BioApp
{
    internal class Program
    {
        private const string meny = "0 - Avsluta programmet\n" + 
                                    "1 - Boka. (Du får ange din ålder för ungdoms- eller pemsionärspris.)\n" +
                                    "2 - Boka sällskap.";
        
        static void Main(string[] args)
        {
            int menyVal = 0;
            
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
                    default:
                        InfoFelInmatning();
                        break;
                }
            }
        }

        private static void InfoFelInmatning(string extra = "")
        {
            Console.WriteLine($"Felaktig inmatning! Försök igen. {extra}");
        }

        private static int BokaBiljett()
        {
            int ålder;

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
                int pris = BokaBiljett();
                if (pris < 0)
                {
                    return;
                }
                totalPris += pris;
            }
            Console.WriteLine($"Ni är {antal} personer. Kostnad för hela sällskapet: {totalPris}.");
        }

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
