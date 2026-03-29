namespace ApartManager
{
    class Program
    {
        static void Main()
        {
            // apartamente de test
            string[] facilitati1 = { "balcon", "parcare", "centrala termica" };
            Apartament ap1 = new Apartament(1, 2, 55.5, 1500, facilitati1);

            string[] facilitati2 = { "balcon", "loc de joaca" };
            Apartament ap2 = new Apartament(7, 0, 80, 2500, facilitati2);

            Apartament ap3 = new Apartament(12, 4, 40, 1200);

            Console.WriteLine("=== Apartamente existente ===");
            Console.WriteLine(ap1.Info());
            Console.WriteLine(ap2.Info());
            Console.WriteLine(ap3.Info());

            Console.WriteLine("\n=== Detalii suplimentare ===");
            Console.WriteLine($"Ap. {ap1.Numar} - Chirie anuala: {ap1.ChirieAnuala} lei, Scump: {ap1.EsteScump}");
            Console.WriteLine($"Ap. {ap2.Numar} - Chirie anuala: {ap2.ChirieAnuala} lei, Scump: {ap2.EsteScump}");
            Console.WriteLine($"Ap. {ap3.Numar} - Chirie anuala: {ap3.ChirieAnuala} lei, Scump: {ap3.EsteScump}");

            // citire apartament de la tastatura
            Console.WriteLine("\n=== Introducere apartament nou ===");
            Console.Write("Numarul apartamentului: ");
            int numar = int.Parse(Console.ReadLine());

            Console.Write("Etajul: ");
            int etaj = int.Parse(Console.ReadLine());

            Console.Write("Suprafata (mp): ");
            double suprafata = double.Parse(Console.ReadLine());

            Console.Write("Pretul chiriei (lei/luna): ");
            double pretChirie = double.Parse(Console.ReadLine());

            Console.Write("Numar facilitati: ");
            int nrFacilitati = int.Parse(Console.ReadLine());

            string[] facilitatiNoi = new string[nrFacilitati];
            for (int i = 0; i < nrFacilitati; i++)
            {
                Console.Write($"Facilitatea {i + 1}: ");
                facilitatiNoi[i] = Console.ReadLine();
            }

            Apartament apNou = new Apartament(numar, etaj, suprafata, pretChirie, facilitatiNoi);
            Console.WriteLine("\nApartament adaugat:");
            Console.WriteLine(apNou.Info());
            Console.WriteLine($"Chirie anuala: {apNou.ChirieAnuala} lei");

            // citire chirias
            Console.WriteLine("\n=== Introducere chirias ===");
            Console.Write("Nume chirias: ");
            string numeCh = Console.ReadLine();
            Console.Write("Prenume chirias: ");
            string prenumeCh = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Numar apartament asociat: ");
            int nrAp = int.Parse(Console.ReadLine());

            Chirias chirias = new Chirias(numeCh, prenumeCh, telefon, nrAp);
            Console.WriteLine("\n" + chirias.Info());

            Console.ReadKey();
        }
    }
}
