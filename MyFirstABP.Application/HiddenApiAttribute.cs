using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP
{
    /// <summary>  
    /// 隐藏接口标识，不生成到swagger文档展示  
    /// </summary>  
    [System.AttributeUsage(AttributeTargets.Interface)]
    public class HiddenApiAttribute : System.Attribute { }
}
