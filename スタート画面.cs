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

        Image img_shizu = Image.FromFile("氷.png");
        Image img_ikaP = Image.FromFile("草原.jpg");

        public スタート画面()
        {
            InitializeComponent();

            // pictureBoxの設定
            bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp1;
        }

        private void スタート画面_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;  //デバッグのたびに寒いパロディを見なきゃいけないのは面倒なので、一時的にfalse→trueに変更
            Global.Conversations = LoadConversation("conversation_demo.csv");
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
            Font fnt = new Font("MS UI Gothic", 20);
            int sp = 5;

            int face = 100;
            int name_x = 150;
            int name_y = 40;

            int dia_x = 600;
            int dia_y = 150;

            //Conversationsの内容と画像パスを紐づける表を辞書型として作っておく

            //描画
            g1.FillRectangle(Brushes.Black, 0, face, name_x, name_y);
            g1.DrawRectangle(pen, 0, face, name_x, name_y);

            g1.FillRectangle(Brushes.Black, 0, face + name_y, dia_x, dia_y);
            g1.DrawRectangle(pen, 0, face + name_y, dia_x, dia_y);

            if (Global.Conversations[conversationCounter].character == "しずちゃん")
            {
                g1.DrawImage(img_shizu, 0, 0, face, face);
            }
            if (Global.Conversations[conversationCounter].character == "イカピー")
            {
                g1.DrawImage(img_ikaP, 0, 0, face, face);
            }
            g1.DrawString(Global.Conversations[conversationCounter].character, fnt, Brushes.White, 0 + sp, face + sp);
            g1.DrawString(Global.Conversations[conversationCounter].dialogue, fnt, Brushes.White, 0 + sp, face + name_y + sp);

            pictureBox1.Image = bmp1;
            g1.Dispose();

            if (conversationCounter < Global.Conversations.Count - 1)
            {
                conversationCounter += 1;
            }
            else
            {
                button1.Enabled = true;
                return;
            }
        }

        /* function fin */

        /* button */

        private void button1_Click(object sender, EventArgs e)
        {
            ステージ選択画面 form2 = new ステージ選択画面();
            form2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            drawConversation();
        }

        /* button fin */

        //conv[0].name = I;
        //conv[0].dialogue = "メッセージウィンドウ!\n" +
        //    "ステージをクリアするごとに物語を展開することで\n" +
        //    "ゲームにストーリ性を持たせることができるっピ！";

        //conv[1].name = S;
        //conv[1].dialogue = "ストーリーは誰が書くの？\n" +
        //    "こんな寒いパロディネタでいいの？\n" +
        //    "諸原はこのレベルの脚本しか作れないよ?";

        //conv[2].name = I;
        //conv[2].dialogue = "しずちゃ（イカピーを踏む音）\n";

        //conv[3].name = S;
        //conv[3].dialogue = "ウィンドのデザインは誰がやるの？\n" +
        //    "こんなクソダサデザインで許してくれるの？\n";

        //conv[4].name = I;
        //conv[4].dialogue = "しずちゃ\n" +
        //    "いた（さらに強くイカピーを踏む音）\n";

        //conv[5].name = S;
        //conv[5].dialogue = "会話システムをゲームに実装するのは誰がやるの？\n" +
        //    "言い出しっぺの諸原がやればいいの？\n";

        //conv[6].name = S;
        //conv[6].dialogue = "いったいどうすればいいって\n" +
        //    "お前に言ってんだよ!!\n";

        //conv[7].name = I;
        //conv[7].dialogue = "……\n";

        //conv[8].name = I;
        //conv[8].dialogue = "……\n";

        //conv[9].name = I;
        //conv[9].dialogue = "わかんないっピ…\n";

        //conv[10].name = -1;
        //conv[10].dialogue = "デモ終了\n";
    }
}
