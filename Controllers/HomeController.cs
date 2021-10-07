using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            if(Happiness==null || Fullness==null || Energy==null || Meals==null)
            {
                Happiness =20;
                Fullness =20;
                Energy =50;
                Meals= 3;            
                HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                HttpContext.Session.SetInt32("Energy", (int)Energy);
                HttpContext.Session.SetInt32("Meals", (int)Meals);
            }

            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");        
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");        
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            if(Energy>=100 && Fullness>=100 && Happiness>=100)
            {
                return View("Win");
            }
            else if(Fullness<=0 || Happiness<=0 || Energy<=0)
            {
                return View("Lose");
            }
            return View();
        }

        [HttpPost("play")]
        public IActionResult Play()
        {
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            if(Energy!=5)
            {
                Random rand = new Random();
                int fun = rand.Next(4);
                if(fun!=0)
                {
                    int happy = rand.Next(5,11);
                    Happiness+=happy;
                    HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                    ViewBag.Happiness = Happiness;

                    Energy-=5;
                    HttpContext.Session.SetInt32("Energy", (int)Energy);
                    ViewBag.Energy = Energy;

                    HttpContext.Session.SetString("Message", "Mochi Had Fun!!!!");
                    string Message = HttpContext.Session.GetString("Message");

                    return RedirectToAction("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                    ViewBag.Happiness = Happiness;

                    Energy-=5;
                    HttpContext.Session.SetInt32("Energy", (int)Energy);
                    ViewBag.Energy = Energy;

                    HttpContext.Session.SetString("Message", "UGH MOCHI HATES GOLF!!!!");
                    string Message = HttpContext.Session.GetString("Message");

                    return RedirectToAction("Index");
                }
            }
            else
            {
                Energy-=5;
                HttpContext.Session.SetInt32("Energy", (int)Energy);
                ViewBag.Energy = Energy;
                HttpContext.Session.SetString("Message", "Mochi DIED OF EXHAUSTION YOU ASSHOLE! GAMEOVER...");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Index");
            }
        }
        [HttpPost("feed")]
        public IActionResult Feed()
        {
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            if(Meals!=0)
            {
                Random rand = new Random();
                int yum = rand.Next(4);
                if(yum!=0)
                {
                    int full = rand.Next(5,11);
                    Fullness+=full;
                    HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                    ViewBag.Fullness = Fullness;
                    Meals--;
                    HttpContext.Session.SetInt32("Meals", (int)Meals);
                    ViewBag.Meals = Meals;

                    HttpContext.Session.SetString("Message", "YUMMY!!!!");
                    string Message = HttpContext.Session.GetString("Message");
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                    ViewBag.Fullness = Fullness;
                    Meals--;
                    HttpContext.Session.SetInt32("Meals", (int)Meals);
                    ViewBag.Meals = Meals;

                    HttpContext.Session.SetString("Message", "YUCKY!!! MOCHI SPIT IT OUT");
                    string Message = HttpContext.Session.GetString("Message");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                HttpContext.Session.SetString("Message", "Not Enough Meals!");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Index");
            }
        }
        [HttpPost("work")]
        public IActionResult Work()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            if(Energy!=5)
            {
                Random rand = new Random();
                int productive = rand.Next(1,4);
                Energy-=5;
                HttpContext.Session.SetInt32("Energy", (int)Energy);
                ViewBag.Energy = Energy;
                Meals+=productive;
                HttpContext.Session.SetInt32("Meals", (int)Meals);
                ViewBag.Meals = Meals;

                HttpContext.Session.SetString("Message", "Mochi made some bomb-ass meal-prep. Pasta FOR DAYS. <3");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetString("Message", "MOCHI DIED OF EXHAUSTION WHILE MAKING THEIR PASTA. >:( GAMEOVER");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Lose");
            }
        }
        [HttpGet("Lose")]
        public IActionResult Lose()
        {
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");

            HttpContext.Session.SetInt32("Happiness", (int)Happiness);
            ViewBag.Happiness = Happiness;
                        
            HttpContext.Session.SetInt32("Fullness", (int)Fullness);
            ViewBag.Fullness = Fullness;
                        
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            ViewBag.Energy = Energy;
                        
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            ViewBag.Meals = Energy;

            return View();
        }
        [HttpPost("sleep")]
        public IActionResult Sleep()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            if(Happiness!=5 && Fullness!=5)
            {
                Energy+=15;
                HttpContext.Session.SetInt32("Energy", (int)Energy);
                ViewBag.Energy = Energy;
                Happiness-=5;
                HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                ViewBag.Happiness = Happiness;
                Fullness-=5;
                HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                ViewBag.Fullness = Fullness;

                HttpContext.Session.SetString("Message", "Mochi had a splendid naptime!!!!! :)");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Index");
            }
            else
            {
                Happiness-=5;
                HttpContext.Session.SetInt32("Happiness", (int)Happiness);
                ViewBag.Happiness = Happiness;
                Fullness-=5;
                HttpContext.Session.SetInt32("Fullness", (int)Fullness);
                ViewBag.Fullness = Fullness;
                HttpContext.Session.SetString("Message", "MOCHI DIED OF SADNESS/HUNGER IN THEIR SLEEP. RIP MOCHI. GAMEOVER!");
                string Message = HttpContext.Session.GetString("Message");
                return RedirectToAction("Index");
            }
        }



        [HttpPost("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
