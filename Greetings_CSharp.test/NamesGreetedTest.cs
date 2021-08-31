using System.Collections.Generic;
using Greetings_CSharp.Models;
using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using Greetings_CSharp.Database;
using Newtonsoft.Json;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class NamesGreeted
    {
        private ICreateReadUpdateDelete createReadUpdateDelete;

        public NamesGreeted([FromServices] ICreateReadUpdateDelete createReadUpdateDelete)
        {
            this.createReadUpdateDelete = createReadUpdateDelete;
        }

        [Fact]
        public void DatabaseReset()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Sawubona, Mcebo", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            Assert.Equal(1, createReadUpdateDelete.CountData());
            createReadUpdateDelete.ResetDB();
            Assert.Equal(0, createReadUpdateDelete.CountData());
        }

        [Fact]
        public void ReturnAllTheNameFoundOnDataBase()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Josiah", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind1 = new Greetings { Name="Cinga", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind2 = new Greetings { Name="Miguel", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind3 = new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0};

            var AllData = new List<Greetings>(){
                new Greetings { Name="Josiah", English=0, Spanish=0,  Isizulu=0, Counts=0},
                new Greetings { Name="Cinga", English=0, Spanish=0,  Isizulu=0, Counts=0},
                new Greetings { Name="Miguel", English=0, Spanish=0,  Isizulu=0, Counts=0},
                new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0},
            };

            Assert.Equal("Hello, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            Assert.Equal("Hello, Cinga", createReadUpdateDelete.CreateAndUpdate(greetBind1, "english"));
            Assert.Equal("Sawubona, Miguel", createReadUpdateDelete.CreateAndUpdate(greetBind2, "isizulu"));
            Assert.Equal("Sawubona, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind3, "isizulu"));

            // var obj1Str = JsonConvert.SerializeObject("");
            // var obj2Str = JsonConvert.SerializeObject("");
            // Assert.Equal(obj1Str, obj2Str );
            
            Assert.Equal(4, createReadUpdateDelete.CountData());
            createReadUpdateDelete.ResetDB();
        }
    }
}