namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPublicEventMessage
    {
        /// <summary>
        /// An integer an application group designates as the authoritative event
        /// </summary>
        int EventNumber { get; set; }

        /// <summary>
        /// The event message which will be publically displayed in unsecured contexts if the "IsPublicMessage" property is set to true
        /// </summary>
        string EventPublicMessage { get; set; }

        /// <summary>
        /// The details to the event message which will be publically displayed in unsecured contexdts if the "IsPublicMessage" property is set to true
        /// </summary>
        string EventPublicDetails { get; set; }
    }
}
