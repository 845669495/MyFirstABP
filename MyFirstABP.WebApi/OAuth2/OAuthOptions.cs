using Abp.Dependency;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP.OAuth2
{
    /// <summary>
    /// Class OAuthOptions.
    /// </summary>
    public class OAuthOptions
    {
        /// <summary>
        /// Gets or sets the server options.
        /// </summary>
        /// <value>The server options.</value>
        private static OAuthAuthorizationServerOptions _serverOptions;



        /// <summary>
        /// OAuth授权地址: http://localhost:6234/api/oauth/token
        /// 登录需要传递的参数如下：
        /// grant_type：该值固定为password
        /// client_id:客户id
        /// client_secret:客户密钥
        /// username:用户名
        /// password:密码
        /// 
        /// 刷新Token需要传递的参数如下：
        /// grant_type:refresh_token
        /// refresh_token:通过登录获取到的refresh_token
        /// client_id:客户id
        /// client_secret:客户密钥
        /// </summary>
        /// <returns>OAuthAuthorizationServerOptions.</returns>
        public static OAuthAuthorizationServerOptions CreateServerOptions()
        {
            if (_serverOptions == null)
            {
                var provider = IocManager.Instance.Resolve<SimpleAuthorizationServerProvider>();
                var refreshTokenProvider = IocManager.Instance.Resolve<SimpleRefreshTokenProvider>();
                _serverOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString("/api/oauth/token"),
                    Provider = provider,
                    RefreshTokenProvider = refreshTokenProvider,
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(3),
                    AllowInsecureHttp = true           
                };
            }
            return _serverOptions;
        }
    }
}
