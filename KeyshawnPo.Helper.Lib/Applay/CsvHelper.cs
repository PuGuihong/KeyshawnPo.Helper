using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Helper.Lib
{
    /// <summary>
    /// CSV文件转换类
    /// </summary>
    public class CsvHelper
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private CsvHelper()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static CsvHelper instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();
        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static CsvHelper Instance
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
                            instance = new CsvHelper();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static bool DtToCsv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                if (string.IsNullOrEmpty(strFilePath))
                {
                    return false;
                }
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
                if (!string.IsNullOrEmpty(tableheader))
                {
                    strmWriterObj.WriteLine(tableheader);
                }
                if (!string.IsNullOrEmpty(columname))
                {
                    strmWriterObj.WriteLine(columname);
                }

                string strBufferLine = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable CsvToDt(string filePath, int n)
        {
            DataTable dt = new DataTable();
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0;
            int m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
