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
    /// <summary>
    /// The main Form and focal point of the application.
    /// </summary>
    public partial class MainWnd : Form
    {
        //Used to 
        private List<ISearchStrategy> searchOptions;

        /// <summary>
        /// Create the main Form for searching.
        /// </summary>
        /// <param name="searchStrats">List of potential destinations for search queries.</param>
        public MainWnd(List<ISearchStrategy> searchStrats)
        {
            searchOptions = searchStrats;

            InitializeComponent();

            //Place window on bottom right of screen
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int wndX = screen.Width - this.Width;
            int wndY = screen.Height - this.Height;
            this.Location = new Point(wndX, wndY);

            var searchNames = searchOptions.Select(x => x.GetName());
            this.searchSrcSelect.DataSource = searchNames.ToList();
        }

        //NOTE: Upon searching, remember to disable the form controls until the search is done.
    }
}
