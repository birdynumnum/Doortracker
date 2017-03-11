
namespace Constants
{
    public static class Constants
    {
        public const string DoorTrackerAPI = "http://localhost:55736/";
        public const string DoorTrackerAngular = "http://localhost:55736/";

        public const string DoorTrackerClientSecret = "TopSecret007";

        public const string DoorTrackerIssuerUri = "https://doortrackersts/identity";
        public const string DoorTrackerSTSOrigin = "https://localhost:44300";
        public const string DoorTrackerSTS = DoorTrackerSTSOrigin + "/identity";
        public const string DoorTrackerSTSTokenEndpoint = DoorTrackerSTS + "/connect/token";
        public const string DoorTrackerSTSAuthorizationEndpoint = DoorTrackerSTS + "/connect/authorize";
        public const string DoorTrackerSTSUserInfoEndpoint = DoorTrackerSTS + "/connect/userinfo";
        public const string DoorTrackerSTSEndSessionEndpoint = DoorTrackerSTS + "/connect/endsession";
        public const string DoorTrackerSTSRevokeTokenEndpoint = DoorTrackerSTS + "/connect/revocation";
    }
}
