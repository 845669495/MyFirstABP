using Abp.Authorization;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace MyFirstABP
{
    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();

            //判断是否添加权限过滤器
            var isAuthorized = apiDescription.ActionDescriptor.GetFilters().Any(filter => filter is IAuthorizationFilter);
            
            if (isAuthorized)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "用户授权：Bearer access_token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}
