# Aplicatie practica – Gestionare tranzactii targ auto

## Tema

Aplicatia are ca scop administrarea si evidenta tranzactiilor de vanzare–cumparare realizate intr-un targ auto.

## Functionalitati

Aplicatia permite:

* introducerea unei tranzactii
* afisarea tranzactiilor realizate intr-o anumita zi
* determinarea celei mai populare masini
* afisarea preturilor tranzactiilor pe un interval de timp
* salvarea si incarcarea tranzactiilor din fisier

## Date stocate pentru fiecare tranzactie

Pentru fiecare masina tranzactionata se vor inregistra:

* Nume vanzator
* Nume cumparator
* Marca masinii
* Modelul masinii
* An fabricatie
* Culoare
* Dotari
* Data tranzactie
* Pret

## Validari

La introducerea unei tranzactii:

* daca un cumparator achizitioneaza mai multe masini intr-o zi, apare o avertizare
* daca un vanzator vinde mai multe masini intr-o zi, apare o avertizare

## Rapoarte

Aplicatia va afisa:

* tranzactiile realizate intr-o anumita zi
* cea mai cautata masina
* preturile tranzactiilor pe un interval de timp

## Observatii

Aplicatia este realizata folosind programare orientata pe obiecte si utilizeaza:

* clase pentru modelarea entitatilor (Tranzactie, Vehicul, Persoana)
* enumerari pentru culoare si dotari
* interfata pentru stocarea datelor
* persistenta datelor in fisier
