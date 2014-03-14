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
        #region Constants
        private static readonly char ENTER_KEY = '\r';
        private static readonly Color COLOR_SUCCESS = Color.Green;
        private static readonly Color COLOR_FAIL = Color.Red;
        private static readonly Color COLOR_DEFAULT = Color.Black;
        #endregion

        #region Components
        private ContextMenu trayMenu;
        #endregion

        private bool isContextMenuClose;

        //Used to 
        private List<ISearchStrategy> searchOptions;

        /// <summary>
        /// Create the main Form for searching.
        /// </summary>
        /// <param name="searchStrats">List of potential destinations for search queries.</param>
        public MainWnd(List<ISearchStrategy> searchStrats)
        {
            searchOptions = searchStrats;
            isContextMenuClose = false;

            //Initialization of components; anything auto-generated stays in *.designer.cs
            InitializeComponent();
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Show", MainWnd_TrayMenuShow);
            trayMenu.MenuItems.Add("Exit", MainWnd_TrayMenuClose);
            notifyIcon.ContextMenu = trayMenu;


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

        #region Private Methods
        private Color GetColorFromSearchState(ISearchStrategy strat)
        {
            bool? success = strat.IsLastSearchSuccess();
            if (success.HasValue)
            {
                return success.Value ? COLOR_SUCCESS : COLOR_FAIL;
            }
            else
            {
                return COLOR_DEFAULT;
            }
                
        }

        /// <summary>
        /// Show MainWnd if it's minimized, or activate if it's not the active application
        /// In addition, do any other operations depenent on these conditions
        /// </summary>
        private void ActivateOrShow()
        {
            this.Show();
            this.Activate();
            searchBox.Focus();
        }
        #endregion

        #region Event Handlers
        private void MainWnd_TrayMenuClose(object sender, EventArgs e)
        {
            isContextMenuClose = true;
            this.Close();
        }

        private void MainWnd_TrayMenuShow(object sender, EventArgs e)
        {
            ActivateOrShow();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            //Assume unintentional search if empty search string
            if (searchBox.Text == String.Empty)
                return;

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

                ShowMsg(result, GetColorFromSearchState(strat));
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

        private void MainWnd_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing && !isContextMenuClose)
            {
                this.Hide();
                e.Cancel = true;
            }
            else return;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ActivateOrShow();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
                this.Visible = false;
                this.ShowInTaskbar = false;
        }

        //TODO: Upon searching, remember to disable the form controls until the search is done.
    }

        #endregion
}
