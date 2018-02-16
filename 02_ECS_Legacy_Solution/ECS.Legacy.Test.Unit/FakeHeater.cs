namespace ECS.Legacy.Test.Unit
{
	public class FakeHeater : IHeater
	{
		public bool Status { get; private set; } = false;
		public bool SelfTestStatus { private get; set; } = true;

		public bool RunSelfTest()
		{
			return SelfTestStatus;
		}

		public void TurnOff()
		{
			Status = false;
		}

		public void TurnOn()
		{
			Status = true;
		}
	}
}