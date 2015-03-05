namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborModel: IAmAHarborBaseType, ICanExtendAnyHarborBaseType<IAmAHarborProperty>, ICanExtendAnyModel
    {
        string Name { get; set; }
        string Caption { get; set; }
        bool IsPublic { get; set; }
    }
}
