# Bordtennis Turnering & Matchhanterare

##  Projektbeskrivning

Detta projekt är en webbapplikation utvecklad för att hantera och spåra bordtennismatcher. Applikationen tillåter användare att skapa matcher, registrera poäng i realtid, se matchhistorik och (beroende på implementering) spelarstatistik. Systemet inkluderar även administrativa funktioner för att hantera matchdata.

Applikationen är byggd med ASP.NET Core Razor Pages, Entity Framework Core och följer en lagerindelad arkitektur för att säkerställa en organiserad och underhållbar kodbas.

## Funktioner

* **Matchskapande:**
    * Registrera två spelare (med förnamn, efternamn, ålder).
    * Välj kön för matchen (Kille, Tjej).
    * Välj matchformat (bäst av 3, 5 eller 7 set).
* **Poängräkning i Realtid:**
    * Interaktiv poängtavla för att lägga till och ta bort poäng för varje spelare.
    * Automatisk hantering av servebyte.
    * Visning av "deuce", "set point" och "match point".
    * Timer för varje set och beräkning av total matchtid.
* **Matchhistorik:**
    * Lista över alla spelade matcher.
    * Sökfunktion för att filtrera matcher baserat på spelarnamn.
    * Visning av matchdatum, spelare, resultat (vunna set), bäst av, kön, total matchtid och matchvinnare (med pokalikon).
    * Popover/tooltip som visar poäng för varje enskilt set.
* **Administration:**
    * Möjlighet att "mjukradera" (markera som inaktiv) matcher från historiken.
    * Möjlighet att återställa mjukraderade matcher.
* **Spelarstatistik:**
    * Möjlighet att välja två spelare och jämföra deras statistik mot varandra (t.ex. antal vunna matcher. Vinstprocent)
* **Ledartavla:**
    * Visar en topp 10-lista över spelare baserad på vinstprocent och spelade matcher. (måste ha spelat minst 10 matcher)

## Teknologistack

* **Backend:**
    * ASP.NET Core Razor Pages (C#)
    * Entity Framework Core (ORM för databasinteraktion)
* **Databas:**
    * SQL Server
* **Frontend:**
    * HTML5
    * CSS3
    * Bootstrap
    * JavaScript (för klientsidans interaktivitet, t.ex. timer, popovers)
* **Autentisering & Auktorisering:**
    * ASP.NET Core Identity

## Arkitekturöversikt

Projektet följer en lagerindelad arkitektur för att främja **Separation of Concerns (SoC)**:

1.  **Presentationslager (UI):**
    * **Razor Pages (`.cshtml`):** Ansvarar för användargränssnittet.
    * **PageModels (`.cshtml.cs`):** Hanterar sidans logik, dataförberedelse och interaktion med service-lagret.
    * **ViewModels (`TableTennis.ViewModels`):** Dataöverföringsobjekt anpassade för specifika vyer (t.ex. `MatchHistoryVM`, `SetVM`).

2.  **Service-lager (Affärslogik - BLL):**
    * **Interfaces (`Services.Match.Interface.IMatchService`, `Services.Set.Interface.ISetService`):** Definierar kontrakten för tjänsterna.
    * **Implementationer (`Services.Match.MatchService`, `Services.Set.SetService`):** Innehåller kärnlogiken för applikationen (matchhantering, poängräkning, vinnarbestämning etc.).

3.  **Datalager (Data Access Layer - DAL):**
    * **Entity Framework Core DbContext (`ApplicationDbContext`):** Hanterar databaskommunikationen.
    * **Domänmodeller/Entiteter (`DataAccessLayer.Data.Models`):** C#-klasser som representerar databastabeller (t.ex. `TableTennisMatch`, `TableTennisSet`).
    * **Data Transfer Objects (DTOs) (`DataAccessLayer.Data.DTO`):** Används för dataöverföring mellan lager (t.ex. `MatchDTO`, `SetsDTO`).

Principer som **Dependency Injection (DI)** används för att hantera beroenden mellan lager, vilket gör koden mer modulär och testbar.

## Användning

* **Starta en ny match:** Navigera till "Starta Match", fyll i spelarinformation och matchinställningar.
* **Poängräkning:** På matchsidan, använd plusknapparna för att ge poäng. Systemet hanterar set- och matchvinnare automatiskt.
* **Matchhistorik:** Gå till "Visa Historik" för att se en lista över spelade matcher. Använd sökfältet för att filtrera. Håll muspekaren över set-resultatet för att se detaljerad poäng per set..
* **Spelarstatistik-sida:** Visa individuell statistik för spelare och jämför med andra spelare. (vinster, förluster, poängskillnad etc.).
* **Ledartavla/Ranking top 10:** Baserat på matchresultat och vinstprocent.

## Teammedlemmar

* Estelle Boo
* Kamyar Shateri Kashi
* Robin Linerudh
* Arvid Södergård
* William Selin
