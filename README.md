# Bordtennis Turnering & Matchhanterare

##  Projektbeskrivning

Detta projekt �r en webbapplikation utvecklad f�r att hantera och sp�ra bordtennismatcher. Applikationen till�ter anv�ndare att skapa matcher, registrera po�ng i realtid, se matchhistorik och (beroende p� implementering) spelarstatistik. Systemet inkluderar �ven administrativa funktioner f�r att hantera matchdata.

Applikationen �r byggd med ASP.NET Core Razor Pages, Entity Framework Core och f�ljer en lagerindelad arkitektur f�r att s�kerst�lla en organiserad och underh�llbar kodbas.

## Funktioner

* **Matchskapande:**
    * Registrera tv� spelare (med f�rnamn, efternamn, �lder).
    * V�lj k�n f�r matchen (Kille, Tjej).
    * V�lj matchformat (b�st av 3, 5 eller 7 set).
* **Po�ngr�kning i Realtid:**
    * Interaktiv po�ngtavla f�r att l�gga till och ta bort po�ng f�r varje spelare.
    * Automatisk hantering av servebyte.
    * Visning av "deuce", "set point" och "match point".
    * Timer f�r varje set och ber�kning av total matchtid.
* **Matchhistorik:**
    * Lista �ver alla spelade matcher.
    * S�kfunktion f�r att filtrera matcher baserat p� spelarnamn.
    * Visning av matchdatum, spelare, resultat (vunna set), b�st av, k�n, total matchtid och matchvinnare (med pokalikon).
    * Popover/tooltip som visar po�ng f�r varje enskilt set.
* **Administration:**
    * M�jlighet att "mjukradera" (markera som inaktiv) matcher fr�n historiken.
    * M�jlighet att �terst�lla mjukraderade matcher.
* **Spelarstatistik:**
    * M�jlighet att v�lja tv� spelare och j�mf�ra deras statistik mot varandra (t.ex. antal vunna matcher. Vinstprocent)
* **Ledartavla:**
    * Visar en topp 10-lista �ver spelare baserad p� vinstprocent och spelade matcher. (m�ste ha spelat minst 10 matcher)

## Teknologistack

* **Backend:**
    * ASP.NET Core Razor Pages (C#)
    * Entity Framework Core (ORM f�r databasinteraktion)
* **Databas:**
    * SQL Server
* **Frontend:**
    * HTML5
    * CSS3
    * Bootstrap
    * JavaScript (f�r klientsidans interaktivitet, t.ex. timer, popovers)
* **Autentisering & Auktorisering:**
    * ASP.NET Core Identity

## Arkitektur�versikt

Projektet f�ljer en lagerindelad arkitektur f�r att fr�mja **Separation of Concerns (SoC)**:

1.  **Presentationslager (UI):**
    * **Razor Pages (`.cshtml`):** Ansvarar f�r anv�ndargr�nssnittet.
    * **PageModels (`.cshtml.cs`):** Hanterar sidans logik, dataf�rberedelse och interaktion med service-lagret.
    * **ViewModels (`TableTennis.ViewModels`):** Data�verf�ringsobjekt anpassade f�r specifika vyer (t.ex. `MatchHistoryVM`, `SetVM`).

2.  **Service-lager (Aff�rslogik - BLL):**
    * **Interfaces (`Services.Match.Interface.IMatchService`, `Services.Set.Interface.ISetService`):** Definierar kontrakten f�r tj�nsterna.
    * **Implementationer (`Services.Match.MatchService`, `Services.Set.SetService`):** Inneh�ller k�rnlogiken f�r applikationen (matchhantering, po�ngr�kning, vinnarbest�mning etc.).

3.  **Datalager (Data Access Layer - DAL):**
    * **Entity Framework Core DbContext (`ApplicationDbContext`):** Hanterar databaskommunikationen.
    * **Dom�nmodeller/Entiteter (`DataAccessLayer.Data.Models`):** C#-klasser som representerar databastabeller (t.ex. `TableTennisMatch`, `TableTennisSet`).
    * **Data Transfer Objects (DTOs) (`DataAccessLayer.Data.DTO`):** Anv�nds f�r data�verf�ring mellan lager (t.ex. `MatchDTO`, `SetsDTO`).

Principer som **Dependency Injection (DI)** anv�nds f�r att hantera beroenden mellan lager, vilket g�r koden mer modul�r och testbar.

## Anv�ndning

* **Starta en ny match:** Navigera till "Starta Match", fyll i spelarinformation och matchinst�llningar.
* **Po�ngr�kning:** P� matchsidan, anv�nd plusknapparna f�r att ge po�ng. Systemet hanterar set- och matchvinnare automatiskt.
* **Matchhistorik:** G� till "Visa Historik" f�r att se en lista �ver spelade matcher. Anv�nd s�kf�ltet f�r att filtrera. H�ll muspekaren �ver set-resultatet f�r att se detaljerad po�ng per set..
* **Spelarstatistik-sida:** Visa individuell statistik f�r spelare och j�mf�r med andra spelare. (vinster, f�rluster, po�ngskillnad etc.).
* **Ledartavla/Ranking top 10:** Baserat p� matchresultat och vinstprocent.

## Teammedlemmar

* Estelle Boo
* Kamyar Shateri Kashi
* Robin Linerudh
* Arvid S�derg�rd
* William Selin
