using TaskDAL.Repository;
using TaskDomain.DomainModel;

namespace TaskServiceLayer
{
    public class LoginServices
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();
        public LoginUserDm GetLogDetails(string name, string password)
        {
            var logDetails = _loginRepository.GetLogDetails(name, password);
            return logDetails;
        }
        public UserdetailDm GetUserDetailsData(int id)
        {
            var userData = _loginRepository.GetUserDetailsData(id);
            return userData;
        }
    }
}
