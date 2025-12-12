namespace Profiles.API.Common.Constants;

public static class RouteConstants
{
    public const string BaseApiRoute = "api";
    public const string DoctorsControllerRoute = BaseApiRoute + "/doctors";
    public const string PatientsControllerRoute = BaseApiRoute + "/patients";
    public const string ReceptionistsControllerRoute = BaseApiRoute + "/receptionists";

    public const string GetAllRoute = "get-all";
    public const string GetCountRoute = "get-counts";
    public const string GetRoute = "get-by-id";
    public const string CreateRoute = "create";
    public const string UpdateRoute = "update";
    public const string DeleteRoute = "delete";
}
