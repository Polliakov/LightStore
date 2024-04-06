namespace LightStore.Persistence.Entities.Base
{
    public enum OrderStatus : byte
    {
        Created   = 0,
        Ready     = 1,
        Finished  = 2,
        Cancelled = 3,
    }
}
