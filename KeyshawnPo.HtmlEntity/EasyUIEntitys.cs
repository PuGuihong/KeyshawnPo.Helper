using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.HtmlEntity
{
    public class EasyUIEntitys
    {
    }

    /// <summary>
    /// Datagrid 数据格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public struct Datagrid<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }

    /// <summary>
    /// Combobox 数据格式
    /// </summary>
    [Serializable]
    public struct Combobox
    {
        /// <summary>
        /// 值
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string text { get; set; }
    }

    /// <summary>
    /// Combobox 数据格式
    /// </summary>
    [Serializable]
    public struct Combotree
    {
        /// <summary>
        /// value的值
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// text显示的值
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 子列表
        /// </summary>
        public List<Combotree> children { get; set; }
    }

    /// <summary>
    /// Tree 数据格式
    /// </summary>
    [Serializable]
    public struct Tree
    {
        public object id { get; set; }
        public object text { get; set; }
        public string iconCls { get; set; }
        public string state { get; set; }

        public attributes attributes { get; set; }

        public List<Tree> children { get; set; }
    }

    [Serializable]
    public struct attributes
    {
        public string url { get; set; }
    }

    /// <summary>
    /// Treegrid 数据格式
    /// </summary>
    [Serializable]
    public struct Treegrid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string size { get; set; }

        public string date { get; set; }
        public string state { get; set; }

        public List<Treegrid> children { get; set; }
    }
}
