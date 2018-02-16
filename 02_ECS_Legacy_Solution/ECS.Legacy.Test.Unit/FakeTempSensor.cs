namespace ECS.Legacy.Test.Unit
{
	public class FakeTempSensor : ITempSensor
	{
		public int Temp { private get; set; } = 0;
		public bool SelfTestStatus { private get; set; } = true;

		public int GetTemp()
		{
			return Temp;
		}

		public bool RunSelfTest()
		{
			return SelfTestStatus;
		}
	}
}