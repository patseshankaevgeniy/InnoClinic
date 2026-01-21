namespace Documents.API.Common;

public static class RouteConstants
{
    public const string BaseApiRoute = "api";
    public const string DocumentsControllerRoute = BaseApiRoute + "/documents";

    public const string GetAllRoute = "get-all";
    public const string GetByIdentityIdRoute = "get-by-identity-id";
    public const string GetByIdRoute = "get-by-id";
    public const string CreateRoute = "create";
    public const string DeleteRoute = "delete";
}
