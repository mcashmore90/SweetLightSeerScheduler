namespace SweetLightSeerScheduler.Interfaces
{
    public interface IGoogleAuthSettings
    {
        string PrivateKey { get; set; }
        string ClientEmail { get; set; }
        string[] Scope { get; set; }
        string ApplicationName { get; set; }
        string ReadingId { get; set; }
    }
}
