namespace LibrarieModele
{
    public class Disciplina
    {
        private const char SEPARATOR = ';';
        private const int ID = 0;
        private const int STUDENT_ID = 1;
        private const int NUME = 2;
        private const int NOTA = 3;

        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Nume { get; set; }
        public double Nota { get; set; }

        public Disciplina()
        {
            Id = 0;
            StudentId = 0;
            Nume = "";
            Nota = 0;
        }

        public Disciplina(int id, int studentId, string nume, double nota)
        {
            Id = id;
            StudentId = studentId;
            Nume = nume;
            Nota = nota;
        }

        // Constructor care preia o linie din fisier
        public Disciplina(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = Convert.ToInt32(date[ID]);
            StudentId = Convert.ToInt32(date[STUDENT_ID]);
            Nume = date[NUME];
            Nota = Convert.ToDouble(date[NOTA]);
        }

        // Conversie obiect la sir pentru scriere in fisier
        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{0}{4}{1}{4}{2}{4}{3}",
                Id, StudentId, Nume, Nota, SEPARATOR);
        }

        public string Info()
        {
            return $"{Nume}: {Nota}";
        }

        public override string ToString()
        {
            return $"{Nume} - Nota: {Nota}";
        }
    }
}
