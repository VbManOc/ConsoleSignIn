namespace ConsoleSignInApp.Services
{
    public interface IScrubValidate
    {
        bool isValidPassword(string password);
        bool isValidPasswordFormat(string password);
        bool isValidPasswordLength(string password);
        bool isValidUserIdLength(string UserID);
        bool hasBadCharacters(string StringToScrub);
        List<string> GetPasswordErrors();
    }
}