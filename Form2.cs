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
            // initialize
            button3.Visible = false;
            button4.Visible = false;
            button1.Visible = false;
            button7.Visible = false;
            button6.Visible = false;
            button5.Visible = false;
            button16.Visible = false;
            button15.Visible = false;
            button14.Visible = false;
            button10.Visible = false;
            button9.Visible = false;
            button8.Visible = false;
            button13.Visible = false;
            button12.Visible = false;
            button11.Visible = false;

            // stage locked
            switch (stageName)
            {
                case "stage1-1":
                    button3.Visible = true;
                    break;
                case "stage1-2":
                    button3.Visible = true;
                    button4.Visible = true;
                    break;
                case "stage1-3":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    break;
                case "stage1-4":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    break;

                case "stage2-1":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    break;
                case "stage2-2":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    break;
                case "stage2-3":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    break;

                case "stage3-1":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    break;
                case "stage3-2":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    break;
                case "stage3-3":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    break;

                case "stage4-1":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    button9.Visible = true;
                    break;
                case "stage4-2":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    button9.Visible = true;
                    button8.Visible = true;
                    break;
                case "stage4-3":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    button9.Visible = true;
                    button8.Visible = true;
                    button13.Visible = true;
                    break;

                case "stage5-1":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    button9.Visible = true;
                    button8.Visible = true;
                    button13.Visible = true;
                    button12.Visible = true;
                    break;
                case "stage5-2":
                    button3.Visible = true;
                    button4.Visible = true;
                    button1.Visible = true;
                    button7.Visible = true;
                    button6.Visible = true;
                    button5.Visible = true;
                    button16.Visible = true;
                    button15.Visible = true;
                    button14.Visible = true;
                    button10.Visible = true;
                    button9.Visible = true;
                    button8.Visible = true;
                    button13.Visible = true;
                    button12.Visible = true;
                    button11.Visible = true;
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

        private void ステージ選択画面_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void ステージ選択画面_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                this.Close();
                ステージ選択画面 form2 = new ステージ選択画面();
                form2.stageName = "stage5-2";
                form2.Show();
            }
        }
    }
}
