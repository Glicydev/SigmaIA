/// name: SigmaIA
/// desciption: IA that does Q-learning to touch an rectangle
/// Author: GlicyDev
/// Last update date: 14.12.2024
/// Version: 1.0.0

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace iaQTable
{
    public class Sigma
    {
        // Instance variables
        private Random rnd = new Random();
        private Dictionary<(int, int), Dictionary<string, double>> _qTable = new Dictionary<(int, int), Dictionary<string, double>>();
        private Panel _target;
        private Panel _sigmaPanel;
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

        // Predefined variables
        private string[] _moves = { "up", "down", "left", "right" };
        private int _stepsToMove = 10;
        private int _episode = 0;
        private int positiveReward = 1;
        private int negativeeReward = -1;

        /// <summary>
        /// Constructor of Sigma IA
        /// </summary>
        /// <param name="mapSize">The size of one side of the map</param>
        /// <param name="learningRate">If the bot will learn much or not (0-1)</param>
        /// <param name="discount">If the bot will use future informations or not (0-1)</param>
        /// <param name="nbMoves">Maximum moves per game</param>
        /// <param name="delay">Delay in milliseconds between each move</param>
        /// <param name="nbGames">Number of games to play</param>
        /// <param name="target">The target element</param>
        /// <param name="sigmaPanel">The Sigma IA element</param>
        /// <param name="lblPoints">The label with the points</param>
        public Sigma(int mapSize, double learningRate, double discount, int nbMoves, int delay, int nbGames, Panel target, Panel sigmaPanel, Label lblPoints)
        {
            _mapSize = mapSize;
            _tableSize = mapSize / _stepsToMove;
            _learningRate = learningRate;
            _discount = discount;
            _target = target;
            _sigmaPanel = sigmaPanel;
            _lblPoints = lblPoints;
            _nbMoves = nbMoves;
            _delay = delay;
            _nbGames = nbGames;
            _sigmaSize = sigmaPanel.Width;

            _sigmaPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_sigmaPanel, true, null);


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
        /// Place the sigma randomly not on the target
        /// </summary>
        private void placeSigma()
        {
            int x = -1;
            int y = -1;
            int max = _mapSize - _sigmaSize;

            while (x < 0 || x % _stepsToMove != 0)
                x = rnd.Next(0, max);
            while (y < 0 || y % _stepsToMove != 0)
                y = rnd.Next(0, max);

            _sigmaPanel.Location = new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// Update the points for each move
        /// </summary>
        /// <param name="sigmaPanel"></param>
        private void UpdatePoints()
        {
            // Initialisation
            int x = _sigmaPanel.Location.X;
            int y = _sigmaPanel.Location.Y;
            int nextX = 0;
            int nextY = 0;

            // Do for each moves
            for (int i = 0; i < 4; i++)
            {
                string move = _moves[i];
                nextX = move == "left" && x > 0 ? x - _stepsToMove : (move == "right" && x < _mapSize ? x + _stepsToMove : x);
                nextY = move == "down" && y > 0 ? y - _stepsToMove : (move == "up" && y < _mapSize ? y + _stepsToMove : y);

                double nextMax = _qTable[(nextX, nextY)].Values.Max();
                double actualPoints = _qTable[(x, y)][move];

                // Bellman equation
                _qTable[(x, y)][move] = actualPoints + _learningRate * (getPoints(nextX, nextY) + _discount * nextMax - actualPoints);
            }
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

            if (x >= _target.Location.X - _sigmaSize && x < _target.Location.X + _target.Width && y > _target.Location.Y - _sigmaSize && y < _target.Location.Y + _target.Height)
            {
                placeSigma();
                points = positiveReward;
            }

            return points;
        }

        private bool checkWin(int x, int y)
        {
            return getPoints(x, y) == positiveReward;
        }

        /// <summary>
        /// Mage sigma move in function of the action
        /// </summary>
        /// <param name="action">The action to do</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        private void move(string action, int x, int y)
        {
            if (action == "up")
                _sigmaPanel.Location = new System.Drawing.Point(x, y - _stepsToMove);

            else if (action == "down")
                _sigmaPanel.Location = new System.Drawing.Point(x, y + _stepsToMove);

            else if (action == "left")
                _sigmaPanel.Location = new System.Drawing.Point(x - _stepsToMove, y);

            else if (action == "right")
                _sigmaPanel.Location = new System.Drawing.Point(x + _stepsToMove, y);
        }

        private void setLblPoints(double points)
        {
            _lblPoints.Text = points.ToString();
        }

        private string RandomFromList(List<string> list)
        {
            return list[rnd.Next(list.Count)];
        }

        /// <summary>
        /// Play one move of the ia
        /// </summary>
        private void playOneMove()
        {

            // Get x and y coordinates
            int x = _sigmaPanel.Location.X;
            int y = _sigmaPanel.Location.Y;

            double max = _qTable[(x, y)].Values.Max();
            string action = _qTable[(x, y)].FirstOrDefault(act => act.Value == max).Key;

            List<string> elementsThatHaveMax = _qTable[(x, y)].Where(kv => kv.Value == max)
                .Select(kv => kv.Key)
                .ToList();

            // If more than 1 have the max value so get one random (not to spam up at start)
            if (elementsThatHaveMax.Count > 1)
            {
                action = RandomFromList(elementsThatHaveMax);
            }

            bool canGoUp = action == "up" && y >= _stepsToMove;
            bool canGoDown = action == "down" && y <= _mapSize - _stepsToMove;
            bool canGoLeft = action == "left" && x >= _stepsToMove;
            bool canGoRight = action == "right" && x <= _mapSize - _stepsToMove;

            // Explorate in function of the exploration
            bool explorate = rnd.NextDouble() < _exploration;

            // Update the points then get the best
            UpdatePoints();

            // If he explorates, so play a random else find the best
            if (!explorate && (canGoUp || canGoDown || canGoLeft || canGoRight))
            {
                setLblPoints(max);
                move(action, x, y);
            }
            else
            {
                List<string> possibleMoves = new List<string>();

                if (y <= _mapSize - _stepsToMove)
                {
                    possibleMoves.Add("down");
                }
                if (x >= _stepsToMove)
                {
                    possibleMoves.Add("left");
                }
                if (x <= _mapSize - _stepsToMove)
                {
                    possibleMoves.Add("right");
                }
                if (y >= _stepsToMove)
                {
                    possibleMoves.Add("up");
                }

                string randomAction = possibleMoves[rnd.Next(possibleMoves.Count)];
                move(randomAction, x, y);
            }
        }

        /// <summary>
        /// Simulate an whole game of the IA
        /// </summary>
        async private Task<bool> SimulateGame()
        {
            placeSigma();

            int x = _sigmaPanel.Location.X;
            int y = _sigmaPanel.Location.Y;

            for (int i = 0; i < _nbMoves; i++)
            {
                playOneMove();

                if (checkWin(x, y))
                {
                    Console.WriteLine("Sigma won");
                    placeSigma();
                    return true;
                }
                await Task.Delay(_delay);
            }

            Console.WriteLine("Sigma lost");
            return true;
        }

        /// <summary>
        /// Simulate all the learning of sigma
        /// </summary>
        async public void Simulate()
        {
            for (_episode = 0; _episode < _nbGames; _episode++)
            {
                _exploration = Math.Max(0.1, 1.0 - (double)_episode / _nbGames);
                await SimulateGame();
            }
        }
    }
}