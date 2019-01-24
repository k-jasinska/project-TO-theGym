using System.Text.RegularExpressions;
namespace GymSystem.App.Models
{
    class DataTester
    {
        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"\A[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z");
            Match match = regex.Match(email);
            return match.Success;
        }
        public static bool IsValidStreet(string street)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\s.\-]+$");
            Match match = regex.Match(street);
            return match.Success;
        }
        public static bool IsValidSurname(string surname)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$");
            Match match = regex.Match(surname);
            return match.Success;
        }
    }
}
