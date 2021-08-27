using System;

namespace DatabaseFeatures
{
    class DatabaseNameGenerator : IDatabaseNameGenerator
    {
        public string Prefix { get; set; } = "databaseTesting";

        public string Generate()
        {
            return $"{Prefix}{DateTime.UtcNow.Ticks}";
        }
    }
}