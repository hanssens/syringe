using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syringe.Tests
{
    public interface IMathNode
    {
        int Calculate();
    }

    public class Zero : IMathNode
    {
        public int Calculate() {
            return 0;
        }
    }

    public class Number : IMathNode
    {
        private int _number;
        public Number(int number)
        {
            _number = number;
        }
        public int Calculate()
        {
            return _number;
        }
    }
}
