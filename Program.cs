using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unilab2023
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ハイDPIの設定やデフォルトフォントの設定など、必要な設定をここで行います。

            Application.Run(new スタート画面());
        }
    }

    public partial class Form1 : Form
    {
        #region フィールド変数

        /// <summary>
        /// スレッド分割用
        /// </summary>
        Thread drawThread;

        #endregion

        #region スレッド分割用関数



        private void InterThreadRefresh(Action _function)
        {
            try
            {
                if (InvokeRequired) Invoke(_function);
                else _function();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        #endregion
    }
}
