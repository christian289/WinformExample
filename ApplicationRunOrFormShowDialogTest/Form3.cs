using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationRunOrFormShowDialogTest
{
    public partial class Form3 : Form
    {
        public Form3(string Text)
        {
            InitializeComponent();
            label1.Text = Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }
    }
}
