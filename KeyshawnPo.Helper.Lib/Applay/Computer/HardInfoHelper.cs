using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Helper.Lib.Applay
{
    public class HardInfoHelper
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private HardInfoHelper()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static HardInfoHelper instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();

        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static HardInfoHelper Instance
        {
            get
            {
                //实例不存在则创建
                if (instance == null)
                {
                    //当线程来的时候进程先挂起
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new HardInfoHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region 概览
        /// <summary>
        /// 电脑型号
        /// </summary>
        public string GetVersion()
        {
            string _str = "查询失败";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_ComputerSystemProduct.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    _str = m[WindowsAPIKeys.Version.ToString()].ToString(); break;
                }
            }
            catch
            {

            }
            return _str;
        }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string GetOS_Version()
        {
            string _str = "Windows 10";
            try
            {
                string _hdId = string.Empty;
                ManagementClass _hardDisk = new ManagementClass(WindowsAPIType.Win32_OperatingSystem.ToString());
                ManagementObjectCollection _hardDiskC = _hardDisk.GetInstances();
                foreach (ManagementObject m in _hardDiskC)
                {
                    _str = m[WindowsAPIKeys.Name.ToString()].ToString().Split('|')[0].Replace("Microsoft", "");
                    break;
                }
            }
            catch
            {

            }
            return _str;
        }

        /// <summary>  
        /// 操作系统的登录用户名  
        /// </summary>  
        /// <returns></returns>   
        public string GetUserName()
        {
            return Environment.UserName;
        }


        /// <summary>  
        /// 获取计算机名  
        /// </summary>  
        /// <returns></returns>  
        public string GetComputerName()
        {
            return Environment.MachineName;
        }

        #endregion

        #region cpu
        /// <summary>  
        /// CPU版本信息  
        /// </summary>  
        /// <returns></returns>  
        public string GetCpuVersion()
        {
            var st = string.Empty;
            var mos = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (var o in mos.Get())
            {
                var mo = (ManagementObject)o;
                st = mo["Version"].ToString();
            }
            return st;
        }

        /// <summary>  
        /// CPU名称信息  
        /// </summary>  
        /// <returns></returns>  
        public string GetCpuName()
        {
            string _st = string.Empty;
            var driveId = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (var o in driveId.Get())
            {
                var _mo = (ManagementObject)o;
                _st = _mo["Name"].ToString();
            }
            return _st;
        }

        /// <summary>  
        /// CPU制造厂商  
        /// </summary>  
        /// <returns></returns>  
        public string GetCpuManufacturer()
        {
            var _st = string.Empty;
            var _mos = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (var o in _mos.Get())
            {
                var _mo = (ManagementObject)o;
                _st = _mo["Manufacturer"].ToString();
            }
            return _st;
        }

        /// <summary>  
        /// 获得CPU编号  
        /// </summary>  
        /// <returns></returns>  
        public string GetCpuid()
        {
            string _strCpuid = string.Empty;
            var _mc = new ManagementClass("Win32_Processor");
            var _moc = _mc.GetInstances();
            foreach (var o in _moc)
            {
                var _mo = (ManagementObject)o;
                _strCpuid = _mo.Properties["ProcessorId"].Value.ToString();
            }
            return _strCpuid;
        }

        /// <summary>
        /// 查找cpu的名称，主频, 核心数
        /// </summary>
        /// <returns></returns>
        public Tuple<string, string> GetCPU()
        {
            Tuple<string, string> _result = null;
            try
            {
                string _str = string.Empty;
                ManagementClass _mcCPU = new ManagementClass(WindowsAPIType.Win32_Processor.ToString());
                ManagementObjectCollection _mocCPU = _mcCPU.GetInstances();
                foreach (ManagementObject m in _mocCPU)
                {
                    string name = m[WindowsAPIKeys.Name.ToString()].ToString();
                    string[] parts = name.Split('@');
                    _result = new Tuple<string, string>(parts[0].Split('-')[0] + "处理器", parts[1]);
                    break;
                }
            }
            catch
            {

            }
            return _result;
        }



        /// <summary>
        /// 获取cpu核心数
        /// </summary>
        /// <returns></returns>
        public string GetCPU_Count()
        {
            string _str = "查询失败";
            try
            {
                int _coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from " + WindowsAPIType.Win32_Processor.ToString()).Get())
                {
                    _coreCount += int.Parse(item[WindowsAPIKeys.NumberOfCores.ToString()].ToString());
                }
                if (_coreCount == 2)
                {
                    return "双核";
                }
                _str = _coreCount.ToString() + "核";
            }
            catch
            {

            }
            return _str;
        }
        #endregion

        #region 内存
        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        public string GetPhisicalMemory()
        {
            ManagementObjectSearcher _searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象 
            _searcher.Query = new SelectQuery(WindowsAPIType.Win32_PhysicalMemory.ToString(), "", new string[] { WindowsAPIKeys.Capacity.ToString() });//设置查询条件 
            ManagementObjectCollection _collection = _searcher.Get();   //获取内存容量 
            ManagementObjectCollection.ManagementObjectEnumerator em = _collection.GetEnumerator();

            long _capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject _baseObj = em.Current;
                if (_baseObj.Properties[WindowsAPIKeys.Capacity.ToString()].Value != null)
                {
                    try
                    {
                        _capacity += long.Parse(_baseObj.Properties[WindowsAPIKeys.Capacity.ToString()].Value.ToString());
                    }
                    catch
                    {
                        return "查询失败";
                    }
                }
            }
            return TypeHelper.BitToGB((double)_capacity, 1024.0);
        }

        #endregion

        #region 硬盘
        /// <summary>
        /// 获取硬盘容量
        /// </summary>
        public string GetDiskSize()
        {
            string _result = string.Empty;
            StringBuilder _sb = new StringBuilder();
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_DiskDrive.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    long capacity = Convert.ToInt64(m[WindowsAPIKeys.Size.ToString()].ToString());
                    _sb.Append(TypeHelper.BitToGB(capacity, 1000.0) + "+");
                }
                _result = _sb.ToString().TrimEnd('+');
            }
            catch
            {

            }
            return _result;
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetDiskSerialNumber()
        {
            //这种模式在插入一个U盘后可能会有不同的结果，如插入我的手机时
            var hDid = string.Empty;
            var mc = new ManagementClass("Win32_DiskDrive");
            var moc = mc.GetInstances();
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                hDid = (string)mo.Properties["Model"].Value;
                //这名话解决有多个物理盘时产生的问题，只取第一个物理硬盘
                break;
            }
            return hDid;
        }

        /// <summary>
        /// 获取逻辑分区信息: 驱动器名称，根目录，可用空间，总空间
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, string, string, string>> GetLogicDisk()
        {
            List<Tuple<string, string, string, string>> _lstTupleDriver = new List<Tuple<string, string, string, string>>();
            DriveInfo[] _arrDriver = null;
            _arrDriver = System.IO.DriveInfo.GetDrives();
            if (_arrDriver != null && _arrDriver.Count() > 0)
            {
                foreach (var _driver in _arrDriver)
                {
                    Tuple<string, string, string, string> _tupleDriver = new Tuple<string, string, string, string>(_driver.Name, _driver.RootDirectory.ToString(), TypeHelper.BitToGB(_driver.TotalFreeSpace, 1000), TypeHelper.BitToGB(_driver.TotalSize, 1000));
                    _lstTupleDriver.Add(_tupleDriver);
                }
            }
            return _lstTupleDriver;
        }
        #endregion

        #region 显卡
        /// <summary>
        /// 获取分辨率
        /// </summary>
        public string GetFenbianlv()
        {
            string _result = "1920*1080";
            try
            {
                string _hdId = string.Empty;
                ManagementClass _hardDisk = new ManagementClass(WindowsAPIType.Win32_DesktopMonitor.ToString());
                ManagementObjectCollection hardDiskC = _hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    _result = m[WindowsAPIKeys.ScreenWidth.ToString()].ToString() + "*" + m[WindowsAPIKeys.ScreenHeight.ToString()].ToString();
                    break;
                }
            }
            catch
            {

            }
            return _result;
        }

        /// <summary>  
        /// 显卡PNPDeviceID  
        /// </summary>  
        /// <returns></returns>  
        public string GetVideoPnpid()
        {
            var st = "";
            var mos = new ManagementObjectSearcher("Select * from Win32_VideoController");
            foreach (var o in mos.Get())
            {
                var mo = (ManagementObject)o;
                st = mo["PNPDeviceID"].ToString();
            }
            return st;
        }

        /// <summary>
        /// 显卡 芯片,显存大小
        /// </summary>
        public Tuple<string, string> GetVideoController()
        {
            Tuple<string, string> _result = null;
            try
            {

                ManagementClass _hardDisk = new ManagementClass(WindowsAPIType.Win32_VideoController.ToString());
                ManagementObjectCollection _hardDiskC = _hardDisk.GetInstances();
                foreach (ManagementObject m in _hardDiskC)
                {
                    _result = new Tuple<string, string>(m[WindowsAPIKeys.VideoProcessor.ToString()].ToString().Replace("Family", ""), TypeHelper.BitToGB(Convert.ToInt64(m[WindowsAPIKeys.AdapterRAM.ToString()].ToString()), 1024.0));
                    break;
                }
            }
            catch
            {

            }
            return _result;
        }

        #endregion

        #region 网卡
        /// <summary>  
        /// 获取网卡硬件地址  
        /// </summary>  
        /// <returns></returns>   
        public string GetMacAddress()
        {
            var mac = "";
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                if (!(bool)mo["IPEnabled"]) continue;
                mac = mo["MacAddress"].ToString();
                break;
            }
            return mac;
        }

        /// <summary>  
        /// 获取IP地址  
        /// </summary>  
        /// <returns></returns>  
        public string GetIpAddress()
        {
            var st = string.Empty;
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                if (!(bool)mo["IPEnabled"]) continue;
                var ar = (Array)(mo.Properties["IpAddress"].Value);
                st = ar.GetValue(0).ToString();
                break;
            }
            return st;
        }
        #endregion

        #region 声卡
        /// <summary>  
        /// 声卡PNPDeviceID  
        /// </summary>  
        /// <returns></returns>  
        public string GetSoundPnpid()
        {
            var st = string.Empty;
            var mos = new ManagementObjectSearcher("Select * from Win32_SoundDevice");
            foreach (var o in mos.Get())
            {
                var mo = (ManagementObject)o;
                st = mo["PNPDeviceID"].ToString();
            }
            return st;
        }
        #endregion

        #region 主板
        /// <summary>  
        /// 主板制造厂商  
        /// </summary>  
        /// <returns></returns>  
        public string GetBoardManufacturer()
        {
            var query = new SelectQuery("Select * from Win32_BaseBoard");
            var mos = new ManagementObjectSearcher(query);
            var data = mos.Get().GetEnumerator();
            data.MoveNext();
            var board = data.Current;
            return board.GetPropertyValue("Manufacturer").ToString();
        }

        /// <summary>  
        /// 主板编号  
        /// </summary>  
        /// <returns></returns>  
        public string GetBoardId()
        {
            var st = string.Empty;
            var mos = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (var o in mos.Get())
            {
                var mo = (ManagementObject)o;
                st = mo["SerialNumber"].ToString();
            }
            return st;
        }

        /// <summary>  
        /// 主板型号  
        /// </summary>  
        /// <returns></returns>  
        public string GetBoardType()
        {
            var st = string.Empty;
            var mos = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (var o in mos.Get())
            {
                var mo = (ManagementObject)o;
                st = mo["Product"].ToString();
            }
            return st;
        }
        #endregion

    }
}
