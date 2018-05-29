using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Helper.Lib
{
    /// <summary>
    /// 2018-05-21
    /// KeyshawnPo.
    /// 指定需要排除的字段。
    /// 实体比较类 用于比较两个实体的异同
    /// 用于对相同实体，相同数据进行Merge 合并。
    /// 用于不同实体的相同字段的合并，并将两个实体异同的部分返回。
    /// </summary>
    public class ModelCompareHelper
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private ModelCompareHelper()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static ModelCompareHelper instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();

        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static ModelCompareHelper Instance
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
                            instance = new ModelCompareHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        /// <summary>
        /// 2018-05-21
        /// KeyshawnPo.
        /// 指定排除字段后的比较和合并。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="M"></typeparam>
        /// <param name="oldModel"></param>
        /// <param name="newModel"></param>
        /// <param name="exceptArry"></param>
        /// <returns></returns>
        public JObject Compare<T, M>(T oldModel, M newModel, string[] exceptArry)
        {

            object _newObj = ExceptPropertis(newModel, exceptArry);
            object _oldObj = ExceptPropertis(oldModel, exceptArry);
            JObject _newJsonObj = _newObj == null ? null : (JObject)_newObj;
            JObject _oldJsonObj = _oldObj == null ? null : (JObject)_oldObj;

            //对相同key 进行合并后的结果
            JObject _strMergyRlt = new JObject();

            //不同key 分别存的结果
            JObject _oldExceRlt = new JObject();
            JObject _newExecRlt = new JObject();


            //如果两个model不相等 且两个对象不为空，即返回更新操作
            if (null != _oldJsonObj && null != _newJsonObj)
            {
                if (!_oldJsonObj.Equals(_newJsonObj))
                {
                    //根据旧表进行更新，旧表字段个数 多于新表字段，旧表字段 少于新表字段
                    foreach (KeyValuePair<string, JToken> _propOld in _oldJsonObj)
                    {
                        KeyValuePair<string, JToken> _kvExists = IsExists(_newJsonObj, _propOld);
                        if (null != _kvExists.Key)
                        {
                            string _displayName = ModelAttributeHelper.Instance.GetDisplayName(typeof(T), _propOld.Key);
                            JProperty _rltJProp = new JProperty(!string.IsNullOrEmpty(_displayName) ? _displayName : _kvExists.Key
                                    ,
                                    (!string.IsNullOrEmpty(_propOld.Value.ToString()) ? _propOld.Value : @"""")
                                    + "->"
                                    + (!string.IsNullOrEmpty(_kvExists.Value.ToString()) ? _kvExists.Value : @"""")
                                    );
                            _strMergyRlt.Add(_rltJProp);
                        }
                        else
                        {
                            JProperty _rltJProp = new JProperty(_propOld.Key, _propOld.Value);
                            _strMergyRlt.Add(_rltJProp);
                        }
                    }
                }
                else
                {
                    //如果两个model全部相等
                    _strMergyRlt = _oldJsonObj;
                }
            }
            //两个对象 不相等 只传递了一个新对象 即 返回新增操作
            else if (null != _newJsonObj)
            {
                foreach (KeyValuePair<string, JToken> _propNew in _newJsonObj)
                {
                    string _displayName = ModelAttributeHelper.Instance.GetDisplayName(typeof(M), _propNew.Key);
                    JProperty _rltJProp = new JProperty(!string.IsNullOrEmpty(_displayName) ? _displayName : _propNew.Key
                                ,
                                @""
                                + "->"
                                + (!string.IsNullOrEmpty(_propNew.Value.ToString()) ? _propNew.Value : @""""));
                    _strMergyRlt.Add(_rltJProp);
                }
            }
            //两个对象不相等 只传递了一个老对象 即返回删除的日志
            else if (null != _oldJsonObj)
            {
                foreach (KeyValuePair<string, JToken> _propOld in _oldJsonObj)
                {
                    string _displayName = ModelAttributeHelper.Instance.GetDisplayName(typeof(T), _propOld.Key);

                    JProperty _rltJProp = new JProperty(!string.IsNullOrEmpty(_displayName) ? _displayName : _propOld.Key
                                ,
                                (!string.IsNullOrEmpty(_propOld.Value.ToString()) ? _propOld.Value : @"""")
                                + "->"
                                + @"");
                    _strMergyRlt.Add(_rltJProp);
                }
            }
            else
            {
                _strMergyRlt = new JObject();
            }
            return _strMergyRlt;
        }

        /// <summary>
        /// 排除字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="exceptArry"></param>
        /// <returns></returns>
        private object ExceptPropertis<T>(T model, string[] exceptArry)
        {
            JObject _jsonRlt = null;
            if (null != model)
            {
                _jsonRlt = new JObject();
                if (0 >= exceptArry.Count()) return _jsonRlt = JObject.Parse(JsonConvert.SerializeObject(model));

                foreach (var except in exceptArry)
                {
                    if ((null != _jsonRlt) && _jsonRlt[except] != null)
                    {
                        _jsonRlt.Remove(except);
                    }
                }
            }

            return _jsonRlt;
        }

        /// <summary>
        /// 判断实体中是否包含某个字段
        /// </summary>
        /// <param name="jObj"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private KeyValuePair<string, JToken> IsExists(JObject jObj, KeyValuePair<string, JToken> prop)
        {
            KeyValuePair<string, JToken> _bRlt = new KeyValuePair<string, JToken>();
            foreach (var item in jObj)
            {
                if (item.Key == prop.Key)
                {
                    _bRlt = item;
                    break;
                }
                else
                {
                    try
                    {
                        JObject _cellObj = JObject.Parse(jObj.Property(item.Key).Value.ToString());
                        _bRlt = IsExists(_cellObj, prop);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return _bRlt;
        }
    }
}
