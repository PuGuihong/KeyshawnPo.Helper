using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace KeyshawnPo.SwaggerDemo.App_Start
{
    public delegate void ConfigChangedDelegate(GlobalConfigModel oldConfig, GlobalConfigModel newConfig);
    public class ConfigClient
    {
        private static object lockObj = new object();
        private static string configServer = string.Empty;
        private static GlobalConfigModel model = null;

        /// <summary>
        /// 配置改变事件
        /// </summary>
        public static event ConfigChangedDelegate OnConfigChanged;

        static ConfigClient()
        {
            configServer = ConfigurationManager.AppSettings["configServer"];
            OnConfigChanged += ConfigClient_OnConfigChanged;
        }

        /// <summary>
        /// 获取当前配置
        /// </summary>
        public static GlobalConfigModel CurrentConfig
        {
            get
            {
                if (model == null)
                {
                    CheckLoadConfig();
                }
                return model;
            }
        }

        /// <summary>
        /// 加载本地缓存配置
        /// </summary>
        private static void LoadCacheConfig()
        {
            string file = System.Web.Hosting.HostingEnvironment.MapPath("~/Config/config.json");
            if (File.Exists(file))
            {
                string json = System.IO.File.ReadAllText(file, Encoding.UTF8);
                model = JsonConvert.DeserializeObject<GlobalConfigModel>(json);
            }
        }

        /// <summary>
        /// 保存本地缓存配置
        /// </summary>
        /// <param name="json"></param>
        private static void SaveCahceConfig(string json)
        {
            string file = System.Web.Hosting.HostingEnvironment.MapPath("~/Config/config.json");
            using (StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                sw.Write(json);
                sw.Flush();
            }
        }

        /// <summary>
        /// 检查配置更新
        /// </summary>
        public static void CheckLoadConfig()
        {
            lock (lockObj)
            {
                if (model == null)
                {
                    LoadCacheConfig();
                }
                try
                {
                    string json = HttpClient.HttpGet(configServer);
                    if (!string.IsNullOrEmpty(json))
                    {
                        var newConfig = JsonConvert.DeserializeObject<GlobalConfigModel>(json);
                        if (model == null || newConfig.LastVersion > model.LastVersion)
                        {
                            var oldConfig = model;
                            model = newConfig;
                            SaveCahceConfig(json);
                            if (OnConfigChanged != null)
                            {
                                OnConfigChanged(oldConfig, newConfig);
                            }
                        }
                    }
                }
                catch (Exception ex) { } //吃掉加载异常
            }
        }

        /// <summary>
        /// 配置改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConfigClient_OnConfigChanged(GlobalConfigModel oldConfig, GlobalConfigModel newConfig)
        {
            if (!string.IsNullOrEmpty(newConfig.IcoUrl))
            {
                HttpClient.DownloadFile(GetConfigServerPath() + newConfig.IcoUrl, System.Web.Hosting.HostingEnvironment.MapPath("~/favicon.ico"));
            }
            //数据库连接有变化
            if (!oldConfig.DbServer.Equals(newConfig.DbServer)
                    || !oldConfig.DbUserName.Equals(newConfig.DbUserName)
                    || !oldConfig.DbPassword.Equals(newConfig.DbPassword)
                    || !oldConfig.Database.Equals(newConfig.Database))
            {
                string file = System.Web.Hosting.HostingEnvironment.MapPath("~/bin/dbinfo.txt");
                using (StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}检测到数据库连接变更！");
                    sw.Flush();
                }
            }
        }
        public static string GetConfigServerPath()
        {
            int index = configServer.IndexOf("/Config/Check?appid", StringComparison.CurrentCultureIgnoreCase);
            if (index > 0)
            {
                return configServer.Substring(0, index);
            }
            return configServer;
        }


    }
}