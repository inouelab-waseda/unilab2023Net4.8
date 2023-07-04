using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unilab2023
{
    public partial class ステージ選択画面 : Form
    {
        Form1 form1 = new Form1();

        public ステージ選択画面()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedStageName = comboBox1.SelectedItem.ToString();
            form1.stageName = selectedStageName;
            form1.Show();
        }

        private void ステージ選択画面_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //ステージ1-1ボタン
        {

        }
    }
}
