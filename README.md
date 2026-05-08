# PIU-Laboratoare

## Aplicatia practica: ApartManager

ApartManager este o aplicatie consola C# pentru gestionarea unei cladiri de apartamente. Sistemul este destinat administratorului care doreste sa tina evidenta apartamentelor, chiriasilor, platilor de chirie si solicitarilor de reparatii.

### Operatii planificate

- **Gestiune apartamente** - adaugare, editare si stergere apartamente (numar, etaj, suprafata, pret chirie)
- **Gestiune chiriasi** - inregistrare chiriasi si asocierea lor cu apartamente
- **Evidenta plati** - urmarirea platilor lunare de chirie (platit/neplatit, calculare restante)
- **Cereri de reparatii** - inregistrare solicitari cu descriere, prioritate si status
- **Rapoarte** - afisare apartamente libere, lista restantieri, reparatii in asteptare
- **Calcul venituri** - total venituri din chirii pe luna

## Structura proiectului

```
PIU-Laboratoare/
├── README.md
├── .gitignore
├── ApartManager.sln              # solutia principala
├── ApartManager/                 # aplicatia consola
├── LibrarieModele/               # clasele Apartament, Chirias, enum-uri
├── NivelStocareDate/             # IStocareData + memorie + fisier text
└── NivelUIWPF/                   # interfata grafica WPF (lab 6+)
```

## Laboratoare

| Nr. | Tema | Descriere |
|-----|------|-----------|
| 1 | Calcul salariu angajat | Citire date, conversii de tip, structuri conditionale |
| 6 | WPF - prima fereastra | Label-uri, culori, fonturi si fereastra maximizata |
| 7 | Validari si controale | TextBox, RadioButton, CheckBox + validari pe formular |
| 8 | Layout managers | DockPanel cu meniu vertical, WrapPanel responsive, cautare apartament dupa numar |
| 9 | ListBox / ComboBox / DatePicker | Modificare apartament cu ComboBox, ListBox multi-select pt facilitati, DatePicker pt data adaugare, ListBox single pt tip finantare |
| 10 | Data Binding | INotifyPropertyChanged pe Chirias, ObservableCollection, binding TwoWay + element-to-element, CRUD complet pt chiriasi |

## Cum rulez aplicatia

1. Deschid `ApartManager.sln` in Visual Studio
2. Click dreapta pe **NivelUIWPF** -> Set as Startup Project
3. F5 (sau Ctrl+F5)

Daca dau eroare ciudata gen "AddStudent" sau alte resturi, sterg folderele `bin` si `obj` din proiecte si dau Build din nou.

## Tehnologii

- C# / .NET 10
- Visual Studio 2022 / 2026
