using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickDex.Properties;
using QuickDex.Search;

namespace QuickDex
{
    public partial class SettingsWnd : Form
    {
        List<ISearchStrategy> searchOptions;
        public SettingsWnd(List<ISearchStrategy> searchStrats)
        {
            searchOptions = searchStrats;
            InitializeComponent();

            //Populate dropdowns
            var searchNames = searchOptions.Select(x => x.GetName());
            this.defaultSrcSelect.DataSource = searchNames.ToList();
            this.defaultSrcSelect.SelectedItem = (string)Settings.Default["DefaultSearchSrc"];

            var genNames = Enum.GetNames(typeof(PokeGeneration));
            this.defaultGenSelect.DataSource = genNames;
            this.defaultGenSelect.SelectedItem = (string)Settings.Default["DefaultGeneration"];

            var shortcuts = Enum.GetNames(typeof(ShortcutEnum));
            this.shortcutSelect.DataSource = shortcuts;
            this.shortcutSelect.SelectedItem = (string)Settings.Default["Shortcut"];
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(defaultSrcSelect.SelectedItem.ToString() + "\n" + defaultGenSelect.SelectedItem.ToString());
            Settings.Default["DefaultSearchSrc"] = this.defaultSrcSelect.SelectedItem;
            Settings.Default["DefaultGeneration"] = this.defaultGenSelect.SelectedItem;
            Settings.Default["Shortcut"] = this.shortcutSelect.SelectedItem;
            Settings.Default.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
