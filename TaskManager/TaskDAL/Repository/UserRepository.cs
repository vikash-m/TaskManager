using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TaskDomain.DomainModel;




namespace TaskDAL.Repository
{
    public class UserRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        private static readonly Random Random = new Random();

        public bool SaveUser(UserdetailDm userdetail)
        {
            try
            {


                var userDetail = new Userdetail()
                {
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    FirstName = userdetail.FirstName,
                    LastName = userdetail.LastName,
                    EmailId = userdetail.EmailId,
                    PhoneNumber = userdetail.PhoneNumber,
                    RoleId = userdetail.RoleId,
                    ManagerId = userdetail.ManagerId
                };
                _taskManagerEntities.Userdetails.Add(userDetail);
                var num = _taskManagerEntities.SaveChanges();

                if (num <= 0) return false;
                //generate random password
                var password = RandomString(8);

                //insert into LoginUser table
                var emailSent = SaveLoginUser(userdetail, password);

                //Send mail
                if (emailSent)
                {
                    SendMail(userdetail.EmailId, password);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<UserdetailDm> ViewUser()
        {

            var result = _taskManagerEntities.Userdetails.ToList().Select(user => new UserdetailDm
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailId = user.EmailId,
                RoleName = user.Role.RoleName,
                ManagerName = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == user.ManagerId) == null
                    ? "No Manager"
                    : _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == user.ManagerId)?.FirstName

            }).ToList();
            return result;
        }


        private bool SaveLoginUser(UserdetailDm userDetailsDm, string password)
        {
            //fetch emp id based on udm.EmailId
            var employee = _taskManagerEntities.Userdetails.First(r => r.EmailId == userDetailsDm.EmailId);

            try
            {
                var loginUserDetails = new LoginUser
                {
                    RoleId = userDetailsDm.RoleId,
                    EmpId = employee.Id,
                    UserName = userDetailsDm.EmailId,
                    Password = password,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _taskManagerEntities.LoginUsers.Add(loginUserDetails);
                _taskManagerEntities.SaveChanges();
                return true;
            }

            catch (Exception)
            {
                return false;
            }


        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private static bool SendMail(string receiver, string password)
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("taskmanager284@gmail.com");
                mail.To.Add(receiver);
                mail.Subject = "Welcome to Task Manager";
                mail.Body = "You can log into Task Manager App using the password " + password;

                smtpServer.Port = 25;
                smtpServer.Credentials = new System.Net.NetworkCredential("taskmanager284@gmail.com", "qwertyqwerty");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<RoleModel> DropdownRoles() => _taskManagerEntities.Roles.Select(m => new RoleModel() { RoleId = m.RoleId.ToString(), RoleName = m.RoleName }).ToList();

        public List<Userdetail> DropdownMgr() => _taskManagerEntities.Userdetails.Where(m => m.RoleId == (long)EnumClass.Roles.Manager).ToList();

        public UserdetailDm EditUser(int id)
            => _taskManagerEntities.Userdetails.Where(x => x.Id == id).Select(user => new UserdetailDm()
            {
                IsDeleted = user.IsDeleted,
                CreateDate = user.CreateDate,
                ModifiedDate = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailId = user.EmailId,
                RoleId = user.RoleId,
                ManagerId = user.ManagerId,
            }).FirstOrDefault();
        //{
        //    var ud = _taskManagerEntities.Userdetails.Find(id);
        //var udm = new UserdetailDm()
        //{
        //    IsDeleted = false,
        //    CreateDate = ud.CreateDate,
        //    ModifiedDate = DateTime.Now,
        //    FirstName = ud.FirstName,
        //    LastName = ud.LastName,
        //    PhoneNumber = ud.PhoneNumber,
        //    EmailId = ud.EmailId,
        //    RoleId = ud.RoleId,
        //    ManagerId = ud.ManagerId,
        //};
        //    return udm;
        //}
        public bool SaveEditUser(UserdetailDm userdetailDm)
        {

            //taskManagerEntities.Entry(udm).State = System.Data.EntityState.Modified;
            //taskManagerEntities.SaveChanges();


            try
            {
                var userDetail = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == userdetailDm.Id);


                //Userdetail ud1 = new Userdetail();
                if (userDetail == null)
                {
                    return false;
                }
                userDetail.Id = userdetailDm.Id;
                userDetail.FirstName = userdetailDm.FirstName;
                userDetail.LastName = userdetailDm.LastName;
                userDetail.EmailId = userdetailDm.EmailId;
                userDetail.PhoneNumber = userdetailDm.PhoneNumber;
                userDetail.RoleId = userdetailDm.RoleId;
                userDetail.ManagerId = userdetailDm.ManagerId;


                //var Udetail = new Userdetail()
                //    {
                //        IsDeleted = false,
                //        CreateDate = DateTime.Now,
                //        ModifiedDate = DateTime.Now,
                //        Id = udm.Id,
                //        FirstName = udm.FirstName,
                //        LastName = udm.LastName,
                //        EmailId = udm.EmailId,
                //        PhoneNumber = udm.PhoneNumber,
                //        RoleId = udm.RoleId,
                //        ManagerId = udm.ManagerId
                //    };

                // taskManagerEntities.Entry(ud1).State = System.Data.EntityState.Modified; ;
                _taskManagerEntities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool DeleteUser(int id)
        {
            var res = _taskManagerEntities.Userdetails.Find(id);

            if (res == null)
            {
                return false;
            }
            res.IsDeleted = true;
            //taskManagerEntities.Entry(res).State = EntityState.;
            _taskManagerEntities.SaveChanges();
            return true;
        }



    }
}