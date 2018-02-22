using System;
using System.Collections.Generic;

namespace RouletteGame.Legacy
{
 
    public class Roulette
    {
        private readonly List<Field> _fields;
        private Field _result;
	    private IRandomizer _randomizer;

        Roulette(IFieldFactory fieldFactory, IRandomizer randomizer)
        {
            _fields = fieldFactory.CreateFields();
            _result = _fields[0];

	        _randomizer = randomizer;
        }

        public void Spin()
        {
            uint n = _randomizer.Next(0, 37);
            _result = _fields[(int) n];
        }

        public Field GetResult()
        {
            return _result;
        }
    }
}