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
    public partial class Form1 : Form
    {
        // create a list of snake objects 
        private List<Circle> Snake = new List<Circle>();
        private CircleHead head = new CircleHead();

        // create circle food object
        private CircleFood food = new CircleFood();
        private CircleBody body = new CircleBody();
        private Dead dead = new Dead();

        public Form1()
        {
            InitializeComponent();

            // set settings to default
            new Settings();
            // start game timer
            GameTimer();
            // start new game
            StartGame();
        }
        private void GameTimer()
        {
            // set game speed assigning value divide by speed to timer interval
            gameTimer.Interval = 1500 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;

            // start gameTimer using start()
            gameTimer.Start();
        }
        
        private void StartGame()
        {
            // label game over label invisible when starting game by setting to false
            lblGameOver.Visible = false;
            NewSnake();
            // set score label to actual score 
            lblScore.Text = Settings.Score.ToString();

            // call function that creates food and places it randomly
            GenerateFood();
        }
        private void NewSnake()
        {
            // set settings to default to start new game
            new Settings();
            // create new player object by using Snake.Clear() to remove all snake objects
            Snake.Clear();
            Snake.Add(head);
        }
        // place random food on game
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            
            Random random = new Random();
            food = new CircleFood();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            // check if the game is over
            if(Settings.GameOver == true)
            {
               
                // check if enter is pressed
                if(Input.KeyPressed(Keys.Enter))
                {
                    
                    StartGame();
                }
                
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                MovePlayer();
            }
            //all data on canvas gets refreshed
            pbCanvas.Invalidate();
        }


        private void MovePlayer()
        {
            
            for(int i = Snake.Count - 1; i >= 0; i--)
            {
                //move head
                if(i == 0)
                {
                    switch(Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }
                    
                    //get maximum X and Y Pos
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    //detect collision with game borders
                    if(Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    {
                        dead.Die();
                    }
                    //detect collision with body
                    for(int j = 1; j < Snake.Count; j++)
                    {
                        if(Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            dead.Die();
                        }
                    }
                    //detect collision with food piece
                    if(Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    //move next circle to front of adjacent to create movement
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void AddToBody()
        {
            // add circle to body
            CircleBody body = new CircleBody()
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
        }
        private void Eat()
        {
          
            ScoreUpdate updateScore = new ScoreUpdate();
            AddToBody();
            lblScore.Text = updateScore.UpdateScore();
            GenerateFood();
           

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
       private void GameOver()
       {
            GameOver gameOver = new GameOver();
            //display game over text when snake is not present
            lblGameOver.Text = gameOver.GamesOver();
            // make game over label visible when game is over
            lblGameOver.Visible = true;
       }

        private void pbCanvas_Paint_1(object sender, PaintEventArgs e)
        {
            // declare graphics object and set it to the 
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                
                // set color of snake by using a loop for the snake list array 
                for (int i = 0; i < Snake.Count; i++)
                {
                    //use Brush graphics class to fill in snake head and body with color
                    Brush snakeColor;
                    Brush snakeFoodColor = Brushes.Green;
                    if (i == 0)
                        snakeColor = Brushes.Black;    // draw head
                    else
                        snakeColor = Brushes.DarkOliveGreen;    // rest of body

                    //draw snake
                    canvas.FillEllipse(snakeColor,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));

                    //draw food
                    canvas.FillEllipse(snakeFoodColor,
                        new Rectangle(food.X * Settings.Width,
                                      food.Y * Settings.Height,
                                      Settings.Width, Settings.Height));
                }
            }
            else
            {
               
                GameOver();
                
            }
        }
    }
}
