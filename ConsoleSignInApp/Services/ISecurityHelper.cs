namespace ConsoleSignInApp.Services
{
    public interface ISecurityHelper
    {
        string GenerateSalt(int SaltLength);
        string HashPassword(string Password, int Salt, int Iterations, int nHash);
    }
}