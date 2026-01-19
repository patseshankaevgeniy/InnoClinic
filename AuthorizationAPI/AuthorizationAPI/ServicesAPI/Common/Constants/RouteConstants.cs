namespace Services.Api.Common.Constants;

public static class RouteConstants
{
    public const string BaseApiRoute = "api";
    public const string ProceduresControllerRoute = BaseApiRoute + "/procedures";
    public const string SpecializationsControllerRoute = BaseApiRoute + "/specializations";

    public const string GetAllRoute = "get-all";
    public const string GetCountRoute = "get-counts";
    public const string GetRoute = "get-by-id";
    public const string CreateRoute = "create";
    public const string UpdateRoute = "update";
    public const string DeleteRoute = "delete";
}
