namespace MyExtensions.Endpoints
{
    public static class ProfileEndpoint
    {
        public static void MapProfile(this IEndpointRouteBuilder builder)
        {
            //? Profiles, Group of Related Endpoints
            var profilesGroup = builder.MapGroup("profiles");
            profilesGroup.MapGet("/r1", () => "profile r1");
            profilesGroup.MapGet("/r2", () => "profile r2");
            profilesGroup.MapGet("/r3", () => "profile r3");
        }
    }
}
