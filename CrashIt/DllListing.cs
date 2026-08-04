using System.Diagnostics;

namespace CrashIt
{
    public partial class DllListing : Form
    {
        public DllListing()
        {
            InitializeComponent();
        }

        public ProcessModule? SelectedModule { get; set; }

        public void SetModules(List<ProcessModule> modules)
        {
            foreach (var module in modules)
            {
                lbModules.Items.Add(new DllListingItem(module.ModuleName, module));
            }
        }

        private void lbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedModule = (lbModules.SelectedItem as DllListingItem)?.Module;
        }

        private void lbModules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectedModule = (lbModules.SelectedItem as DllListingItem)?.Module;
            DialogResult = DialogResult.OK;
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            SelectedModule = null;
            DialogResult = DialogResult.OK;
        }
    }

    class DllListingItem
    {
        public string Name { get; set; }
        public ProcessModule Module { get; set; }
        public DllListingItem(string name, ProcessModule module)
        {
            Name = name;
            Module = module;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
