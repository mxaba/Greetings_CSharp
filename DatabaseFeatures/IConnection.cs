namespace DatabaseFeatures
{
    interface IConnection
    {
        void Execute(string connectionString, string command);
    }
}