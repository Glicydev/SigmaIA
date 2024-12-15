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
        public Form1()
        {
            InitializeComponent();
            Sigma sigma = new Sigma(600, 0.25, 1, 100, 1, 500, target, sigmaImg, lblPoints);

            sigma.Simulate();
        }
    }
}
