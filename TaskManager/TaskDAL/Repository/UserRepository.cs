using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;
using System.Data;



namespace TaskDAL.Repository
{
    public class UserRepository
    {
        
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();
        private static Random random = new Random();

        public bool SaveUser(UserdetailDm udm)
        {
            try
            {


                var Udetail = new Userdetail()
                {
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    FirstName = udm.FirstName,
                    LastName = udm.LastName,
                    EmailId = udm.EmailId,
                    PhoneNumber = udm.PhoneNumber,
                    RoleId = udm.RoleId,
                    ManagerId = udm.ManagerId
                };
                taskManagerEntities.Userdetails.Add(Udetail);
                int num = taskManagerEntities.SaveChanges();

                if (num > 0)
                {
                    //generate random password
                    var password = RandomString(8);

                    //insert into LoginUser table
                    var emailSent = SaveLoginUser(udm, password);

                    //Send mail
                    if (emailSent == true)
                    {
                        SendMail(udm.EmailId, password);
                    }
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
            var res = taskManagerEntities.Userdetails.ToList();
            List<UserdetailDm> udm = new List<UserdetailDm>();


            foreach (var item in res)
            {
                if (item.IsDeleted == false)
                {
                    UserdetailDm ud = new UserdetailDm();
                    ud.Id = item.Id;
                    ud.FirstName = item.FirstName;
                    ud.LastName = item.LastName;
                    ud.PhoneNumber = item.PhoneNumber;
                    ud.EmailId = item.EmailId;
                    ud.RoleName = item.Role.RoleName;
                    var MgrName = taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == item.ManagerId);
                    udm.Add(ud);
                    if (MgrName == null)
                    {
                        ud.ManagerName = "No Manager";
                    }
                    else
                    {
                        ud.ManagerName = MgrName.FirstName;

                    }
                }
                
            }
            return udm;
        }

            public bool SaveLoginUser(UserdetailDm userDetailsDm, string password)
            {
                //fetch emp id based on udm.EmailId
                var employee = taskManagerEntities.Userdetails.First(r => r.EmailId == userDetailsDm.EmailId);

                try
                {
                    var LoginUserDetails = new LoginUser
                    {
                        RoleId = userDetailsDm.RoleId,
                        EmpId = employee.Id,
                        UserName = userDetailsDm.EmailId,
                        Password = password,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    taskManagerEntities.LoginUsers.Add(LoginUserDetails);
                    int num = taskManagerEntities.SaveChanges();
                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }


            }

            public static string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            public static bool SendMail(string receiver, string password)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("taskmanager284@gmail.com");
                    mail.To.Add(receiver);
                    mail.Subject = "Welcome to Task Manager";
                    mail.Body = "You can log into Task Manager App using the password " + password;

                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("taskmanager284@gmail.com", "qwertyqwerty");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public List<RoleModel> DropdownRoles()
            {
                var dropRoles = taskManagerEntities.Roles.Select(m => new RoleModel() { RoleId = m.RoleId.ToString(), RoleName = m.RoleName }).ToList();
                return dropRoles;
            }
            public List<Userdetail> DropdownMgr()
            {
                var dropMgr = taskManagerEntities.Userdetails.Where(m => m.RoleId ==(long)EnumClass.Roles.Manager).ToList();
                return dropMgr;
            }
            public UserdetailDm EditUser(int id)
            {
                var ud = taskManagerEntities.Userdetails.Find(id);
                UserdetailDm udm = new UserdetailDm()
                {
                    IsDeleted = false,
                    CreateDate = ud.CreateDate,
                    ModifiedDate = DateTime.Now,
                    FirstName = ud.FirstName,
                    LastName = ud.LastName,
                    PhoneNumber = ud.PhoneNumber,
                    EmailId = ud.EmailId,
                    RoleId = ud.RoleId,
                    ManagerId = ud.ManagerId,
                };
                return udm;
            }
            public bool SaveEditUser(UserdetailDm udm)
            {

            //taskManagerEntities.Entry(udm).State = System.Data.EntityState.Modified;
            //taskManagerEntities.SaveChanges();

           
                try
                {
                  var ud1 = taskManagerEntities.Userdetails.Where(m=>m.Id==udm.Id).FirstOrDefault();


                //Userdetail ud1 = new Userdetail();
                ud1.Id = udm.Id;
                ud1.FirstName = udm.FirstName;
                ud1.LastName = udm.LastName;
                ud1.EmailId = udm.EmailId;
                ud1.PhoneNumber = udm.PhoneNumber;
                ud1.RoleId = udm.RoleId;
                ud1.ManagerId = udm.ManagerId;


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
                    int count = taskManagerEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
            public Userdetail DeleteUser(int id)
            {
                var res = taskManagerEntities.Userdetails.Find(id);

                res.IsDeleted = true;
                //taskManagerEntities.Entry(res).State = EntityState.;
                int count = taskManagerEntities.SaveChanges();
                return res;
            }



        }
    }