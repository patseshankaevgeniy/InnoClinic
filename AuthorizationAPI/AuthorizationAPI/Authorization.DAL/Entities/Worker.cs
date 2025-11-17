namespace Authorization.DAL.Entities;

public class Worker : Identity
{
    public WorkerStatus Status { get; set; } = default;
}
