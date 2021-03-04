# Fiks-Protokollvalidator

## Oppsett og kjøring i lokalt utviklingsmiljø

Validatoren består av to applikasjoner, en front-end (web-app) og en back-end (.NET web-API)

Følgende må være installert på utviklingsmaskinen:
 - .NET 5.0 SDK https://dotnet.microsoft.com/download
 - Visualstudio (min. v.16.8) https://visualstudio.microsoft.com/downloads/
 - SQL EXPRESS https://www.microsoft.com/en-us/sql-server/sql-server-downloads
 - Node + NPM https://nodejs.org/en/download/
 
### Back-end

* Opprett/oppdater databasen ved å navigere til *fiks-protokoll-validator/api/KS.FiksProtokollValidator.WebAPI/* og kjøre `dotnet ef database update` (eller med Visual Studios Package Manager Console: `Update-Database`).

* Konfigurer validatoren for FIKS ved å kopiere malfilen *fiks-protokoll-validator/api/KS.FiksProtokollValidator.WebAPI/FiksIO/**fiks-io-config.template.json***, til **fiks-io-config.json**, under samme katalog som malfilen, og endre innholdet i denne til aktuelt FIKS-oppsett. Oppsettet krever en privatnøkkel (.pem-fil) som navngis/plasseres iht. filreferansen i **fiks-io-config.json**.

* Start back-end ved å åpne **fiks-protokoll-validator/api/KS.FiksProtokollValidator.sln** i Visual Studio og bygge/kjøre prosjektet **KS.FiksProtokollValidator.WebAPI**.

### Front-end

 * Hent inn web-applikasjonens avhengigheter ved å navigere til **fiks-protokoll-validator/web-ui/** og å kjøre `npm install`.
 * Start front-end på localhost fra **fiks-protokoll-validator/web-ui/** med `npm run serve`.


## Hvordan utvide validatoren med nye arkivsystem-tester

Validatorens tester er/blir definert som kataloger under Tests/ i prosjektet **KS.FiksProtokollValidator.TestCases**. Test-katalogenes navn er/blir testenes navn i databasen og dermed i brukergrensesnittet. Alle validatorens tester skal ha ulike navn.

* Opprett en ny katalog og gi den navnet på den nye testen.
* I den nye test-katalogen opprettes filene "arkivmelding.xml", "testCriteria.json" og eventuelt en katalog for vedlegg.
  * Filen "arkivmelding.xml" skal inneholde arkivmeldingen man ønsker å teste arkivsystemet med.
  * Filen "testCriteria.json" definerer hvordan validatoren skal kontrollere svarmeldingen fra arkivsystemet.
  * Dersom arkivmeldingen skal ha vedlegg må det, i den gjeldende testkatalogen, opprettes en katalog med navnet "Attachments", som vedleggene plasseres under.

### Eksempel på katalogstruktur
```
Tests
  |
  └─|Ny innkommende journalpost
    |  |
    |  └─|arkivmelding.xml
    |    |testCriteria.json
    |    |Attachments
    |    └─|Rekvisisjon.pdf
    |      |Vedlegg-1A.pdf
    |
    |Ny utgående journalpost
       |
       └─|arkivmelding.xml
         |testCriteria.json
```

 ### Eksempel på innhold i testCriteria.json
```yaml
{
  "messageType": "no.ks.fiks.gi.arkivintegrasjon.oppdatering.basis.arkivmelding.v1",
  "queriesWithExpectedValues": [
    {
      "payloadQuery": "/arkivmelding/registrering",
      "expectedValue": "journalpost",
      "valueType": 1 // 0 = Verdi, 1 = Attributt
    },
    {
      "payloadQuery": "/arkivmelding/registrering/journalaar",
      "expectedValue": "2020",
      "valueType": 0 // 0 = Verdi, 1 = Attributt
    },
    {
      "payloadQuery": "/arkivmelding/registrering/systemID",
      "expectedValue": "*", // * = en hvilken som helst verdi som ikke er tom, null eller whitespace
      "valueType": 0 // 0 = Verdi, 1 = Attributt
    }
  ]
}
```

- **messageType** definerer meldingstypen på meldingen som går til arkivsystemet.
- **payloadQuery** definerer en XPath-spørring som skal utføres på XML-en som kommer som svar fra arkivsystemet.
- **expectedValue** definerer hvilken verdi man forventer å finne på elementet man spør etter i **payloadQuery**. Man kan bruke "\*" for å sjekke om elementet har en verdi som ikke er tom, null eller whitespace. Dersom man kun skal sjekke om elementet finnes, behøver man hverken å oppgi denne parameteren eller **valueType**.
- **valueType** definerer om man spør etter en verdi (0), eller et attributt (1).
