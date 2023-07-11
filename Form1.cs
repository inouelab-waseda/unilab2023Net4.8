﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;


namespace unilab2023
{
    public partial class Form1 : Form
    {
        Bitmap bmp1, bmp2, bmp3;

        Brush goalBackgroundColor = new SolidBrush(Color.Yellow);
        Brush startBackgroundColor = new SolidBrush(Color.Blue);

        Image img_tanuki = Image.FromFile("キャラ_たぬき.png");
        Image img_kitune = Image.FromFile("キャラ_きつね.png");
        Image img_azarasi = Image.FromFile("キャラ_あざらし.png");
        Image img_hukurou = Image.FromFile("キャラ_ふくろう.png");

        Image character_me = Image.FromFile("忍者_正面.png");
        Image character_enemy1 = Image.FromFile("キャラ_一つ目小僧.png");
        Image character_enemy2 = Image.FromFile("キャラ_唐傘一反.png");
        Image character_enemy3 = Image.FromFile("キャラ_カッパ.png");
        Image character_enemy4 = Image.FromFile("キャラ_てんぐ.png");
        Image character_enemy5 = Image.FromFile("キャラ_赤鬼.png");
        //Image character_enemy6 = Image.FromFile("キャラ_ヤマタノオロチ.png");

        Image img_way = Image.FromFile("マップ_草原.png");
        Image img_noway = Image.FromFile("マップ_岩場.png");
        Image img_ice = Image.FromFile("マップ_氷.png");
        Image img_tree = Image.FromFile("マップ_木.png");
        Image img_jump = Image.FromFile("マップ_ジャンプ1.png");
        Image animatedImage_up = Image.FromFile("マップ_動く床_上.gif");
        Image animatedImage_right = Image.FromFile("マップ_動く床_右.gif");
        Image animatedImage_down = Image.FromFile("マップ_動く床_下.gif");
        Image animatedImage_left = Image.FromFile("マップ_動く床_左.gif");


        //MemoryStream stream = new MemoryStream();
        //byte[] bytes = File.ReadAllBytes("右.gif");
        //stream.Write(bytes, 0, bytes.Length);
        //Bitmap animatedImage_right = new Bitmap(stream);

        //アニメ開始
        //ImageAnimator.Animate(animatedImage_right, Image_FrameChanged);
        //DoubleBuffered = true;


        //ステージ名の受け渡し
        private string _stageName;
        public string stageName
        {
            get { return _stageName; }
            set { _stageName = value; }
        }

        public Form1()
        {
            InitializeComponent();

            //pictureBoxの設定
            pictureBox2.Parent = pictureBox1;
            pictureBox1.Location = new Point(600, 50);
            pictureBox2.Location = new Point(0, 0);
            pictureBox1.ClientSize = new Size(600, 600);
            pictureBox2.ClientSize = new Size(600, 600);
            pictureBox2.BackColor = Color.Transparent;

            bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            bmp3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            pictureBox1.Image = bmp1;
            pictureBox2.Image = bmp2;
            pictureBox3.Image = bmp3;

            this.Load += Form1_Load;
        }

        public class Global //グローバル変数格納
        {
            public static int[,] map = new int[10, 10]; //map情報
            public static int x_start; //スタート位置ｘ
            public static int y_start; //スタート位置ｙ
            public static int x_goal; //ゴール位置ｘ
            public static int y_goal; //ゴール位置ｙ
            public static int x_now; //現在位置ｘ
            public static int y_now; //現在位置 y
           
            public static int count = 0; //試行回数カウント
            public static int miss_count = 0; //ミスカウント

            public static List<int[]> move;  //プレイヤーの移動指示を入れるリスト

            //listBoxに入れられる行数の制限
            public static int limit_LB1;
            public static int limit_LB3;
            public static int limit_LB4;

            public static List<Conversation> Conversations = new List<Conversation>();  //会話文を入れるリスト
        }

        public class Conversation
        {
            public string character = "";
            public string dialogue = "";
            public string img = "";
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Global.map = CreateStage(stageName); //ステージ作成
            Global.Conversations = LoadConversation("conversation_demo.csv"); //会話読み込み

            // 1行文の高さ
            int element_height = listBox1.ItemHeight;

            // それぞれの枠の高さ
            int height_LB1 = 0;
            int height_LB3 = 0;
            int height_LB4 = 0;

            using (StreamReader sr = new StreamReader($"stage_frame.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');


                    if (values[0] == _stageName)
                    {
                        Global.limit_LB1 = int.Parse(values[1]);
                        Global.limit_LB3 = int.Parse(values[2]);
                        Global.limit_LB4 = int.Parse(values[3]);
                        break;
                    }
                }
            }

            height_LB1 = Global.limit_LB1 + 1;
            height_LB3 = Global.limit_LB3 + 1;
            height_LB4 = Global.limit_LB4 + 1;

            if (height_LB1 == 1)
            {
                listBox1.Visible = false;
                textBox1.Visible = false;
                listBox5.Items.Remove("A");
                button2.Visible = false;
                button2.Enabled = false;

            }

            if (height_LB3 == 1)
            {
                listBox3.Visible = false;
                textBox2.Visible = false;
                listBox5.Items.Remove("B");
                button3.Visible = false;
                button3.Enabled = false;
            }

            if(height_LB1 == 1 && height_LB3 == 1)
            {
                listBox5.Visible = false;
                listBox2.Location = new Point(listBox4.Location.X, listBox4.Location.Y + 300);
            }


            listBox1.Height = element_height * height_LB1;
            listBox3.Height = element_height * height_LB3;
            listBox4.Height = element_height * height_LB4;

            //ListBox1のイベントハンドラを追加
            listBox1.SelectionMode = SelectionMode.One;
            listBox1.DragEnter += new DragEventHandler(ListBox_DragEnter);
            listBox1.DragDrop += new DragEventHandler(ListBox_DragDrop);
            listBox2.MouseDown += new MouseEventHandler(ListBox_MouseDown);
            listBox3.SelectionMode = SelectionMode.One;
            listBox3.DragEnter += new DragEventHandler(ListBox_DragEnter);
            listBox3.DragDrop += new DragEventHandler(ListBox_DragDrop);
            listBox4.SelectionMode = SelectionMode.One;
            listBox4.DragEnter += new DragEventHandler(ListBox_DragEnter);
            listBox4.DragDrop += new DragEventHandler(ListBox_DragDrop);
            listBox5.MouseDown += new MouseEventHandler(ListBox_MouseDown);

            //ヒントを教えるキャラのアイコンを表示
            Graphics g3 = Graphics.FromImage(bmp3);
            g3.DrawImage(img_tanuki, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            g3.DrawRectangle(Pens.Black, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            g3.Dispose();
        }

        /****button****/
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button1.Enabled = false;
            Global.move = Movement(); //ユーザーの入力を読み取る
            label6.Visible = false;
            SquareMovement(Global.x_now, Global.y_now, Global.map, Global.move); //キャラ動かす
            label3.Text = Global.count.ToString(); //試行回数の表示

            if (Global.x_goal == Global.x_now && Global.y_goal == Global.y_now)
            {
                label6.Text = "クリア！！";
                label6.Visible = true;

                //button1.Visible = false;
                //button1.Enabled = false;
                //button4.Enabled = true;
                //button5.Enabled = true;
                //button4.Visible = true;
                //button5.Visible = true;


            }
        }

        private void button2_Click(object sender, EventArgs e)//リセットボタン、選択したものだけを消す。選択なければすべて消す。
        {
            if (listBox1.SelectedIndex > -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            else
            {
                listBox1.Items.Clear();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            }
            else
            {
                listBox3.Items.Clear();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex > -1)
            {
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            }
            else
            {
                listBox4.Items.Clear();
            }
        }

        /*
        A, Bボタン削除
        private void button4_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            SquareMovement(Global.x_now, Global.y_now, Global.map, Global.move.Item1); //キャラ動かす
            label3.Text = Global.count.ToString(); //試行回数の表示

            if(Global.x_goal == Global.x_now && Global.y_goal == Global.y_now)
            {
                label6.Text = "成功！！";
                label6.Visible = true;
                button4.Visible = false;
                button4.Enabled = false;
                button5.Visible = false;
                button5.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            var move = Global.move.Item2;
            SquareMovement(Global.x_now, Global.y_now, Global.map, move); //キャラ動かす
            label3.Text = Global.count.ToString(); //試行回数の表示

            if (Global.x_goal == Global.x_now && Global.y_goal == Global.y_now)
            {
                label6.Text = "クリア！！";
                label6.Visible = true;
                button4.Visible = false;
                button4.Enabled = false;
                button5.Visible = false;
                button5.Enabled = false;
            }
        }
        */

        private void button6_Click(object sender, EventArgs e)//出発ボタン
        {
            //初期位置に戻す
            Global.x_now = Global.x_start; 
            Global.y_now = Global.y_start;

            character_me=Image.FromFile("忍者_正面.png");

            //初期位置に書き換え
            Graphics g2 = Graphics.FromImage(bmp2);
            g2.Clear(Color.Transparent);
            int cell_length = pictureBox1.Width / 10;
            g2.DrawImage(character_me, Global.x_now * cell_length, Global.y_now * cell_length, cell_length, cell_length);
            g2.DrawImage(character_enemy2, Global.x_goal * cell_length, Global.y_goal * cell_length, cell_length, cell_length);
            this.Invoke((MethodInvoker)delegate
            {
                // pictureBox2を同期的にRefreshする
                pictureBox2.Refresh();
            });

            //初期設定に戻す
            button1.Visible = true;
            button1.Enabled = true;
            label6.Visible = false;
            Global.count = 0;
        }

        /******button fin******/


        /*******関数******/
        //ListBox要素操作
        bool isEnableDrop = true;
        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            //マウスの左ボタンだけが押されている時のみドラッグできるようにする
            if (e.Button == MouseButtons.Left)
            {
                //ドラッグの準備
                ListBox lbx = (ListBox)sender;
                //ドラッグするアイテムのインデックスを取得する
                int itemIndex = lbx.IndexFromPoint(e.X, e.Y);
                if (itemIndex < 0) return;
                //ドラッグするアイテムの内容を取得する
                string itemText = (string)lbx.Items[itemIndex];

                //ドラッグ&ドロップ処理を開始する
                DragDropEffects dde =
                    lbx.DoDragDrop(itemText, DragDropEffects.All);

                ////ドロップ効果がMoveの時はもとのアイテムを削除する
                //if (dde == DragDropEffects.Move)
                //    lbx.Items.RemoveAt(itemIndex);

                isEnableDrop = true;
            }
        }
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            //ドラッグされているデータがstring型か調べ、
            //そうであればドロップ効果をMoveにする
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Move;
            else
                //string型でなければ受け入れない
                e.Effect = DragDropEffects.None;
        }

        //listbox内の行数を制限しない場合
        //private void ListBox_DragDrop(object sender, DragEventArgs e)
        //{
        //    //ドロップされたデータがstring型か調べる
        //    if (e.Data.GetDataPresent(typeof(string)) && isEnableDrop)
        //    {
        //        ListBox target = (ListBox)sender;
        //        //ドロップされたデータ(string型)を取得
        //        string itemText =
        //            (string)e.Data.GetData(typeof(string));
        //        //ドロップされたデータをリストボックスに追加する
        //        target.Items.Add(itemText);

        //        isEnableDrop = false;
        //    }
        //}


        //listboxの行数を制限する場合
        private void ListBox_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがstring型か調べる
            if (e.Data.GetDataPresent(typeof(string)) && isEnableDrop)
            {
                ListBox target = (ListBox)sender;

                //listBoxの名前によって制限数を設定
                int limit = 0;
                switch (target.Name)
                {
                    case "listBox1":
                        limit = Global.limit_LB1;
                        break;
                    case "listBox3":
                        limit = Global.limit_LB3;
                        break;
                    case "listBox4":
                        limit = Global.limit_LB4;
                        break;
                    //default:
                    //    throw new Exception("Unsupported ListBox name.");
                }

                // ドロップによってアイテム数が制限数を超える場合はドロップを拒否
                if (target.Items.Count >= limit)
                {
                    MessageBox.Show($"{target.Name} can only contain up to {limit} items.");
                    return;
                }

                //ドロップされたデータ(string型)を取得
                string itemText = (string)e.Data.GetData(typeof(string));

                //ドロップされたデータをリストボックスに追加する
                target.Items.Add(itemText);

                isEnableDrop = false;
            }
        }

        //これなに
        void Image_FrameChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            //フレームを進める
            ImageAnimator.UpdateFrames(animatedImage);
            //画像の表示
            e.Graphics.DrawImage(animatedImage, 0, 0);

            base.OnPaint(e);
        }
        */

        //ステージ描写
        private int[,] CreateStage(string stage_name)
        {
            int[,] map = new int[10, 10];

            using (StreamReader sr = new StreamReader($"{stage_name}.csv"))
            {
                int x;
                int y = 0;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    x = 0;

                    foreach (var value in values)
                    {
                        // enum 使ったほうが分かりやすそう
                        map[y, x] = int.Parse(value);
                        x++;
                    }
                    y++;
                }
            }

            Graphics g1 = Graphics.FromImage(bmp1);
            Graphics g2 = Graphics.FromImage(bmp2);

            int cell_length = pictureBox1.Width / 10;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    g1.DrawImage(img_way, x * cell_length, y * cell_length, cell_length, cell_length);

                    switch (map[y, x])
                    {
                        case 0:
                            g1.DrawImage(img_noway, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 1:
                            g1.DrawImage(img_way, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 2:
                            g1.DrawImage(img_ice, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 3:
                            g1.DrawImage(img_jump, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 4:
                            ImageAnimator.UpdateFrames(animatedImage_up);
                            g1.DrawImage(animatedImage_up, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 5:
                            ImageAnimator.UpdateFrames(animatedImage_right);
                            g1.DrawImage(animatedImage_right, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 6:
                            ImageAnimator.UpdateFrames(animatedImage_down);
                            g1.DrawImage(animatedImage_down, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 7:
                            ImageAnimator.UpdateFrames(animatedImage_left);
                            g1.DrawImage(animatedImage_left, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 8:
                            g1.DrawImage(img_tree, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 100:
                            g1.FillRectangle(startBackgroundColor, x * cell_length, y * cell_length, cell_length, cell_length);
                            Global.x_start = x;
                            Global.y_start = y;
                            Global.x_now = x;
                            Global.y_now = y;
                            g2.DrawImage(character_me, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 101:
                            g1.FillRectangle(goalBackgroundColor, x * cell_length, y * cell_length, cell_length, cell_length);
                            //ステージごとにゴールのキャラを変えたい
                            g2.DrawImage(character_enemy2, x * cell_length, y * cell_length, cell_length, cell_length);
                            Global.x_goal = x;
                            Global.y_goal = y;
                            break;
                    }
                }
            }

            return map;
        }

        //ユーザーの入力を変換
        public List<int[]> Movement()
        {
            var move_a = new List<int[]>();
            var move_b = new List<int[]>();
            string[] get_move_a = this.listBox1.Items.Cast<string>().ToArray();
            string[] get_move_b = this.listBox3.Items.Cast<string>().ToArray();
            get_move_a = exchange_move(get_move_a, get_move_a.Length);
            get_move_b = exchange_move(get_move_b, get_move_b.Length);
            var get_move_a_list = new List<string>();
            var get_move_b_list = new List<string>();

            get_move_a_list.AddRange(get_move_a);
            get_move_b_list.AddRange(get_move_b);

            int loop_count = 0;
            while (get_move_a_list.Count <= 30 || get_move_b_list.Count <= 30)
            {
                var get_move_a_list_copy = new List<string>(get_move_a_list);
                var get_move_b_list_copy = new List<string>(get_move_b_list);
                get_move_a_list.Clear();
                get_move_b_list.Clear();

                for (int i=0; i<get_move_a_list_copy.Count; i++)
                {
                    
                    if (get_move_a_list_copy[i] == "B")
                    {
                        get_move_a_list.AddRange(get_move_b);

                    }else if (get_move_a_list_copy[i] == "A")
                    {
                        get_move_a_list.AddRange(get_move_a_list_copy);

                    }else
                    {
                        get_move_a_list.Add(get_move_a_list_copy[i]);
                    }
                }

                for (int i = 0; i < get_move_b_list_copy.Count; i++)
                {

                    if (get_move_b_list_copy[i] == "B")
                    {
                        get_move_b_list.AddRange(get_move_b);

                    }
                    else if (get_move_b_list_copy[i] == "A")
                    {
                        get_move_b_list.AddRange(get_move_a_list_copy);

                    }
                    else
                    {
                        get_move_b_list.Add(get_move_b_list_copy[i]);
                    }
                }
                loop_count++;

                if(loop_count > 5)
                {
                    break;
                }
            }

            
            if (get_move_a.Length != 0)
            {
                //string[] get_move_a = this.listBox1.Items.Cast<string>().ToArray();

                for (int i = 0; i < get_move_a_list.Count; i++)
                {
                    if (get_move_a_list[i].StartsWith("for"))
                    {
                        int start = i + 1;
                        int trial = int.Parse(Regex.Replace(get_move_a_list[i], @"[^0-9]", ""));

                        int goal = 0; //後で設定

                        for (int j = 0; j < trial; j++)
                        {

                            int k = start;

                            do
                            {
                                if (get_move_a_list[k].StartsWith("for")) //二重ループ
                                {
                                    int trial2 = int.Parse(Regex.Replace(get_move_a_list[k], @"[^0-9]", ""));
                                    for (int l = 0; l < trial2; l++)
                                    {
                                        k = start + 1;
                                        do
                                        {
                                            if (get_move_a_list[k] == "endfor")
                                            {
                                                break;
                                            }

                                            else if (get_move_a_list[k] == "up")
                                            {
                                                move_a.Add(new int[2] { 0, -1 });
                                            }
                                            else if (get_move_a_list[k] == "down")
                                            {
                                                move_a.Add(new int[2] { 0, 1 });
                                            }
                                            else if (get_move_a_list[k] == "right")
                                            {
                                                move_a.Add(new int[2] { 1, 0 });
                                            }
                                            else if (get_move_a_list[k] == "left")
                                            {
                                                move_a.Add(new int[2] { -1, 0 });
                                            }
                                            k++;
                                        } while (true);
                                    }
                                }
                                else if (get_move_a_list[k] == "endfor")
                                {
                                    goal = k;
                                    break;
                                }
                                else if (get_move_a_list[k] == "up")
                                {
                                    move_a.Add(new int[2] { 0, -1 });
                                }
                                else if (get_move_a_list[k] == "down")
                                {
                                    move_a.Add(new int[2] { 0, 1 });
                                }
                                else if (get_move_a_list[k] == "right")
                                {
                                    move_a.Add(new int[2] { 1, 0 });
                                }
                                else if (get_move_a_list[k] == "left")
                                {
                                    move_a.Add(new int[2] { -1, 0 });
                                }
                                k++;
                            } while (true);
                        }
                        i = goal;
                    }
                    else
                    {
                        if (get_move_a_list[i] == "up")
                        {
                            move_a.Add(new int[2] { 0, -1 });
                        }
                        else if (get_move_a_list[i] == "down")
                        {
                            move_a.Add(new int[2] { 0, 1 });
                        }
                        else if (get_move_a_list[i] == "right")
                        {
                            move_a.Add(new int[2] { 1, 0 });
                        }
                        else if (get_move_a_list[i] == "left")
                        {
                            move_a.Add(new int[2] { -1, 0 });
                        }
                    }
                }
            }

            if (get_move_b.Length != 0)
            {
                //string[] get_move_b = this.listBox3.Items.Cast<string>().ToArray();

                for (int i = 0; i < get_move_b_list.Count; i++)
                {
                    if (get_move_b_list[i].StartsWith("for"))
                    {
                        int start = i + 1;
                        int trial = int.Parse(Regex.Replace(get_move_b_list[i], @"[^0-9]", ""));

                        int goal = 0; //後で設定

                        for (int j = 0; j < trial; j++)
                        {
                            int k = start;
                            do
                            {
                                if (get_move_b_list[k].StartsWith("for")) //二重ループ
                                {
                                    int trial2 = int.Parse(Regex.Replace(get_move_b_list[k], @"[^0-9]", ""));
                                    for (int l = 0; l < trial2; l++)
                                    {
                                        k = start + 1;
                                        do
                                        {
                                            if (get_move_b_list[k] == "endfor")
                                            {
                                                break;
                                            }

                                            else if (get_move_b_list[k] == "up")
                                            {
                                                move_b.Add(new int[2] { 0, -1 });
                                            }
                                            else if (get_move_b_list[k] == "down")
                                            {
                                                move_b.Add(new int[2] { 0, 1 });
                                            }
                                            else if (get_move_b_list[k] == "right")
                                            {
                                                move_b.Add(new int[2] { 1, 0 });
                                            }
                                            else if (get_move_b_list[k] == "left")
                                            {
                                                move_b.Add(new int[2] { -1, 0 });
                                            }
                                            k++;
                                        } while (true);
                                    }
                                }
                                else if (get_move_b_list[k] == "endfor")
                                {
                                    goal = k;
                                    break;
                                }
                                else if (get_move_b_list[k] == "up")
                                {
                                    move_b.Add(new int[2] { 0, -1 });
                                }
                                else if (get_move_b_list[k] == "down")
                                {
                                    move_b.Add(new int[2] { 0, 1 });
                                }
                                else if (get_move_b_list[k] == "right")
                                {
                                    move_b.Add(new int[2] { 1, 0 });
                                }
                                else if (get_move_b_list[k] == "left")
                                {
                                    move_b.Add(new int[2] { -1, 0 });
                                }
                                k++;
                            } while (true);
                        }
                        i = goal;
                    }
                    else
                    {
                        if (get_move_b_list[i] == "up")
                        {
                            move_b.Add(new int[2] { 0, -1 });
                        }
                        else if (get_move_b_list[i] == "down")
                        {
                            move_b.Add(new int[2] { 0, 1 });
                        }
                        else if (get_move_b_list[i] == "right")
                        {
                            move_b.Add(new int[2] { 1, 0 });
                        }
                        else if (get_move_b_list[i] == "left")
                        {
                            move_b.Add(new int[2] { -1, 0 });
                        }
                    }
                }

            }

            string[] get_move_main = this.listBox4.Items.Cast<string>().ToArray();
            get_move_main = exchange_move(get_move_main, get_move_main.Length);
            var move = new List<int[]>();

            if (get_move_main.Length != 0)
            {
                for (int i = 0; i < get_move_main.Length; i++)
                {

                    if (get_move_main[i].StartsWith("for"))
                    {
                        int start = i + 1;
                        int trial = int.Parse(Regex.Replace(get_move_main[i], @"[^0-9]", ""));

                        int goal = 0; //後で設定

                        for (int j = 0; j < trial; j++)
                        {
                            int k = start;
                            do
                            {
                                if (get_move_main[k].StartsWith("for")) //二重ループ
                                {
                                    int trial2 = int.Parse(Regex.Replace(get_move_main[k], @"[^0-9]", ""));
                                    for (int l = 0; l < trial2; l++)
                                    {
                                        k = start + 1;
                                        do
                                        {
                                            if (get_move_main[k] == "endfor")
                                            {
                                                break;
                                            }

                                            else if (get_move_main[k] == "up")
                                            {
                                                move.Add(new int[2] { 0, -1 });
                                            }
                                            else if (get_move_main[k] == "down")
                                            {
                                                move.Add(new int[2] { 0, 1 });
                                            }
                                            else if (get_move_main[k] == "right")
                                            {
                                                move.Add(new int[2] { 1, 0 });
                                            }
                                            else if (get_move_main[k] == "left")
                                            {
                                                move.Add(new int[2] { -1, 0 });
                                            }
                                            else if (get_move_main[k] == "A")
                                            {
                                                move.AddRange(move_a);
                                            }
                                            else if (get_move_main[k] == "B")
                                            {
                                                move.AddRange(move_b);
                                            }
                                            k++;
                                        } while (true);
                                    }
                                }
                                else if (get_move_main[k] == "endfor")
                                {
                                    goal = k;
                                    break;
                                }
                                else if (get_move_main[k] == "up")
                                {
                                    move.Add(new int[2] { 0, -1 });
                                }
                                else if (get_move_main[k] == "down")
                                {
                                    move.Add(new int[2] { 0, 1 });
                                }
                                else if (get_move_main[k] == "right")
                                {
                                    move.Add(new int[2] { 1, 0 });
                                }
                                else if (get_move_main[k] == "left")
                                {
                                    move.Add(new int[2] { -1, 0 });
                                }
                                else if (get_move_main[k] == "A")
                                {
                                    move.AddRange(move_a);
                                }
                                else if (get_move_main[k] == "B")
                                {
                                    move.AddRange(move_b);
                                }
                                k++;
                            } while (true);
                        }
                        i = goal;
                    }
                    else
                    {
                        if (get_move_main[i] == "up")
                        {
                            move.Add(new int[2] { 0, -1 });
                        }
                        else if (get_move_main[i] == "down")
                        {
                            move.Add(new int[2] { 0, 1 });
                        }
                        else if (get_move_main[i] == "right")
                        {
                            move.Add(new int[2] { 1, 0 });
                        }
                        else if (get_move_main[i] == "left")
                        {
                            move.Add(new int[2] { -1, 0 });
                        }
                        else if (get_move_main[i] == "A")
                        {
                            move.AddRange(move_a);
                        }
                        else if (get_move_main[i] == "B")
                        {
                            move.AddRange(move_b);
                        }
                    }
                }

            }

            return move;
        }

        //矢印変換の関数
        public string[] exchange_move(string[] get_move, int l)
        {
            List<string> newget_move = get_move.ToList();
            for (int i = 0; i < l; i++)
            {
                if (newget_move[i] == "↑")
                {
                    newget_move[i] = "up";
                }
                if (newget_move[i] == "→")
                {
                    newget_move[i] = "right";
                }
                if (newget_move[i] == "←")
                {
                    newget_move[i] = "left";
                }
                if (newget_move[i] == "↓")
                {
                    newget_move[i] = "down";
                }
            }
            return newget_move.ToArray();
        }


        //当たり判定
        public bool Colision_detection(int x, int y, int[,] Map, List<int[]> move)
        {
            int max_x = Map.GetLength(0);
            int max_y = Map.GetLength(1);

            int new_x = x + move[0][0];
            int new_y = y + move[0][1];

            if ((new_x + 1) <= 0 || (max_x - new_x) <= 0 || (new_y + 1) <= 0 || (max_y - new_y) <= 0) return false;
            else if (Map[new_y, new_x] == 0) return false;
            else
            {
                //move.RemoveAt(0);
                return true;
            }
        }

        //キャラの座標更新
        public void SquareMovement(int x, int y, int[,] Map, List<int[]> move)
        {
            Graphics g2 = Graphics.FromImage(bmp2);
            int cell_length = pictureBox1.Width / 10;
            if (move.Count == 0)
            {
                return;
            }

            List<int[]> move_copy = new List<int[]>();
            for (int i = 0; i < move.Count; i++)
            {
                move_copy.Add(move[i]);
            }

            bool jump = false;
            bool move_floor = false;
            int waittime = 250; //ミリ秒

            while (true)
            {
                if (!Colision_detection(x, y, Map, move_copy) && !jump)
                {
                    //忍者を動かしてからミスの表示を出す
                    x += move_copy[0][0];
                    y += move_copy[0][1];
                    g2.Clear(Color.Transparent);
                    if (move_copy[0][0] == -1)      character_me = Image.FromFile("忍者_左面.png");
                    else if(move_copy[0][0] == 1)   character_me = Image.FromFile("忍者_右面.png");
                    if (move_copy[0][1] == -1)      character_me = Image.FromFile("忍者_背面.png");
                    else if (move_copy[0][1] == 1)  character_me = Image.FromFile("忍者_正面.png");
                    g2.DrawImage(character_me, x * cell_length, y * cell_length, cell_length, cell_length);
                    //ステージごとにゴールのキャラを変えたい
                    g2.DrawImage(character_enemy2, Global.x_goal * cell_length, Global.y_goal * cell_length, cell_length, cell_length);
                
                    //pictureBoxの中身を塗り替える
                    this.Invoke((MethodInvoker)delegate
                    {
                        // pictureBox2を同期的にRefreshする
                        pictureBox2.Refresh();
                    });

                    //ミスラベル
                    label6.Visible = true;
                    Thread.Sleep(300);
                    //label6.Visible = false;
                    Global.miss_count += 1;
                    label5.Text = Global.miss_count.ToString();
                    break;
                }

                //移動先が木の場合、木の方向には進めない
                if (!jump && Map[y + move_copy[0][1], x + move_copy[0][0]] == 8)
                {
                    if (move_copy[0][0] == -1) character_me = Image.FromFile("忍者_左面.png");
                    else if (move_copy[0][0] == 1) character_me = Image.FromFile("忍者_右面.png");
                    if (move_copy[0][1] == -1) character_me = Image.FromFile("忍者_背面.png");
                    else if (move_copy[0][1] == 1) character_me = Image.FromFile("忍者_正面.png");
                    g2.DrawImage(character_me, x * cell_length, y * cell_length, cell_length, cell_length);
                    //move_copy[0] = new int[] { 0, 0 };
                    move_copy.RemoveAt(0);
                    //500ミリ秒=0.5秒待機する
                    Thread.Sleep(waittime);
                    continue;
                }

                x += move_copy[0][0];
                y += move_copy[0][1];

                Global.x_now = x;
                Global.y_now = y;

                g2.Clear(Color.Transparent);
                //ステージごとにゴールのキャラを変えたい
                g2.DrawImage(character_enemy2, Global.x_goal * cell_length, Global.y_goal * cell_length, cell_length, cell_length);
                //忍者の動きに合わせて向きが変わる
                if (move_copy[0][0] == -1)      character_me = Image.FromFile("忍者_左面.png");
                else if(move_copy[0][0] == 1)   character_me = Image.FromFile("忍者_右面.png");
                if (move_copy[0][1] == -1)      character_me = Image.FromFile("忍者_背面.png");
                else if (move_copy[0][1] == 1)  character_me = Image.FromFile("忍者_正面.png");
                g2.DrawImage(character_me, x * cell_length, y * cell_length, cell_length, cell_length);


                //pictureBoxの中身を塗り替える
                this.Invoke((MethodInvoker)delegate
                {
                    // pictureBox2を同期的にRefreshする
                    pictureBox2.Refresh();
                });

                if (Map[y, x] == 101)
                {
                    character_me = Image.FromFile("忍者_正面.png");
                    break;
                }
                //移動先が氷の上なら同じ方向にもう一回進む
                if (Map[y, x] == 2)
                {
                    //500ミリ秒=0.5秒待機する
                    Thread.Sleep(waittime);
                    continue;
                }

                //移動先がジャンプ台なら同じ方向に二回進む（１個先の障害物は無視）
                if (Map[y, x] == 3 || jump)
                {
                    
                    if (jump) //次の移動で着地
                    {
                        jump = false;
                    }
                    else //ジャンプ台の上（次の移動でジャンプ）
                    {
                        jump = true;
                    }

                    //500ミリ秒=0.5秒待機する
                    Thread.Sleep(waittime);
                    continue;
                }

                //上に移動するマスを踏んだ場合1つ上に進む
                if (Map[y, x] == 4)
                {
                    move_copy[0] = new int[2] { 0, -1 };
                    Thread.Sleep(waittime);
                    continue;
                }

                //右に移動するマスを踏んだ場合1つ右に進む
                if (Map[y, x] == 5)
                {
                    move_copy[0] = new int[2] { 1, 0 };
                    Thread.Sleep(waittime);
                    continue;
                }

                //下に移動するマスを踏んだ場合1つ下に進む
                if (Map[y, x] == 6)
                {
                    move_copy[0] = new int[2] { 0, 1 };
                    Thread.Sleep(waittime);
                    continue;
                }

                //左に移動するマスを踏んだ場合1つ左に進む
                if (Map[y, x] == 7)
                {
                    move_copy[0] = new int[2] { -1, 0 };
                    Thread.Sleep(waittime);
                    continue;
                }


                move_copy.RemoveAt(0);
                if (move_copy.Count == 0)//動作がすべて終了した場合
                {
                    character_me = Image.FromFile("忍者_正面.png");

                    break;
                }

                //500ミリ秒=0.5秒待機する
                Thread.Sleep(waittime);
            }
            Global.count += 1;
        }

        //会話文読み込み
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
                                    endIndex = newlineIndex[k + 1];
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

        /*******関数 fin******/


        /******つかわない******/
        //作業中？
        bool visible = false;
        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)    //アイコンをクリックすることでヒントを表示
        {
            Font fnt = new Font("MS UI Gothic", 15);
            int sp = 8;

            Bitmap bmp3 = new Bitmap(pictureBox3.Image);
            Graphics g3 = Graphics.FromImage(bmp3);

            if (!visible)
            {
                g3.DrawRectangle(Pens.White, 100, 100, 100, 100);
                g3.DrawString("ヒントです", fnt, Brushes.Black, bmp3.Height + sp, 0 + sp);
            }
            else
            {
                bmp3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                g3 = Graphics.FromImage(bmp3);
                g3.DrawImage(img_tanuki, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
                g3.DrawRectangle(Pens.Black, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            }

            visible = !visible;

            pictureBox3.Image = bmp3;
            g3.Dispose();
            
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string command = listBox1.SelectedItem.ToString();

                if (command.StartsWith("for"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox1.SelectedIndex;
                    listBox1.Items[id] = "for (" + (num % 9 + 1).ToString() + ")";

                    listBox1.Refresh();
                }
            }
        }
        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                string command = listBox3.SelectedItem.ToString();

                if (command.StartsWith("for"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox3.SelectedIndex;
                    listBox3.Items[id] = "for (" + (num % 9 + 1).ToString() + ")";

                    listBox3.Refresh();
                }
            }
        }

        private void listBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox4.SelectedItem != null)
            {
                string command = listBox4.SelectedItem.ToString();

                if (command.StartsWith("for"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox4.SelectedIndex;
                    listBox4.Items[id] = "for (" + (num % 9 + 1).ToString() + ")";

                    listBox4.Refresh();
                }
            }
        }

        //以下空の関数（消さない）
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)//マップに戻るボタン
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ここに処理を書く
        }
    }
}
