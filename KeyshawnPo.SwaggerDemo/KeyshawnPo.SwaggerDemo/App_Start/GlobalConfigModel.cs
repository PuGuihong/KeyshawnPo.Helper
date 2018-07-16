using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyshawnPo.SwaggerDemo.App_Start
{
    public class GlobalConfigModel
    {
        /// <summary>
        /// 最后修改TimeSpan
        /// </summary>
        public long LastVersion { get; set; }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        [Required]
        public string DbServer { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        [Required]
        public string Database { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        [Required]
        public string DbUserName { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        [Required]
        public string DbPassword { get; set; }

        private string _systemName = string.Empty;
        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        public string SystemName
        {
            get
            {
                if (string.IsNullOrEmpty(_systemName))
                {
                    return "好伙伴";
                }
                return _systemName;
            }
            set
            {
                _systemName = value;
            }
        }

        /// <summary>
        /// 极光推送司机Key
        /// </summary>
        public string DriverPushKey { get; set; }
        /// <summary>
        /// 极光推送司机Secret
        /// </summary>
        public string DriverPushSecret { get; set; }
        /// <summary>
        /// 极光推送承运Key
        /// </summary>
        public string CarrierPushKey { get; set; }
        /// <summary>
        /// 极光推送承运Secret
        /// </summary>
        public string CarrierPushSecret { get; set; }
        /// <summary>
        /// 极光推送发货人Key
        /// </summary>
        public string ShipperPushKey { get; set; }
        /// <summary>
        /// 极光推送发货人Secret
        /// </summary>
        public string ShipperPushSecret { get; set; }
        /// <summary>
        /// 极光推送经济人Key
        /// </summary>
        public string AgentPushKey { get; set; }
        /// <summary>
        /// 极光推送经济人Secret
        /// </summary>
        public string AgentPushSecret { get; set; }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string ImgServer { get; set; }
        /// <summary>
        /// 司机轨迹地址
        /// </summary>
        public string DriverMonitorUrl { get; set; }
        /// <summary>
        /// 百度地图SID
        /// </summary>
        public string BaiduMapSID { get; set; }
        /// <summary>
        /// 百度地图轨迹AK
        /// </summary>
        public string BaidumapAK { get; set; }
        /// <summary>
        /// 百度路径耗时计算公共AK
        /// </summary>
        public string BaidumapGlobalAK { get; set; }
        /// <summary>
        /// 站点图标ICO文件
        /// </summary>
        public string IcoUrl { get; set; }
        /// <summary>
        /// 百度鹰眼API地址
        /// </summary>
        public string YingyanApiUrl { get; set; }
        /// <summary>
        /// PC端登录界面背景图
        /// </summary>
        public string LoginBackGroundImage { get; set; }
        /// <summary>
        /// 系统级电话
        /// </summary>
        public string SystemTel { get; set; }
        /// <summary>
        /// 系统级地址
        /// </summary>
        public string SystemAddress { get; set; }
        /// <summary>
        /// Logo图标
        /// </summary>
        public string LogoImage { get; set; }
        /// <summary>
        /// 系统说明
        /// </summary>
        public string Explain { get; set; }
    }
}
