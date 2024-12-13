using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iaQTable
{
    public class Sigma
    {
        private int _mapSize;
        private int _nbCases;
        private Dictionary<(int, int), Dictionary<string, double>> _qTable = new Dictionary<(int, int), Dictionary<string, double>>();
        private double _learningRate;
        private double _discount;

        /// <summary>
        /// Constructor of Sigma
        /// </summary>
        /// <param name="mapSize">The size of one side of the map</param>
        /// <param name="learningRate">If the bot will learn much or not (0-1)</param>
        /// <param name="discount">If the bot will use future informations or not (0-1)</param>
        public Sigma(int mapSize, double learningRate, double discount)
        {
            _mapSize = mapSize;
            _learningRate = learningRate;
            _discount = discount;
            _nbCases = (int)Math.Pow(mapSize, 2);
        }

        /// <summary>
        /// Initialize the QTable
        /// </summary>
        public void initQTable()
        {
            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    // Put 0 points foreach action in the coordinate
                    _qTable.Add((x, y), new Dictionary<string, double>{
                        { "up", 0.0 },
                        { "down", 0.0 },
                        { "left", 0.0 },
                        { "right", 0.0 }
                    });
                }
            }
        }
    }
}