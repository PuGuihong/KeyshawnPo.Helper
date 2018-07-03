using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.HtmlEntity
{
    public class ArticleTmpleteEntity
    {
        public Dictionary<TempleteType, string> AriticleTemplte;
    }

    public enum TempleteType
    {
        title,
        content,
        other
    }
}
