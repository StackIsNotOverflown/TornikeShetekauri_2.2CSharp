using Game.Models;

using Microsoft.AspNetCore.Mvc;

namespace HigherLowerGame.Controllers
{
    public class GameController : Controller
    {
        private static List<Card> AllCards = new List<Card>
        {
new Card { Title = "Keygen Church", AVG_Search = 22200, Image_URI = "https://upload.wikimedia.org/wikipedia/commons/d/d0/Keygen_Church_live_2017.jpg" },
    new Card { Title = "Gulab Jamun", AVG_Search = 2000, Image_URI = "https://upload.wikimedia.org/wikipedia/commons/0/07/Gulab_Jamun.jpg" },
    new Card { Title = "Lionel Messi", AVG_Search = 500000, Image_URI = "https://upload.wikimedia.org/wikipedia/commons/8/88/Lionel_Messi_20180626.jpg" },
    new Card { Title = "Minecraft", AVG_Search = 1000000, Image_URI = "https://upload.wikimedia.org/wikipedia/en/5/51/Minecraft_cover.png" },
    new Card { Title = "Instagram", AVG_Search = 900000, Image_URI = "https://upload.wikimedia.org/wikipedia/commons/e/e7/Instagram_logo_2016.svg" },
        };

        private static Card CurrentCard;
        private static Card NextCard;
        private static int Score = 0;

        public IActionResult Game()
        {
            if (CurrentCard == null)
            {
                Random rnd = new Random();
                CurrentCard = AllCards[rnd.Next(AllCards.Count)];
                NextCard = AllCards[rnd.Next(AllCards.Count)];

                while (NextCard.Title == CurrentCard.Title)
                    NextCard = AllCards[rnd.Next(AllCards.Count)];
            }

            ViewBag.Current = CurrentCard;
            ViewBag.Next = NextCard;
            ViewBag.Score = Score;

            return View();
        }

        [HttpPost]
        public IActionResult Check(string guess)
        {
            bool isCorrect = false;

            if ((guess == "Higher" && NextCard.AVG_Search >= CurrentCard.AVG_Search) ||
                (guess == "Lower" && NextCard.AVG_Search <= CurrentCard.AVG_Search))
            {
                isCorrect = true;
            }

            if (isCorrect)
            {
                Score++;
                CurrentCard = NextCard;
                Random rnd = new Random();
                NextCard = AllCards[rnd.Next(AllCards.Count)];
                while (NextCard.Title == CurrentCard.Title)
                    NextCard = AllCards[rnd.Next(AllCards.Count)];

                return RedirectToAction("Game");
            }
            else
            {
                int finalScore = Score;
                ResetGame();
                ViewBag.FinalScore = finalScore;
                return View("GameOver");
            }
        }

        public IActionResult Restart()
        {
            ResetGame();
            return RedirectToAction("Game");
        }

        private void ResetGame()
        {
            CurrentCard = null;
            NextCard = null;
            Score = 0;
        }

        public IActionResult GameOver()
        {
            return View();
        }
    }
}
