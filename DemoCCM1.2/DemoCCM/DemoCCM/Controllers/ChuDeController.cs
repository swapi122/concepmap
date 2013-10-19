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


        public ActionResult aa()
        {
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.ToList();

            List<String> level = new List<string>();
            foreach (var k in conceptForTopics)
           {
               int len = k.Levels.Length;
            
                   string[] catchuoi = k.Levels.Split(new char[] { ',' });
                   foreach (string s1 in catchuoi)
                   {
                       if (s1.Trim() != "")
                       {
                           level.Add(s1);
                       }
                   }
                
            }

            for(int i=0;i<level.Count();i++)
            {
                ViewBag.l =ViewBag.l+ level[i].ToString();
            }
            
         
            return View();
        }

        public PartialViewResult _ConceptOfTopicPartial(String idLevel)
        {
           /* List<ConceptsForTopic> conceptForTopics;            
            conceptForTopics = db.ConceptsForTopics.Where(p => p.TopicID.Equals(idTopic)).ToList();
            */

            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.ToList();
            int i=0;
            List<String> level = new List<string>();
            foreach (var k in conceptForTopics)
            {
                //Hàng thứ nhất
                int len = k.Levels.Length; 

                string[] catchuoi = k.Levels.Split(new char[] { ',' });
                foreach (string s1 in catchuoi)
                { 
                    if (s1.Trim() != "")
                    {
                        level.Add(s1);
                    }
                }
                if (idLevel == level[i].ToString())
                {
                    i++;
                    ViewBag.c = k.ConceptAll.ConceptID;
                    
                }

            }
            return PartialView();
        }

    }
}
