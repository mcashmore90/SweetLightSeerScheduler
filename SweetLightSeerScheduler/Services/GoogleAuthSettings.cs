using SweetLightSeerScheduler.Interfaces;

namespace SweetLightSeerScheduler.Services
{
    public class GoogleAuthSettings : IGoogleAuthSettings
    {
        public string PrivateKey { get; set; } = "";
        public string ClientEmail { get; set; } = "";
        public string[] Scope { get; set; } = [""];
        public string ApplicationName { get; set; } = "";
        public string ReadingId { get; set; } = "";
    }
}
