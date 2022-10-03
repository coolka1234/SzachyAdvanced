using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PionkiLib;

namespace SzachyAdvanced
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
        }

        private void GameOver_Load(object sender, EventArgs e)
        {
            if(Pionki.result==0)
            {
                labelResult.Text = "Remis.";
            }
            else if(Pionki.result==1)
            {
                labelResult.Text = "Białe wygrywają przez mata.";
            }
            else if(Pionki.result==2)
            {
                labelResult.Text = "Czarne wygrywają przez mata.";
            }
            else if(Pionki.result==3)
            {
                labelResult.Text = "Białe wygrywają przez kapitulację czarnych.";
            }
            else if(Pionki.result==4)
            {
                labelResult.Text = "Czarne wygrywają przez kapitulację białych";
            }
        }
    }
}
