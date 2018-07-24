using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    /// <summary>
    /// 功能类型
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 不校验
        /// </summary>
        None = 0,
        /// <summary>
        /// 访问
        /// </summary>
        Access = 1,
        /// <summary>
        /// 添加操作
        /// </summary>
        Insert = 2,
        /// <summary>
        /// 删除操作
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 修改操作
        /// </summary>
        Update = 4,
        /// <summary>
        /// 审核
        /// </summary>
        Audit = 5,
        /// <summary>
        /// 领取
        /// </summary>
        Draw = 6,
        /// <summary>
        /// 接收
        /// </summary>
        Receive = 7,
        /// <summary>
        /// 打印
        /// </summary>
        Print = 8,
        /// <summary>
        /// 处理
        /// </summary>
        Handle = 9,
        /// <summary>
        /// 通报回复
        /// </summary>
        HF = 10,
        /// <summary>
        /// 通报回复审核
        /// </summary>
        HFSH = 11,
        /// <summary>
        /// 抽查
        /// </summary>
        SpotCheck = 12,
        /// <summary>
        /// 作废
        /// </summary>
        Nullify = 13,
        /// <summary>
        /// 导出Excel
        /// </summary>
        ExportExcel = 14
    }
}

