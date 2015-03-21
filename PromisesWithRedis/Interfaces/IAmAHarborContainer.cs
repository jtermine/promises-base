namespace Termine.Promises.WithRedis.Interfaces
{
	public interface IAmAHarborContainer
	{
		void AddModel(IAmAHarborModel model);
		void AddRelationship(IAmAHarborRelationship relationship);


	}
}