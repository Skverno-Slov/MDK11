using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lection311215
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void gameBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.gameBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ispp3114DataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "ispp3114DataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.ispp3114DataSet.Category);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "ispp3114DataSet.Game". При необходимости она может быть перемещена или удалена.
            this.gameTableAdapter.Fill(this.ispp3114DataSet.Game);

        }
    }
}
