using LabWork23.Contexts;
using LabWork23.Models;

namespace LabWork23.Service
{
    public class GameService(GameContext context)
    {
        GameContext _context = context;

        public List<Game> GetGames()
            => _context.Games.ToList();

        public List<Game> UpdateGameLogo(Game game, string filePath)
        {
            game.LogoFile = filePath;
            _context.Games.Update(game);
            _context.SaveChanges();

            return GetGames();
        }
    }
}
