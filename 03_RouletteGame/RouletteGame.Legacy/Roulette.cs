using System;
using System.Collections.Generic;

namespace RouletteGame.Legacy
{
 
    public class Roulette
    {
        private readonly List<Field> _fields;
        private Field _result;

        public Roulette(IFieldFactory fieldFactory)
        {
            _fields = fieldFactory.CreateFields();
            _result = _fields[0];
        }

        public void Spin()
        {
            var n = (uint) new Random().Next(0, 37);
            _result = _fields[(int) n];
        }

        public Field GetResult()
        {
            return _result;
        }
    }
}