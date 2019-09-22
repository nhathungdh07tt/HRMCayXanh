using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using HRM.Domain.Entity;
using HRM.Services;
using HRM.ViewModels.Employee;
using HRM.ViewModels.System;
using Excel = Microsoft.Office.Interop.Excel;

namespace HRM.WebSite.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICountryService _countryService;
        private readonly IEducationService _educationService;
        private readonly IEthnicGroupService _ethnicGroupService;
        private readonly IReligionService _religionService;
        private readonly ISkillService _skillService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public EmployeeController(IEmployeeService employeeService,
            ICountryService countryService,
            IEducationService educationService,
            IEthnicGroupService ethnicGroupService,
            IReligionService religionService,
            ISkillService skillService,
            IDepartmentService departmentService,
            IRoleService roleService,
            IUserService userService
            )
        {
            this._employeeService = employeeService;
            this._countryService = countryService;
            this._educationService = educationService;
            this._ethnicGroupService = ethnicGroupService;
            this._religionService = religionService;
            this._skillService = skillService;
            this._departmentService = departmentService;
            this._userService = userService;
            this._roleService = roleService;
        }
        // GET: Employee
        public ActionResult Index(int? department, long? contracttype, int? from, int? to, int? month1, int? nguoithan, int? education, int? phongban, DateTime? fromdate, DateTime? todate, int? gender, int? skill, string sugget = "")
        {

            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
           

            var skills = _skillService.GetSkills();
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            int _depart = department.HasValue ? department.Value : -1;
            int _from = from.HasValue ? from.Value : -1;
            int _to = to.HasValue ? to.Value : -1;
            Session["_depart"] = _depart;
            Session["sugget"] = "";
            if (fromdate.HasValue)
            {
                var model = _employeeService.SearchByDateOfBirth(_depart, fromdate, todate, sugget);
                Session["baocao"] = "1";
                Session["_depart"] = _depart;
                Session["fromdate"] = fromdate;
                Session["todate"] = todate;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (gender.HasValue)
            {
                var model = _employeeService.SearchByGender(_depart, gender, sugget);
                Session["baocao"] = "2";
                Session["_depart"] = _depart;
                Session["gender"] = gender;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (education.HasValue)
            {
                var model = _employeeService.SearchByEducation(_depart, education, sugget);
                Session["baocao"] = "3";
                Session["_depart"] = _depart;
                Session["education"] = education;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (nguoithan.HasValue)
            {
                var model = _employeeService.SearchByNguoiThan(nguoithan);
                Session["baocao"] = "4";
                Session["_depart"] = _depart;
                Session["nguoithan"] = nguoithan;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (skill.HasValue)
            {
                var model = _employeeService.SearchBySkill(_depart, skill, sugget);
                Session["baocao"] = "5";
                Session["_depart"] = _depart;
                Session["skill"] = skill;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (_from > 0)
            {
                var model = _employeeService.SearchByOld(_depart, _from, _to, sugget);
                Session["baocao"] = "6";
                Session["_depart"] = _depart;
                Session["_from"] = _from;
                Session["_to"] = _to;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (_from > 0)
            {
                var model = _employeeService.SearchByOld(_depart, _from, _to, sugget);
                Session["baocao"] = "6";
                Session["_depart"] = _depart;
                Session["_from"] = _from;
                Session["_to"] = _to;
                Session["sugget"] = sugget;
                return View(model);
            }
            else if (month1 > 0)
            {
                var model = _employeeService.SearchByDayOfBirth(month1);
                Session["baocao"] = "7";
                Session["month1"] = month1;
                return View(model);
            }
            //else if (contracttype > 0)
            //{
            //    var model = _employeeService.SearchByContractTypes(_depart, contracttype);
            //    Session["baocao"] = "8";
            //    Session["_depart"] = _depart;
            //    Session["contracttype"] = contracttype;
            //    return View(model);
            //}
            else
            {
                var model = _employeeService.GetAll();
                Session["baocao"] = "9";              
                return View(model);
            }
        }


        public ActionResult IndexNT(int? department, int? from, int? nguoithan, int? to, int? education, int? phongban, DateTime? fromdate, DateTime? todate, int? gender, int? skill, string sugget = "")
        {

            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var skills = _skillService.GetSkills();
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            int _depart = department.HasValue ? department.Value : -1;
            int _from = from.HasValue ? from.Value : -1;
            int _to = to.HasValue ? to.Value : -1;


            if (fromdate.HasValue)
            {
                var model = _employeeService.SearchByDateOfBirth(_depart, fromdate, todate, sugget);
                return View(model);
            }
            else if (gender.HasValue)
            {
                var model = _employeeService.SearchByGender(_depart, gender, sugget);
                return View(model);
            }
            else if (education.HasValue)
            {
                var model = _employeeService.SearchByEducation(_depart, education, sugget);
                return View(model);
            }
            else if (skill.HasValue)
            {
                var model = _employeeService.SearchBySkill(_depart, skill, sugget);
                return View(model);
            }
            else if (nguoithan.HasValue)
            {
                var model = _employeeService.SearchByNguoiThan(nguoithan);
                return View(model);
            }
            else if (_from > 0)
            {
                var model = _employeeService.SearchByOld(_depart, _from, _to, sugget);
                return View(model);
            }
            else
            {
                var model = _employeeService.GetAll1();
                return View(model);
            }
        }


        // GET: Employee/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            EmployeeViewModel employeeViewModel = _employeeService.GetInfo(id);

            if (employeeViewModel == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            return View(employeeViewModel);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var model = new EmployeeViewModel();
            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var roles = _roleService.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Đoàn viên"),
                new KeyValuePair<int, string>(0, "Đảng viên")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            model.DateOfBirth = DateTime.Now.ToLocalTime();
            model.DateIssueIdentity = DateTime.Now.ToLocalTime();
            model.DateIssueSocialInsurance = DateTime.Now.ToLocalTime();

            return View(model);
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["Avatar"];
                if (f != null && f.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                    var avatarpath = "/Uploads/Congvandens";
                    string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                    // file is uploaded
                    f.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        f.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    employeeViewModel.Avatar = string.Join("/", avatarpath, pic);
                }

                //var role = new RoleViewModel();
                //var Roles = new List<RoleViewModel>();
                //var user = new UserViewModel();
                //user.UserName = employeeViewModel.LastName;
                //user.Password = "123456";
                employeeViewModel.EmployeeRelationType = 0;
                //user.Roles.Add(Roles);
                //_userService.Insert(user);

                _userService.Add(new User()
                {
                    UserName = employeeViewModel.LastName,
                    Password = "123456",
                    Roles = new List<Role>()
                    {
                        new Role() {Name = employeeViewModel.Roles}
                    }

                });

                _employeeService.Insert(employeeViewModel);
                _employeeService.Save();
                Session["hinhanh"] = employeeViewModel.Avatar;
                return RedirectToAction("Index", "Employee");
            }

            var model = _employeeService.ConvertToModel(employeeViewModel);
            var data = _employeeService.ConvertToData(model);

            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", data.SelectedSkills);
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            return View(data);
        }


        public ActionResult CreateNT()
        {
            var model = new EmployeeViewModel();

            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Đoàn viên"),
                new KeyValuePair<int, string>(0, "Đảng viên")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            model.DateOfBirth = DateTime.Now.ToLocalTime();
            model.DateIssueIdentity = DateTime.Now.ToLocalTime();
            model.DateIssueSocialInsurance = DateTime.Now.ToLocalTime();

            return View(model);
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNT(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["Avatar"];
                if (f != null && f.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                    var avatarpath = "/Uploads/Congvandens";
                    string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                    // file is uploaded
                    f.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        f.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    employeeViewModel.Avatar = string.Join("/", avatarpath, pic);
                }

                employeeViewModel.EmployeeRelationType = 1;
                _employeeService.Insert(employeeViewModel);
                _employeeService.Save();
                return RedirectToAction("IndexNT", "Employee");
            }

            var model = _employeeService.ConvertToModel(employeeViewModel);
            var data = _employeeService.ConvertToData(model);

            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", data.SelectedSkills);
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            return View(data);
        }




        // GET: Employee/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            EmployeeViewModel employeeViewModel = _employeeService.GetInfo(id);

            if (employeeViewModel == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            return View(employeeViewModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["Avatar"];
                if (f != null && f.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                    var avatarpath = "/Uploads/Congvandens";
                    string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                    // file is uploaded
                    f.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        f.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    employeeViewModel.Avatar = string.Join("/", avatarpath, pic);
                }
                var employee = _employeeService.Update(employeeViewModel);
                _employeeService.Save();
                Session["hinhanh"] = employeeViewModel.Avatar;
                if (employee != null)
                {
                    return RedirectToAction("Index", "Employee");
                }

                var countries = _countryService.GetCountries();
                ViewBag.Countries = new SelectList(countries, "Id", "Name");

                var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
                ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

                var religions = _religionService.GetReligions();
                ViewBag.Religions = new SelectList(religions, "Id", "Name");

                var educations = _educationService.GetEducations();
                ViewBag.Educations = new SelectList(educations, "Id", "Title");

                var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

                ViewBag.Genders = new SelectList(genders, "Key", "Value");

                var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

                ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

                var skills = _skillService.GetSkills();
                //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
                ViewBag.Skills = new SelectList(skills, "Id", "Title");


            }

            return View(employeeViewModel);
        }


        public ActionResult EditNT(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("IndexNT", "Employee");
            }

            EmployeeViewModel employeeViewModel = _employeeService.GetInfo(id);

            if (employeeViewModel == null)
            {
                return RedirectToAction("IndexNT", "Employee");
            }

            var countries = _countryService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
            ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

            var religions = _religionService.GetReligions();
            ViewBag.Religions = new SelectList(religions, "Id", "Name");

            var educations = _educationService.GetEducations();
            ViewBag.Educations = new SelectList(educations, "Id", "Title");

            var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

            ViewBag.Genders = new SelectList(genders, "Key", "Value");

            var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

            ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

            var skills = _skillService.GetSkills();
            //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            return View(employeeViewModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNT(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["Avatar"];
                if (f != null && f.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                    var avatarpath = "/Uploads/Congvandens";
                    string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                    // file is uploaded
                    f.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        f.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    employeeViewModel.Avatar = string.Join("/", avatarpath, pic);
                }
                var employee = _employeeService.Update(employeeViewModel);
                _employeeService.Save();
                if (employee != null)
                {
                    return RedirectToAction("IndexNT", "Employee");
                }

                var countries = _countryService.GetCountries();
                ViewBag.Countries = new SelectList(countries, "Id", "Name");

                var ethnicGroups = _ethnicGroupService.GetEthnicGroups();
                ViewBag.EthnicGroups = new SelectList(ethnicGroups, "Id", "Name");

                var religions = _religionService.GetReligions();
                ViewBag.Religions = new SelectList(religions, "Id", "Name");

                var educations = _educationService.GetEducations();
                ViewBag.Educations = new SelectList(educations, "Id", "Title");

                var genders = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(0, "Nữ"),
                new KeyValuePair<int, string>(1, "Nam")
            };

                ViewBag.Genders = new SelectList(genders, "Key", "Value");

                var communistYouthUnions = new List<KeyValuePair<int, string>> {
                new KeyValuePair<int, string>(1, "Có"),
                new KeyValuePair<int, string>(0, "Không")
            };

                ViewBag.CommunistYouthUnions = new SelectList(communistYouthUnions, "Key", "Value");

                var skills = _skillService.GetSkills();
                //ViewBag.Skills = new MultiSelectList(skills, "Id", "Title", new long[] {});
                ViewBag.Skills = new SelectList(skills, "Id", "Title");


            }

            return View(employeeViewModel);
        }



        // GET: Employee/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            EmployeeViewModel employeeViewModel = _employeeService.GetInfo(id);

            if (employeeViewModel == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            return View(employeeViewModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {

            var employee = _employeeService.Find(id);

            if (employee != null)
            {
                _employeeService.Delete(employee);
            }

            return RedirectToAction("Index", "Employee");
        }



        public ActionResult Export(int? department, long? contracttype, int? from, int? to, int? month1, int? nguoithan, int? education, int? phongban, DateTime? fromdate, DateTime? todate, int? gender, int? skill, string sugget = "")
        {
            int _depart = department.HasValue ? department.Value : -1;
            int _from = from.HasValue ? from.Value : -1;
            int _to = to.HasValue ? to.Value : -1;

            //try
            //{
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            EmployeeViewModel pm = new EmployeeViewModel();
            var model = _employeeService.GetAll();
            _depart = (int)Session["_depart"];
            sugget = Session["sugget"].ToString();

            if (Session["baocao"].ToString() == "1")
            {
                fromdate = (DateTime)Session["fromdate"];
                todate = (DateTime)Session["todate"];
                model = _employeeService.SearchByDateOfBirth(_depart, fromdate, todate, sugget);
            }
            else if (Session["baocao"].ToString() == "2")
            {
                gender = (int)Session["gender"];
                model = _employeeService.SearchByGender(_depart, gender, sugget);
            }
            else if (Session["baocao"].ToString() == "3")
            {
                education = (int)Session["education"];
                model = _employeeService.SearchByEducation(_depart, education, sugget);
            }
            else if (Session["baocao"].ToString() == "4")
            {
                nguoithan = (int)Session["nguoithan"];
                model = _employeeService.SearchByNguoiThan(nguoithan);
            }
            else if (Session["baocao"].ToString() == "5")
            {
                skill = (int)Session["skill"];
                model = _employeeService.SearchBySkill(_depart, skill, sugget);
            }
            else if (Session["baocao"].ToString() == "6")
            {
                _from = (int)Session["_from"];
                _to = (int)Session["_to"];
                model = _employeeService.SearchByOld(_depart, _from, _to, sugget);
            }
            else if (Session["baocao"].ToString() == "7")
            {
                month1 = (int)Session["month1"];
                _to = (int)Session["_to"];
                model = _employeeService.SearchByDayOfBirth(month1);
            }
            //else if (Session["baocao"].ToString() == "8")
            //{
            //    Session["baocao"] = "8";
            //    contracttype = (int)Session["contracttype"];              
            //    model = _employeeService.SearchByContractTypes(_depart, contracttype);
            //}
            else
            {
                model = _employeeService.GetAll();
            }


            worksheet.Cells[1, 1] = "Mã NV";
            worksheet.Cells[1, 2] = "Tên NV";
            worksheet.Cells[1, 3] = "Quốc tịch";
            worksheet.Cells[1, 4] = "Giới tính";
            worksheet.Cells[1, 5] = "CMND";
            worksheet.Cells[1, 6] = "Ngày sinh";
            worksheet.Cells[1, 7] = "Nơi sinh";
            worksheet.Cells[1, 8] = "Ngày cấp";
            worksheet.Cells[1, 9] = "Nơi cấp";
            worksheet.Cells[1, 10] = "Dân tộc";
            worksheet.Cells[1, 11] = "Tôn giáo";
            worksheet.Cells[1, 12] = "Địa chỉ";
            worksheet.Cells[1, 13] = "Điện thoại";
            worksheet.Cells[1, 14] = "Email";
            worksheet.Cells[1, 15] = "Ngày nghỉ phép";
            worksheet.Cells[1, 16] = "Trình độ";
            worksheet.Cells[1, 17] = "Học vấn";
            worksheet.Cells[1, 18] = "Chuyên môn";
            worksheet.Cells[1, 19] = "Đoàn viên";
            worksheet.Cells[1, 20] = "BHXH";
            worksheet.Cells[1, 21] = "Ngày tham gia BHXH";
            worksheet.Cells[1, 22] = "Số tài khoản ngân hàng";
            worksheet.Cells[1, 23] = "Ngân hàng";
            worksheet.Cells[1, 24] = "Ngày ký hợp đồng";
            worksheet.Cells[1, 25] = "Ngày kết thúc hợp đông";

            int row = 2;
            foreach (EmployeeViewModel p in model)
            {
                worksheet.Cells[row, 1] = p.LastName;
                worksheet.Cells[row, 2] = p.FirstName;
                worksheet.Cells[row, 3] = p.Nationality.Name;
                worksheet.Cells[row, 4] = p.Gender;
                worksheet.Cells[row, 5] = p.IdentityNo;
                worksheet.Cells[row, 6] = p.DateOfBirth;
                worksheet.Cells[row, 7] = p.PlaceOfBirth;
                worksheet.Cells[row, 8] = p.DateIssueIdentity;
                worksheet.Cells[row, 9] = p.PlaceIssueIdentity;
                worksheet.Cells[row, 10] = p.EthnicGroup.Name;
                worksheet.Cells[row, 11] = p.Religion.Name;
                worksheet.Cells[row, 12] = p.Address;
                worksheet.Cells[row, 13] = p.Phone;
                worksheet.Cells[row, 14] = p.Email;
                worksheet.Cells[row, 15] = p.YearDayOff;
                worksheet.Cells[row, 16] = p.Education.Title;
                worksheet.Cells[row, 17] = p.DetailEducation;
                worksheet.Cells[row, 18] = p.Certificate;
                worksheet.Cells[row, 19] = p.CommunistYouthUnion;
                worksheet.Cells[row, 20] = p.SocialInsuranceNo;
                worksheet.Cells[row, 21] = p.DateIssueSocialInsurance;
                worksheet.Cells[row, 22] = p.BankAccount;
                worksheet.Cells[row, 23] = p.Bank;
                worksheet.Cells[row, 24] = p.DateSignContract;
                worksheet.Cells[row, 25] = p.DateOffContract;
                row++;
            }

            workbook.SaveAs("d:\\myexcel.xls");
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);
            ViewBag.Result = "Done";
            return View("Export");

        }


        public JsonResult CheckExistingUsername(string username)
        {
            var exist = _employeeService.SearchByUserName(username);

            return exist.Count() == 1
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyInformation()
        {
            long id = (long)Session["id"];
            //EmployeeViewModel employeeViewModel = _employeeService.GetInfo(id);
            var model = _employeeService.GetAll().Where(x=>x.Id==id).ToList();
            return View(model);

        }

    }

}

