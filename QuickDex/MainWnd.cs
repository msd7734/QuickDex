﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using QuickDex.Properties;
using QuickDex.Search;

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

        //Set to true if close event fired from TrayIcon context menu
        private bool isContextMenuClose;

        //Set to true if close event fired by pressing escape and confirming quit
        private bool isEscapeBtnClose;

        //Used to serve search queries from different sources
        private List<ISearchStrategy> searchOptions;

        //Determine if the form is hidden 
        //Need this because Form.Visible prevents attempts to set it after init
        private bool isVisible;

        /// <summary>
        /// Create the main Form for searching.
        /// </summary>
        /// <param name="searchStrats">List of potential destinations for search queries.</param>
        public MainWnd(List<ISearchStrategy> searchStrats)
        {
            searchOptions = searchStrats;
            isContextMenuClose = false;
            isEscapeBtnClose = false;
            isVisible = false;

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

            //Set dropdowns to user-defined default values
            this.searchSrcSelect.SelectedItem = Settings.Default["DefaultSearchSrc"];
            this.genSelect.SelectedItem = Settings.Default["DefaultGeneration"];
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

        /// <summary>
        /// Show the form using a universal key shortcut.
        /// </summary>
        public void ShortcutFormShow()
        {
            //There is some mysterious timing issue here that makes the form not activate when isVisible = true
            //20ms is about the minimum to get fix it consistently, 10ms only works sometimes (wtf???)
            //Maybe one day I'll find out the real reason for this. Until now... Sleep(20) is the solution.
            //Potential solution: http://stackoverflow.com/questions/853960/
            System.Threading.Thread.Sleep(20);
            ActivateOrShow(true);
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
        /// <param name="forceSearchBoxFocus">Force focus on the search box regardless of this method's internal logic</param>
        /// </summary>
        private void ActivateOrShow(bool forceSearchBoxFocus = false)
        {
            //Forces the BalloonTip to hide if it's still there
            notifyIcon.Visible = false;
            notifyIcon.Visible = true;

            if (isVisible)
            {
                this.Activate();
            }
            else
            {
                this.Show();
                isVisible = true;
                searchBox.Focus();
                return;
            }

            if (forceSearchBoxFocus)
                searchBox.Focus();
        }

        /// <summary>
        /// Change the state of all controls on MainWnd to the values defined
        /// in Settings.
        /// </summary>
        private void ResetControls()
        {
            //Set dropdowns to user-defined default values
            this.searchSrcSelect.SelectedItem = Settings.Default["DefaultSearchSrc"];
            this.genSelect.SelectedItem = Settings.Default["DefaultGeneration"];
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

        //TODO: Upon searching, remember to disable the form controls until the search is done.
        // - Caveat, only bother if search time becomes an issue
        private void searchBtn_Click(object sender, EventArgs e)
        {
            //Assume unintentional search if empty search string
            if (searchBox.Text == string.Empty || searchBox.Text.Trim() == string.Empty)
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

        //To allow this handler to be called, set MainWnd.KeyPreview = true
        private void MainWnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                DialogResult res = MessageBox.Show(this, "Close the QuickDex?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.OK)
                {
                    isEscapeBtnClose = true;
                    this.Close();
                }
            }
        }

        private void MainWnd_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing
                && !isContextMenuClose
                && !isEscapeBtnClose)
            {
                this.Hide();
                isVisible = false;
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
            isVisible = false;
            this.ShowInTaskbar = false;
            //Setting TabStop in designer doesn't work for some reason...
            this.settingsLink.TabStop = false;
            string shortcutStr = Settings.Default["Shortcut"].ToString();
            notifyIcon.ShowBalloonTip(1000, "QuickDex", "QuickDex is now running! Use the " + shortcutStr + " shortcut to perform a quick search.", ToolTipIcon.Info);
        }

        private void settingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingsWnd settings = new SettingsWnd(this.searchOptions);
            DialogResult result = settings.ShowDialog(this);

            if (result == DialogResult.OK)
                ResetControls();
        }

        private void MainWnd_Shown(object sender, EventArgs e)
        {
            //This may potentially fix some timing issues
            this.Activate();
        }
    }

        #endregion
}
