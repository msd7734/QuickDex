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
        private static readonly char ENTER_KEY = '\r';

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

            //Populate option dropdowns
            var searchNames = searchOptions.Select(x => x.GetName());
            this.searchSrcSelect.DataSource = searchNames.ToList();

            var genNames = Enum.GetNames(typeof(PokeGeneration));
            this.genSelect.DataSource = genNames;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            int index = this.searchSrcSelect.SelectedIndex;
            ISearchStrategy strat = searchOptions[index];

            try
            {
                int dexNum = -1;
                string result;
                PokeGeneration gen = PokeGeneration.XY;
                Enum.TryParse<PokeGeneration>(this.genSelect.Text, out gen);

                if (int.TryParse(this.searchBox.Text, out dexNum))
                {
                    result = strat.GotoPokemonEntry(dexNum, gen);
                }
                else
                {
                    result = strat.GotoPokemonEntry(this.searchBox.Text, gen);
                }

                //TODO: Method to handle writing a message to display (/w color options)
                this.msgDisplay.Text = result;
                this.msgDisplay.SelectionStart = 0;
                this.msgDisplay.SelectionLength = result.Length;
                this.msgDisplay.SelectionColor = Color.Green;
            }
            catch (NotImplementedException nie)
            {
                
                this.msgDisplay.Text = nie.Message;
                this.msgDisplay.SelectionStart = 0;
                this.msgDisplay.SelectionLength = nie.Message.Length;
                this.msgDisplay.SelectionColor = Color.Red;
            }
        }

        //Turns out this isn't needed; just set the AcceptBtn property to use searchBtn_Click
        //Keeping it around to handle potential future need to consume keypresses
        private void MainWnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //To get this to work, had to set MainWnd.KeyPreview = true
            if (e.KeyChar == ENTER_KEY)
            {
                searchBtn_Click(sender, e);
            }
        }

        //TODO: Upon searching, remember to disable the form controls until the search is done.
    }
}
