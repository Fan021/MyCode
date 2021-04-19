using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace LaserEzd
{
    public class EzCadErrorMessage
    {
        public static string[] ErrorMessage = { "成功",
                                                "发现EzCad正在运行",
                                                "找不到EzCad.cfg",
                                                "打开LMC1失败",
                                                "没有有效的LMC1设备",
                                                "LMC1版本错误",
                                                "找不到设备配置文件",
                                                "报警信号",
                                                "用户停止",
                                                "不明错误",
                                                "超时",
                                                "未初始化",
                                                "读取文件错误",
                                                "窗口为空",
                                                "找不到指定名称的字体",
                                                "错误的笔号",
                                                "指定名称的对象不是文本对象",
                                                "保存文件失败",
                                                "找不到指定的对象",
                                                "当前状态下不能执行此操作",
                                                "参数错误",
                                                "硬件错误"
                                                };
    }

    public class LMCDriver
    {
        #region "Static Variable"

        //所有函数都返回一个整形值
        public static int LMC1_ERR_SUCCESS = 0;  //成功
        public static int LMC1_ERR_EZCADRUN = 1; //发现EZCAD在运行
        public static int LMC1_ERR_NOFINDCFGFILE = 2; //找不到EZCAD.CFG
        public static int LMC1_ERR_FAILEDOPEN = 3; //打开LMC1失败
        public static int LMC1_ERR_NODEVICE = 4;  //没有有效的lmc1设备
        public static int LMC1_ERR_HARDVER = 5;  //lmc1版本错误
        public static int LMC1_ERR_DEVCFG = 6; //找不到设备配置文件
        public static int LMC1_ERR_STOPSIGNAL = 7;  //报警信号
        public static int LMC1_ERR_USERSTOP = 8;  //用户停止
        public static int LMC1_ERR_UNKNOW = 9; //不明错误
        public static int LMC1_ERR_OUTTIME = 10; //超时
        public static int LMC1_ERR_NOINITIAL = 11;//未初始化
        public static int LMC1_ERR_READFILE = 12;//读文件错误
        public static int LMC1_ERR_OWENWNDNULL = 13; //窗口为空
        public static int LMC1_ERR_NOFINDFONT = 14;//找不到指定名称的字体
        public static int LMC1_ERR_PENNO = 15; //错误的笔号
        public static int LMC1_ERR_NOTTEXT = 16; //指定名称的对象不是文本对象
        public static int LMC1_ERR_SAVEFILE = 17; //保存文件失败
        public static int LMC1_ERR_NOFINDENT = 18;//找不到指定对象
        public static int LMC1_ERR_STATUE = 19;//当前状态下不能执行此操作
        public static int LMC1_ERR_PARAM = 20; //参数错误
        public static int LMC1_ERR_DEVICE = 21;//硬件错误

        #endregion

        #region "Equipment"

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_Initial([MarshalAs(UnmanagedType.LPWStr)]string strEzCadPath, Boolean bTestMode, IntPtr hOwenWnd);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_Close();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_SetDevCfg();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_SetRotateMoveParam(double dMoveX, double dMoveY, double dCenterX, double dCenterY, double dRotateAng);

        #endregion

        #region "Files"
        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_LoadEzdFile(string strFileName);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_SaveEntLibToFile(string strFileName);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lmc1_GetPrevBitmap(IntPtr hOwenWnd, int nBMPWIDTH, int nBMPHEIGHT);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lmc1_GetPrevBitmap2(int nBMPWIDTH, int nBMPHEIGHT);

        #endregion

        #region"Text"

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_ChangeTextByName([MarshalAs(UnmanagedType.LPWStr)]string strTextName, [MarshalAs(UnmanagedType.LPWStr)]string strTextNew);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_GetTextByName([MarshalAs(UnmanagedType.LPWStr)]string strTextName, string szEntText);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_TextResetSn(string pTextName);

        #endregion

        #region "Mark"

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_Mark(bool bFlyMark);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_MarkEntity(string strEntName);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_MarkFlyByStartSignal();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_MarkEntityFly(string strEntName);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_MarkLine(double x1, double y1, double x2, double y2, int pen);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_MarkPoint(double x, double y, double delay, int pen);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lmc1_IsMarking();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_StopMark();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_RedLightMark();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_RedLightMarkContour();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_RedLightMarkByEnt();

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_RedLightMark(string strEntName, bool bContour);

        [DllImport("MarkEzd.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lmc1_GetFlySpeed(ref double FlySpeed);

        #endregion

        #region "Private Methods"

        //IntPtr imagePtr = new IntPtr();
        //IntPtr imagePtrNew = new IntPtr();
        private Image CsGetHbitmap(int width, int height)
        {
            Bitmap image = new Bitmap(width, height);
            IntPtr imagePtr = LMCDriver.lmc1_GetPrevBitmap2(width, height);
            if (imagePtr != null)// imagePtrNew)
                image = Image.FromHbitmap(imagePtr);
            return image;
        }

        //[System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        //private static extern bool DeleteObject(IntPtr hObject);
        //IntPtr imagePtr = new IntPtr();
        //IntPtr imagePtrNew = new IntPtr();
        //private Image GetImage2(int width, int height)
        //{
        //    Bitmap image = new Bitmap(width, height);
        //    imagePtr = lmc1_GetPrevBitmap2(width, height);
        //    if (imagePtr != imagePtrNew)
        //        image = Image.FromHbitmap(imagePtr);
        //    DeleteObject(imagePtr);
        //    return image;
        //}

        #endregion
    }
}
