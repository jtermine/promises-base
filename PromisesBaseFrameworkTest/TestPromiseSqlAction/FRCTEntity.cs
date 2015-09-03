using System.Runtime.Serialization;

namespace PromisesBaseFrameworkTest.TestPromiseSqlAction
{
    [DataContract]
    public class FRCTEntity
    {
        [DataMember(Name = "transactionCodeId")]
        public int TransactionCodeId { get; set; }

        [DataMember(Name = "siteId")]
        public int SiteId { get; set; }

        [DataMember(Name = "transactionCode")]
        public string TransactionCode { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "glAccount")]
        public int GlAccount { get; set; }

        [DataMember(Name = "freq")]
        public int Freq { get; set; }

        [DataMember(Name = "avgAmount")]
        public decimal AvgAmount { get; set; }

        [DataMember(Name = "maxAmount")]
        public decimal MaxAmount { get; set; }

    }
}