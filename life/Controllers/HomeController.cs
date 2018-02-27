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
        GameGrid gameService;

        public HomeController(GameGrid gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //12x12 grid, the outside edges are the dead zone, and not displayed
            
            //int [,] grid = new int [101,101];

            ViewData["Grid"] = gameService.grid;

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
                    if (gameService.grid[iIndex[0], iIndex[1]] == 0)
                    {
                        gameService.grid[iIndex[0], iIndex[1]] = 1;
                    }
                    else if (gameService.grid[iIndex[0], iIndex[1]] == 1)
                    {
                        gameService.grid[iIndex[0], iIndex[1]] = 0;
                    }
                    else
                    {
                        //Invalid grid index somehow
                    }
                }
                catch
                {

                }
            }
            ViewData["Grid"] = gameService.grid;
            return View("About");
        }

        [HttpPost]
        public IActionResult AdvanceGame()
        {
            gameService.advance();
            ViewData["Grid"] = gameService.grid;
            return View("About");
        }

        [HttpPost]
        public IActionResult Start()
        {
            if (!gameService.isRunning)
            {
                gameService.isRunning = true;
                Task.Factory.StartNew(() => gameService.run());
            }
            ViewData["Grid"] = gameService.grid;
            return View("About");
        }

        [HttpPost]
        public IActionResult Stop()
        {
            if (gameService.isRunning)
            {
                gameService.isRunning = false;
            }
            ViewData["Grid"] = gameService.grid;
            return View("About");
        }

        public IActionResult Contact(String cell)
        {
            // Singleton.AddToContainer();


            ViewData["Message"] = "Your contact page.";
           // Container.Array = new Cell[100];


            if (cell != null)
            {
                Cell activeCell = new Cell();
                int cellLocation = Int32.Parse(cell);
                activeCell.ID = cellLocation;
                activeCell.Active = 1;
               // Container.Array[cellLocation] = activeCell;

                Singleton.grid[cellLocation] = new Cell();
                Singleton.grid[cellLocation].Active = 1;
                Singleton.grid[cellLocation].ID = cellLocation;


            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
