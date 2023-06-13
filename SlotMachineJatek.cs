using System;

namespace SlotMachine
{
    class SlotMachineJatek
    {
        private int egyenleg;

        public SlotMachineJatek()
        {
            egyenleg = 5000;
        }

        public void JatekInditas()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("┌────────────────────────────────────┐");
            Console.WriteLine("│Üdvözöllek a félkarú rabló játékban!│");
            Console.WriteLine("└────────────────────────────────────┘");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Console.Write("\nAktuális egyenleg: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(egyenleg);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\nAdj meg egy tétet vagy írd be 'Kilépek' a kilépéshez: ");

                string input = Console.ReadLine();
                Console.Clear();
                if (input.ToLower() == "kilépek")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n┌───────────────────────────┐");
                    Console.WriteLine("|Viszlát! A játék véget ért.│");
                    Console.WriteLine("└───────────────────────────┘");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                int tetOsszeg;
                bool ervenyesTet = int.TryParse(input, out tetOsszeg);

                if (!ervenyesTet)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHibás tétet adtál meg. Próbáld újra!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (tetOsszeg > egyenleg)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nincs elegendő egyenleged. Adj meg kisebb tétet!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                char[] tárcsák = GeneraltDobokockak();
                KijelzesFélkaruRabló(tárcsák);

                int aSzamlalo = SzimbolumSzamlalas(tárcsák, 'A');
                int bSzamlalo = SzimbolumSzamlalas(tárcsák, 'B');
                int cSzamlalo = SzimbolumSzamlalas(tárcsák, 'C');
                int dSzamlalo = SzimbolumSzamlalas(tárcsák, 'D');
                int eSzamlalo = SzimbolumSzamlalas(tárcsák, 'E');

                int szorzó = 0;

                if (aSzamlalo >= 2)
                {
                    szorzó = aSzamlalo == 2 ? 40 : 60;
                }
                else if (bSzamlalo >= 2)
                {
                    szorzó = bSzamlalo == 2 ? 61 : 80;
                }
                else if (cSzamlalo >= 2)
                {
                    szorzó = cSzamlalo == 2 ? 70 : 91;
                }
                else if (dSzamlalo >= 2)
                {
                    szorzó = dSzamlalo == 2 ? 89 : 101;
                }
                else if (eSzamlalo >= 2)
                {
                    szorzó = eSzamlalo == 2 ? 140 : 200;
                }

                if (szorzó > 0)
                {
                    int nyeremeny = tetOsszeg * szorzó / 100;
                    egyenleg += nyeremeny;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Nyertél! Nyeremény: {nyeremeny}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    egyenleg -= tetOsszeg;
                    Console.WriteLine("Sajnos nem nyertél.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private char[] GeneraltDobokockak()
        {
            char[] tárcsák = new char[3];
            Random veletlen = new Random();

            for (int i = 0; i < 3; i++)
            {
                int index = veletlen.Next(0, 5);
                tárcsák[i] = SzimbolumLekerdezese(index);
            }

            return tárcsák;
        }

        private char SzimbolumLekerdezese(int index)
        {
            char[] szimbolumok = { 'A', 'B', 'C', 'D', 'E' };
            return szimbolumok[index];
        }

        private void KijelzesFélkaruRabló(char[] tárcsák)
        {
            Console.WriteLine("┌───────────────────────────────┐");
            Console.WriteLine("│\t \t \t \t│");
            Console.WriteLine("│\t \t \t \t│");
            Console.WriteLine($"│\t{tárcsák[0]}\t{tárcsák[1]}\t{tárcsák[2]}\t│");
            Console.WriteLine("│\t \t \t \t│");
            Console.WriteLine("│\t \t \t \t│");
            Console.WriteLine("└───────────────────────────────┘");
        }

        private int SzimbolumSzamlalas(char[] tárcsák, char szimbolum)
        {
            int szamlalo = 0;
            foreach (char tarcsa in tárcsák)
            {
                if (tarcsa == szimbolum)
                {
                    szamlalo++;
                }
            }
            return szamlalo;
        }
    }

}