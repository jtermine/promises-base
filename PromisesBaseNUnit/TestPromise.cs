using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termine.Promises.Interfaces;

namespace Termine.Promises.BaseNUnit
{
	public class TestPromise : IHandlePromiseActions
	{
		private List<IHandleEventMessage> Messages => new List<IHandleEventMessage>();

		public bool IsBlocked { get; }
		public bool IsTerminated { get; }
		public string PromiseId { get; }
		public int AuthChallengersCount { get; }
		public int ValidatorsCount { get; }
		public int ExecutorsCount { get; }

		public TestPromise()
		{
			PromiseId = Guid.NewGuid().ToString("N");
		}
		
		public string SerializeWorkload()
		{
			throw new NotImplementedException();
		}

		public string SerializeRequest()
		{
			throw new NotImplementedException();
		}

		public string SerializeConfig()
		{
			throw new NotImplementedException();
		}

		public string SerializeResponse()
		{
			throw new NotImplementedException();
		}

		public bool HasBlocked { get; set; }

		public IHandleEventMessage LastBlockedEventMesssage { get; set; }

		public void Block(IHandleEventMessage message)
		{
			HasBlocked = true;
			LastBlockedEventMesssage = message;
			Messages.Add(message);
		}

		public void Trace(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Debug(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Info(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Warn(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Error(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Fatal(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Abort(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void AbortOnAccessDenied(IHandleEventMessage message)
		{
			throw new NotImplementedException();
		}

		public void Block(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Trace(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Debug(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Info(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Warn(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Error(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Fatal(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void Abort(Exception ex)
		{
			throw new NotImplementedException();
		}

		public void AbortOnAccessDenied(Exception ex)
		{
			throw new NotImplementedException();
		}


	}
}
