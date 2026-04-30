// See https://aka.ms/new-console-template for more information

using System;

namespace BioApp
{
    internal class Program
    {
        private const string meny = "0 - Avsluta programmet\n" + 
                                    "1 - Boka. (Du får ange din ålder för ungdoms- eller pemsionärspris.)";
        
        static void Main(string[] args)
        {
            int menyVal = 0;
            
            Console.WriteLine("*** Välkommen till bokningssystemet ***");

            while (true)
            {
                Console.WriteLine("±nHuvudmeny:\n- Du navigerar i menyn genom att skriva in siffran för önskat menyval.");
                Console.WriteLine(meny);
                Console.Write("Val: ");
                
                bool inputOk = int.TryParse(Console.ReadLine(), out menyVal);
                if (!inputOk)
                {
                    Console.WriteLine(("Felaktig inmatning! Försök igen."));
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
                    default:
                        Console.WriteLine(("Felaktig inmatning! Försök igen."));
                        break;
                }
            }


        }

        private static void BokaBiljett()
        {
            int ålder;

            Console.Write("Ange din ålder: ");
            if (!int.TryParse(Console.ReadLine(), out ålder))
            {
                Console.WriteLine("Felaktig inmatning. Du anger din ålder med siffror.");
                return;
            }

            (string biljett, int pris) = BiljettPris(ålder);
            Console.WriteLine($"{biljett}: {pris}kr");
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
