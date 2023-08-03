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
        string selectedStageName;

        public ステージ選択画面()
        {
            InitializeComponent();

        }

        private string _stageName;
        public string stageName
        {
            get { return _stageName; }
            set { _stageName = value; }
        }



        private void ステージ選択画面_Load(object sender, EventArgs e)
        {
           
            switch (stageName)
            {
                case "stage1-1":
                    button3.Enabled = true;
                    break;
                case "stage1-2":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    break;
                case "stage1-3":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    break;
                case "stage1-4":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    break;

                case "stage2-1":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    break;
                case "stage2-2":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    break;
                case "stage2-3":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    break;

                case "stage3-1":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    break;
                case "stage3-2":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    break;
                case "stage3-3":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    break;

                case "stage4-1":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    button9.Enabled = true;
                    break;
                case "stage4-2":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    button9.Enabled = true;
                    button8.Enabled = true;
                    break;
                case "stage4-3":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    button9.Enabled = true;
                    button8.Enabled = true;
                    button13.Enabled = true;
                    break;

                case "stage5-1":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    button9.Enabled = true;
                    button8.Enabled = true;
                    button13.Enabled = true;
                    button12.Enabled = true;
                    break;
                case "stage5-2":
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Enabled = true;
                    button7.Enabled = true;
                    button6.Enabled = true;
                    button5.Enabled = true;
                    button16.Enabled = true;
                    button15.Enabled = true;
                    button14.Enabled = true;
                    button10.Enabled = true;
                    button9.Enabled = true;
                    button8.Enabled = true;
                    button13.Enabled = true;
                    button12.Enabled = true;
                    button11.Enabled = true;
                    break;
            }
        }
        
        private void createForm1(string selectedStageName)
        {
            Form1 form1 = new Form1();
            form1.stageName = selectedStageName;
            this.Close();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter(object sender, EventArgs e)//1面たぬきの里
        {
           
        }

        private void button2_Click(object sender, EventArgs e)//stage1-1
        {
            createForm1("stage1-1");

        }

        private void button3_Click(object sender, EventArgs e)//stage1-2
        {
            createForm1("stage1-2");

        }

        private void button4_Click(object sender, EventArgs e)//stage1-3
        {
            createForm1("stage1-3");
        }

        private void button1_Click_1(object sender, EventArgs e)//stage1-4
        {
            createForm1("stage1-4");
        }

        private void groupBox2_Enter(object sender, EventArgs e)//2面きつねの村
        {

        }

        private void button7_Click(object sender, EventArgs e)//stage2-1
        {
            createForm1("stage2-1");
        }

        private void button6_Click(object sender, EventArgs e)//stage2-2
        {
            createForm1("stage2-2");
        }

        private void button5_Click(object sender, EventArgs e)//stage2-3
        {
            createForm1("stage2-3");
        }

        private void groupBox3_Enter(object sender, EventArgs e)//3面あざらしの湖
        {

        }

        private void button16_Click(object sender, EventArgs e)//stage3-1
        {
            createForm1("stage3-1");
        }

        private void button15_Click(object sender, EventArgs e)//stage3-2
        {
            createForm1("stage3-2");
        }

        private void button14_Click(object sender, EventArgs e)//stage3-3
        {
            createForm1("stage3-3");
        }

        private void groupBox4_Enter(object sender, EventArgs e)//4面ふくろうの林
        {

        }

        private void button10_Click(object sender, EventArgs e)//stage4-1
        {
            createForm1("stage4-1");
        }

        private void button9_Click(object sender, EventArgs e)//stage4-2
        {
            createForm1("stage4-2");
        }

        private void button8_Click(object sender, EventArgs e)//stage4-3
        {
            createForm1("stage4-3");
        }

        private void groupBox5_Enter(object sender, EventArgs e)//5面あやかしの山
        {

        }

        private void button13_Click(object sender, EventArgs e)//stage5-1
        {
            createForm1("stage5-1");
        }

        private void button12_Click(object sender, EventArgs e)//stage5-2
        {
            createForm1("stage5-2");
        }

        private void button11_Click(object sender, EventArgs e)//stage5-3
        {
            createForm1("stage5-3");
        }

        private void ステージ選択画面_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                this.Close();
                ステージ選択画面 form2 = new ステージ選択画面();
                form2.stageName = "stage5-2";
                form2.Show();
            }
        }

        private void ステージ選択画面_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
