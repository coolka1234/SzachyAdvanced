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
    public partial class PromotionPanel : Form
    {
        public PromotionPanel()
        {
            InitializeComponent();
        }

        private void PromotionPanel_Load(object sender, EventArgs e)
        {
            if(PionkiLib.Pionki.whiteOrBlack==0)
            {
                buttonRook.BackgroundImage = Properties.Resources.WiezaB;
                buttonSkoczek.BackgroundImage = Properties.Resources.KonB;
                buttonGoniec.BackgroundImage = Properties.Resources.GoniecB;
                buttonQrolowa.BackgroundImage = Properties.Resources.KrolowaB;
            }
            else
            {
                buttonRook.BackgroundImage = Properties.Resources.WiezaC;
                buttonSkoczek.BackgroundImage = Properties.Resources.KonC;
                buttonGoniec.BackgroundImage = Properties.Resources.GoniecC;
                buttonQrolowa.BackgroundImage = Properties.Resources.KrolowaC;
            }
        }

        private void buttonRook_Click(object sender, EventArgs e)
        {
            if (Pionki.whiteOrBlack == 0)
            {
                Pionki.promoted = 2;
            }
            else
            {
                Pionki.promoted = 9;
            }
            Close();
        }

        private void buttonSkoczek_Click(object sender, EventArgs e)
        {
            if (Pionki.whiteOrBlack == 0)
            {
                Pionki.promoted = 3;
            }
            else
            {
                Pionki.promoted = 10;
            }
            Close();
        }

        private void buttonGoniec_Click(object sender, EventArgs e)
        {
            if (Pionki.whiteOrBlack == 0)
            {
                Pionki.promoted = 4;
            }
            else
            {
                Pionki.promoted = 11;
            }
            Close();
        }

        private void buttonQrolowa_Click(object sender, EventArgs e)
        {
            if (Pionki.whiteOrBlack == 0)
            {
                Pionki.promoted = 5;
            }
            else
            {
                Pionki.promoted = 12;
            }
            Close();
        }
    }
}
