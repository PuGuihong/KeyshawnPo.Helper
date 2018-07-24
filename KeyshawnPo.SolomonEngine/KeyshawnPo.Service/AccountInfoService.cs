using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KeyshawnPo.Service
{
    public class AccountInfoService
    {
        #region Property
        /// <summary>
        /// 获取用户session
        /// 超时或未登录返回null
        /// </summary>
        public static UserSession UserInfo
        {
            get { return HttpContext.Current.Session["UserInfo"] as UserSession; }
            set { HttpContext.Current.Session.Add("UserInfo", value); }
        }

        /// <summary>
        /// 获取用户session
        /// 超时或未登录返回null
        /// </summary>
        public static UserCookie UserInfoByCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
                {
                    string json = HttpContext.Current.Request.Cookies["UserInfo"].Value;
                    return DeSerialize(json);
                }
                else if (UserInfo != null)
                {
                    UserCookie user = new UserCookie();
                    //user.Department = UserInfo.Mechanism;
                    //user.User = UserInfo.User;
                    return user;
                }
                return null;
            }
            set
            {
                string json = Serialize(value);
                HttpCookie userInfo = new HttpCookie("UserInfo");
                userInfo.Value = json;
                userInfo.Expires = DateTime.Now.AddDays(11);
                HttpContext.Current.Request.Cookies.Add(userInfo);
            }
        }
        #endregion

        #region Method

        #region Public Method
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Serialize(UserCookie user)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            ser.Serialize(mStream, user);
            byte[] buf = mStream.ToArray();
            mStream.Close();
            return Convert.ToBase64String(buf);
        }

        /// <summary>
        /// 将从Cookie中取出的字符串反序列化成一个FGTWeb.Model.M_Login类
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static UserCookie DeSerialize(string strUserCookie)
        {
            byte[] binary = Convert.FromBase64String(strUserCookie);
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream(binary);
            UserCookie o = (UserCookie)ser.Deserialize(mStream);
            mStream.Close();
            return o;
        }

        ///// <summary>
        ///// 设置用户Session
        ///// </summary>
        ///// <param name="user">用户信息</param>
        //public static void SetUserSession(SYS_USER_MODEL user)
        //{
        //    if (user == null)
        //    {
        //        UserInfo = null;
        //        return;
        //    }

        //    UserSession session = new UserSession();
        //    session.User = user;

        //    //设置用户角色信息
        //    session.Role = SYS_ROLE_BLL.Instance.Dal.Find(user.ROLEID);

        //    List<SYS_MENU_MODEL> mList = new List<SYS_MENU_MODEL>();

        //    //设置用户权限信息
        //    session.Permissions = GetUserPermissions(session.Role, user, ref mList);

        //    if (user.LOGINNAME.Trim().ToUpper() == "ADMIN")
        //    {
        //        session.MenuList = mList;
        //    }
        //    else
        //    {
        //        //设置用户菜单信息
        //        session.MenuList = GetMenuList(string.Join(",", session.Permissions.Keys), user.LOGINNAME).OrderBy(li => li.SORTCODE).ToList();
        //    }

        //    //设置用户快捷菜单
        //    session.QuickMenu = GetUserQuickMenu(user.LOGINNAME);

        //    UserInfo = session;
        //}
        //#endregion

        //#region Private Method
        ///// <summary>
        ///// 获取可操作部门信息
        ///// </summary>
        ///// <param name="user">用户信息</param>
        ///// <returns>部门集合</returns>
        //private static List<SYS_DEPARTMENT_MODEL> GetMechanismList(SYS_USER_MODEL user)
        //{
        //    string bmbh = user.SSBMBH;
        //    return SYS_DEPARTMENT_BLL.Instance.GetSubMechanismList(bmbh, 2, false);
        //}


        ///// <summary>
        ///// 获取用户权限
        ///// </summary>
        ///// <param name="role">角色信息</param>
        ///// <returns>用户权限</returns>
        //private static Dictionary<string, List<string>> GetUserPermissions(SYS_ROLE_MODEL role, SYS_USER_MODEL user, ref List<SYS_MENU_MODEL> mList)
        //{
        //    Dictionary<string, List<string>> dict_permis = new Dictionary<string, List<string>>();
        //    if (user.LOGINNAME.Trim().ToUpper() == "ADMIN")
        //    {
        //        #region 超级管理员
        //        string mids = string.Empty;
        //        mList = GetMenuList("", "admin");
        //        if (mList != null)
        //        {
        //            mList = mList.OrderBy(li => li.SORTCODE).ToList();
        //            foreach (var item in mList.FindAll(li => li.LEVELNUM == 3))
        //            {
        //                mids += ",'" + item.MID + "'";
        //            }
        //            mids = mids.Trim(',');
        //            List<SYS_MODULE_MODEL> moList = SYS_MODULE_BLL.Instance.GetListByMIDS(mids);
        //            foreach (var item in moList)
        //            {
        //                if (!string.IsNullOrEmpty(item.FUNCTIONS))
        //                {
        //                    dict_permis.Add(item.ID.ToString(), item.FUNCTIONS.Split(',').ToList());
        //                }
        //                else
        //                {
        //                    dict_permis.Add(item.ID.ToString(), new List<string>());
        //                }
        //            }
        //        }
        //        #endregion
        //    }
        //    else if (role != null && !string.IsNullOrEmpty(role.PERMISSIONS))
        //    {
        //        dict_permis = DesDecryptPermis(role.PERMISSIONS);
        //    }
        //    else if ((!string.IsNullOrEmpty(user.PERMISSIONS)))
        //    {
        //        dict_permis = DesDecryptPermis(user.PERMISSIONS);
        //    }
        //    return dict_permis;
        //}

        ///// <summary>
        ///// 解密权限
        ///// </summary>
        ///// <param name="permissions">权限信息</param>
        ///// <returns>权限信息</returns>
        //public static Dictionary<string, List<string>> DesDecryptPermis(string permissions)
        //{
        //    if (string.IsNullOrEmpty(permissions)) return new Dictionary<string, List<string>>();
        //    Dictionary<string, List<string>> dict_permis = new Dictionary<string, List<string>>();
        //    string p = SecureHelper.DesDecrypt(permissions, SysParam.SecretKey);
        //    string[] permis = p.Split(';');
        //    foreach (var item in permis)
        //    {
        //        if (!string.IsNullOrEmpty(item))
        //        {
        //            string key = item.Split('|')[0];
        //            if (item.Contains("|"))
        //            {
        //                List<string> value = item.Split('|')[1].Split(',').ToList();
        //                if (dict_permis.ContainsKey(key))
        //                {
        //                    value.AddRange(dict_permis[key]);
        //                    dict_permis[key] = value;
        //                }
        //                else
        //                {
        //                    dict_permis.Add(key, value);
        //                }
        //            }
        //            else
        //            {
        //                dict_permis.Add(key, new List<string>());
        //            }
        //        }
        //    }
        //    return dict_permis;
        //}

        ///// <summary>
        ///// 获取用户快捷菜单集合
        ///// </summary>
        ///// <param name="loginName">登录账号</param>
        ///// <returns>快捷菜单集合，若不存在返回空字符串</returns>
        //private static string GetUserQuickMenu(string loginName)
        //{
        //    SYS_QUICK_MENU_MODEL quick = SYS_QUICK_MENU_BLL.Instance.FindByKey(loginName);
        //    if (quick != null)
        //    {
        //        return quick.Mids;
        //    }
        //    return "";
        //}

        ///// <summary>
        ///// 获取用户具有权限的菜单
        ///// </summary>
        ///// <param name="mids">权限标识</param>
        ///// <param name="loginName">账号</param>
        ///// <returns>符合条件的菜单集合</returns>
        //private static List<SYS_MENU_MODEL> GetMenuList(string mids, string loginName)
        //{
        //    if (loginName.Trim() == "admin")
        //    {
        //        mids = "-1";
        //    }
        //    return SYS_MENU_BLL.Instance.GetListByMIDS(mids);
        //}
        //#endregion

        //#region 根据部门编号获取下级机构集合
        ///// <summary>
        ///// 获取指定ID下的所有机构编号集合
        ///// </summary>
        ///// <param name="id">标识</param>
        ///// <returns></returns>
        //public static List<SYS_DEPARTMENT_MODEL> GetSubMechanismList(string ssbmbh)
        //{
        //    List<SYS_DEPARTMENT_MODEL> list = new List<SYS_DEPARTMENT_MODEL>();
        //    if (CacheDictionary.MechanismList.Exists(li => li.BMBH == ssbmbh))
        //    {
        //        decimal id = CacheDictionary.MechanismList.Find(li => li.BMBH == ssbmbh).ID;
        //        Add(id, ref list);
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 添加机构信息
        ///// </summary>
        ///// <param name="id">标识</param>
        ///// <param name="list">集合</param>
        //private static void Add(decimal id, ref List<SYS_DEPARTMENT_MODEL> list)
        //{
        //    List<SYS_DEPARTMENT_MODEL> mlist = CacheDictionary.MechanismList.Where(li => li.PARENTID == id).ToList();
        //    foreach (var item in mlist)
        //    {
        //        list.Add(item);
        //        Add(item.ID, ref list);
        //    }
        //}
        //#endregion
        //#endregion
    }
}
