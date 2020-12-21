using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Meteor.AspCore.Filters;
using Meteor.Operation;
using Meteor.Operation.Db;
using Meteor.Operation.Db.Default;
using Meteor.Sample.Operations.Db.Models.User;
using Meteor.Sample.Operations.Db.Models.User.Commands;
using Meteor.Sample.Operations.Db.Models.User.Queries;

namespace Meteor.Sample.Controllers
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