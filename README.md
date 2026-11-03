# ApartManager

Aplicație C# pentru administratorul unei clădiri cu apartamente închiriate: evidența apartamentelor și a chiriașilor. Am construit-o pas cu pas la laboratoarele de Proiectarea Interfețelor Utilizator (PIU), întâi ca aplicație consolă, apoi cu interfață grafică WPF.

<!-- SCREENSHOT: o captură cu fereastra WPF -->

## Ce face acum

- **Apartamente:** adaug, modific și caut apartamente după număr, etaj sau preț maxim. Fiecare apartament are tip (garsonieră până la penthouse), suprafață, preț, facilități (balcon, parcare, centrală etc.), tip de finanțare și data adăugării.
- **Chiriași:** adaug, modific și șterg chiriași (nume, prenume, telefon, apartament), cu validare pe fiecare câmp și căutare care filtrează lista pe măsură ce scriu.
- **Salvare:** datele rămân între rulări, într-un fișier text sau doar în memorie. Formatul se alege din `App.config` (`FormatSalvare`).

### Planificat, încă neimplementat

- evidența plăților de chirie și a restanțelor;
- cereri de reparații cu prioritate și status;
- rapoarte (apartamente libere, restanțieri) și venitul lunar din chirii.

## Structură

Soluția e împărțită pe niveluri, astfel încât interfața să nu depindă de felul în care sunt salvate datele:

```
ApartManager.sln
├── LibrarieModele/     clasele Apartament și Chirias, plus enum-urile
├── NivelStocareDate/   interfața IStocareData, cu salvare în memorie sau în fișier text
├── ApartManager/       versiunea consolă (laboratoarele 1–5)
└── NivelUIWPF/         interfața grafică WPF (laboratoarele 6–11)
```

## Laboratoare

| Nr. | Temă | Ce am făcut |
|-----|------|-------------|
| 1 | Calcul salariu angajat | Citire date, conversii de tip, structuri condiționale |
| 2 | Clase | Clasele `Apartament` și `Chirias` |
| 3 | Colecții | Meniu în consolă și liste `List<T>` |
| 4 | Enum-uri și LINQ | Tip apartament și facilități ca enum, căutări cu LINQ, separarea în proiecte |
| 5 | Fișiere | Salvare în fișier text, interfața `IStocareData` și un factory pentru tipul de stocare |
| 6 | WPF, prima fereastră | Label-uri, culori, fonturi, fereastră maximizată |
| 7 | Validări și controale | TextBox, RadioButton, CheckBox și validarea formularului |
| 8 | Layout managers | DockPanel cu meniu vertical, WrapPanel, căutare apartament după număr |
| 9 | ListBox, ComboBox, DatePicker | Modificare apartament, facilități cu selecție multiplă, data adăugării, tip finanțare |
| 10 | Data binding | `INotifyPropertyChanged`, `ObservableCollection`, binding TwoWay, CRUD pentru chiriași |
| 11 | Validări și MVVM | `IDataErrorInfo` pe `Chirias`, filtrare live cu `CollectionViewSource`, un ViewModel simplu |

## Cum rulez aplicația

Am nevoie de Windows, Visual Studio 2026 și .NET 10.

1. Deschid `ApartManager.sln` în Visual Studio.
2. Click dreapta pe **NivelUIWPF** → **Set as Startup Project**. Pentru versiunea consolă aleg **ApartManager**.
3. Apăs F5.

Dacă build-ul dă erori ciudate după ce schimb branch-ul, șterg folderele `bin` și `obj` din proiecte și dau din nou Build.

## Limitări

- Merge doar pe Windows, pentru că interfața e WPF.
- Fișierul text nu e protejat la scrieri simultane, deci aplicația e gândită pentru un singur utilizator.
- Plățile, reparațiile și rapoartele din descrierea temei nu sunt încă făcute.

## Drepturi

© 2026 Tudor Baranai. Toate drepturile rezervate. Codul e public ca să poată fi citit, nu ca să fie refolosit.
