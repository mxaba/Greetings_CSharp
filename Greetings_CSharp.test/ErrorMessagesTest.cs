using System.Collections.Generic;
using Greetings_CSharp.Models;
using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class ErrorMessages
    {
        private ICreateReadUpdateDelete createReadUpdateDelete;

        public ErrorMessages([FromServices] ICreateReadUpdateDelete createReadUpdateDelete)
        {
            this.createReadUpdateDelete = createReadUpdateDelete;
        }

        [Fact]
        public void NoLanguageSelected()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Please select a language! ❌", createReadUpdateDelete.CreateAndUpdate(greetBind, ""));
        }

        [Fact]
        public void NoNamePassed()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Please enter a valid name with only Letters! ❌", createReadUpdateDelete.CreateAndUpdate(greetBind, "english"));
        }

        [Fact]
        public void PassingAnInvalidName()
        {
            createReadUpdateDelete.ResetDB();
            var greetBind = new Greetings { Name="Mcebo09@#$%ˆ&*!±", English=0, Spanish=0,  Isizulu=0, Counts=0};
            Assert.Equal("Please enter a valid name with only Letters! ❌", createReadUpdateDelete.CreateAndUpdate(greetBind, "spanish"));
            createReadUpdateDelete.ResetDB();
        }
    }
}