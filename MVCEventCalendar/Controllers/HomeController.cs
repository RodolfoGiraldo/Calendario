﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace MVCEventCalendar.Controllers
{
    public class HomeController : Controller
    {
         


        public ActionResult Index()
        {
            
            if (Session["usuariologueado"] == null)
            {
                 Response.Redirect("../login.aspx");
            }
            return View();
        }

        public JsonResult GetEvents()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var idUser = Session["idUsuario"];

                    var events = dc.Events.Where(b => b.IdUser == (long)idUser).ToList();
                List<Events> eventoFinal = new List<Events>();
                    foreach (Events value in events)
                {
                    
                    value.Users = new Users();

                    eventoFinal.Add(value);
                }
                    return new JsonResult { Data = eventoFinal, JsonRequestBehavior = JsonRequestBehavior.AllowGet,};
                
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Events e){
            var idUser = Session["idUsuario"];
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                bool bandera = true;
                var events = dc.Events.Where(b => b.IdUser == (long)idUser).ToList();
                foreach (Events value in events)
                {
                    if (e.Start >= value.Start && e.Start < value.End)
                    {
                        bandera = false;
                    }
                }


                if (bandera)
                {
                    if (e.EventID > 0)
                    {
                        var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Subject = e.Subject;
                            v.Start = e.Start;
                            v.End = e.End;
                            v.Description = e.Description;
                            v.IsFullDay = e.IsFullDay;
                            v.ThemeColor = e.ThemeColor;
                            v.IdUser = (long)idUser;
                        }
                    }
                    else
                    {
                        e.IdUser = (long)idUser;
                        dc.Events.Add(e);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities()) {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if(v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }       
            }
            return new JsonResult { Data = new { status = status } };

        }
    
        
    }
}