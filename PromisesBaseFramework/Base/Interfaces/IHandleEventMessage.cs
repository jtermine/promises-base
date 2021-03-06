﻿namespace Termine.Promises.Base.Interfaces
 
{
	/// <summary>
	/// Defines a class that presents an event message.
	/// </summary>
    public interface IHandleEventMessage
    {
		/// <summary>
		/// An optional value an application can use to distinguish multiple variations of the same event
		/// </summary>
		int MinorEventNumber { get; set; }

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

		/// <summary>
		/// Designates whether the event message and details should be displayed in unsecured contexts
		/// </summary>
		bool IsSensitiveMessage { get; set; }
        bool IsFailure { get; set; }
    }
}
