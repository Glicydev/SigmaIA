﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iaQTable
{
    public partial class Form1 : Form
    {
        Dictionary<(int, int), Dictionary<string, double>> qTable = new Dictionary<(int, int), Dictionary<string, double>>();
        List<Sigma> sigmas = new List<Sigma>();
        int stepsToMove = 20;
        int mapSize = 600;
        int nbMoves = 200;
        int nbGames = 5;
        int delay = 1;
        double learningRate = 0.25;
        int discount = 1;
        int nbSigmas = 20;
        int tableSize = 0;

        public Form1()
        {
            InitializeComponent();

            tableSize = mapSize / stepsToMove;
            fillQTable();

            generateAllSigmas();
            sigmaTimer.Start();
            makeSigmasPlay();
        }

        private void fillQTable()
        {
            // Fill the QTable with 0 values
            for (int x = 0; x <= tableSize; x++)
            {
                for (int y = 0; y <= tableSize; y++)
                {
                    // Put 0 points foreach action in the coordinate
                    qTable.Add((x * stepsToMove, y * stepsToMove), new Dictionary<string, double>{
                        { "up", 0.0 },
                        { "down", 0.0 },
                        { "left", 0.0 },
                        { "right", 0.0 }
                    });
                }
            }
        }

        private bool makeSigmasPlay()
        {
            foreach (Sigma sigma in sigmas)
            {
                sigma.Simulate();
            }
            return true;
        }

        private void generateAllSigmas()
        {
            for(int i = 0; i < nbSigmas; i++)
            {
                sigmas.Add(generateSigma());
            }
        }

        private Sigma generateSigma()
        {
            return new Sigma(mapSize, learningRate, discount, nbMoves, delay, nbGames, target, 80, lblPoints, ref qTable, this, stepsToMove, ref sigmas);
        }

        private void sigmaTimer_Tick(object sender, EventArgs e)
        {
            foreach(Sigma sigma in sigmas)
            {
                sigma.Time++;
            }
        }
    }
}
