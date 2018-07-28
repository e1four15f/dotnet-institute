using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using lab8Lib;

namespace lab8
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void myButton_Centered(object sender, MouseEventArgs e)
        {
            MyButton button = (MyButton)sender;
            if (Math.Sqrt(Math.Pow((e.X - button.Width / 2), 2) + Math.Pow((e.Y - button.Height / 2), 2)) <= button.Radius)
            {
                button.BackColor = Color.Cyan;
                MessageBox.Show("Centered!");
            }
        }
    }
}
