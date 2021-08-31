using System.Collections.Generic;
using Greetings_CSharp.Models;
using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class Counter
    {
        private ICreateReadUpdateDelete createReadUpdateDelete;

        public Counter([FromServices] ICreateReadUpdateDelete createReadUpdateDelete)
        {
            this.createReadUpdateDelete = createReadUpdateDelete;
        }

        [Fact]
        public void ShouldGreetAndAddItToTheDatabase()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Sawubona, Mcebo", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            Assert.Equal(1, createReadUpdateDelete.CountData());
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void ShouldAddThePeopleGreeted()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Josiah", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind1 = new Greetings { Name="Cinga", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind2 = new Greetings { Name="Miguel", English=0, Spanish=0,  Isizulu=0, Counts=0};
            var greetBind3 = new Greetings { Name="Andre", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hello, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            Assert.Equal("Hello, Cinga", createReadUpdateDelete.CreateAndUpdate(greetBind1, "english"));
            Assert.Equal("Sawubona, Miguel", createReadUpdateDelete.CreateAndUpdate(greetBind2, "isizulu"));
            Assert.Equal("Sawubona, Andre", createReadUpdateDelete.CreateAndUpdate(greetBind3, "isizulu"));

            Assert.Equal(4, createReadUpdateDelete.CountData());
            createReadUpdateDelete.ResetDB();
        }

        [Fact]
        public void ShouldNotAddTheSamePersonTwice()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Josiah", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Hello, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            Assert.Equal("Hello, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
            Assert.Equal("Sawubona, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));
            Assert.Equal("Sawubona, Josiah", createReadUpdateDelete.CreateAndUpdate(greetBind, "isizulu"));

            Assert.Equal(1, createReadUpdateDelete.CountData());
            createReadUpdateDelete.ResetDB();
        }
    }
}