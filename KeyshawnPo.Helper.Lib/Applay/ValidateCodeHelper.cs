using System.Drawing;

namespace KeyshawnPo.Helper.Lib
{
    /// <summary>
    /// 随机验证码 
    /// Verion:
    /// Description:随机生成设定验证码，并随机旋转一定角度，字体颜色不同
    /// </summary>
    public class ValidateCodeHelper
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private ValidateCodeHelper()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static ValidateCodeHelper instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();

        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static ValidateCodeHelper Instance
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
                            instance = new ValidateCodeHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        /// 
        /// 生成随机码随机码个数
        /// 
        public static string CreateRandomCode(int length)
        {
            string randomcode = string.Empty;
            //生成一定长度的验证码
            var random = new System.Random();
            while (randomcode.Length <= length)
            {
                randomcode += random.Next(0, 10).ToString();
            }
            return randomcode;
        }

        /// 
        /// 创建随机码图片
        /// 
        /// 随机码
        public static void CreateImage(string checkCode)
        {
            int iwidth = checkCode.Length * 13;
            var image = new System.Drawing.Bitmap(iwidth, 23);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色 
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体 
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            System.Random rand = new System.Random();
            //随机输出噪点 
            for (int i = 0; i < 50; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //输出不同字体和颜色的验证码字符 
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new Font(font[findex], 10, FontStyle.Bold);
                Brush b = new SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * 12), ii);
            }
            //画一个边框 
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);
            //输出到浏览器 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Web.HttpContext.Current.Response.ClearContent();
            //Response.ClearContent(); 
            System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
            System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}
