# UE1 07.03.2023

To-Do Liste dieser Übungseinheit:

### 1\. Erstellen M365 Dev Tenant

Um einen M365 DEV Tenant anzulegen bitte [hier](https://developer.microsoft.com/en-us/microsoft-365/dev-program) dem Link folgen und sich anmelden. Nach der Anmeldung bitte auf den Button `Join now` klicken. Nun bitte die Anweisungen auf der Seite befolgen. Nachdem der Tenant erstellt wurde bitte die Zugangsdaten notieren.

### 2\. Vertraut machen mit Azure AD

Um sich mit Azure AD vertraut zu machen bitte [hier](https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/active-directory-whatis) dem Link folgen und sich mit den Grundlagen vertraut machen.

Danach bitte der Anleitung unter [hier](https://learn.microsoft.com/en-us/azure/active-directory/fundamentals/add-users-azure-active-directory) folgen und einen neuen User in Azure AD erstellen.

Anschließend bitte Ihren eigenen FH-User als B2B Externen User in Azure AD einladen. Dazu bitte [hier](https://learn.microsoft.com/en-us/azure/active-directory/external-identities/add-users-administrator) dem Link folgen und die Anleitung befolgen. Stellen Sie bitte sicher, dass der User in der Gruppe "Externe" angelegt wird. Entweder direkt bei der Einladung oder nachträglich. (Gruppe müssen Sie zuerst erstellen)

### 3\. EmailReader

Folgen Sie der Anleitung von Tobias Zimmergren [hier](https://zimmergren.net/reading-emails-with-microsoft-graph-using-net/) und erstellen Sie eine Console App, die Emails aus dem Posteingang ausliest. Bitte beachten Sie, dass Sie im Gegensatz zum Blog Post den Code aus diesem GitHub Repository verwenden.

Hint: Um mit User Secrets in Visual Studio Code zu arbeiten, bitte die Extension ".NET Core User Secrets" installieren. Damit können Sie die User Secrets Datei über einen Rechtsklick auf das Projekt (.csproj) und dann auf `Manage User Secrets` klicken.
