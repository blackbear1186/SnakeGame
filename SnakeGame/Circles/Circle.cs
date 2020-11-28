using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Circles
{
    public class Circle 
    {
        private int _x;
        private int _y;
        public int X { get; set;  }
        public int Y { get; set; }

        
        public Circle(int X, int Y)
        {
            _x = X;
            _y = Y;
        }
        public Circle()
        {
            //initialize circle class 
            X = 0;
            Y = 0;
        }

    }
}
