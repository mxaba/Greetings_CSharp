using Greetings_CSharp.Database;
using Greetings_CSharp.Models;

namespace Greetings_CSharp.Controllers
{
    public class GreetMessage {

        private string _errorMessage;
        private string _message;
        public string Message(Greetings objList, string language){
            if(language == "english"){
                _message = $"Hello, {objList.Name}";
            } if (language == "spanish"){
                _message = $"Hola, {objList.Name}";
            } if (language == "isizulu") {
                _message = $"Sawubona, {objList.Name}";
            }
            return _message;
        }

        public string ErrorMessage(Greetings objList, string language){

            if (string.IsNullOrEmpty(objList.Name) && string.IsNullOrEmpty(language)) {
                _errorMessage = "Please enter a name and select a language ❌";
            } else if (string.IsNullOrEmpty(language)){
                _errorMessage = "Please select a language! ❌";
            }
            else if (string.IsNullOrEmpty(objList.Name)){
                _errorMessage = "Please enter a name! ❌";
            }
            return _errorMessage;
        }
    }
}