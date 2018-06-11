using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using KeyshawnPo.Helper.Lib;
using System.Text.RegularExpressions;
using KeyshawnPo.Demo.Entity;
using KeyshawnPo.HtmlEntity;
using Dao.EntityFramework;


namespace KeyshawnPo.Demo.HtmlFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * 选择标签和组件
             * **/

            //html源码
            //string _html = "<html><head><title></title><style type=\"text/css\">         .f {             display: -webkit-box;             display: -moz-box;             display: -webkit-flex;             display: flex;         }          .fdr {             -webkit-box-orient: horizontal;             -webkit-flex-direction: row;             -ms-flex-direction: row;             flex-direction: row;         }              .fdr > .f1, .fdr > .f2, .fdr > .f3, .fdr > .f4, .fdr > .f5, .fdr > .f6, .fdr > .f7, .fdr > .f8 {                 width: 0;             }          .fdc {             -webkit-box-orient: vertical;             -webkit-flex-direction: column;             -ms-flex-direction: column;             flex-direction: column;         }              .fdc > .f1, .fdc > .f2, .fdc > .f3, .fdc > .f4, .fdc > .f5, .fdc > .f6, .fdc > .f7, .fdc > .f8 {                 height: 0;             }          .f1 {             -webkit-box-flex: 1;             -moz-box-flex: 1;             -webkit-flex: 1;             -ms-flex: 1;             flex: 1;         }          .f2 {             -webkit-box-flex: 2;             -moz-box-flex: 2;             -webkit-flex: 2;             -ms-flex: 2;             flex: 2;         }          .f3 {             -webkit-box-flex: 3;             -moz-box-flex: 3;             -webkit-flex: 3;             -ms-flex: 3;             flex: 3;         }          .jc-s {             -webkit-box-pack: start;             -moz-box-pack: start;             justify-content: flex-start;         }          .jc-c {             -webkit-box-pack: center;             -moz-box-pack: center;             justify-content: center;         }          .jc-e {             -webkit-box-pack: end;             -moz-box-pack: end;             justify-content: flex-end;         }          .ai-s {             -webkit-box-align: start;             -moz-box-align: start;             align-items: flex-start;         }          .ai-c {             -webkit-box-align: center;             -moz-box-align: center;             align-items: center;         }          .ai-e {             -webkit-box-align: end;             -moz-box-align: end;             align-items: flex-end;         }      </head>    body {             margin: 0;             padding: 0;             background-color: #efeff4;             color: #333;             overflow:auto;         }          body, tr, td {             font-size: 16px;         }          a {             color: cornflowerblue;             text-decoration: none;         }          em {             font-style: normal;         }          .ss-orq .ss-ort {             height: 3rem;             padding: 0 1rem;             background-color: #fff;         }              .ss-orq .ss-ort .ss-ort-num {                 font-size: 1rem;                 font-weight: 700;             }                  .ss-orq .ss-ort .ss-ort-num em {                     font-size: 0.75rem;                 }              .ss-orq .ss-ort .ss-ort-prot a {                 font-size: 0.875rem;             }          .ss-orq .ss-addr-list {             background-color: #fff;             padding: 0 1rem;         }              .ss-orq .ss-addr-list .ss-addr-item {                 border-top: 1px solid #ddd;                 min-height: 3rem;                 padding: 0.4rem 0;             }                  .ss-orq .ss-addr-list .ss-addr-item .ss-addr-item-dir {                     padding-right: 1rem;                 }          .ss-orq .ss-dir {             width: 2.5rem;             height: 2.5rem;             -webkit-border-radius: 100%;             -moz-border-radius: 100%;             border-radius: 100%;             color: #fff;             text-align: center;             line-height: 2.5rem;             font-size: 1.125rem;         }              .ss-orq .ss-dir.ss-dir-shou {                 background-color: cornflowerblue;             }              .ss-orq .ss-dir.ss-dir-fa {                 background-color: #00ad63;             }          .ss-orq .ss-addr-list .ss-addr-item .ss-addr-item-info {             font-size: 0.875rem;             color: #666;         }              .ss-orq .ss-addr-list .ss-addr-item .ss-addr-item-info .ss-info-row {                 height: 1.5rem;             }                  .ss-orq .ss-addr-list .ss-addr-item .ss-addr-item-info .ss-info-row:last-child {                     font-size: 0.75rem;                     color: #999;                 }          .ico {             width: 1rem;             height: 1rem;             background-size: contain;             background-repeat: no-repeat;             margin-right: 0.4rem;         }          .ico-home {             background-image: url(../../Content/images/ico-home.png);         }          .ico-location {             background-image: url(../../Content/images/ico-location.png);         }          .ico-person {             background-image: url(../../Content/images/ico-person.png);         }          .ico-tel {             background-image: url(../../Content/images/ico-tel.png);         }          .ss-sec {             margin-top: 1rem;         }              .ss-sec .ss-sec-title {                 color: #999;                 font-size: 0.75rem;                 padding: 0 1rem;             }              .ss-sec .ss-sec-content {                 background-color: #fff;                 min-height: 3rem;                 margin-top: 0.4rem;                 padding: 0.4rem 1rem;             }          .ss-table {             font-size: 0.75rem;         }          .ss-table-head {             font-weight: bold;             height: 1.8rem;             border-bottom: 1px solid #ddd;         }          .ss-table-row {             height: 1.8rem;             border-bottom: 1px solid #ddd;         }          .g9 {             color: #999;         }          .f12 {             font-size: 0.75rem;         }          .f14 {             font-size: 0.875rem;         }          .f16 {             font-size: 1rem;         }          .f18 {             font-size: 1.125rem;         }          .f20 {             font-size: 1.25rem;         }          .f24 {             font-size: 1.5rem;         }          .ss-sum-info {             padding: 0.4rem 0;         }          .row1 {             height: 1rem;         }          .row15 {             height: 1.5rem;         }          .row2 {             height: 2rem;         }          .row3 {             height: 3rem;         }          .ss-trace-item {             padding-left: 2rem;             position: relative;             background: url(../../Content/images/dot.png) repeat-y 1rem 0;         }          .ss-trace-item-wrap {             border-bottom: 1px solid #ddd;             padding: 0.4rem 0;             line-height: 1.5rem;         }          .ss-ball {             position: absolute;             left: 0.75rem;             top: 0.95rem;             width: 0.5rem;             height: 0.5rem;             background-color: #ccc;             -webkit-border-radius: 100%;             -moz-border-radius: 100%;             border-radius: 100%;         }          .ss-cur-ball {             background-color: #00AD63;             left: 0.625rem;             top: 0.75rem;             width: 0.75rem;             height: 0.75rem;         }          .ss-tran-info {             background-image: -webkit-linear-gradient(90deg, cornflowerblue, blueviolet);             background-image: -moz-linear-gradient(90deg, cornflowerblue, blueviolet);             background-image: -ms-linear-gradient(90deg, cornflowerblue, blueviolet);             background-image: -o-linear-gradient(90deg, cornflowerblue, blueviolet);             background-image: linear-gradient(90deg,cornflowerblue,blueviolet);             color: #fff;         }     </style> </head>  <body>     <div class=\"ss-orq\">         <div class=\"ss-ort f ai-c\">             <div class=\"f1\">                 <div class=\"ss-ort-num\">                     cd0000001                 </div>             </div>             <div class=\"ss-ort-prot\">                 <a href=\"javascript:;\"></a>             </div>         </div>          <div class=\"ss-addr-list\">             <div class=\"ss-addr-item f\">                 <div class=\"ss-addr-item-dir f ai-c\">                     <div class=\"ss-dir ss-dir-fa\">发</div>                 </div>                 <div class=\"ss-addr-item-info f1\">                     <div class=\"ss-info-row f ai-c\">                         <div class=\"ico ico-home\"></div>                         <div class=\"f1\">###发货单位###</div>                     </div>                     <div class=\"ss-info-row f\">                         <div class=\"f1 f ai-c\">                             <div class=\"ico ico-person\"></div>                             <div class=\"f1\">###发货人###</div>                         </div>                                              </div>                     <div class=\"ss-info-row f ai-c\">                         <div class=\"ico ico-location\"></div>                         <div class=\"f1\">###发货网点###</div>                     </div>                 </div>             </div>             <div class=\"ss-addr-item f\">                 <div class=\"ss-addr-item-dir f ai-c\">                     <div class=\"ss-dir ss-dir-shou\">收</div>                 </div>                 <div class=\"ss-addr-item-info f1\">                     <div class=\"ss-info-row f ai-c\">                         <div class=\"ico ico-home\"></div>                         <div class=\"f1\">###收货单位###</div>                     </div>                     <div class=\"ss-info-row f\">                         <div class=\"f1 f ai-c\">                             <div class=\"ico ico-person\"></div>                             <div class=\"f1\">###收货人###</div>                         </div>                         <div class=\"f1 f ai-c\">                             <div class=\"ico ico-tel\"></div>                             <div class=\"f1\"><a href=\"tel:18245678966\">###收货电话###</a></div>                         </div>                     </div>                     <div class=\"ss-info-row f ai-c\">                         <div class=\"ico ico-location\"></div>                         <div class=\"f1\">###收货单位###</div>                     </div>                 </div>             </div>         </div>          <div class=\"ss-sec\">             <div class=\"ss-sec-title\">货物信息</div>             <div class=\"ss-sec-content\">                 <div class=\"ss-table\">                     <div class=\"ss-table-head f ai-c jc-c\">                         <div class=\"f1\">货品</div>                         <div class=\"f1\">包装</div>                         <div class=\"f1\">件数</div>                         <div class=\"f1\">                             <div>体积</div>                         </div>                         <div class=\"f1\">重量</div>                     </div>                         <div class=\"ss-table-row f ai-c jc-c\">                             <div class=\"f1\">###货物名称###</div>                             <div class=\"f1\"></div>                             <div class=\"f1\">###件数###</div>                             <div class=\"f1\">###体积###</div>                             <div class=\"f1\">###重量###</div>                                                      </div>                 </div>                  <div class=\"ss-sum-info f\">                     <div class=\"f1 f14\" style=\"line-height: 1.5;\">                         <div>代收货款：###代收款###</div>                                              </div>                 </div>             </div>         </div>          <!--物流信息[[-->         <div class=\"ss-sec\">             <div class=\"ss-sec-title\">物流信息</div>             <div class=\"ss-sec-content\">                 <div>                          <div class=\"ss-trace-item f14\">                                 <div class=\"ss-ball ss-cur-ball\"></div>                             <div class=\"ss-trace-item-wrap\">                                 <div class=\"\"> ###起始地###</div>                                 <div class=\"f12 g9\">###托运日期###</div>                             </div>                         </div>                         <div class=\"ss-trace-item f14\">                                 <div class=\"ss-ball\"></div>                              <div class=\"ss-trace-item-wrap\">                                 <div class=\"\"> ###到达地###</div>                                 <div class=\"f12 g9\">###预到日期###</div>                             </div>                         </div>                 </div>             </div>         </div>         <!--物流信息]]-->     </div> </body></html>";

            //读取网址，生成html静态文件
            string _rltMsg = string.Empty;
            bool _bRlt = GetUrl2Html("http://www.qimhb.com", ref _rltMsg);


            //读取模板生成html静态文件
            ArticleEntity _article = new ArticleEntity();
            _article.Title = "测试标题";
            _article.Content = "内容";
            _article.Author = "作者";
            _bRlt = GetHtmlByTmpl(_article, ref _rltMsg);

        }

        /// <summary>
        /// html 解析
        /// </summary>
        private void HtmlPack()
        {
            HtmlWeb _HtmlWeb = new HtmlWeb();
            HtmlDocument _HtmlDocument = _HtmlWeb.Load("http://www.qimhb.com");

            HtmlNode _HtmlNode = new HtmlNode(HtmlNodeType.Text, _HtmlDocument, 1);
            HtmlNodeCollection _HtmlNodeCollection = _HtmlDocument.DocumentNode.SelectNodes(".//a[@href]");
            if (_HtmlNodeCollection != null)
            {
                foreach (HtmlNode href in _HtmlNodeCollection)
                {
                    HtmlAttribute att = href.Attributes["href"];
                    //doSomething(att.Value);
                }
            }
        }

        /// <summary>
        /// 【生成静态html】【方案一】传入URL返回网页的html代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="rltMsg"></param>
        /// <returns></returns>
        private static bool GetUrl2Html(string url, ref string rltMsg)
        {
            bool _bRlt = false;
            string _strHtmlRlt = string.Empty;

            try
            {
                WebRequest _WebRequest = WebRequest.Create(url);
                WebResponse _WebResponse = _WebRequest.GetResponse();

                Stream _Stream = _WebResponse.GetResponseStream();
                StreamReader _StreamReader = new StreamReader(_Stream, Encoding.UTF8);
                _strHtmlRlt = _StreamReader.ReadToEnd();
                FileHelper.WriteFile(Environment.CurrentDirectory + "/wwwhtml/" + _WebResponse.ResponseUri.Host + ".html", _strHtmlRlt);
                _bRlt = true;
            }
            catch (Exception ex)
            {
                _bRlt = false;
                _strHtmlRlt = null;
                rltMsg = ex.Message;
            }
            return _bRlt;
        }

        /// <summary>
        /// 【生成静态html】【方案二】传根据模板中的替换标记，生成html代码
        /// </summary>
        /// <param name="articleEntity"></param>
        /// <param name="rltMsg"></param>
        /// <returns></returns>
        private static bool GetHtmlByTmpl(ArticleEntity articleEntity, ref string rltMsg)
        {
            bool _bRlt = false;

            string _path = Environment.CurrentDirectory + "/tmpl/";

            //读取模板文件
            string _tmplFile = _path + "tmpl1.html";
            string _strHtml = string.Empty;

            try
            {
                _strHtml = FileHelper.ReadFile(_tmplFile);
            }
            catch (Exception ex)
            {
                rltMsg = ex.Message;
            }


            string htmlfilename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            // 替换内容
            // 这时,模板文件已经读入到名称为str的变量中了
            _strHtml = _strHtml.Replace("ShowArticle", articleEntity.Title); //模板页中的ShowArticle
            _strHtml = _strHtml.Replace("biaoti", articleEntity.Title);
            _strHtml = _strHtml.Replace("content", articleEntity.Content);
            _strHtml = _strHtml.Replace("author", articleEntity.Author);

            try
            {
                FileHelper.WriteFile(Environment.CurrentDirectory + "/HtmlRlt/" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".html", _strHtml);
                _bRlt = true;
            }
            catch (Exception ex)
            {
                rltMsg = ex.Message;
            }

            return _bRlt;
        }


        public string VirtualArticle()
        {
            string _article = string.Empty;

            List<VirtualParams> _lstVirtualParams = new List<VirtualParams>();

            VirtualParams _VirtualParams = new VirtualParams();
            _VirtualParams.ParamId = "CompanyAName";
            _VirtualParams.ParamValue = "CompanyAName";
            _VirtualParams.ParamRemarks = "甲方公司名称";
            _VirtualParams.ParamVersion = "V1.0";

            _lstVirtualParams.Add(_VirtualParams);

            _VirtualParams = new VirtualParams();
            _VirtualParams.ParamId = "CompanyBName";
            _VirtualParams.ParamValue = "CompanyBName";
            _VirtualParams.ParamRemarks = "乙方公司名称";
            _VirtualParams.ParamVersion = "V1.0";

            string _articleTmp = " 企业合作推广模板 \br" + " 年月日 ，{CompanyAName} 与 {CompanyBName} 合作 ！";







            Entities _dbEntity = new Entities();
            List<Tmplete> _tmpl = _dbEntity.Tmplete.ToList();
            foreach (var item in _tmpl)
            {
                _articleTmp.Replace(item.ParamKey, item.ParamValue);

            }

            return _article;
        }
    }
}
