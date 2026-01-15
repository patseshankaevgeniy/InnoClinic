namespace Appointment.Api.Common;

public static class RouteConstants
{
    public const string BaseApiRoute = "api";
    public const string AppointmentsControllerRoute = BaseApiRoute + "/appointments";
    public const string AppointmentResultsControllerRoute = BaseApiRoute + "/appointment-results";

    public const string GetAllRoute = "get-all";
    public const string GetFilteredByDateRoute = "get-by-date";
    public const string GetRoute = "get-by-id";
    public const string CreateRoute = "create";
    public const string UpdateRoute = "update";
    public const string DeleteRoute = "delete";
}
