using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public class HarborProperty: IAmAHarborProperty, ICanExtendAnyProperty
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public EnumAllowNull AllowNull { get; set; }

        public enum EnumAllowNull
        {
            AllowNullAndStoreAsNull = 0,
            AllowNullAndOmitStoreAsNull = 1,
            BlockNull = 2,
        }

        public IAmAHarborProperty Property
        {
            get { return this; }
        }
    }
}
