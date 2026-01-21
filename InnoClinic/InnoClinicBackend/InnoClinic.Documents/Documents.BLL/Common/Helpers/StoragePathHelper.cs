using Documents.DAL.Common.Constants;

namespace Documents.BLL.Common.Helpers;

public static class StoragePathHelper
{
    public static string GetUrl(string fileKey) =>
        $"{DocumentConstants.StorageBucket}/{fileKey}";
}
