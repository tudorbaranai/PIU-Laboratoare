using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AddApartament(Apartament ap);
        List<Apartament> GetApartamente();

        void AddChirias(Chirias ch);
        List<Chirias> GetChiriasi();

        void AddStudent(Student st);
        List<Student> GetStudenti();
        Student CautaStudentDupaId(int id);

        Apartament CautaDupaNumar(int numar);
        List<Apartament> CautaDupaEtaj(int etaj);
        List<Apartament> CautaDupaPretMaxim(double pretMax);
        List<Chirias> CautaChiriasiDupaNume(string nume);
    }
}
