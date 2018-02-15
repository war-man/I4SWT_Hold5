namespace ECS.Legacy.Test.Unit
{
	public class FakeTempSensor : ITempSensor
	{
		public int GetTemp()
		{
			throw new System.NotImplementedException();
		}

		public bool RunSelfTest()
		{
			throw new System.NotImplementedException();
		}
	}
}