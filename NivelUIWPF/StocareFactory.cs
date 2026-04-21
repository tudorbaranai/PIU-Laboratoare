using NivelStocareDate;
using System.Configuration;
using System.IO;

namespace NivelUIWPF
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER = "NumeFisier";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "";

            //calea catre directorul solutiei (din bin/Debug/netX-windows)
            string locatieFisierSolutie =
                Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? "";
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            if(formatSalvare != null)
            {
                switch(formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareApartamenteMemorie();
                    case "txt":
                        return new AdministrareApartamenteFisierText(caleCompletaFisier);
                }
            }
            return null;
        }
    }
}
