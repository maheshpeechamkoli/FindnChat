using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindnChitChat.Model;
using Microsoft.EntityFrameworkCore;

namespace FindnChitChat.Data
{
    public class FindingRepository : IFindingRepository
    {
        public readonly DataContext _context;
        public FindingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id); 

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
             var user = await _context.Users.Include(p => p.Photos).ToListAsync();

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhoto(int id)
        {
             var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }
        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
             var photo = await _context.Photos.Where(p => p.UserId == userId)
                        .FirstOrDefaultAsync(p => p.IsMain);

            return photo;
        }

    }
}