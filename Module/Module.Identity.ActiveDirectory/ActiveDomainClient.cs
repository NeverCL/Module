using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Identity.ActiveDirectory
{
    public class ActiveDomainClient : IDisposable
    {
        private readonly PrincipalContext _context;
        public ActiveDomainClient(string domain)
        {
            _context = new PrincipalContext(ContextType.Domain, domain);
        }

        public bool Validate(string userName, string password)
        {
            return _context.ValidateCredentials(userName, password);
            //// 工号一定要全
            //using (var userPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userName))
            //{
            //    if (userPrincipal == null)
            //    {
            //        //return "账号不正确,请重新输入";
            //        return false;
            //    }
            //    if (!pc.ValidateCredentials(userName, password))
            //    {
            //        //return @"密码输入错误,请重新输入";
            //        return false;
            //    }
            //    //GivenName是用户名称,Surname是工号(无前缀),Name是用户名称+工号(无前缀)
            //    //PersonDetailInfo personDetailInfo = new PersonDetailInfo()
            //    //{
            //    //    SearchName = userPrincipal.Name,
            //    //    UserName = userPrincipal.GivenName,
            //    //    JobNumber = userPrincipal.SamAccountName,
            //    //    EmailAddress = userPrincipal.EmailAddress
            //    //};
            //    return true;
            //}
            
        }

        /// <summary>
        /// 取Domain版本 2003 2008等
        /// </summary>
        //private void DomainModeCheck(string UserName, string Password, string Domain)
        //{
        //    DirectoryContext oContext = null;

        //    oContext = new DirectoryContext(DirectoryContextType.Domain, Domain, UserName, Password);
        //    DomainMode DM = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(oContext).DomainMode;

        //}

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
