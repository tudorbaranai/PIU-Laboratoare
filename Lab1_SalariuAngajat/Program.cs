using System;

class Program
{
    static void Main()
    {
        // Citire numar de ore lucrate (int)
        Console.Write("Introduceti numarul de ore lucrate: ");
        int oreLucrate = int.Parse(Console.ReadLine());

        // Citire tarif pe ora (double)
        Console.Write("Introduceti tariful pe ora (lei): ");
        double tarifPeOra = double.Parse(Console.ReadLine());

        // Calcul salariu (conversie implicita int -> double la inmultire)
        double salariu = oreLucrate * tarifPeOra;

        // Afisare salariu
        Console.WriteLine($"Salariul dumneavoastra este: {salariu:F2} lei");

        // Verificare prag salarial
        if (salariu > 3000)
        {
            Console.WriteLine("Salariu mare");
        }
        else
        {
            Console.WriteLine("Ati lucrat prea putine ore pentru a avea un salariu mare!");
        }
    }
}
