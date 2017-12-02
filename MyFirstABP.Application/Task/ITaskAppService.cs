using Abp.Application.Services;
using MyFirstABP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyFirstABP
{
    /// <summary>
    /// Task API服务
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {
        /// <summary>
        /// 获取Task集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        GetTasksOutput GetTasks(GetTasksInput input);
        /// <summary>
        /// 更新Task
        /// </summary>
        /// <param name="input"></param>
        void UpdateTask(UpdateTaskInput input);
        /// <summary>
        /// 创建Task
        /// </summary>
        /// <param name="input"></param>
        void CreateTask(CreateTaskInput input);
    }
}
