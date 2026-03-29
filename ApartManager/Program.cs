namespace ApartManager
{
    class Program
    {
        // lista de apartamente
        static List<Apartament> apartamente = new List<Apartament>();
        static List<Chirias> chiriasi = new List<Chirias>();

        static Apartament CitireApartament()
        {
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

            string[] facilitati = new string[nrFacilitati];
            for (int i = 0; i < nrFacilitati; i++)
            {
                Console.Write($"Facilitatea {i + 1}: ");
                facilitati[i] = Console.ReadLine();
            }

            return new Apartament(numar, etaj, suprafata, pretChirie, facilitati);
        }

        static Chirias CitireChirias()
        {
            Console.Write("Nume: ");
            string nume = Console.ReadLine();
            Console.Write("Prenume: ");
            string prenume = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Numar apartament: ");
            int nrAp = int.Parse(Console.ReadLine());

            return new Chirias(nume, prenume, telefon, nrAp);
        }

        // afisare apartamente din lista
        static void AfisareApartamente(List<Apartament> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nu exista apartamente.");
                return;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine(lista[i].Info());
            }
        }

        static void AfisareChiriasi(List<Chirias> lista)
        {
            foreach (Chirias ch in lista)
            {
                Console.WriteLine(ch.Info());
            }
        }

        // cautare apartamnet dupa numar
        static Apartament CautaDupaNumar(List<Apartament> lista, int numar)
        {
            foreach (Apartament ap in lista)
            {
                if (ap.Numar == numar)
                    return ap;
            }
            return null;
        }

        // cauta apartamente pe un etaj
        static List<Apartament> CautaDupaEtaj(List<Apartament> lista, int etaj)
        {
            List<Apartament> rezultat = new List<Apartament>();
            foreach (Apartament ap in lista)
            {
                if (ap.Etaj == etaj)
                    rezultat.Add(ap);
            }
            return rezultat;
        }

        // cauta apartamente cu chiria sub un pret
        static List<Apartament> CautaDupaPretMaxim(List<Apartament> lista, double pretMax)
        {
            List<Apartament> rezultat = new List<Apartament>();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].PretChirie < pretMax)
                    rezultat.Add(lista[i]);
            }
            return rezultat;
        }

        static void Main()
        {
            string optiune;

            do
            {
                Console.WriteLine("\n===== ApartManager =====");
                Console.WriteLine("C. Citire apartament nou");
                Console.WriteLine("H. Citire chirias nou");
                Console.WriteLine("A. Afisare toate apartamentele");
                Console.WriteLine("L. Afisare toti chiriasii");
                Console.WriteLine("E. Cautare apartamente dupa etaj");
                Console.WriteLine("N. Cautare apartament dupa numar");
                Console.WriteLine("P. Cautare apartamente dupa pret maxim");
                Console.WriteLine("X. Iesire");
                Console.Write("\nAlegeti optiunea: ");

                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "C":
                        Apartament ap = CitireApartament();
                        apartamente.Add(ap);
                        Console.WriteLine("Apartamentul a fost adaugat!");
                        break;

                    case "H":
                        Chirias ch = CitireChirias();
                        chiriasi.Add(ch);
                        Console.WriteLine("Chiriasul a fost adaugat!");
                        break;

                    case "A":
                        AfisareApartamente(apartamente);
                        break;

                    case "L":
                        AfisareChiriasi(chiriasi);
                        break;

                    case "E":
                        Console.Write("Etajul cautat: ");
                        int etajCautat = int.Parse(Console.ReadLine());
                        List<Apartament> gasiteEtaj = CautaDupaEtaj(apartamente, etajCautat);
                        if (gasiteEtaj.Count > 0)
                        {
                            Console.WriteLine("Apartamente pe etajul " + etajCautat + ":");
                            AfisareApartamente(gasiteEtaj);
                        }
                        else
                            Console.WriteLine("Nu s-au gasit apartamente pe etajul " + etajCautat);
                        break;

                    case "N":
                        Console.Write("Numarul apartamentului: ");
                        int nrCautat = int.Parse(Console.ReadLine());
                        Apartament gasit = CautaDupaNumar(apartamente, nrCautat);
                        if (gasit != null)
                            Console.WriteLine(gasit.Info());
                        else
                            Console.WriteLine("Apartamentul " + nrCautat + " nu a fost gasit.");
                        break;

                    case "P":
                        Console.Write("Pretul maxim al chiriei (lei): ");
                        double pretMax = double.Parse(Console.ReadLine());
                        List<Apartament> gasitePret = CautaDupaPretMaxim(apartamente, pretMax);
                        if (gasitePret.Count > 0)
                            AfisareApartamente(gasitePret);
                        else
                            Console.WriteLine("Nu exista apartamente cu chiria sub " + pretMax + " lei.");
                        break;

                    case "X":
                        Console.WriteLine("La revedere!");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != "X");
        }
    }
}
