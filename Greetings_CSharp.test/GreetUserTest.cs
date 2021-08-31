using System.Collections.Generic;
using Greetings_CSharp.Models;
using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class GreetUser
    {
        private ICreateReadUpdateDelete createReadUpdateDelete;

        public GreetUser([FromServices] ICreateReadUpdateDelete createReadUpdateDelete)
        {
            this.createReadUpdateDelete = createReadUpdateDelete;
        }

        [Fact]
        public void GreetMceboInIsizulu()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Sawubona, Mcebo", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetMceboInEnglish()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hello, Mcebo", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetMceboInSpanish()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hola, Mcebo", createReadUpdateDelete.CreateAndUpdate(greetBind, "spanish"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetAndreInAllLanguage()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Sawubona, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            Assert.Equal("Hello, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            Assert.Equal("Hola, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind, "spanish"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetJosiahInIsizulu()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Josiah", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Sawubona, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetCingaInEnglish()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Cinga", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hello, Cinga", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void GreetspanishInSpanish()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hola, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind, "spanish"));
            createReadUpdateDelete.ResetDB();
        }
    }
}