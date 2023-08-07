using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace unilab2023
{
    public partial class スタート画面 : Form
    {
        Bitmap bmp1;

        Image img_shizu = Image.FromFile("マップ_氷.png");
        Image img_ikaP = Image.FromFile("マップ_草原.png");

        public スタート画面()
        {
            InitializeComponent();

            // pictureBoxの設定
            bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp1;
        }

        private void スタート画面_Load(object sender, EventArgs e)
        {
            Graphics g1 = Graphics.FromImage(bmp1);
            g1.DrawImage(Image.FromFile("マップ_草原.png"), 0, 0, 190, 960);
            g1.DrawImage(Image.FromFile("マップ_草原.png"), 1350, 0, 190, 960);
            g1.DrawImage(Image.FromFile("わせ忍_アイコン2.png"), 185, 0, 1200, 960);
            g1.DrawImage(Image.FromFile("キャラ_あざらし.png"), 30, 8, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_きつね.png"), 30, 176, 120, 120);
            g1.DrawImage(Image.FromFile("忍者_正面.png"), 30, 344, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_たぬき.png"), 30, 512, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_ふくろう.png"), 30, 670, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_唐傘一反.png"), 1380, 28, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_一つ目小僧.png"), 1380, 176, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_赤鬼.png"), 1380, 344, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_カッパ.png"), 1380, 512, 120, 120);
            g1.DrawImage(Image.FromFile("キャラ_てんぐ.png"), 1380, 670, 120, 120);

            button1.Enabled = true;  //デバッグのたびにパロディを見なきゃいけないのは面倒なので、一時的にfalse→trueに変更
            Global.Conversations = LoadConversation("わせ忍0章.csv");
        }

        public class Global
        {
            public static List<Conversation> Conversations = new List<Conversation>();
        }

        public class Conversation
        {
            public string character = "";
            public string dialogue = "";
            public string img = "";
        }

        /* function */

        private List<Conversation> LoadConversation(string conv_stagename)
        {
            List<Conversation> conversations = new List<Conversation>();
            
            using (StreamReader sr = new StreamReader($"{conv_stagename}"))
            {
                int i = 0;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    if (i == 0)  //escape 1st row
                    {
                        i += 1;
                        continue;
                    }

                    //改行文字のための処理；文字列の中に\nが出てきたら切ってつなげる
                    for (int j = 0; j < values.Length; j++)
                    {
                        string searchWord = "\\n";
                        int foundIndex = values[j].IndexOf(searchWord);
                        List<int> newlineIndex = new List<int>();
                        while (0 <= foundIndex)
                        {
                            //indexを格納
                            newlineIndex.Add(foundIndex);
                            int nextIndex = foundIndex + searchWord.Length;
                            if (nextIndex < values[j].Length)
                            {
                                foundIndex = values[j].IndexOf(searchWord, nextIndex);
                            }
                            else
                            {
                                //最後まで検索したら終了
                                break;                                
                            }
                        }
                        //改行文字が無かったら無視
                        if (newlineIndex.Count > 0)
                        {
                            string originalValue = values[j];
                            values[j] = "";
                            int startIndex = 0;
                            int endIndex = newlineIndex[0];
                            for (int k = 0; k < newlineIndex.Count + 1; k++)
                            {
                                values[j] = values[j] + originalValue.Substring(startIndex, endIndex - startIndex) + "\n";
                                startIndex = endIndex + 2;
                                if (k < newlineIndex.Count - 1)
                                {
                                    endIndex = newlineIndex[k+1];
                                }
                                else
                                {
                                    endIndex = originalValue.Length;
                                }

                            }
                        }
                    }

                    conversations.Add(new Conversation());

                    i -= 1;

                    conversations[i].character = values[0];
                    conversations[i].dialogue = values[1];
                    conversations[i].img = values[2];

                    i += 2;
                }
            }
            return conversations;
        }

        int conversationCounter = 0;  // 脚本のカウンタ

        private void drawConversation()
        {
            //描画準備
            Graphics g1 = Graphics.FromImage(bmp1);

            Pen pen = new Pen(Color.FromArgb(100, 255, 100), 2);
            Font fnt = new Font("游明朝", 20);
            int sp = 5;

            int face = 100;
            int name_x = 300;
            int name_y = 60;

            int dia_x = 1500;
            int dia_y = 200;

            int chousei_x = 420;

            g1.FillRectangle(Brushes.Black, 15, chousei_x + face, name_x, name_y);
            g1.DrawRectangle(pen, 15, chousei_x + face, name_x, name_y);

            g1.FillRectangle(Brushes.Black, 15, chousei_x + face + name_y, dia_x, dia_y);
            g1.DrawRectangle(pen, 15, chousei_x + face + name_y, dia_x, dia_y);

            if (Global.Conversations[conversationCounter].img == "img_shizu")
            {
                //switch文の方がいいかもしれない、あるいはdictionaryなど
                g1.DrawImage(img_shizu, 15, chousei_x, face, face);
            }
            else if (Global.Conversations[conversationCounter].img == "img_ikaP")
            {
                g1.DrawImage(img_ikaP, 15, chousei_x, face, face);
            }
            g1.DrawString(Global.Conversations[conversationCounter].character, fnt, Brushes.White, 15 + sp, chousei_x + face + sp);
            g1.DrawString(Global.Conversations[conversationCounter].dialogue, fnt, Brushes.White, 15 + sp, chousei_x + face + name_y + sp);

            pictureBox1.Image = bmp1;
            g1.Dispose();

            if (conversationCounter < Global.Conversations.Count - 1)
            {
                conversationCounter += 1;
            }
            else
            {
                button1.Enabled = true;
                button2.Text = "次へ";
                return;
            }
        }

        /* function fin */

        /* button */
        bool flag_button = false;

        private void button1_Click(object sender, EventArgs e)
        {
            //ステージ選択画面 form2 = new ステージ選択画面();
            //form2.Show();

            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = true;
            Graphics g1 = Graphics.FromImage(bmp1);
            g1.Clear(BackColor);
            flag_button = true;
            Image img = Image.FromFile("わせ忍_背景_薄暗.png");
            g1.DrawImage(img, 0, 0, 1550, 800);
            drawConversation();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(flag_button == true)
            {
                drawConversation();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ステージ選択画面 form2 = new ステージ選択画面();
            form2.Show();
        }

        /* button fin */
    }
}
