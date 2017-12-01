using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using MyFirstABP.Caching;
using MyFirstABP.DTO;
using MyFirstABP.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstABP
{
    public class TaskAppService : MyFirstABPAppServiceBase, ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;
        //可以注入泛型
        private readonly IRepository<Person> _personRepository;

        private readonly ICacheService _cacheService;

        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public TaskAppService(ITaskRepository taskRepository, IRepository<Person> personRepository, ICacheService cacheService)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _cacheService = cacheService;
        }


        public void CreateTask(CreateTaskInput input)
        {
            Logger.Info("Creating a task for input: " + input);

            var task = new Task() { Description = input.Description, AssignedPersonId = input.AssignedPersonId };

            //调用仓储基类的Insert方法把实体保存到数据库中
            _taskRepository.Insert(task);
        }

        [AbpAuthorize("AdminPermission")]
        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedPersonId, input.State);

            var task = tasks.First();

            var user = GetCurrentUser();

            var entity = _cacheService.GetCachedEntity<long,Task>(task.Id);
            entity.Description = "aaafdsfgdg";
            _cacheService.Set(task.Id, entity);

            //用AutoMapper自动将List<Task>转换成List<TaskDto>
            return new GetTasksOutput()
            {
                Tasks = Mapper.Map<List<TaskDto>>(tasks)
            };
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            //可以直接Logger,它在ApplicationService基类中定义的
            Logger.Info("Updating a task for input: " + input);

            //找不到会抛出异常
            var task = _taskRepository.Get(input.TaskId);

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }
            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            //我们都不需要调用Update方法
            //因为应用服务层的方法默认开启了工作单元模式（Unit of Work）
            //ABP框架会工作单元完成时自动保存对实体的所有更改，除非有异常抛出。有异常时会自动回滚，因为工作单元默认开启数据库事务。
        }
    }
}
