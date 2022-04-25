# ConsoleSignIn  - Manuel Plaza
##Simple Sign In App FYI

*Entities Folder*
Contains User Model 

*Services Folder*
- ServiceFactory.cs : is used to inject dependent classes
- AccountManager.cs : responsible for Insert logic
- Authenticate.cs : responsible for authenticating logic
- SecurityHelper.cs : encryption logic
- ScrubValidate.cs : scrubbing and validatiion of user input

*Infrastructure*
- DataStore.cs : plain dictionary faking a datastore
- DataStoreCommand.cs : data store command operations here
- DataQueries.cs : data store look up operations

*Manager*
-Main.cs : logic application flow manager

*Application Logic Flow*
> Program > Manager > Services > Infrastructure

