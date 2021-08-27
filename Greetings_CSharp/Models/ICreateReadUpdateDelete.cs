using System.Collections.Generic;
using System.Linq;

namespace Greetings_CSharp.Models
{
    public interface ICreateReadUpdateDelete
    {
        string CapitalizeFirstLetterAndLowerRest(string name);
        int CountData();
        string CreateAndUpdate(Greetings bindedData, string language);
        void DeleteName(int? id);
        void ResetDB();
        Greetings FindUserById(int? id);
        IEnumerable<Greetings> GreetedNames();
    }
}