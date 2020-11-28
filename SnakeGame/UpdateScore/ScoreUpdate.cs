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
using System.Windows.Controls.Ribbon;

namespace SnakeGame
{
    public class ScoreUpdate
    {
        public string UpdateScore()
        {
            Settings.Score += Settings.Points;

            return Settings.Score.ToString();
        }

    }
}
