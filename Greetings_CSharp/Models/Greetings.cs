using System;
using System.ComponentModel.DataAnnotations;

namespace Greetings_CSharp.Models
{
    public class Greetings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int English { get; set; }
        public int Spanish { get; set; }
        public int Isizulu { get; set; }
        public int Counts { get; set; }
    }
}
