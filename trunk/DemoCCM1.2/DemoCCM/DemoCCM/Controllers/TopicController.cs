using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCCM.Models;

namespace DemoCCM.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /ChuDe/

        ConceptMapDBContext db = new ConceptMapDBContext();

        public ActionResult Index(String LevelID1, String topicId1)
        {
            ViewBag.cd = new SelectList(db.ConceptsForTopics, "Question", "Question");
            ViewBag.levelID2 = LevelID1;
            ViewBag.topicID2 = topicId1;
            return View();

        }

        //
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


        public PartialViewResult _Concept_Topic_TopicPartial(String LevelID,String TopicID)
        {
            List<ConceptsForTopic> conceptForTopics;
            conceptForTopics = db.ConceptsForTopics.Where(p=>p.TopicID.Equals(TopicID)).ToList();
            List<ConceptsForTopic> conceptForTopics2 = new List<ConceptsForTopic>();
            ConceptsForTopic concept = new ConceptsForTopic();

            foreach (var k in conceptForTopics)
            {
                string[] catchuoi = k.Levels.Split(new char[] { ',' });
                foreach (string s1 in catchuoi)
                {

                    if (s1.Trim() != "")
                    {
                        if (s1.ToString().Equals(LevelID))
                        {

                            concept = conceptForTopics.Find(p => p.ConceptID.Equals(k.ConceptID));
                            conceptForTopics2.Add(concept);
                            
                        }
                    }

                }

            }

            return PartialView(conceptForTopics2);
        }

       
    }
}
