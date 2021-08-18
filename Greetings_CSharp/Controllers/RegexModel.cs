using System.Text.RegularExpressions;

namespace Greetings_CSharp.Controllers
{
    public class RegexModel
    {
        public bool IsAlphabets(string inputString) {
        Regex r = new Regex("^[a-zA-Z ]+$");
        if (r.IsMatch(inputString))
            return true;
        else
            return false;
        }
    }
}