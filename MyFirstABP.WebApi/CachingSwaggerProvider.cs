using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFirstABP
{
    public class CachingSwaggerProvider : ISwaggerProvider
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =
            new ConcurrentDictionary<string, SwaggerDocument>();

        private readonly ISwaggerProvider _swaggerProvider;

        public CachingSwaggerProvider(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);

                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        private static ConcurrentDictionary<string, string> GetControllerDesc()
        {
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            string[] ss = Directory.GetFiles(string.Format("{0}/bin", System.AppDomain.CurrentDomain.BaseDirectory), "MyFirstABP.*.xml");
            foreach (var xmlpath in ss)
            {
                if (File.Exists(xmlpath))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlpath);
                    foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                    {
                        string type = node.Attributes["name"].Value;
                        if (type.StartsWith("T:"))
                        {
                            string[] arrPath = type.Split('.');
                            string className = arrPath[arrPath.Length - 1];
                            if (className.EndsWith("Controller"))
                            {
                                //获取控制器注释
                                XmlNode summaryNode = node.SelectSingleNode("summary");
                                string key = className.Replace("Controller", "");
                                if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                                {
                                    controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                }
                            }
                            else if (className.StartsWith("I") && className.EndsWith("AppService") && className != "IAppService")
                            {
                                //获取服务接口注释
                                XmlNode summaryNode = node.SelectSingleNode("summary");
                                string key = "app" + "95" + className.Replace("AppService", "").Substring(1).ToLower();

                                if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                                {
                                    controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                }
                            }
                        }
                    }
                }
            }          
            return controllerDescDict;
        }
    }
}
