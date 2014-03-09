using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace QuickDex
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();

            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int wndX = screen.Width - this.Width;
            int wndY = screen.Height - this.Height;
            this.Location = new Point(wndX, wndY);
        }
    }
}
