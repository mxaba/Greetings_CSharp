using System.Collections.Generic;
using System.Linq;
using System;
using Greetings_CSharp.Database;
using Greetings_CSharp.Models;

namespace Greetings_CSharp.Controllers
{
    public class CRUD
    {
        private readonly ApplicationDbContext _db;
        public CRUD(ApplicationDbContext db){
            _db = db;
        }

        public void DeleteName(int? id){
            var obj = _db.Greetings.Find(id);
            _db.Greetings.Remove(obj);
            _db.SaveChanges();
        }

        public void ResetDB(){
            IEnumerable<Greetings> objectList = _db.Greetings;
            foreach (var obj in objectList)
            {
                _db.Greetings.Remove(obj);
            }
            _db.SaveChanges();
        }

        public void CreteAndUpdate(Greetings objList, string language){
            var name = objList.Name;
            var _id = 0;
            IEnumerable<Greetings> objectList = _db.Greetings;
            foreach (var obj in objectList)
            {
                if (obj.Name == name)
                {
                    _id = obj.Id;
                    break;
                }
            }
            if(objectList.Count() > 0 && _id != 0)
            {
                objList = Update(language, name, ref _id, objectList);

            }
            else Create(objList, language);
        }

        private void Create(Greetings objList, string language)
        {
            objList.Counts = 1;
            if (language == "isizulu")
            {
                objList.Isizulu = 1;

            }
            else if (language == "spanish")
            {
                objList.Spanish = 1;
            }
            else if (language == "english")
            {
                objList.English = 1;
            }
            _db.Greetings.Add(objList);
            _db.SaveChanges();
        }

        private Greetings Update(string language, string name, ref int _id, IEnumerable<Greetings> objectList)
        {
            Greetings objList;
            objList = _db.Greetings.Find(_id);
            if (language == "isizulu")
            {
                objList.Isizulu++;
                objList.Counts++;

            }
            else if (language == "spanish")
            {
                objList.Spanish++;
                objList.Counts++;
            }
            else if (language == "english")
            {
                objList.English++;
                objList.Counts++;
            }
            _db.Greetings.Update(objList);
            _db.SaveChanges();
            return objList;
        }
    }
}