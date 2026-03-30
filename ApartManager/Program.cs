 using LibrarieModele;
using NivelStocareDate;

namespace ApartManager
{
    class Program
    {
        static IStocareData admin = StocareFactory.GetAdministratorStocare();

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

            // alegere tip aparatment din enum
            Console.WriteLine("Tip apartament:");
            foreach(int val in Enum.GetValues(typeof(TipApartament)))
            {
                Console.WriteLine($"  {val} - {(TipApartament)val}");
            }
            Console.Write("Alegeti: ");
            TipApartament tip;
            try{
                int optTip = int.Parse(Console.ReadLine());
                tip = (TipApartament)optTip;
            }
            catch(FormatException){
                Console.WriteLine("Input invalid, se seteaza Garsoniera");
                tip = TipApartament.Garsoniera;
            }

            //alegere facilitatti
            Console.WriteLine("Facilitati (puteti alege mai multe, separate prin virgula):");
            foreach(int val in Enum.GetValues(typeof(FacilitatiApartament)))
            {
                if(val == 0) continue;
                Console.WriteLine($"  {val} - {(FacilitatiApartament)val}");
            }
            Console.Write("Introduceti valorile: ");
            FacilitatiApartament facilitati = FacilitatiApartament.Niciuna;
            try
            {
                string[] vals = Console.ReadLine().Split(',');
                foreach(string v in vals)
                {
                    int f = int.Parse(v.Trim());
                    facilitati = facilitati | (FacilitatiApartament)f;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Nu s-au putut citi facilitatile");
            }

            return new Apartament(numar, etaj, suprafata, pretChirie, tip, facilitati);
        }

        static Chirias CitireChirias()
        {
            Console.Write("Nume: ");
            string nume = Console.ReadLine();
            Console.Write("Prenume: ");
            string prenume = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Nr apartament: ");
            int nrAp = int.Parse(Console.ReadLine());
            return new Chirias(nume,prenume,telefon,nrAp);
        }

        static void AfisareApartamente(List<Apartament> lista)
        {
            if(lista.Count == 0){
                Console.WriteLine("Nu exista apartamente.");
                return;
            }
            for(int i=0;i<lista.Count;i++)
            {
                Console.WriteLine(lista[i].Info());
            }
        }
        static void AfisareChiriasi(List<Chirias> lista)
        {
            if(lista.Count==0)
            {
                Console.WriteLine("Nu exista chiriasi.");
               return;
            }
            foreach(Chirias ch in lista)
                Console.WriteLine(ch.Info());
        }

        static void Main()
        {
            string optiune;
            Apartament ultimulApartament = null;

            do
            {
                Console.WriteLine("\n===== ApartManager =====");
                Console.WriteLine("C. Citire apartament nou");
                Console.WriteLine("H. Citire chirias nou");
                Console.WriteLine("I. Afisare ultimul apartament");
                Console.WriteLine("A. Afisare toate apartamentele");
                Console.WriteLine("L. Afisare toti chiriasii");
                Console.WriteLine("E. Cautare dupa etaj");
                Console.WriteLine("N. Cautare dupa numar");
                Console.WriteLine("P. Cautare dupa pret maxim");
                Console.WriteLine("X. Iesire");
                Console.Write("Optiunea: ");

                optiune = Console.ReadLine().ToUpper();
                switch(optiune)
                {
                    case "C":
                        try{
                            ultimulApartament = CitireApartament();
                            admin.AddApartament(ultimulApartament);
                            Console.WriteLine("Adaugat!");
                        }catch(Exception ex){
                            Console.WriteLine("Eroare la citire: "+ex.Message);
                        }
                        break;
                    case "H":
                        try{
                            Chirias ch = CitireChirias();
                            admin.AddChirias(ch);
                            Console.WriteLine("Chirias adaugat!");
                        }catch(Exception ex){
                            Console.WriteLine("Eroare: "+ex.Message);
                        }
                        break;
                    case "I":
                        if(ultimulApartament!=null)
                            Console.WriteLine(ultimulApartament.Info());
                        else
                            Console.WriteLine("Nu a fost citit niciun apartament.");
                        break;
                    case "A":
                        AfisareApartamente(admin.GetApartamente());
                        break;
                    case "L":
                        AfisareChiriasi(admin.GetChiriasi());
                        break;
                    case "E":
                        Console.Write("Etajul: ");
                        int etaj = int.Parse(Console.ReadLine());
                        var gasiteEtaj = admin.CautaDupaEtaj(etaj);
                        if(gasiteEtaj.Count>0)
                            AfisareApartamente(gasiteEtaj);
                        else
                            Console.WriteLine("Nimic gasit pe etajul "+etaj);
                        break;
                    case "N":
                        Console.Write("Nr apartament: ");
                        int nr = int.Parse(Console.ReadLine());
                        var gasit = admin.CautaDupaNumar(nr);
                        if(gasit!=null) Console.WriteLine(gasit.Info());
                        else Console.WriteLine("Nu exista ap. " + nr);
                        break;
                    case "P":
                        Console.Write("Pret maxim: ");
                        double pret = double.Parse(Console.ReadLine());
                        var gasitePret = admin.CautaDupaPretMaxim(pret);
                        if(gasitePret.Count>0)
                            AfisareApartamente(gasitePret);
                        else Console.WriteLine("Nimic sub "+pret+" lei");
                        break;
                    case "X":
                        Console.WriteLine("Pa!");
                        break;
                    default:
                        Console.WriteLine("Optiune gresita!");
                        break;
                }
            } while(optiune != "X");
        }
    }
}
