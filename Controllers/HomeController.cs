using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamThree.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExamTwo.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(e => e.UserName == newUser.UserName))
                {
                    ModelState.AddModelError("UserName", "Name Already In Use");
                    return View ("Index");
                } else {
                //     int year = newUser.DoB.Year;
                //     if(DateTime.Today.Year - year < 18)
                //     {
                //         ModelState.AddModelError("DoB", "User Must Be Over 18!");
                //         return View("Index");
                //     } else if(DateTime.Today.Year - year == 18)
                //     {
                //         if(DateTime.Today.Month - newUser.DoB.Month < 0)
                //         {
                //             ModelState.AddModelError("DoB", "User Must Be Over 18!");
                //             return View("Index");
                //         }
                //     } else if(DateTime.Today.Day - newUser.DoB.Month == 0)
                //     {
                //         if(DateTime.Today.Day - newUser.DoB.Day < 0)
                //         {
                //             ModelState.AddModelError("DoB","User Must Be Over 18!");
                //             return View("Index");
                //         }
                // } 

                    PasswordHasher<User>Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("loggedIn", newUser.UserId);
                    return RedirectToAction("Dashboard");
                }
            } else{
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LogUser login)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.UserName == login.LogUserName);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LogUserName","Invalid Login Attempt");
                    return View("Index");
                } else {
                    var hasher = new PasswordHasher<LogUser>();
                    var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.LogPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LogUserName", "Invalid Login Attempt");
                        return View("Index");
                    }
                    HttpContext.Session.SetInt32("loggedIn", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
            } else {
                return View("Index");
            }
        }
        
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        
        
        
        
        
        
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            if(loggedIn != null)
            {
                ViewBag.loggedIn = _context.Users.FirstOrDefault(a => a.UserId == (int)loggedIn);
                ViewBag.AllHobbies = _context.Hobbies.Include(t => t.UserHobbies).ToList();
                ViewBag.loggedIn = _context.Users.Include(s => s.UserHobbies).FirstOrDefault(a => a.UserId == (int)loggedIn);

                return View();
            } else{
                return RedirectToAction("Index");
            }
        }






        [HttpGet("AddHobby")]
        public IActionResult AddHobby()
        {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            if(loggedIn == null)
            {
                return RedirectToAction ("Index");
            }
            return View();
        }








        [HttpPost("CreateHobby")]
        public IActionResult CreateHobby(Hobby newHobby)
        {
            if(ModelState.IsValid)
            {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            newHobby.UserId = (int)loggedIn;
            _context.Add(newHobby);
            _context.SaveChanges();
            return RedirectToAction ("Dashboard");
            } else{
                return View("AddHobby");
            }
        }






        [HttpGet("ShowHobby/{HobbyId}")]
        public IActionResult ShowHobby(int HobbyId)
        {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            if(loggedIn == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.OneHobby = _context.Hobbies.Include(w=> w.UserHobbies).ThenInclude(e =>e.Creator).FirstOrDefault(s => s.HobbyId == HobbyId);
            ViewBag.User = loggedIn;
            ViewBag.OrderedHobby =_context.Hobbies.Include(d =>d.UserHobbies).Where(a =>a.HobbyId == HobbyId).ToList();
            ViewBag.AllUsers = _context.Users.ToList();
            
            return View("ShowHobby");
        }









        [HttpPost("OrderHobby")]
        public IActionResult OrderHobby(UserHobby newHobby)
        {
            int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
            if(loggedIn == null)
            {
                return RedirectToAction("Index");
            }
            _context.Add(newHobby);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpGet("Edit/{HobbyId}")]
        public IActionResult Edit(int HobbyId)
        {
            ViewBag.Hobby=_context.Hobbies.FirstOrDefault(f=>f.HobbyId == HobbyId);
            return View();
        }
    }

}
