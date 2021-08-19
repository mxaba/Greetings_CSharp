namespace Greetings_CSharp.Models
{
    public interface ICreateReadUpdateDelete
    {
        void CreteAndUpdate(Greetings bindedData, string language);
        void DeleteName(int? id);
        void ResetDB();
    }
}