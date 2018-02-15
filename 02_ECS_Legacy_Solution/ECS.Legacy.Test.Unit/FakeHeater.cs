namespace ECS.Legacy.Test.Unit
{
	public class FakeHeater : IHeater
	{
		public bool Status { get; private set; }

		public bool RunSelfTest()
		{
			return true;
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