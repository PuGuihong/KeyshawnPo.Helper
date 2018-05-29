using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KeyshawnPo.Helper.Lib
{
    /// <summary>
    /// 获取MD5值帮助类
    /// </summary>
    public class MD5Encrypt
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private MD5Encrypt()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static MD5Encrypt instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();
        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static MD5Encrypt Instance
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
                            instance = new MD5Encrypt();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 得到字符串的md5值
        /// </summary>
        /// <param name="str">传入的字符串</param>
        /// <returns></returns>
        public static string GetStrMD5(string str)
        {
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] bt = md.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.Append(bt[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到文件的md5值
        /// </summary>
        /// <param name="str">传入的文件路径</param>
        /// <returns></returns>
        public static string GetFileMD5(string path)
        {
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] bt;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bt = md.ComputeHash(fs);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.Append(bt[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
