# Informacje o Programie

## Nazwa programu: WebAppProject

## Opis: WebAppProject to aplikacja internetowa do zarządzania biblioteką. Umożliwia użytkownikom przeglądanie katalogu książek oraz składanie zamówień (jeśli są zalogowani). 
Administratorzy mogą zarządzać zasobami bibliotecznymi oraz przeglądać raporty i katalog książek, ale nie mogą składać zamówień.

## Funkcje:
 - przeglądanie katalogu książek
 - składanie zamówień na książki
 - administracja zasobami bibliotecznymi (dodawanie, edytowanie, usuwanie książek)
 - przeglądanie raportów zamówień

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Środowisko

## Wymagania:
 - .NET 8.0
 - ASP.NET Core
 - Entity Framework Core
 - Microsoft SQL Server
 - Visual Studio 2022 lub nowszy
 - Przeglądarka internetowa (np. Google Chrome, Mozilla Firefox)

## Konfiguracja bazy danych:
 1) Otwórz projekt i zaktualizuj plik appsettings.json, aby wskazywał na odpowiednią bazę danych:

	"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WebAppProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

 2) Wykonaj Update bazy danych:

	Update-Database

 3) Kompiluj projekt

## Instalacja i Uruchomienie:
 1) Otwórz projekt w Visual Studio.
 2) Przeprowadź migracje bazy danych, aby utworzyć strukturę tabel.
 3) Uruchom aplikację.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Korzystanie z Programu

## Strona Główna:
- użytkownicy mogą przeglądać katalog książek i wyszukiwać tytuły,
- zalogowani użytkownicy mogą składać zamówienia,
- administratorzy będą widzieć stronę z ograniczeniami,

## Rejestracja:
 1) Kliknij na "Register" w prawym górnym rogu.
 2) Wprowadź swoje dane.
 3) Kliknij "Register", aby utworzyć nowe konto.

## Logowanie:
 1) Kliknij na "Login" w prawym górnym rogu.
 2) Wprowadź swoje dane logowania.
 3) Kliknij "Login", aby się zalogować.

## Zarządzanie Zasobami: Administratorzy mogą dodawać, edytować oraz usuwać książki, autorów, kategorie, języki, gatunki oraz wydawców z odpowiednich sekcji w panelu administracyjnym.

## Składanie Zamówień: Zalogowani użytkownicy mogą dodawać książki do koszyka oraz składać zamówienia z poziomu katalogu książek.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Logowanie się jako Administrator

## Domyślne dane logowania admina:

	Email: admin@example.com
	Hasło: Admin@123

## Procedura logowania:
 1) Kliknij na "Login" w prawym górnym rogu.
 2) Wprowadź email i hasło podane powyżej.
 3) Kliknij "Login", aby zalogować się jako administrator.

## Po zalogowaniu jako admin będziesz miał dostęp do panelu administracyjnego, gdzie możesz zarządzać zasobami bibliotecznymi.
