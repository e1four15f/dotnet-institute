using System;
using System.Windows.Forms;

namespace lab8Lib
{
    public class MyButton : Button
    {
        public int Radius { get; set; }

        public event MouseEventHandler Centered;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Centered(this, e);
        }
    }
}
