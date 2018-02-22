using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using life.Models;

namespace life.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //int [,] grid =
            //{
            //    { 2,2,2,2,2,2,2,2,2,2,2,2 },
            //    { 2,0,0,0,0,0,0,0,0,0,0,2 },
            //    { 2,0,0,0,0,0,0,0,0,0,0,2 },
            //    { 2,0,0,0,1,0,0,1,0,0,0,2 },
            //    { 2,0,0,0,1,0,0,1,0,0,0,2 },
            //    { 2,0,0,0,1,0,0,1,0,0,0,2 },
            //    { 2,0,1,0,0,0,0,0,0,1,0,2 },
            //    { 2,0,0,1,0,0,0,0,1,0,0,2 },
            //    { 2,0,0,0,1,1,1,1,0,0,0,2 },
            //    { 2,0,0,0,0,0,0,0,0,0,0,2 },
            //    { 2,0,0,0,0,0,0,0,0,0,0,2 },
            //    { 2,2,2,2,2,2,2,2,2,2,2,2 }
            //};
            int [,] grid = new int [101,101];

            ViewData["Grid"] = grid;

            return View();
        }

        public IActionResult Contact(String cell)
        {
            ViewData["Message"] = "Your contact page.";
            Container.Array = new Cell[100];


            if (cell != null)
            {
                Cell activeCell = new Cell();
                int cellLocation = Int32.Parse(cell);
                activeCell.ID = cellLocation;
                activeCell.Active = 1;
                Container.Array[cellLocation] = activeCell;
            }

            
            
            

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
