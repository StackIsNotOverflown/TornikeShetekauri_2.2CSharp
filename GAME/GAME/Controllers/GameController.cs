using Microsoft.AspNetCore.Mvc;
using GAME.Models;

namespace HigherLowerGame.Controllers
{
    public class GameController : Controller
    {
        private static int _target = new Random().Next(1, 101);
        private static int _count = 0;

        [HttpGet]
        public IActionResult Index()
        {
            return View(new GameModel
            {
                Message = "Guess a number between 1 and 100!"
            });
        }

        [HttpPost]
        public IActionResult Index(GameModel model)
        {
            _count++;

            if (!model.Guess.HasValue)
            {
                model.Message = "Please enter a number.";
            }
            else if (model.Guess < _target)
            {
                model.Message = "Too low!";
            }
            else if (model.Guess > _target)
            {
                model.Message = "Too high!";
            }
            else
            {
                model.Message = $"Correct! You guessed it in {_count} attempts.";
                _target = new Random().Next(1, 101);
                _count = 0;
            }

            model.GuessCount = _count;
            return View(model);
        }
    }
}
