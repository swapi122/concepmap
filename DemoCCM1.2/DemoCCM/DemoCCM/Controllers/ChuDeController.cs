using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;

namespace DemoCCM.Controllers
{
    public class ChuDeController : Controller
    {
        //
        // GET: /ChuDe/

        ConceptMapDBContext db = new ConceptMapDBContext();

        public ActionResult Index(String idTopic)
        {
            ViewBag.cd = new SelectList(db.ConceptsForTopics, "Question", "Question");
            return View();
            
        }

        //ghghgh
        public PartialViewResult _LinkOfTopicPartial(String idTopic)
        {
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.Where(p => p.TopicID.Equals(idTopic)).ToList();

           
            var link = from db1 in db.ConceptsForTopics
                       join db2 in db.ConceptAlls on db1.ConceptID equals db2.ConceptID
                       join db3 in db.Links on db2.ConceptID equals db3.ConceptID1
                       where db1.TopicID.Equals(idTopic)
                       select new { linkID = db3.LinkID, linkName = db3.Text };

            foreach (var k in link)
            {
                ViewBag.a =ViewBag.a + "  |  " + k.linkName;
            }
          //  ViewBag.a = link.ToList();
            return PartialView();
        }

        public PartialViewResult _ConceptOfTopicPartial(String idTopic)
        {
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.Where(p => p.TopicID.Equals(idTopic)).ToList();     
            return PartialView(conceptForTopics);
        }

    }
}
