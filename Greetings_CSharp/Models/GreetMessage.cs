using System.Text.RegularExpressions;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.Models
{
    public class GreetMessage : IGreetMessage
    {
        private string _message;
        public string Message(Greetings objList, string language)
        {
            if(!ErrorMessage(objList, language)){
                if (language == "english") {
                    _message = $"Hello, {objList.Name}";
                } if (language == "spanish") {
                    _message = $"Hola, {objList.Name}";
                } if (language == "isizulu") {
                    _message = $"Sawubona, {objList.Name}";
                }
            }
            return _message;
        }

        private bool ErrorMessage(Greetings objList, string language)
        {
            var regexTrue = Regex.IsMatch(objList.Name, @"^[a-zA-Z]+$");
            if (string.IsNullOrEmpty(objList.Name) && string.IsNullOrEmpty(language))
            {
                _message = "Please enter a name and select a language ❌";
                return true;
            }
            else if (string.IsNullOrEmpty(language))
            {
                _message = "Please select a language! ❌";
                return true;
            }
            else if (!regexTrue)
            {
                _message = "Please enter a valid name with only Letters! ❌";
                return true;
            }
            else if (string.IsNullOrEmpty(objList.Name))
            {
                _message = "Please enter a name! ❌";
                return true;
            }
            return false;
        }

        string IGreetMessage.ErrorMessage(Greetings objList, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}