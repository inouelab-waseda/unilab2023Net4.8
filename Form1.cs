using System;
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
        Bitmap bmp1, bmp2, bmp3, bmp4;

        Brush goalBackgroundColor = new SolidBrush(Color.Yellow);
        Brush startBackgroundColor = new SolidBrush(Color.Blue);

        Image[] img_otomo = new Image[4] {
            Image.FromFile("キャラ_たぬき.png"),
            Image.FromFile("キャラ_きつね.png"),
            Image.FromFile("キャラ_あざらし.png"),
            Image.FromFile("キャラ_ふくろう.png")
        };

        Image character_me = Image.FromFile("忍者_正面.png");
        Image[] character_enemy = new Image[6] {
            Image.FromFile("キャラ_一つ目小僧.png"),
            Image.FromFile("キャラ_唐傘一反.png"),
            Image.FromFile("キャラ_カッパ.png"),
            Image.FromFile("キャラ_てんぐ.png"),
            Image.FromFile("キャラ_赤鬼.png"),
            Image.FromFile("キャラ_ヤマタノオロチ.png")
        };

        Image img_maki = Image.FromFile("マップ_巻物.png");
        Image img_way = Image.FromFile("マップ_草原.png");
        Image img_noway = Image.FromFile("マップ_岩場.png");
        Image img_ice = Image.FromFile("マップ_氷.png");
        Image img_tree = Image.FromFile("マップ_木.png");
        Image img_jump = Image.FromFile("マップ_ジャンプ1.png");
        Image animatedImage_up = Image.FromFile("マップ_動く床_上.gif");
        Image animatedImage_right = Image.FromFile("マップ_動く床_右.gif");
        Image animatedImage_down = Image.FromFile("マップ_動く床_下.gif");
        Image animatedImage_left = Image.FromFile("マップ_動く床_左.gif");
        Image cloud_ul = Image.FromFile("マップ_雲_左上.png");
        Image cloud_left = Image.FromFile("マップ_雲_左.png");
        Image cloud_bl = Image.FromFile("マップ_雲_左下.png");
        Image cloud_bottom = Image.FromFile("マップ_雲_下.png");
        Image cloud_br = Image.FromFile("マップ_雲_右下.png");
        Image cloud_right = Image.FromFile("マップ_雲_右.png");
        Image cloud_ur = Image.FromFile("マップ_雲_右上.png");
        Image cloud_upside = Image.FromFile("マップ_雲_上.png");

        public Image Draw_Icon(string name)
        {
            switch (name)
            {
                case "コタロウ":
                    return character_me;
                case "たぬき":
                case "チマキ":
                    return img_otomo[0];

                case "きつね":
                case "イナリ":
                    return img_otomo[1];

                case "あざらし":
                case "スリミ":
                    return img_otomo[2];

                case "ふくろう":
                case "ツクネ":
                    return img_otomo[3];

                case "ヤマタノオロチ":
                    return character_enemy[5];
                case "ナレ":
                    return img_maki;
            }
            Bitmap bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.White);

            return bmp;
        }
        //ゴールのキャラを返す関数
        public Image goal_obj(string _stageName)
        {
            switch (_stageName)
            {
                case "stage1-3":
                    return img_otomo[0];
                case "stage1-4":
                    return character_enemy[0];
                case "stage2-2":
                    return img_otomo[1];
                case "stage2-3":
                    return character_enemy[1];
                case "stage3-2":
                    return img_otomo[2];
                case "stage3-3":
                    return character_enemy[2];
                case "stage4-2":
                    return img_otomo[3];
                case "stage4-3":
                    return character_enemy[3];
                case "stage5-1":
                    return character_enemy[5];
                case "stage5-2":
                case "stage5-3":
                    return character_enemy[4];
            }
            return img_maki;
        }

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
        //private bool[] _stageClear=new bool[16];//ステージを1度クリアしたかどうか
        //public bool[] stageClear
        //{
        //    get { return _stageClear; }
        //    set { _stageClear = value; }
        //}
        //public int Name2Number(string _stageName)//ステージ名から番号に変換
        //{
        //    switch (_stageName)
        //    {
        //        case "stage1-1"://チュートリアル
        //            return 0;
        //        case "stage1-2":
        //            return 1;
        //        case "stage1-3"://たぬき
        //            return 2;
        //        case "stage1-4"://一つ目小僧
        //            return 3;
        //        case "stage2-1":
        //            return 4;
        //        case "stage2-2"://きつね
        //            return 5;
        //        case "stage2-3"://唐傘一反
        //            return 6;
        //        case "stage3-1":
        //            return 7;
        //        case "stage3-2"://あざらし
        //            return 8;
        //        case "stage3-3"://カッパ
        //            return 9;
        //        case "stage4-1":
        //            return 10;
        //        case "stage4-2"://ふくろう
        //            return 11;
        //        case "stage4-3"://てんぐ
        //            return 12;
        //        case "stage5-1"://ヤマタノオロチ
        //            return 13;
        //        case "stage5-2"://鬼
        //            return 14;
        //        case "stage5-3"://鬼
        //            return 15;
        //    }
        //    return 1;
        //}

        public Form1()
        {
            InitializeComponent();

            //Form全体にdrop可能に
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(ListBox_DragDrop);
            this.DragEnter += new DragEventHandler(ListBox_DragEnter);


            //pictureBoxの設定
            pictureBox2.Parent = pictureBox1;
            //pictureBox1.Location = new Point(600, 50);
            pictureBox2.Location = new Point(0, 0);
            pictureBox1.ClientSize = new Size(684, 684);
            pictureBox2.ClientSize = new Size(684, 684);
            pictureBox2.BackColor = Color.Transparent;

            bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            bmp3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            //bmp4 = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            pictureBox1.Image = bmp1;
            pictureBox2.Image = bmp2;
            pictureBox3.Image = bmp3;
            //pictureBox4.Image = bmp4;
            this.Load += Form1_Load;
        }

        public class Global //グローバル変数格納
        {
            public static int[,] map = new int[12, 12]; //map情報
            public static int x_start; //スタート位置ｘ
            public static int y_start; //スタート位置ｙ
            public static int x_goal; //ゴール位置ｘ
            public static int y_goal; //ゴール位置ｙ
            public static int x_now; //現在位置ｘ
            public static int y_now; //現在位置 y
           
            public static int count = 0; //試行回数カウント
            public static int miss_count = 0; //ミスカウント

            public static int count_walk = 0; //歩数カウント

            public static List<int[]> move;  //プレイヤーの移動指示を入れるリスト

            //listBoxに入れられる行数の制限
            public static int limit_LB1;
            public static int limit_LB3;
            public static int limit_LB4;

            public static string hint;
            public static string hint_character;
            public static string hint_name;

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
            //button5.Visible = false;
            //_stageName = "stage2-3";
            Global.map = CreateStage(stageName); //ステージ作成

            string str_num = Regex.Replace(stageName, @"[^0-9]", "");
            int num = int.Parse(str_num) / 10;

            string file_name = "わせ忍" + num.ToString() + "章.csv";

            Global.Conversations = LoadConversation(file_name); //会話読み込み

            // 1行文の高さ
            int ItemHeight = 20;
            listBox1.ItemHeight = ItemHeight;
            listBox2.ItemHeight = ItemHeight;
            listBox3.ItemHeight = ItemHeight;
            listBox4.ItemHeight = ItemHeight;
            listBox5.ItemHeight = ItemHeight;

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
                label4.Visible = false;
                listBox5.Items.Remove("A");
                button2.Visible = false;
                button2.Enabled = false;

            }

            if (height_LB3 == 1)
            {
                listBox3.Visible = false;
                label5.Visible = false;
                listBox5.Items.Remove("B");
                button3.Visible = false;
                button3.Enabled = false;
            }

            if(height_LB1 == 1 && height_LB3 == 1)
            {
                listBox5.Visible = false;
            }

            Global.hint = null;
            Global.hint_character = null;
            Global.hint_name = null;

            // CSVから読み込んだテキストを設定します。
            using (StreamReader sr = new StreamReader($"hint.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');


                    if (values[0] == _stageName)
                    {
                        Global.hint_character = values[1];
                        Global.hint = values[2];
                        Global.hint_name = values[3];
                        break;
                    }
                }
            }
            
            if (Global.hint == null)
            {
                button8.Visible = false;
            }
            else
            {
                button8.Visible = true;
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
            Bitmap bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.White);
            g3.DrawImage(bmp, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            g3.DrawRectangle(Pens.Black, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            g3.Dispose();

            //チュートリアルステージでは、マップに戻るボタンを消す。ゴールしたら見える
            if(stageName == "stage1-1")
            {
                button5.Visible = false;
            }
            //for文をステージ1-1,1-2で消す
            if(stageName == "stage1-1" || stageName == "stage1-2")
            {
                listBox2.Items.Remove("連チャンの術 (1)");
                listBox2.Items.Remove("連チャンの術おわり");
            }

            //ストーリー強制視聴
            listBox2.Enabled = false;
            listBox5.Enabled = false;
            drawConversation();
        }

        /****button****/
       private void button2_Click(object sender, EventArgs e)//ListBoxのリセットボタン、選択したものだけを消す。選択なければすべて消す。
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

        private void button1_Click(object sender, EventArgs e)//出発ボタン
        {
            button1.Visible = false;
            button1.Enabled = false;
            label6.Visible = false;
            Global.move = Movement(); //ユーザーの入力を読み取る
            SquareMovement(Global.x_now, Global.y_now, Global.map, Global.move); //キャラ動かす
            Global.count += 1;
            if (Global.x_goal == Global.x_now && Global.y_goal == Global.y_now)
            {
                label1.Text = "クリア！！";
                label1.Visible = true;
                button5.Visible = true;
            }
            else
            {
                resetStage("miss");
            }
        }

        

        // 枠からはみ出す大きさ
        int extra_length = 7;

        public void resetStage(string type) // リセット関連まとめ
        {
            if (type == "quit")
            {
                ステージ選択画面 form2 = new ステージ選択画面();
                form2.stageName = "stage5-2";
                form2.Show();
                this.Close();
                return;
            }

            // 曇から落ちたミス
            else if (type == "miss_out")
            {
                label6.Text = "そこは止まれないよ！やり直そう！";
                label6.Visible = true;
                Thread.Sleep(300);
                Global.miss_count += 1;
            }

            //木に刺されたミス
            else if (type == "miss_tree")
            {
                label6.Text = "木に刺された！やり直そう！";
                label6.Visible = true;
                Thread.Sleep(300);
                Global.miss_count += 1;
            }

            //無限ループの時のミス
            else if (type == "miss_countover")
            {
                label6.Text = "これ以上は移動できない！やり直そう！";
                label6.Visible = true;
                Thread.Sleep(300);
                Global.miss_count += 1;
            }

            //止まった時ゴール到着してないミス
            else if (type == "miss_end")
            {
                label6.Text = "ゴールまで届いてないね！やり直そう！";
                label6.Visible = true;
                Thread.Sleep(300);
                Global.miss_count += 1;
            }

            // リトライボタン
            else if (type == "retry")
            {
                //初期位置に戻す
                Global.x_now = Global.x_start;
                Global.y_now = Global.y_start;

                //初期位置に書き換え
                Graphics g2 = Graphics.FromImage(bmp2);
                g2.Clear(Color.Transparent);
                int cell_length = pictureBox1.Width / 12;
                character_me = Image.FromFile("忍者_正面.png");
                g2.DrawImage(character_me, Global.x_now * cell_length - extra_length, Global.y_now * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);

                g2.DrawImage(goal_obj(_stageName), Global.x_goal * cell_length - extra_length, Global.y_goal * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
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
                Global.miss_count = 0;
                label6.Text = "ミス！";
            }
        }

        private void button6_Click(object sender, EventArgs e) //リトライボタン
        {
            resetStage("retry");
        }

        private void button5_Click(object sender, EventArgs e) //マップに戻るボタン
        {
            resetStage("quit");
        }

        private void button7_Click(object sender, EventArgs e) // 使ってない？
        {
            ステージ選択画面 form2 = new ステージ選択画面();
            form2.stageName = stageName;
            form2.Show();
            this.Close();
        }

        /******button fin******/

        /******ListBox******/

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


        private ListBox GetNearestListBox(Point point)
        {
            // 3つのListBoxをリストに格納する
            List<ListBox> listBoxes = listBoxes = new List<ListBox> { listBox1, listBox3, listBox4 };

            // Bボタンがあるかないかの場合分け
            if (Global.limit_LB3 == 0)
            {
                listBoxes = new List<ListBox> { listBox1, listBox4 };
            }

            // Aボタンない時
            if (Global.limit_LB1 == 0)
            {
                listBoxes = new List<ListBox> { listBox3, listBox4 };
            }

            // A.Bボタンない時
            if (Global.limit_LB1 == 0 && Global.limit_LB3 == 0)
            {
                listBoxes = new List<ListBox> { listBox4 };
            }
            double minDistance = double.MaxValue;
            ListBox nearestListBox = null;

            foreach (var listBox in listBoxes)
            {
                // ListBoxの中心座標を計算する
                Point listBoxCenter = new Point(listBox.Location.X + listBox.Width / 2, listBox.Location.Y + listBox.Height / 2);

                // ドラッグされたポイントとListBoxの中心との間の距離を計算する
                double distance = Math.Sqrt(Math.Pow(listBoxCenter.X - point.X, 2) + Math.Pow(listBoxCenter.Y - point.Y, 2));

                // これまでの最小距離よりも小さい場合は、更新する
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestListBox = listBox;
                }
            }

            // 最も近いListBoxを返す
            return nearestListBox;
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

        private void DisplayImageAndTextOnPictureBox(PictureBox pictureBox, string image, string text)
        {
            // 画像ファイルを読み込む。
            Image img = Image.FromFile(image);

            Bitmap bmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g = Graphics.FromImage(bmp);

            Font fnt = new Font("游明朝", 20);
            int sp = 8;

            g.DrawImage(img, 0, 0, bmp.Height - 1, bmp.Height - 1);
            g.DrawRectangle(Pens.Black, 0, 0, bmp.Height - 1, bmp.Height - 1);

            g.DrawRectangle(Pens.White, 100, 100, 100, 100);
            g.DrawString(text, fnt, Brushes.Black, bmp.Height + sp, 0 + sp);

            pictureBox.Image = bmp;
            //label2.Text = Global.hint_name;


            g.Dispose();
         }





            /*
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
            */

            //listboxの行数を制限する場合
            private void ListBox_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがstring型か調べる
            if (e.Data.GetDataPresent(typeof(string)) && isEnableDrop)
            {
                Point point = this.PointToClient(new Point(e.X, e.Y));
                ListBox target = GetNearestListBox(point); // ここでマウス位置に最も近いリストボックスを取得

                if (target == null) // 最も近いリストボックスがない場合は何もしない
                    return;

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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string command = listBox1.SelectedItem.ToString();

                if (command == "連チャンの術おわり")
                {
                    return;
                }
                if (command.StartsWith("連チャンの術"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox1.SelectedIndex;
                    listBox1.Items[id] = "連チャンの術 (" + (num % 9 + 1).ToString() + ")";

                    listBox1.Refresh();
                }
            }
        }

        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                string command = listBox3.SelectedItem.ToString();

                if (command == "連チャンの術おわり")
                {
                    return;
                }
                if (command.StartsWith("連チャンの術"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox3.SelectedIndex;
                    listBox3.Items[id] = "連チャンの術 (" + (num % 9 + 1).ToString() + ")";

                    listBox3.Refresh();
                }
            }
        }

        private void listBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox4.SelectedItem != null)
            {
                string command = listBox4.SelectedItem.ToString();

                if (command == "連チャンの術おわり")
                {
                    return;
                }
                if (command.StartsWith("連チャンの術"))
                {
                    string str_num = Regex.Replace(command, @"[^0-9]", "");
                    int num = int.Parse(str_num);

                    int id = listBox4.SelectedIndex;
                    listBox4.Items[id] = "連チャンの術 (" + (num % 9 + 1).ToString() + ")";

                    listBox4.Refresh();
                }
            }
        }


        /******ListBox fin******/

        /*******関数******/

        //これなに？
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
            int[,] map = new int[12, 12];

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

            int cell_length = pictureBox1.Width / 12;

            for(int y = 1; y < 11; y++)
            {
                for(int x = 1; x < 11; x++)
                {
                    g1.DrawImage(img_way, x * cell_length, y * cell_length, cell_length, cell_length);
                }
            }

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 12; x++)
                {
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
                        case 20:
                            g1.DrawImage(cloud_ul, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 21:
                            g1.DrawImage(cloud_left, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 22:
                            g1.DrawImage(cloud_bl, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 23:
                            g1.DrawImage(cloud_bottom, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 24:
                            g1.DrawImage(cloud_br, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 25:
                            g1.DrawImage(cloud_right, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 26:
                            g1.DrawImage(cloud_ur, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 27:
                            g1.DrawImage(cloud_upside, x * cell_length, y * cell_length, cell_length, cell_length);
                            break;
                        case 100:
                            g1.FillRectangle(startBackgroundColor, x * cell_length, y * cell_length, cell_length, cell_length);
                            Global.x_start = x;
                            Global.y_start = y;
                            Global.x_now = x;
                            Global.y_now = y;
                            g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2*extra_length, cell_length + 2*extra_length, cell_length + 2*extra_length);
                            break;
                        case 101:
                            g1.FillRectangle(goalBackgroundColor, x * cell_length, y * cell_length, cell_length, cell_length);
                            //ステージごとにゴールのキャラを変えたい
                            g2.DrawImage(goal_obj(_stageName), x * cell_length - extra_length, y * cell_length - 2*extra_length, cell_length + 2*extra_length, cell_length + 2*extra_length);
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
                if (newget_move[i].StartsWith("連チャンの術 ("))
                {
                    string str_num = Regex.Replace(newget_move[i], @"[^0-9]", "");
                    int num = int.Parse(str_num);
                    newget_move[i] = "for (" + (num % 9).ToString() + ")";
                }
                if (newget_move[i].StartsWith("連チャンの術おわり"))
                {
                    newget_move[i] = "endfor";
                }
                if (newget_move[i] == "Aの術")
                {
                    newget_move[i] = "A";
                }
                if (newget_move[i] == "Bの術")
                {
                    newget_move[i] = "B";
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

            if (new_x <= 0 || (max_x - new_x) <= 1 || new_y <= 0 || (max_y - new_y) <= 1) return false;
            else if (Map[new_y, new_x] == 0) return false;
            else
            {
                //move.RemoveAt(0);
                return true;
            }
        }

        //動きにあわせて忍者の画像を返す
        Image Ninja_Image(int x, int y, int steps, bool jump, Image Ninja)
        {
            int a = steps % 4;//歩き差分を識別
            if (a == 1)
            {
                if (x == -1) Ninja = Image.FromFile("忍者_左面_右足.png");
                if (x == 1) Ninja = Image.FromFile("忍者_右面_右足.png");
                if (y == -1) Ninja = Image.FromFile("忍者_背面_右足.png");
                if (y == 1) Ninja = Image.FromFile("忍者_正面_右足.png");
            }else if (a == 3)
            {
                if (x == -1) Ninja = Image.FromFile("忍者_左面_左足.png");
                if (x == 1) Ninja = Image.FromFile("忍者_右面_左足.png");
                if (y == -1) Ninja = Image.FromFile("忍者_背面_左足.png");
                if (y == 1) Ninja = Image.FromFile("忍者_正面_左足.png");

            }
            else
            {
                if (x == -1) Ninja = Image.FromFile("忍者_左面.png");
                if (x == 1) Ninja = Image.FromFile("忍者_右面.png");
                if (y == -1) Ninja = Image.FromFile("忍者_背面.png");
                if (y == 1) Ninja = Image.FromFile("忍者_正面.png");
            }
            

            if (jump)
            {
                if (x == -1) Ninja = Image.FromFile("忍者_左面_ジャンプ.png");
                if (x == 1) Ninja = Image.FromFile("忍者_右面_ジャンプ.png");
                if (y == -1) Ninja = Image.FromFile("忍者_背面_ジャンプ.png");
                if (y == 1) Ninja = Image.FromFile("忍者_正面_ジャンプ.png");
            }


            return Ninja;
        }

        //キャラの座標更新
        public void SquareMovement(int x, int y, int[,] Map, List<int[]> move)
        {
            Graphics g2 = Graphics.FromImage(bmp2);
            int cell_length = pictureBox1.Width / 12;
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
            Global.count_walk = 1;//何マス歩いたか、歩き差分用

            while (true)
            {
                if(move_copy.Count > 0)
                {
                    if (!Colision_detection(x, y, Map, move_copy) && !jump)
                    {
                        //忍者を動かしてからミスの表示を出す
                        x += move_copy[0][0];
                        y += move_copy[0][1];
                        g2.Clear(Color.Transparent);
                        character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], Global.count, jump, character_me);
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        //ステージごとにゴールのキャラを変えたい
                        g2.DrawImage(goal_obj(_stageName), Global.x_goal * cell_length - extra_length, Global.y_goal * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                
                        //pictureBoxの中身を塗り替える
                        this.Invoke((MethodInvoker)delegate
                        {
                            // pictureBox2を同期的にRefreshする
                            pictureBox2.Refresh();
                        });
                        resetStage("miss_out");
                        character_me = Image.FromFile("忍者_正面.png");
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        break;
                    }
                    if (jump && Map[y + move_copy[0][1] * 2, x + move_copy[0][0] * 2] == 8) //jumpの時着地先が木の場合、ゲームオーバー
                    {
                        x += move_copy[0][0];
                        y += move_copy[0][1];
                        Global.x_now = x;
                        Global.y_now = y;
                        g2.Clear(Color.Transparent);
                        //ステージごとにゴールのキャラを変えたい
                        g2.DrawImage(goal_obj(_stageName), Global.x_goal * cell_length - extra_length, Global.y_goal * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        //忍者の動きに合わせて向きが変わる
                        character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], Global.count, jump, character_me);
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        pictureBox2.Refresh();
                        Thread.Sleep(waittime);
                        bool J = false;
                        x += move_copy[0][0];
                        y += move_copy[0][1];
                        Global.x_now = x;
                        Global.y_now = y;
                        g2.Clear(Color.Transparent);
                        //ステージごとにゴールのキャラを変えたい
                        g2.DrawImage(goal_obj(_stageName), Global.x_goal * cell_length - extra_length, Global.y_goal * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        //忍者の動きに合わせて向きが変わる
                        character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], Global.count, J, character_me);
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        pictureBox2.Refresh();
                        resetStage("miss_tree");
                        character_me = Image.FromFile("忍者_正面.png");
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        break;
                    }
                    if(Global.count_walk > 50) //無限ループ対策
                    {
                        resetStage("miss_countover");
                        character_me = Image.FromFile("忍者_正面.png");
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                        break;
                    }
                }
                else
                {
                    character_me = Image.FromFile("忍者_正面.png");
                    g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                    break;
                }

                //jumpでない時移動先が木の場合、木の方向には進めない
                if (!jump && Map[y + move_copy[0][1], x + move_copy[0][0]] == 8)
                {
                    character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], Global.count_walk, jump, character_me);
                    g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
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
                g2.DrawImage(goal_obj(_stageName), Global.x_goal * cell_length - extra_length, Global.y_goal * cell_length - 2*extra_length, cell_length + 2*extra_length, cell_length + 2*extra_length);
                //忍者の動きに合わせて向きが変わる

                character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], Global.count_walk, jump, character_me);
                g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                //Thread.Sleep(waittime);//マスの間に歩く差分を出そうとしたけど。。。
                //g2.Clear(Color.Transparent);
                //character_me = Ninja_Image(move_copy[0][0], move_copy[0][1], stepCount, jump, character_me);
                //g2.DrawImage(character_me, x * cell_length + move_copy[0][0] * cell_length / 2, y * cell_length + move_copy[0][1] * cell_length / 2, cell_length, cell_length);


                //pictureBoxの中身を塗り替える
                this.Invoke((MethodInvoker)delegate
                {
                    // pictureBox2を同期的にRefreshする
                    pictureBox2.Refresh();
                });

                if (Map[y, x] == 101)
                {
                    character_me = Image.FromFile("忍者_正面.png");
                    g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                    break;
                }

                //移動先が氷の上なら同じ方向にもう一回進む
                if (!jump &&  Map[y, x] == 2)
                {
                    //500ミリ秒=0.5秒待機する
                    Thread.Sleep(waittime);
                    continue;
                }

                //移動先がジャンプ台なら同じ方向に二回進む（１個先の障害物は無視）
                if (Map[y, x] == 3 || jump)
                {
                    if (move_floor)
                    {
                        move_floor = false;
                        move_copy.RemoveAt(0);
                    }

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
                    if(Global.x_now != Global.x_goal || Global.y_now != Global.y_goal)
                    {
                        resetStage("miss_end");
                        Thread.Sleep(300);
                        character_me = Image.FromFile("忍者_正面.png");
                        g2.DrawImage(character_me, x * cell_length - extra_length, y * cell_length - 2 * extra_length, cell_length + 2 * extra_length, cell_length + 2 * extra_length);
                    }
                    break;
                }

                //500ミリ秒=0.5秒待機する
                Thread.Sleep(waittime);
                Global.count_walk++;
            }

            
        }

        //会話文読み込み
        private List<Conversation> LoadConversation(string conv_stagename)
        {
            List<Conversation> conversations = new List<Conversation>();

            string stage = Regex.Replace(stageName, @"[^0-9]", "");
            int stage_num = int.Parse(stage) % 10;

            bool load = false;

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

                    if (load) {
                        conversations.Add(new Conversation());

                        i -= 1;

                        conversations[i].character = values[0];
                        conversations[i].dialogue = values[1];
                        conversations[i].img = values[2];

                        i += 2;
                    }
                    int start_num = values[1].IndexOf("start");
                    if (start_num > 0)
                    {
                        string stg = Regex.Replace(values[1], @"[^0-9]", "");
                        int stg_num = int.Parse(stg) % 10;
                        if (stg_num == stage_num) load = true;
                        else if (stg_num == stage_num + 1)
                        {
                            int count = conversations.Count();
                            if (count != 0) conversations.RemoveAt(count - 1);
                            load = false;
                        }
                    }
                }
            }
            return conversations;
        }

        int conversationCounter = 0;  // 脚本のカウンタ

        bool visible = true;
        private void drawConversation()
        {
            int play_num = Global.Conversations[conversationCounter].dialogue.IndexOf("play");
            int end_num = Global.Conversations[conversationCounter].dialogue.IndexOf("終わり");
            bool Goal = (Global.x_goal == Global.x_now && Global.y_goal == Global.y_now);

            if ((play_num > 0 && !Goal) || end_num > 0)
            {
                //ストーリー強制視聴解除
                listBox2.Enabled = true;
                listBox5.Enabled = true;

                return;
            }
            else if (play_num > 0) conversationCounter += 1;

            //描画準備
            Bitmap bmp3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g3 = Graphics.FromImage(bmp3);

            Font fnt = new Font("游明朝", 20);
            int sp = 8;

            g3.DrawImage(Draw_Icon(Global.Conversations[conversationCounter].character), 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            g3.DrawRectangle(Pens.Black, 0, 0, bmp3.Height - 1, bmp3.Height - 1);
            if (visible)
            {
                //label2.Text = Global.Conversations[conversationCounter].character;
                g3.DrawRectangle(Pens.White, 100, 100, 100, 100);
                g3.DrawString(Global.Conversations[conversationCounter].dialogue, fnt, Brushes.Black, bmp3.Height + sp, 0 + sp);

                pictureBox3.Image = bmp3;
            }

            g3.Dispose();

            if (conversationCounter < Global.Conversations.Count - 1)
            {
                conversationCounter += 1;
                //ストーリー強制視聴解除
                if (Global.Conversations[conversationCounter].dialogue.IndexOf("play") > 0)
                {
                    listBox2.Enabled = true;
                    listBox5.Enabled = true;
                }
            }
            else
            {
                visible = false;
            }
        }

        private void conversation()
        {
            drawConversation();
            int end_num = Global.Conversations[conversationCounter].dialogue.IndexOf("終わり");
            if (end_num > 0)
            {
                button5.Enabled = true;
            }
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)    //アイコンをクリックすることでヒントを表示
        {
            conversation();
        }

        //public void OtomoDraw(bool[] stageClear)
        //{
        //    //    Graphics g1 = Graphics.FromImage(bmp1);
        //    //g1.DrawImage(Image.FromFile("マップ_草原.png"), 0, 0, 190, 960);
        //    Graphics g = Graphics.FromImage(bmp4);
        //        g.DrawImage(img_maki, pictureBox4.Location.X, pictureBox4.Location.Y, 150, 150);
        //    if (stageClear[2])
        //    {
        //        g.DrawImage(img_otomo[0], pictureBox4.Location.X, pictureBox4.Location.Y, 150, 150);
        //    }
        //    else
        //    {
        //    }
        //    if (stageClear[5])
        //    {
        //        g.DrawImage(img_otomo[1], pictureBox4.Location.X+150, pictureBox4.Location.Y, 150, 150);
        //    }
        //    if (stageClear[8])
        //    {
        //        g.DrawImage(img_otomo[2], pictureBox4.Location.X, pictureBox4.Location.Y+150, 150, 150) ;
        //    }
        //    if (stageClear[11])
        //    {
        //        g.DrawImage(img_otomo[3], pictureBox4.Location.X+150, pictureBox4.Location.Y+150, 150, 150);
        //    }
        //}
        /*******関数 fin******/


        /******つかわない******/

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Global.hint != null) 
            {
                DisplayImageAndTextOnPictureBox(pictureBox3, Global.hint_character, Global.hint);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
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
