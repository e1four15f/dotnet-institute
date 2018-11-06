using System;
using System.Windows.Forms;
using System.Drawing;

namespace lab8Lib
{
    public class MyButton : Button
    {
        public int Radius { get; set; }

        public event MouseEventHandler Centered;
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Math.Sqrt(Math.Pow((e.X - Width / 2), 2) + Math.Pow((e.Y - Height / 2), 2)) <= Radius)
            {
                BackColor = Color.Cyan;
                Centered(this, e);
            }
        }
    }
}
