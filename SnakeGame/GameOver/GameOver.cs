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

namespace SnakeGame
{
    public class GameOver
    {
       
        private string _gameFinished;
        
        public string GameFinished
        {
            get
            {
                return _gameFinished;
            }
            set
            {
                _gameFinished = value;
            }
        }
        public GameOver()
        {
            _gameFinished = GameFinished;
        }
        public string GamesOver()
        {
           
            //display game over text when snake is not present
            _gameFinished = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
            return _gameFinished;
        }
    }
}
