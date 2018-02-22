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
        //int[,] grid =
        //    {
        //        { 2,2,2,2,2,2,2,2,2,2,2,2 },
        //        { 2,0,0,0,0,0,0,0,0,0,0,2 },
        //        { 2,0,0,0,0,0,0,0,0,0,0,2 },
        //        { 2,0,0,0,1,0,0,1,0,0,0,2 },
        //        { 2,0,0,0,1,0,0,1,0,0,0,2 },
        //        { 2,0,0,0,1,0,0,1,0,0,0,2 },
        //        { 2,0,1,0,0,0,0,0,0,1,0,2 },
        //        { 2,0,0,1,0,0,0,0,1,0,0,2 },
        //        { 2,0,0,0,1,1,1,1,0,0,0,2 },
        //        { 2,0,0,0,0,0,0,0,0,0,0,2 },
        //        { 2,0,0,0,0,0,0,0,0,0,0,2 },
        //        { 2,2,2,2,2,2,2,2,2,2,2,2 }
        //    };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //12x12 grid, the outside edges are the dead zone, and not displayed
            
            //int [,] grid = new int [101,101];

            ViewData["Grid"] = GameGrid.grid;

            return View();
        }

        [HttpPost]
        public IActionResult Toggle(string cell)
        {
            string[] sIndex;
            int[] iIndex = new int[2];

            if (cell != null)
            {
                try
                {
                    sIndex = cell.Split(',');
                    iIndex[0] = Convert.ToInt32(sIndex[0]);
                    iIndex[1] = Convert.ToInt32(sIndex[1]);
                    if (GameGrid.grid[iIndex[0], iIndex[1]] == 0)
                    {
                        GameGrid.grid[iIndex[0], iIndex[1]] = 1;
                    } else if(GameGrid.grid[iIndex[0], iIndex[1]] == 1)
                    {
                        GameGrid.grid[iIndex[0], iIndex[1]] = 0;
                    } else
                    {
                        //Invalid grid index somehow
                    }
                } catch
                {

                }
            }
            ViewData["Grid"] = GameGrid.grid;
            return View("About");
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
