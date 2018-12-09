using Ajj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Interface
{
    public interface IAPICallingService
    {
        IResponse CreateUserInGB(JobSeeker jobseeker);
        IResponse UpdatePasswordInGB(string userEmail, string passwordOld, string passwordNew);
        IResponse GetUserFromGB(string email);
        IResponse UpdateUserInGB(JobSeeker jobseeker);
        IResponse ResetPasswordInGB(string email,string password);
    }
}
