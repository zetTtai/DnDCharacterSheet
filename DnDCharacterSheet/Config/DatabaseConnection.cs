public class DatabaseConnection
{
    public const string Environment = "Development";
    public string ServerName { get; set; } = String.Empty;
    public string DatabaseName { get; set;} = String.Empty;
    public bool UseTrustedConnection { get; set; }
    public bool Encrypt { get; set; } = false;

}