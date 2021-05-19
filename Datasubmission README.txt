
********DOKUMENTASJON PÅ INNSENDING AV DATA TIL DATAVAREHUS********


Hva README inneholder:

- Kort info
- Oppretting av Tennant (API)
- Innsending av data (API)
- Behandling av error


---KORT INFO---

Denne README filen gir en kort beskrivelse på hvordan DatasubmissionControlleren blir brukt.


----OPPRETTING AV TENNANT----

Denne API-en brukes for å oprette en tennant i datavarehuset.

	- URL : https://Domenenavn:port/submission/registerTennant

Forventet innhold:

	- Gyldig Token fra en admin bruker

	- Navn på tennant: 			string tennantName 	(x-www-form-urlencoded)
	- BusinessId (Organisasjonsnummer):	string businessId	(x-www-form-urlencoded)
	- API-key: 				string apiKey		(x-www-form-urlencoded) [Required]


Forventet Svar: 200 OK



----Innsending av data----

Denne API-en brukes for innsending av data til datavarehuset.

	- URL : https://Domenenavn:port/submission/add?apiKey=apikey

Forventet innhold:

	- Gyldig ApiKey :	string apiKey	(query)

	- JSON body : 		Request body 	(application/json)


Forventet Svar: 200 OK



Når data blir sendt inn vil programmet forvente at JSON body inneholder flere lister. Disse må være av samme type som de 
forskjellige modellene i datavarehuset. Samtidig er det også forventet et felt med navn "apiKey" av type string. 

Programmet sammenligner apiKey-en i både URL og JSON body for å autentisere bruker.

Disse modellene er:
	- AbsenceRegister
	- Account
	- BalanceAndBudget
	- Client
	- Employee
	- FinancialYear
	- Invoice
	- InvoiceLine
	- Order
	- Post
	- TimeRegister
	- Voucher

Hvis man sender inn data som allerede ligger inne i systemet, vil både selve objeket og alt som er knyttet til dette 
bli slettet, og må sendes inn på nytt igjen.
Det er ID-er fra Cordel sitt system som vil bli brukt når fremmednøkkelene blir bestemt, så ID-ene i et objekt må kunne
knyttes opp mot ID-ene i andre objekter også. 

Hvis for eksempel ingen nye Client skal bli sendt inn, må denne listen fortsatt være med, men den skal da være tom.
Dette gjelder for alle listene.

Eksempel på innsending av data:

{
    "apiKey": abcdefghijkklm12,
    
    "Client": [],

    "BalanceAndBudgets": [],

    "AbsenceRegisters": [
        {
            "absenceRegisterId": 333,
            "abcenseTypeText": "Sykemelding",
            "absenceId": 69,
            "fromDate": "2020-02-12",
            "toDate": "2020-02-24",
            "duration": 12
            "soleCaretaker": true,
            "abcenseType": "alone",
            "comment": "Syk",
            "degreeDisability": "Syk",
            "employeeId": 66
        }
    ],

    "Employees": [],

    "Orders": [],

    "TimeRegisters": [],

    "Vouchers": [],

    "Invoices": [],

    "InvoiceLines": [],

    "FinancialYears": [],

    "Accounts": [],

    "Posts": []
}


----Behandling av error----

Skulle det oppstå problemer med innsending av data eller oppretting av en tennant vil en error bli logget i datavarehuset.
Disse feilmeldingen vil man kunne se i mobilappen som cordel har tilgang til.



