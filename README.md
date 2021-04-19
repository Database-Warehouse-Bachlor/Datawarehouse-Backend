# Datawarehouse-Backend

Model instruksjoner:
Hver model har en egen ID som vi genererer og forholder oss til ved senere kall, som instruktert av oppdragsgiver at vi må benytte oss av egne IDer.
I tillegg til disse IDene har hver model en ekstra ID som heter <model>ID, hvor oppdragsgiver kan sende inn sin egen ID for å spesifisere hvilken model den snakker om.

Eksempel:
Order har en egen ID som genereres ved opprettelse av objektet.
Når oppdragsgiver sender inn en Order-model kan de sende med sin egen ID som blir lagret som OrderId.  Denne IDen vil kun bli lagret i modellen, men ikke knyttet opp til en annen model. 

Hvis cordel sender inn en ny InvoiceOutbound og sier at den skal knyttes opp mot en Order de har sendt inn tidligere eller samtidig: Så sender de med sin egene ID, altså OrderID, som vi kan benytte oss av for å finne riktig Order.  Når vi finner Orderen de tenker på, henter vi ut IDen vår for så å knytte disse to Modellene opp mot hverandre mtp. relasjonsdatabasen.

Oppretting av tennant:
Det er to registreringskall som eksisterer i APIen.  
1. InitRegister  - Denne kan kunn bli kalt 

