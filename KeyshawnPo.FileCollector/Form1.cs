using KeyshawnPo.Helper.Lib;
using KeyshawnPo.Helper.Lib.Applay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyshawnPo.FileCollector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ColectFile("D:\\BaiduNetdiskDownload", "Web.Config");
        }

        //指定需要采集的名称或多个文件名称
        //需要采集的逻辑分区
        //按照指定分区进行采集
        //将采集后的文件进行统一归档，将路径和当前采集时间转换为文件名，进行存储。

        List<string> _lstFileName = new List<string>();

        public List<string> GetLogic(List<string> DirectName)
        {
            List<string> _lstDic = new List<string>();

            List<Tuple<string, string, string, string>> _tupleLogicDriverInfo = HardInfoHelper.Instance.GetLogicDisk();
            if (_tupleLogicDriverInfo.Count() > 0)
            {
                foreach (var item in _tupleLogicDriverInfo)
                {
                    _lstDic.Add(item.Item2);
                }
            }
            return _lstDic;
        }

        public void ColectFile(string DirectName, string fileName)
        {
            //新建目录
            string _appDirc = DirectoryHelper.GetRootDirec();
            string _fileDirc = _appDirc + "CollectFile";
            DirectoryHelper.CreateDirectory(_appDirc + "CollectFile");
            bool _bContains = DirectoryHelper.Contains(DirectName, fileName, true);
            if (_bContains)
            {
                List<string> _arrDic = DirectoryHelper.GetDirectory(DirectName);
                foreach (var item in _arrDic)
                {
                    bool _fileContains = DirectoryHelper.Contains(item, fileName, false);
                    if (_fileContains)
                    {
                        string _fileName = _fileDirc + "\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + fileName;
                        DirectoryHelper.CopyDiskFile(item + "\\" + fileName, _fileName);
                    }
                }
            }
        }


    }
}
