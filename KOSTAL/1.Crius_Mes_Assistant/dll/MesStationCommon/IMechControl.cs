using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// IO控制
    /// </summary>
    public interface IMechControl
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool init(string parameter);


        /// <summary>
        /// 设置设备入口端的挡停止
        /// </summary>
        /// <param name="isHold"></param>
        void set_entryside_hold(bool isHold);

        /// <summary>
        /// 获取设备端的输入传感器
        /// </summary>
        /// <returns></returns>
        bool get_entryside_sensor();

        /// <summary>
        /// 设置设备出口端的挡停
        /// </summary>
        /// <param name="isHold"></param>
        void set_exitside_hold(bool isHold);

        /// <summary>
        /// 获取设备出口端的传感器状态
        /// </summary>
        /// <returns></returns>
        bool get_exitside_sensor();


         /// <summary>
        /// 设置设备出口端电机
        /// </summary>
        /// <param name="isHold"></param>
        void set_entryside_motor(bool isRunning);

        /// <summary>
        /// 设置设备出口端电机
        /// </summary>
        /// <param name="isHold"></param>
        void set_exitside_motor(bool isRunning);

        /// <summary>
        /// 板子流走,入口端
        /// </summary>
        void entryside_go();

        /// <summary>
        /// 出口端
        /// </summary>
        void exitside_go();


        /// <summary>
        /// 获取可执行的IO对
        /// </summary>
        /// <returns></returns>
        List<MechIOItem> getIOPair();
    }
}
