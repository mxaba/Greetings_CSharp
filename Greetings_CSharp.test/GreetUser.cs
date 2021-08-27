using System.Collections.Generic;
using Greetings_CSharp.Database;
using Greetings_CSharp.Models;
using Xunit;
using System;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class GreetUser
    {
        private ApplicationDbContext testDbContext;
        private CreateReadUpdateDelete crud;
        private GreetMessage message;

        public GreetUser(DatabaseFixture databaseFixture)
        {
            testDbContext = databaseFixture.DbContext;
        }

        private void CrudConstructor(){
            crud = new CreateReadUpdateDelete(testDbContext);
            message = new GreetMessage();
        }

        [Fact]
        public void GreetMceboInAllLanguage()
        {
            CrudConstructor();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var grettingsDatabase = testDbContext.Greetings;
            Assert.Equal("Sawubona, Mcebo", message.Message(greetBind, "isizulu"));
            Assert.Equal("Hello, Mcebo", message.Message(greetBind, "english"));
            Assert.Equal("Hola, Mcebo", message.Message(greetBind, "spanish"));
            // Assert.Equal();
        }

        [Fact]
        public void GreetAndreInAllLanguage()
        {
            CrudConstructor();
            var greetBind = new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var grettingsDatabase = testDbContext.Greetings;
            Assert.Equal("Sawubona, Andre", message.Message(greetBind, "isizulu"));
            Assert.Equal("Hello, Andre", message.Message(greetBind, "english"));
            Assert.Equal("Hola, Andre", message.Message(greetBind, "spanish"));
            // Assert.Equal();
        }
    }
}