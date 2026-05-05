namespace LibrarieModele
{
    public class Student
    {
        // Colecție predefinita cu formele de finanțare
        public static readonly string[] FormeFinantare = { "cu taxa", "buget", "cu bursa" };

        private const char SEPARATOR = ';';
        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int FORMA_FINANTARE = 3;

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string FormaFinantare { get; set; }

        // Proprietate calculată pentru afișare completă
        public string NumeComplet => $"{Prenume} {Nume}";

        public Student()
        {
            Id = 0;
            Nume = "";
            Prenume = "";
            FormaFinantare = "cu taxa";
        }

        public Student(int id, string nume, string prenume, string formaFinantare)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            FormaFinantare = formaFinantare;
        }

        // Constructor care preia o linie din fisier
        public Student(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = Convert.ToInt32(date[ID]);
            Nume = date[NUME];
            Prenume = date[PRENUME];
            FormaFinantare = date[FORMA_FINANTARE];
        }

        // Conversie obiect la sir pentru scriere in fisier
        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{0}{4}{1}{4}{2}{4}{3}",
                Id, Nume, Prenume, FormaFinantare, SEPARATOR);
        }

        public string Info()
        {
            return $"ID: {Id} | {NumeComplet} | Forma: {FormaFinantare}";
        }

        public override string ToString()
        {
            return NumeComplet;
        }
    }
}
