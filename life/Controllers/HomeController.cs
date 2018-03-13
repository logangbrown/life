using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using life.Models;

namespace life.Controllers
{
    public class HomeController : Controller
    {
        public static GameGrid gameService;

        public HomeController(GameGrid gameServicePassed)
        {
            gameService = gameServicePassed;
        }

        public IActionResult Index()
        {
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
                gameService.updated = true;
            }
            ViewData["Grid"] = gameService.grid;
            return View("Index");
        }

        [HttpPost]
        public IActionResult AdvanceGame()
        {
            gameService.advance();
            ViewData["Grid"] = gameService.grid;
            return View("Index");
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
            return View("Index");
        }

        [HttpPost]
        public IActionResult Stop()
        {
            if (gameService.isRunning)
            {
                gameService.isRunning = false;
            }
            ViewData["Grid"] = gameService.grid;
            return View("Index");
        }

        [HttpPost]
        public int[,] Update()
        {
            return gameService.grid;
        }

        [HttpPost]
        public void Clear()
        {
            gameService.grid = new int[GameGrid.y, GameGrid.x];
            gameService.isRunning = false;
            gameService.updated = true;
        }

        [HttpPost]
        public void ChangeTime(string milliseconds)
        {
            try
            {
                gameService.milliseconds = Int32.Parse(milliseconds);
            }
            catch { }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
