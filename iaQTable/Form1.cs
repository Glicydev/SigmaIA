using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iaQTable
{
    public partial class Form1 : Form
    {
        Sigma sigma = null;

        public Form1()
        {
            InitializeComponent();
            sigma = new Sigma(600, 0.25, 1, 200, 1, 100, target, sigmaImg, lblPoints);

            sigmaTimer.Start();
            sigma.Simulate();
        }

        private void sigmaTimer_Tick(object sender, EventArgs e)
        {
            sigma.Time++;
        }
    }
}
