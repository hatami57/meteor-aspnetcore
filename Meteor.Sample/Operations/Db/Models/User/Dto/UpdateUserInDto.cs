using Meteor.Operation.Db.Default;

namespace Meteor.Sample.Operations.Db.Models.User.Dto
{
    public record UpdateUserInDto : AddUserInDto, IDbModel<int>
    {
        public int Id { get; set; }
    }
}
