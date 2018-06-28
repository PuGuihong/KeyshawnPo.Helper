using KeyshawnPo.Helper.Lib;
using KeyshawnPo.Helper.Lib.Applay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ValueTuple;

namespace Dao.AdoNet.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //ReadData();
            GetHardInfo();
        }


        public static void ReadData()
        {
            //DataSet _ds = DbHelperSQL.Query("select * from Account;");
            DataSet _ds = SearchAllDB();
            SqlConnection _dbConn = new SqlConnection();


            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0] != null)
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    string _rltMsg = string.Empty;
                    bool _setRlt = DbHelperSQL.SetConn(ref _dbConn, ref _rltMsg);
                    //string _connStr = _ds.Tables[0].Rows[i][2].ToString();
                    //DbHelperSQL.connectionString = _connStr;

                    string _dbName = _ds.Tables[0].Rows[i][0].ToString();
                    //查出原始数据
                    //if (false == _setRlt) continue;

                    string _url = System.AppDomain.CurrentDomain.BaseDirectory;
                    _url += "/ColectScript/";
                    List<string> _lstFileName = new List<string>();
                    _lstFileName.Add(@_url + "G3_CompactIndex.sql");
                    _lstFileName.Add(@_url + "G5_CompactInfoRecipient.sql");
                    _lstFileName.Add(@_url + "G5_CompactInfoSend.sql");
                    DataSet _DataSetRlt = new DataSet();
                    foreach (var item in _lstFileName)
                    {
                        string _strSql = FileHelper.ReadFile(item);
                        _DataSetRlt = DbHelperSQL.Query(_dbConn, _strSql, "MInfoSet", _dbName);
                        if (_DataSetRlt == null || _DataSetRlt.Tables == null || _DataSetRlt.Tables.Count < 0)
                            continue;
                        DbHelperSQL.connectionString = "Data Source=.;Initial Catalog=MyDatabase;User ID=sa;Password=Z6626294@";
                        _setRlt = DbHelperSQL.SetConn(ref _dbConn, ref _rltMsg);
                        if (false == _setRlt) continue;
                        SqlHelper.BulkInsert("Data Source=.;Initial Catalog=MyDatabase;User ID=sa;Password=Z6626294@", _DataSetRlt.Tables[0]);
                    }

                    DbHelperSQL.CloseConn(_dbConn);
                }
            }
        }

        public static DataSet SearchAllDB()
        {
            DataSet _ds = DbHelperSQL.Query("SELECT name, database_id, create_date  FROM sys.databases ; ");
            return _ds;
        }

        public void GetHardInfo()
        {
            StringBuilder _strHardInfo = new StringBuilder();
            //cpu
            Tuple<string, string> _cpuInfo = HardInfoHelper.Instance.GetCPU();
            _strHardInfo.Append("CPU     ：" + _cpuInfo + "\n");
            string _strCpuCount = HardInfoHelper.Instance.GetCPU_Count();
            _strHardInfo.Append("核心数  ：" + _cpuInfo + "\n");
            string _strOsVertion = HardInfoHelper.Instance.GetOS_Version();
            _strHardInfo.Append("系统版本：" + _strOsVertion + "\n");
            string _strDiskSize = HardInfoHelper.Instance.GetDiskSize();
            _strHardInfo.Append("硬盘容量：" + _strDiskSize + "\n");
            string _strDisSearia = HardInfoHelper.Instance.GetDiskSerialNumber();
            _strHardInfo.Append("硬盘序列：" + _strDisSearia + "\n");
            List<Tuple<string, string, string, string>> _strLogicSearia = HardInfoHelper.Instance.GetLogicDisk();
            _strHardInfo.Append("硬盘分区：\n");
            if (_strLogicSearia != null && _strLogicSearia.Count() > 0)
            {
                foreach (var item in _strLogicSearia)
                {
                    _strHardInfo.Append(item + "\n");
                }
            }

            lbHardInfo.Text = _strHardInfo.ToString();
        }

    }
}
