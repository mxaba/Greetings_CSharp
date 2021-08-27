namespace Greetings_CSharp.Models
{
    public interface IGreetMessage
    {
        string ErrorMessage(Greetings objList, string language);
        string Message(Greetings objList, string language);
    }
}