using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KeyshawnPo.Helper.Lib.Applay.WidGet
{
    public static class WeGitSelectListItem
    {
        /// <summary>
        /// 创建 SelectItem 组件
        /// </summary>
        /// <typeparam name="T">原始数据模型</typeparam>
        /// <param name="lstT">传入的数据实例</param>
        /// <param name="text">转换的显示文本</param>
        /// <param name="value">转换的选项值</param>
        /// <param name="selectValue">根据传入值进行选中</param>
        /// <param name="addDefault">是否添加默认选择项</param>
        /// <param name="addDefaultText">默认选择项显示文本</param>
        /// <param name="addDefaultValue">默认选择项值</param>
        /// <returns></returns>
        public static List<SelectListItem> CreateListSelectItem<T>(this IList<T> lstT, string text, string value, string selectValue, bool addDefault = true, string addDefaultText = "全部", string addDefaultValue = "-1")
        {
            List<SelectListItem> _lstSelectListIttem = new List<SelectListItem>();
            //添加默认选择项
            if (addDefault)
            {
                _lstSelectListIttem.Add(new SelectListItem { Text = addDefaultText, Value = addDefaultValue });
            }
            //遍历生成SelectListItem ,根据传入的selectValue 值，判断是否进行选中
            foreach (var _itemT in lstT)
            {
                var _propers = _itemT.GetType().GetProperty(text);
                var _valpropers = _itemT.GetType().GetProperty(value);
                if (selectValue == _valpropers.GetValue(_itemT, null).ToString())
                {
                    _lstSelectListIttem.Add(new SelectListItem { Text = _propers.GetValue(_itemT, null).ToString(), Value = _valpropers.GetValue(_itemT, null).ToString(), Selected = true });
                }
                else
                {
                    _lstSelectListIttem.Add(new SelectListItem { Text = _propers.GetValue(_itemT, null).ToString(), Value = _valpropers.GetValue(_itemT, null).ToString() });
                }
            }
            return _lstSelectListIttem;
        }
    }
}
