using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Xml.Linq;
using UserManagementUsingMVC.Models;

namespace UserManagementUsingMVC.Controllers
{
    public class UserListController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult UserList()
        {
            UserList userListModel = new UserList(_configuration);

            ViewBag.table = userListModel.GetData();
            return View();
        }

        public IActionResult AddForm()
        {
            UserList AddForm = new UserList(_configuration);

            ViewBag.table = AddForm;
            return View();
        }

        [HttpPost]
        public ActionResult UserList(Models.UserDataModel sm)
        {
            string Name=sm.Name;
            String Description =sm.Description;
            string Email=sm.Email;
    
            ViewBag.Name = Name;
            ViewBag.Description = Description;  
            ViewBag.Email = Email;

            UserList AddForm = new UserList(_configuration);
            AddForm.setData(Name, Description, Email);
            ViewBag.table = AddForm.GetData();

            return View();
        }

        public  ActionResult delete(int parameter1)
        {
            var id = parameter1;
            UserList AddForm = new UserList(_configuration);
            AddForm.Delete(id);
            return RedirectToAction("UserList","UserList");
        }

        public ActionResult update(int id,string name, string Description, string email)
        {
            ViewBag.UpdateId = id;
            return View("UpdateForm");
        }

        [HttpPost]
        public ActionResult updateAction(Models.UserDataModel sm, int id) {

            //int newId=id;
            int UpdateId = sm.Id;
            string updateName = sm.Name;
            string updateDescription = sm.Description;
            string updatedemail = sm.Email;

            UserList AddForm = new UserList(_configuration);
            AddForm.updateData(UpdateId, updateName, updateDescription, updatedemail);
            ViewBag.table = AddForm.GetData();
            return View("UserList");
        }
    }
}
