using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kitap_Satış_Sistemi
{
    public partial class istatistik : Form
    {
        public istatistik()
        {
            InitializeComponent();
        }

        private void istatistik_Load(object sender, EventArgs e)
        {
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }
    }
}
