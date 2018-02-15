using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ECS.Legacy.Test.Unit")]

namespace ECS.Legacy
{
    public interface ITempSensor
    {
        int GetTemp();
        bool RunSelfTest();
    }
    internal class TempSensor : ITempSensor
    {
        public int GetTemp()
        {
            return 25;
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}