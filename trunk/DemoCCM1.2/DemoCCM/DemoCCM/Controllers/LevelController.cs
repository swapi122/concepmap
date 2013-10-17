using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;
namespace DemoCCM.Controllers
{
    public class LevelController : Controller
    {

        //
        // GET: /ChuDe/
        ConceptMapDBContextt db = new ConceptMapDBContextt();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Topic_Level(String idLevel)
        {
            List<TopicOfLevel> topicOfLevel = db.TopicOfLevels.Where(p => p.Level.LevelID.Equals(idLevel)).ToList() ;
          
            TopicOfLevel tp = topicOfLevel.Find(p=>p.LevelID.Equals(idLevel));
            if(tp!=null)
            ViewBag.name = tp.Level.LevelName;

            ViewBag.Title = "Level";
            return View(topicOfLevel);
        }

    }
}
