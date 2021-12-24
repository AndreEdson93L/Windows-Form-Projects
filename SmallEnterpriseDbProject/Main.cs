using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallEnterpriseDbProject
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void businessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BizContacts formBizContacts = new BizContacts();
            formBizContacts.MdiParent = ActiveForm; //or use this keyword
            formBizContacts.Show();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}
