using ConsoleSignInApp.Manager;
using ConsoleSignInApp.Entities;

var main = new Main();
bool RunApp = true;
string UserOption;

main.ShowStartOptions();
while (RunApp)
{
    //Get User Action Request
    UserOption = Console.ReadLine();
    if (UserOption == "e")
    {
        RunApp = false;
    }
    else
    {
        main.OptionsToRun(UserOption);
        RunApp = true;
    }


}
Environment.Exit(0);

