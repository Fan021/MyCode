using MesStationCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPort.Net.Windows;
using Device.Net.Windows;
using System.Threading;
using System.Runtime.InteropServices;

namespace DatalogicScanner
{
    public class DatalogicScanner : IScanner
    {

        //#region api

        //public const uint GENERIC_READ = (0x80000000u);
        //public const uint GENERIC_WRITE = (0x40000000u);
        //public const uint GENERIC_EXECUTE = (0x20000000u);
        //public const uint GENERIC_ALL = (0x10000000u);

        //public const int OPEN_EXISTING = 3;


        ////        HANDLE CreateFile(
        ////LPCTSTR lpFileName,                        // file name对象路径名
        ////DWORDdwDesiredAccess,                     // access mode控制模式
        ////DWORDdwShareMode,                         // share mode共享模式
        ////LPSECURITY_ATTRIBUTES lpSecurityAttributes, // SD 安全属性(也即销毁方式)
        ////DWORD dwCreationDisposition,               // how to create
        ////DWORDdwFlagsAndAttributes,                // file attributes
        ////HANDLEhTemplateFile                       // handle to template file
        ////);



        //[DllImport("Kernel32.dll")]
        //public extern static IntPtr CreateFile(
        //    string lpszFileName,
        //    uint DesireAccess,
        //    uint shareMode,
        //    IntPtr lpsecurityAttributes,
        //    uint dwCreateDispositon,
        //    IntPtr hTemplateFile);

        ////  BOOL ReadFile(
        ////  HANDLE hFile,
        ////  LPVOID lpBuffer,
        ////  DWORD nNumberOfBytesToRead,
        ////  LPDWORD lpNumberOfBytesRead,
        ////  LPOVERLAPPED lpOverlapped

        ////);

        //[DllImport("Kernel32.dll")]
        //public extern static IntPtr ReadFile (
        //    IntPtr hFile,
        //    IntPtr buffer,
        //    uint nNumberOfBytesToRead,
        //    ref uint lpNumberOfBytesRead,
        //    IntPtr lpOverlapped);

        //[DllImport("Kernel32.dll")]
        //public extern static IntPtr CloseHandle(
        //    IntPtr hFile);

        ////HANDLE hFile,
        ////LPCVOID      lpBuffer,
        ////DWORD nNumberOfBytesToWrite,
        ////LPDWORD      lpNumberOfBytesWritten,
        ////LPOVERLAPPED lpOverlapped
        //[DllImport("Kernel32.dll")]
        //public extern static IntPtr WriteFile(
        //  IntPtr hFile,
        //  IntPtr buffer,
        //  uint nNumberOfBytesToWrite,
        //  ref uint lpNumberOfBytesWritten,
        //  IntPtr lpOverlapped);

        [DllImport("SerialComm.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int Open(int port);

        [DllImport("SerialComm.dll",CallingConvention = CallingConvention.Cdecl)]
        public extern static void Close();


        [DllImport("SerialComm.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte ReadByte();


        [DllImport("SerialComm.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetInputBufferCount();




        //private WindowsSerialPortDevice _serial = null;
        private IntPtr _handle = IntPtr.Zero;

        private const Byte STX = 0x02;
        private const Byte ETX = 0x0D;

        private bool _bStopRead = false;
        private Thread _readThread = null;

       // private CancellationTokenSource _cts = new CancellationTokenSource();
        private string _current_barcode = "";
        private string _strBarcode = "";

        public void Clear()
        {
        	while(GetInputBufferCount() > 0)
        	{
        		ReadByte();
        	}
        	
        	getBarcode(500);
        }


        public void ReadThread()
        {
            Stopwatch wh = new Stopwatch();

            try
            {
                wh.Start();

                //wait for CR
                bool bsuffix = false;
                do
                {
                    if (GetInputBufferCount() > 0)
                    {
                        //read data
                        var r = ReadByte();

                        if (r == ETX)
                        {
                            bsuffix = true;
                            //break;
                        }
                        else{
                        	_strBarcode = _strBarcode + (Char)r;
                        }
                        
                        if (bsuffix)
                        {
                            bsuffix = false;
                            _current_barcode = _strBarcode;
                        }

                    }
                    else
                    {
                        Thread.Sleep(50);
                    }

                } while (!_bStopRead);

            }
            catch(OperationCanceledException e)
            {
                //log exit event;
            }
            catch (Exception ex)
            {
                //log error
                throw ex;
            }

        }

        public string getBarcode(int nTimeout)
        {
            string ret = _current_barcode;
            _current_barcode = "";
            _strBarcode = "";
            return ret;
        }

        public bool Init(string paramters)
        {
            string nPort;
            int nBaudRate;

            try
            {
                string[] setting = paramters.Split(',');

                nPort = setting[0];
                nBaudRate = Convert.ToInt32(setting[1]);

                if(Open(int.Parse(nPort)) != 1) return false;
                
                if(_handle.ToInt32() == -1 && _handle.ToInt32() == 0)
                {
                    return false;
                }
 
          
                _current_barcode = "";
                _bStopRead = false;
                _readThread = new Thread(ReadThread);
                _readThread.Start();

                return true;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Quit()
        {
            try
            {
       
                _bStopRead = true;
                             
                if(_readThread != null)
                {
                    while(_readThread.ThreadState != System.Threading.ThreadState.Stopped)
                    {
                        Thread.Sleep(10);
                    }
                }
                _readThread = null;

                Close();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool isBarcodeReady(Func<bool> sensor = null)
        {
            return !string.IsNullOrEmpty(_current_barcode);
        }
    }
}
