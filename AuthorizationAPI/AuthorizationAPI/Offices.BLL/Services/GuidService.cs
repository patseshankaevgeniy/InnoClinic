using Offices.BLL.Services.Interfaces;

namespace Offices.BLL.Services;

public sealed class GuidService : IGuidService
{
    public Guid NewGuid() => Guid.NewGuid();
}
