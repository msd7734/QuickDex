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

        private static readonly Color COLOR_SUCCESS = Color.Green;
        private static readonly Color COLOR_FAIL = Color.Red;
        private static const Color COLOR_DEFAULT = Color.Black;

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

        #region Public Methods
        /// <summary>
        /// Display a message in the RichTextBox message area.
        /// </summary>
        /// <param name="msg">Message to display</param>
        public void ShowMsg(string msg)
        {
            this.msgDisplay.Text = msg;
        }

        /// <summary>
        /// Display a message in the RichTextBox message area.
        /// </summary>
        /// <param name="msg">Message to display</param>
        /// <param name="color">Highlight all of msg with given color</param>
        public void ShowMsg(string msg, Color color)
        {
            this.msgDisplay.Text = msg;
            this.msgDisplay.SelectionStart = 0;
            this.msgDisplay.SelectionLength = msg.Length;
            this.msgDisplay.SelectionColor = color;
        }
        #endregion

        #region Event Handlers
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

                ShowMsg(result, COLOR_SUCCESS);
            }
            catch (NotImplementedException nie)
            {

                ShowMsg(nie.Message, COLOR_FAIL);
            }
        }

        //Turns out this isn't needed; just set the AcceptBtn property to use searchBtn_Click
        //Keeping it around to handle potential future need to consume keypresses
        //To do this, set MainWnd.KeyPreview = true
        private void MainWnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        //TODO: Upon searching, remember to disable the form controls until the search is done.
    }

        #endregion
}
