﻿/// name: SigmaIA
/// desciption: IA that does Q-learning to touch an rectangle
/// Author: GlicyDev
/// Last update date: 14.12.2024
/// Version: 1.0.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iaQTable
{
    public class Sigma
    {
        // Instance variables
        private Random rnd = new Random();
        private Dictionary<(int, int), Dictionary<string, double>> _qTable = new Dictionary<(int, int), Dictionary<string, double>>();
        private Panel _target;
        private PictureBox _sigmaImg;
        private Label _lblPoints;

        private int _sigmaSize;
        private int _mapSize;
        private int _tableSize;
        private int _nbGames;
        private double _learningRate;
        private double _discount;
        private double _exploration;
        private int _nbMoves;
        private int _delay;
        private int _x, _y;

        // Predefined variables
        private string[] _moves = { "up", "down", "left", "right" };
        private int _stepsToMove = 10;
        private int _episode = 0;
        private int positiveReward = 100;
        private int negativeeReward = -1;
        private ConsoleColor winColor = ConsoleColor.Green;
        private ConsoleColor looseColor = ConsoleColor.Red;
        private ConsoleColor normalColor = Console.ForegroundColor;
        private List<string> _possibleMoves = new List<string>();

        /// <summary>
        /// Constructor of Sigma IA
        /// </summary>
        /// <param name="mapSize">The size of one side of the map</param>
        /// <param name="learningRate">If the bot will learn much or not (0-1)</param>
        /// <param name="discount">If the bot will use future informations or not (0-1)</param>
        /// <param name="nbMoves">Maximum moves per game</param>
        /// <param name="delay">Delay in milliseconds between each Move</param>
        /// <param name="nbGames">Number of games to play</param>
        /// <param name="target">The target element</param>
        /// <param name="sigmaImg">The Sigma IA element</param>
        /// <param name="lblPoints">The label with the points</param>
        public Sigma(int mapSize, double learningRate, double discount, int nbMoves, int delay, int nbGames, Panel target, PictureBox sigmaImg, Label lblPoints)
        {
            _mapSize = mapSize;
            _tableSize = mapSize / _stepsToMove;
            _learningRate = learningRate;
            _discount = discount;
            _target = target;
            _sigmaImg = sigmaImg;
            _lblPoints = lblPoints;
            _nbMoves = nbMoves;
            _delay = delay;
            _nbGames = nbGames;
            _sigmaSize = sigmaImg.Width;
            _x = _mapSize / 2;
            _y = _mapSize / 2;


            _sigmaImg.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_sigmaImg, true, null);


            // Fill the QTable with 0 values
            for (int x = 0; x <= _tableSize; x++)
            {
                for (int y = 0; y <= _tableSize; y++)
                {
                    // Put 0 points foreach action in the coordinate
                    _qTable.Add((x * _stepsToMove, y * _stepsToMove), new Dictionary<string, double>{
                        { "up", 0.0 },
                        { "down", 0.0 },
                        { "left", 0.0 },
                        { "right", 0.0 }
                    });
                }
            }
        }

        /// <summary>
        /// Set the end of the run
        /// </summary>
        /// <param name="winned">If Sigma won or not</param>
        private void End(bool winned)
        {
            if (winned)
            {
                Console.ForegroundColor = winColor;
                Console.WriteLine("Sigma won");
            }
            else
            {
                Console.ForegroundColor = looseColor;
                Console.WriteLine("Sigma lost");
            }

            placeSigma();
            Console.ForegroundColor = normalColor;
        }

        /// <summary>
        /// Place the sigma randomly not on the target
        /// </summary>
        private void placeSigma()
        {
            _x = _mapSize / 2;
            _y = _mapSize / 2;
            int targetX = _target.Location.X;
            int targetY = _target.Location.Y;

            int max = _mapSize - _sigmaSize;

            while (_x < 0 || _x % _stepsToMove != 0 || SigmaTouchTarget(_x, _y))
                _x = rnd.Next(0, max);
            while (_y < 0 || _y % _stepsToMove != 0 || SigmaTouchTarget(_x, _y))
                _y = rnd.Next(0, max);

            _sigmaImg.Location = new System.Drawing.Point(_x, _y);
        }

        /// <summary>
        /// Update the points for each Move
        /// </summary>
        /// <param name="sigmaPanel"></param>
        private void UpdatePoints()
        {
            // Initialisation
            int nextX = 0;
            int nextY = 0;

            // Do for each moves
            for (int i = 0; i < 4; i++)
            {
                string move = _moves[i];
                nextX = move == "left" && _x > 0 ? _x - _stepsToMove : (move == "right" && _x < _mapSize - _sigmaSize ? _x + _stepsToMove : _x);
                nextY = move == "down" && _y > 0 ? _y - _stepsToMove : (move == "up" && _y < _mapSize - _sigmaSize ? _y + _stepsToMove : _y);

                if (nextX >= 0 && nextX <= 600 && nextY >= 0 && nextY <= 600 && _x >= 0 && _x <= 600 && _y >= 0 && _y <= 600)
                {
                    double nextMax = _qTable[(nextX, nextY)].Values.Max();
                    double actualPoints = _qTable[(_x, _y)][move];

                    // Bellman equation
                    _qTable[(_x, _y)][move] = actualPoints + _learningRate * (getPoints(nextX, nextY) + _discount * nextMax - actualPoints);
                }
            }
        }

        private bool SigmaTouchTarget(int x, int y)
        {
            return x >= _target.Location.X - _sigmaSize && x < _target.Location.X + _target.Width && y > _target.Location.Y - _sigmaSize && y < _target.Location.Y + _target.Height;
        }

        /// <summary>
        /// Get the number of points in an state
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        private int getPoints(int x, int y)
        {
            int points = negativeeReward;

            if (SigmaTouchTarget(x, y))
            {
                points = positiveReward;
            }

            return points;
        }

        /// <summary>
        /// Check if the sigma won
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckWin(int x, int y)
        {
            return getPoints(x, y) == positiveReward;
        }

        /// <summary>
        /// Mage sigma Move in function of the action
        /// </summary>
        /// <param name="action">The action to do</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        private void Move(string action, int x, int y)
        {
            if (action == "up")
                _sigmaImg.Location = new System.Drawing.Point(x, y - _stepsToMove);

            else if (action == "down")
                _sigmaImg.Location = new System.Drawing.Point(x, y + _stepsToMove);

            else if (action == "left")
                _sigmaImg.Location = new System.Drawing.Point(x - _stepsToMove, y);

            else if (action == "right")
                _sigmaImg.Location = new System.Drawing.Point(x + _stepsToMove, y);
        }

        /// <summary>
        /// Change the points in the label
        /// </summary>
        /// <param name="points"></param>
        private void SetLblPoints(double points)
        {
            _lblPoints.Text = points.ToString();
        }

        /// <summary>
        /// Play one Move of the ia
        /// </summary>
        private void PlayOneMove()
        {
            bool explorate = true;
            string action = string.Empty;
            double max = 0;
            List<string> elementsThatHaveMax = new List<string>();

            try
            {
                max = _qTable[(_x, _y)].Values.Max();
                action = _qTable[(_x, _y)].FirstOrDefault(act => act.Value == max).Key;

                explorate = rnd.NextDouble() < _exploration;

                elementsThatHaveMax = _qTable[(_x, _y)].Where(kv => kv.Value == max)
                    .Select(kv => kv.Key)
                    .ToList();
            }
            catch (Exception ex)
            {
                explorate = true;
                Console.WriteLine("Key not found");
            }
            bool conditionUp = _y >= _stepsToMove;
            bool conditionDown = _y <= _mapSize - _stepsToMove - _sigmaSize * 2;
            bool conditionLeft = _x >= _stepsToMove;
            bool conditionRight = _x <= _mapSize - _stepsToMove - _sigmaSize * 2;

            bool canGoUp = action == "up" && conditionUp;
            bool canGoDown = action == "down" && conditionDown;
            bool canGoLeft = action == "left" && conditionLeft;
            bool canGoRight = action == "right" && conditionRight;

            // If more than 1 have the max value so get one random (not to spam up at start)
            if (elementsThatHaveMax.Count > 1)
            {
                action = elementsThatHaveMax[rnd.Next(elementsThatHaveMax.Count)];
            }

            // Update the points then change the points in the lbl points
            UpdatePoints();
            SetLblPoints(max);

            // If he explorates, so play a random else find the best
            if (!explorate && (canGoUp || canGoDown || canGoLeft || canGoRight))
            {
                Move(action, _x, _y);
            }
            else
            {
                _possibleMoves.Clear();

                if (conditionDown)
                    _possibleMoves.Add("down");

                if (conditionLeft)
                    _possibleMoves.Add("left");

                if (conditionRight)
                    _possibleMoves.Add("right");

                if (conditionUp)
                    _possibleMoves.Add("up");

                string randomAction = _possibleMoves[rnd.Next(_possibleMoves.Count)];
                Move(randomAction, _x, _y);
            }
        }

        /// <summary>
        /// Simulate an whole game of the IA
        /// </summary>
        async private Task<bool> SimulateGame()
        {
            for (int i = 0; i < _nbMoves; i++)
            {
                _x = _sigmaImg.Location.X;
                _y = _sigmaImg.Location.Y;

                PlayOneMove();

                if (CheckWin(_x, _y))
                {
                    End(true);
                    return true;
                }
                await Task.Delay(_delay);
            }

            End(false);
            return true;
        }

        /// <summary>
        /// Simulate all the learning of sigma
        /// </summary>
        async public void Simulate()
        {
            placeSigma();

            for (_episode = 0; _episode < _nbGames; _episode++)
            {
                _exploration = Math.Max(0.1, 1.0 - (double)_episode / _nbGames);
                await SimulateGame();
            }
        }
    }
}