namespace Meteor.Sample.Operations.Db.Models.User.Dto
{
    public record AddUserInDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Username { get; init; }
    }
}
