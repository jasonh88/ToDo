using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers.Api
{
    [RoutePrefix("api/tasks")]
    public class TaskApiController : ApiController
    {
        [HttpGet]
        [Route]
        public HttpResponseMessage GetTasks()
        {
            TaskService taskSvc = new TaskService();
            List<Tasks> taskList = taskSvc.GetTasks();
            return Request.CreateResponse(HttpStatusCode.OK, taskList);
        }

        [HttpPost]
        [Route]
        public HttpResponseMessage CreateTask(TaskAddRequest model)
        {
            TaskService taskSvc = new TaskService();
            int id = taskSvc.CreateTask(model);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateTask([FromUri] int id, [FromBody]Tasks model)
        {
            TaskService taskSvc = new TaskService();
            taskSvc.UpdateTask(id, model);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteTask(int id)
        {
            TaskService taskSvc = new TaskService();
            taskSvc.DeleteTask(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
