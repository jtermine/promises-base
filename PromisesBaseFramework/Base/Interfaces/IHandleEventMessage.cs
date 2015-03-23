namespace Termine.Promises.Base.Interfaces
 
{
	/// <summary>
	/// Defines a class that presents an event message.
	/// </summary>
    public interface IHandleEventMessage
    {
		/// <summary>
		/// The application group Id that the event originates from.  NOTE: An application can only raise events that belong to its authorized application group.
		/// </summary>
		int EventApplicationGroup { get; set; }

		/// <summary>
		/// The type of event being raised (e.g. info, success, requestError, serverError)
		/// </summary>
		EnumEventType EventType { get; set; }

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
		bool IsPublicMessage { get; set; }
    }

	/// <summary>
	/// Describes the type of event
	/// </summary>
	public enum EnumEventType
	{
		/// <summary>
		/// for informational events
		/// </summary>
		Info = 100,

		/// <summary>
		/// for successful events
		/// </summary>
		Success = 200,

		/// <summary>
		/// for events where the request is in error or unauthorized
		/// </summary>
		RequestError =400,

		/// <summary>
		/// for events the promise was unable to execute
		/// </summary>
		ServerError = 500
	}

}
