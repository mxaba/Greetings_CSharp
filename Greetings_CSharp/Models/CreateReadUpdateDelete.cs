using System.Collections.Generic;
using System.Linq;
using System;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.Models
{
    public class CreateReadUpdateDelete : ICreateReadUpdateDelete
    {
        private readonly ApplicationDbContext _db;
        public CreateReadUpdateDelete(ApplicationDbContext db)
        {
            _db = db;
        }

        public void DeleteName(int? id)
        {
            var obj = _db.Greetings.Find(id);
            _db.Greetings.Remove(obj);
            _db.SaveChanges();
        }

        public void ResetDB()
        {
            IEnumerable<Greetings> greetingsDatabase = _db.Greetings;
            _db.Greetings.RemoveRange(greetingsDatabase);
            _db.SaveChanges();
        }

        public void CreteAndUpdate(Greetings bindedData, string language)
        {
            var name = bindedData.Name;
            var _id = 0;
            IEnumerable<Greetings> greetingsDatabase = _db.Greetings;
            foreach (var obj in greetingsDatabase)
            {
                if (obj.Name == name)
                {
                    _id = obj.Id;
                    break;
                }
            }
            if (greetingsDatabase.Count() > 0 && _id != 0)
            {
                bindedData = Update(language, name, ref _id, greetingsDatabase);

            }
            else Create(bindedData, language);
        }

        private void Create(Greetings bindedData, string language)
        {
            bindedData.Counts = 1;
            if (language == "isizulu")
            {
                bindedData.Isizulu = 1;

            }
            else if (language == "spanish")
            {
                bindedData.Spanish = 1;
            }
            else if (language == "english")
            {
                bindedData.English = 1;
            }
            _db.Greetings.Add(bindedData);
            _db.SaveChanges();
        }

        private Greetings Update(string language, string name, ref int _id, IEnumerable<Greetings> greetingsDatabase)
        {
            Greetings bindedData;
            bindedData = _db.Greetings.Find(_id);
            if (language == "isizulu")
            {
                bindedData.Isizulu++;
                bindedData.Counts++;

            }
            else if (language == "spanish")
            {
                bindedData.Spanish++;
                bindedData.Counts++;
            }
            else if (language == "english")
            {
                bindedData.English++;
                bindedData.Counts++;
            }
            _db.Greetings.Update(bindedData);
            _db.SaveChanges();
            return bindedData;
        }

        public string CapitalizeFirstLetterAndLowerRest(string name){
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }
    }
}