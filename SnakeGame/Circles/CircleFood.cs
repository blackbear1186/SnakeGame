using SnakeGame.Circles;
using System;
using System.Collections.Generic;//needed for generic list
using System.ComponentModel;
using System.Data;
using System.Drawing;//needed for graphics drawing
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Circles
{
    public class CircleFood : Circle
    {
        public CircleFood() : base ()
        {
            X = 0;
            Y = 0;
   
        }
    }
}
