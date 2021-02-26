using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 扫描仪接口
    /// </summary>
    public interface IScanner
    {
        /// <summary>
        /// 初始化扫描仪
        /// </summary>
        /// <param name="paramters">初始化设置，比如串口端口号，波特率等</param>
        /// <returns></returns>
        bool Init(string paramters);

        /// <summary>
        /// 清除当前条码信息
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取条码内容
        /// </summary>
        /// <param name="nTimeout"></param>
        /// <returns></returns>
        string getBarcode(int nTimeout);

        /// <summary>
        /// 条码是否已经OK
        /// </summary>
        /// <param name="mech">传感器状判断函数</param>
        /// <returns></returns>
        bool isBarcodeReady(Func<bool> sensor = null);

        /// <summary>
        /// 退出条码扫描
        /// </summary>
        void Quit();
    }
}
