using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda.Controllers
{
    public class AgendaController : Controller
    {
        static IList<Assignment> assignmentList = new List<Assignment>() {};


        // GET: Agenda
        public ActionResult Index()
        {
            return View(assignmentList);
        }

        //Remove passed
        public ActionResult RemovePassed()
        {
            for (var i = 0; i < assignmentList.Count(); i++)
            {
                if (assignmentList[i].Date < System.DateTime.Today)
                {
                    assignmentList.RemoveAt(i);
                }
            }
            return RedirectToAction("Index");
        }


        // Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "AssignmentID,Date,Time,Title,Discription")] Assignment newAssignment)
        {
            //add new
            newAssignment.assignmentID = assignmentList.Count();
            assignmentList.Add(newAssignment);

            return RedirectToAction("Index");
        }

        // Edit
        [HttpGet]
        public ActionResult Edit(int assignmentID)
        {
            var tmp = assignmentList[assignmentID];
            return View(tmp);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "AssignmentID,Date,Time,Title,Discription")] Assignment newAssignment)
        {
            //delete old and add new
            assignmentList.RemoveAt(newAssignment.assignmentID);
            for (var i = 0; i < assignmentList.Count(); i++)
            {
                if (assignmentList[i].assignmentID > newAssignment.assignmentID)
                {
                    assignmentList[i].assignmentID--;
                }
            }

            newAssignment.assignmentID = assignmentList.Count();
            assignmentList.Add(newAssignment);

            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public ActionResult Delete(int assignmentID)
        {
            //delete old
            assignmentList.RemoveAt(assignmentID);
            for (var i = 0; i < assignmentList.Count(); i++)
            {
                if (assignmentList[i].assignmentID > assignmentID)
                {
                    assignmentList[i].assignmentID--;
                }
            }

            return RedirectToAction("Index");
        }
    }
}