using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace LaserEzd
{
    public partial class Debug : Form
    {

        string _ezdFileName = ""; //用来存储模板文件的文件路径

        public Debug()
        {
            InitializeComponent();

            try
            {
                //string path = "C:\\Users\\Administrator\\Desktop\\开发库 单头\\开发库\\LaserEzd\\bin\\Debug\\";
                //int err = LMCDriver.lmc1_Initial(path, false, this.Handle); //初始化

                ////int err = LMCDriver.lmc1_Initial(path, false, System.Diagnostics.Process.GetCurrentProcess().Handle); //初始化

                ////int err = LMCDriver.lmc1_Initial(path, false, Process.GetCurrentProcess().MainWindowHandle); //初始化

                //err = LMCDriver.lmc1_LoadEzdFile(@"C:\\Users\\Administrator\\Desktop\\开发库 单头\\开发库\\生产文件\\lisi.ezd"); //加载打印模板

                //err = LMCDriver.lmc1_ChangeTextByName("01", "zhu57");  //设置打印内容

                //err = LMCDriver.lmc1_Mark(false); //光刻

                //Get bitmap
                //Bitmap m = Image.FromHbitmap(LMCDriver.lmc1_GetPrevBitmap(this.Handle, 500, 300)); //预览
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Debug_Load(object sender, EventArgs e)
        {
            int r = LMCDriver.lmc1_Initial(Application.StartupPath, false, this.Handle);

            if (r != 0)
            {
                MessageBox.Show("初始化光刻设备出错\r\n错误代码：" + r + "\r\n错误信息:" + EzCadErrorMessage.ErrorMessage[r]);
            }
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            buttonInit.BackColor = Color.FromName("Control");

            int r = LMCDriver.lmc1_Initial(Application.StartupPath, false, this.Handle);

            if (r != 0)
            {
                buttonInit.BackColor = Color.DarkRed;
                MessageBox.Show("初始化光刻设备出错\r\n错误代码：" + r + "\r\n错误信息:" + EzCadErrorMessage.ErrorMessage[r]);
            }
            else
            {
                buttonInit.BackColor = Color.DarkGreen;
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Multiselect = false;//多个文件
            dialog.Title = "请选择光刻文件";
            dialog.Filter = "Ezd文件(*.Ezd)|*.ezd";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _ezdFileName = dialog.FileName;
                textBoxFileName.Text = dialog.SafeFileName;
                textBoxFileName.BackColor = Color.LightGreen;
            }

            int r = LMCDriver.lmc1_LoadEzdFile(_ezdFileName);//加载光刻模板文件

            if (r != 0)
            {
                MessageBox.Show("加载光刻模板文件出错\r\n错误代码：" + r + "\r\n错误信息:" + EzCadErrorMessage.ErrorMessage[r]);
            }

        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            Bitmap bm = null;
            try
            {
                if (Environment.OSVersion.Version.Major > 5) //主版本号大于五为Vasta以上系统
                {
                    bm = Image.FromHbitmap(LMCDriver.lmc1_GetPrevBitmap(Handle, 560, 320)); //获取预览图片
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取预览图时发生错误：" + ex.Message);
                return;
            }

            if (bm == null)
            {
                MessageBox.Show("获取预览图失败！！！");
                LMCDriver.lmc1_Close();
                return;
            }
            else
            {
                pictureBox.Image = bm;
            }
        }

        private void redline_btn_Click(object sender, EventArgs e)
        {
            LMCDriver.lmc1_RedLightMark();
            Thread.Sleep(10);
            LMCDriver.lmc1_RedLightMark();
        }

        private void mark_btn_Click(object sender, EventArgs e)
        {
            int r = LMCDriver.lmc1_Mark(false);//执行镭雕
            if (r != 0)
            {
                MessageBox.Show("光刻时出错\r\n错误代码：" + r + "\r\n错误信息:" + EzCadErrorMessage.ErrorMessage[r]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int r = LMCDriver.lmc1_Close();
            if (r != 0)
            {
                MessageBox.Show("关闭出错\r\n错误代码：" + r + "\r\n错误信息:" + EzCadErrorMessage.ErrorMessage[r]);
            }
        }

    }
}
