namespace DatabaseFeatures
{
    public interface ITestDatabase
    {
        void Create();
        void RunScripts(string scriptFolderPath);
        string ConnectionString { get; }
        void Drop();
    }
}