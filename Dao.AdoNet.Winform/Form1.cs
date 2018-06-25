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

namespace Dao.AdoNet.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReadData();
        }


        public static void ReadData()
        {
            DataSet _ds = DbHelperSQL.Query("select * from Account;");
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0] != null)
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    string _rltMsg = string.Empty;
                    string _connStr = _ds.Tables[0].Rows[i][2].ToString();
                    DbHelperSQL.connectionString = _connStr;
                    SqlConnection _dbConn = new SqlConnection();
                    bool _setRlt = DbHelperSQL.SetConn(ref _dbConn, ref _rltMsg);
                    //查出原始数据
                    if (false == _setRlt) continue;
                    DataSet _DataSetRlt = DbHelperSQL.Query(" select * from CompactIndex");

                    DbHelperSQL.connectionString = "Data Source=.;Initial Catalog=MyDatabase;User ID=sa;Password=Z6626294@";
                    _setRlt = DbHelperSQL.SetConn(ref _dbConn, ref _rltMsg);
                    if (false == _setRlt) break;
                    //插入表数据
                    SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();
                    SqlCommandBuilder _SqlCommandBuilder = new SqlCommandBuilder(_SqlDataAdapter);
                    string _strSqlSelect = "select * from CompactIndex";
                    //string _strSqlInsert = "Insert Into CompactIndex([BillID]) values(@BillID)";
                    _SqlDataAdapter.SelectCommand = new SqlCommand(_strSqlSelect, _dbConn);
                    //_SqlDataAdapter.InsertCommand = new SqlCommand(_strSqlInsert, _dbConn);
                    _SqlDataAdapter.InsertCommand = new SqlCommand();
                    _SqlDataAdapter.TableMappings.Add("ds", "CompactIndex");   // 设置对象名称 
                    _SqlDataAdapter.Fill(_DataSetRlt, "CompactIndex");
                    _SqlDataAdapter.InsertCommand = _SqlCommandBuilder.GetInsertCommand();
                    _SqlDataAdapter.DeleteCommand = _SqlCommandBuilder.GetDeleteCommand();
                    _SqlDataAdapter.UpdateCommand = _SqlCommandBuilder.GetUpdateCommand();

                    //foreach (DataRow item in _DataSetRlt.Tables[0].Rows)
                    //{
                    //    item.SetAdded();
                    //}

                    int _intRlt = _SqlDataAdapter.Update(_DataSetRlt, "CompactIndex");

                    DbHelperSQL.CloseConn(_dbConn);
                }
            }
        }
    }
}
