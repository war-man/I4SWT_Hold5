namespace ECS.Legacy.Test.Unit
{
	public class FakeTempSensor : ITempSensor
	{
		public int Temp { private get; set; } = 0;

		public int GetTemp()
		{
			return Temp;
		}

		public bool RunSelfTest()
		{
			return true;
		}
	}
}