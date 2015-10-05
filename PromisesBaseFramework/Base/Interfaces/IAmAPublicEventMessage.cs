using System;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPublicEventMessage
    {
        DateTime EventTimestamp { get; set; }

        /// <summary>
        /// The HTTP response code to return ot the client when this message is engaged.
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

        /// <summary>
        /// The specific notification event number.
        /// </summary>
        int MinorEventNumber { get; set; }

        bool IsFailure { get; set; }
    }
}
