using Greetings_CSharp.Database;
using Greetings_CSharp.Models;
using Xunit;

namespace Greetings_CSharp.test
{
    [Collection("Database")]
    public class ErrorMessages
    {
        private GreetMessage message;

        private void CrudConstructor(){
            message = new GreetMessage();
        }

        [Fact]
        public void LanguageNotSelected()
        {
            CrudConstructor();
            var greetBind = new Greetings { Name="Mcebo"};
            Assert.Equal("Please select a language! ❌", message.Message(greetBind, ""));
        }

        [Fact]
        public void NameSelectedButWithNumbers()
        {
            CrudConstructor();
            var greetBind = new Greetings { Name="Mcebo12" };
            Assert.Equal("Please enter a valid name with only Letters! ❌", message.Message(greetBind, "english"));
        }

        [Fact]
        public void NameAndLanguageNotSelected()
        {
            CrudConstructor();
            var greetBind = new Greetings { Name="" };
            Assert.Equal("Please enter a name and select a language ❌", message.Message(greetBind, ""));
        }
    }
}