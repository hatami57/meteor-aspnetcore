using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User;
using Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Commands;
using Meteor.AspNetCore.Dapper.Sample.Operations.Db.Models.User.Queries;
using Meteor.AspNetCore.Filters;
using Meteor.Database.Dapper.Operations.Db;
using Meteor.Database.Dapper.Operations.Db.Default;
using Meteor.Operation;

namespace Meteor.AspNetCore.Dapper.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OperationResult]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly OperationFactory _operationFactory;

        public UserController(ILogger<UserController> logger, OperationFactory operationFactory)
        {
            _logger = logger;
            _operationFactory = operationFactory;
        }

        [HttpGet]
        public Task<QueryPage<User>?> GetPage([FromQuery] DefaultQueryPageInput input)
        {
            return _operationFactory.ExecuteAsync<GetUserPage, DefaultQueryPageInput, QueryPage<User>>(input);
        }

        [HttpPost]
        public Task<int> Add(AddUserInputDto input)
        {
            return _operationFactory.New<AddUser>().ExecuteAsync(input);
        }

        [HttpPut]
        public Task<bool> Update(UpdateUserInputDto input)
        {
            return _operationFactory.New<UpdateUser>().ExecuteAsync(input);
        }

        [HttpDelete("{id}")]
        public Task<OperationResult<bool>> Remove(int id)
        {
            return _operationFactory.New<DeleteUser>().TryExecuteAsync(new DbModel<int> {Id = id});
        }
    }
}