# Aplicație Practică – Gestionare Tranzacții Târg Auto

## Tema
Aplicația are ca scop administrarea, evidența și centralizarea tranzacțiilor de vânzare-cumpărare realizate într-un târg auto. Aceasta oferă o interfață grafică modernă și organizată, configurată pe o arhitectură solidă, asigurând o gestionare fluidă a parcului auto și a istoricului clienților.

---

## Functionalitati
Aplicația permite:
* **Introducerea și salvarea unei tranzacții complete:** Detalii despre vânzător, cumpărător, modelul mașinii, preț, culoare, dotări și data tranzacției.
* **Modificarea și ștergerea tranzacțiilor:** Posibilitatea de a edita complet datele unei tranzacții direct din tabel sau de a o elimina.
* **Filtrare și căutare în timp real:** Căutarea rapidă a tranzacțiilor după modelul mașinii, fără riscul de a pierde sau șterge datele din memorie.
* **Gestiune avansată a clienților:** Un panou dedicat care colectează automat clienții unici și afișează istoricul detaliat al mașinilor tranzacționate de aceștia (specificând dacă au fost Vânzători sau Cumpărători).
* **Sistem de Ghost Text (Placeholder):** Ghidaj text discret în interiorul căsuțelor (ex: "Nume Vânzător..."), care dispare când începi să tastezi și revine dacă lași câmpul gol.

---

## Date stocate pentru fiecare tranzactie
Pentru fiecare mașină tranzacționată se vor înregistra:
* Nume vânzător
* Nume cumpărător
* Modelul mașinii (legat direct prin structura MVVM)
* Preț (legat direct prin structura MVVM)
* Culoare (selectabilă dintr-un dropdown dedicat)
* Dotări / Opțiuni extinse (bifate ca flag-uri: Aer Condiționat, Navigație, Cutie Automată, Decapotabilă, 4x4, Geamuri Electrice)
* Data tranzacției

---

## Stocare
Datele sunt persistate automat și local într-un fișier numit `date_salvate.txt`:
* **La pornire:** Aplicația citește automat fișierul și reia toate informațiile introduse anterior.
* **La închidere:** În momentul în care apeși pe X pentru a închide programul, toate tranzacțiile din listă sunt salvate automat în fișier pentru a nu pierde nimic la următorul restart.

---

## Rapoarte
Aplicația oferă următoarele elemente de vizualizare:
* **Istoricul tranzacțiilor pe client:** Selectarea unui client din lista dedicată încarcă instant toate mașinile legate de numele său, prețurile și datele calendaristice aferente.
* **Afișare fluidă a dotărilor:** În tabelul principal, dacă o mașină are foarte multe dotări bifate, textul se așază automat pe mai multe rânduri (Text Wrapping) pentru a fi complet vizibil.

---

## Observatii
Aplicația este realizată folosind programarea orientată pe obiecte și este structurată pe o arhitectură curată multi-tier (WPF, LibrarieModele, NivelStocareDate).
* **Validarea automată a datelor (MVVM):** Câmpurile pentru Model și Preț sunt monitorizate în timp real prin interfața `IDataErrorInfo`. Dacă un câmp este lăsat gol sau prețul este invalid (litere, numere mai mici sau egale cu 0), căsuța primește automat un chenar roșu și un mesaj de atenționare, blocând salvarea tranzacției până când eroarea este corectată de utilizator.
* **Rularea corectă:** Pentru a deschide proiectul fără erori pe alt calculator, deschideți fișierul principal al soluției (`.sln`) în Visual Studio 2022 și rulați comanda **Build -> Rebuild Solution** pentru a genera folderele locale necesare rulării.
