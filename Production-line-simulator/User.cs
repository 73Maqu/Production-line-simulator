using BCrypt.Net;

namespace POSK3
{
    class User
    {
        public string login { get; private set; }
        public string passHash { get; private set; }
        public string passSalt { get; private set; }

        public User(string login, string password, string extraSalt)
        {
            this.passSalt = BCrypt.Net.BCrypt.GenerateSalt();
            this.login = login;
            this.passHash = BCrypt.Net.BCrypt.HashPassword(password + extraSalt, this.passSalt);

        }
        public bool CheckPass(string password, string extraSalt)
        {
            return BCrypt.Net.BCrypt.Verify(password + extraSalt, this.passHash);
        }
    }
}