using Microsoft.EntityFrameworkCore;
using PhotoContest.Models.Models;
using PhotoContest.Repository.Contracts;
using PhotoContest.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private readonly ApplicationDbContext _context;
        public PictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Picture GetPictureById(int pictureId)
        {
            var picture = this._context
               .Pictures
               .Where(p => p.Id == pictureId)
               .Include(p => p.Vote)
               .FirstOrDefault();
            return picture;
        }
        //Check if there is a picture of this user in this contest
        public bool IsUserPictures(int id, string username)
        {
            IQueryable<Picture> isContestId = this._context.Pictures.Where(c => c.ContestId == id);
            bool isAuthor = isContestId.Any(c => c.Author.Username == username);

            return isAuthor;

        }

        public IQueryable<Picture> GetPictures(int contestId)
        {
            return this._context
                .Pictures
                .Where(c => c.ContestId == contestId);
        }
        public Vote GetPictureVote(int pictureId)
        {
            var picture = this.GetPictureById(pictureId);
            return picture.Vote;
        }
        public IEnumerable<Picture> GetPictures(int contestId, PictureQueryParameters filterparameters)
        {
            string title = !string.IsNullOrEmpty(filterparameters.Title) ? filterparameters.Title.ToLowerInvariant() : string.Empty;
            string username = !string.IsNullOrEmpty(filterparameters.Username) ? filterparameters.Username.ToLowerInvariant() : string.Empty;
            string sortCriteria = !string.IsNullOrEmpty(filterparameters.SortBy) ? filterparameters.SortBy.ToLowerInvariant() : string.Empty;
            string sortOreder = !string.IsNullOrEmpty(filterparameters.SortOreder) ? filterparameters.SortOreder.ToLowerInvariant() : string.Empty;

            IQueryable<Picture> picture = this.GetPictures(contestId);

            picture = FilterByTitle(picture, title);
            picture = SortBy(picture, sortCriteria);
            picture = FilterByUser(picture, username);
            picture = Order(picture, sortOreder);


            return picture;
        }
        private static IQueryable<Picture> FilterByTitle(IQueryable<Picture> pictures, string title)
        {
            return pictures.Where(picture => picture.Title.Contains(title));
        }
        private static IQueryable<Picture> FilterByUser(IQueryable<Picture> pictures, string username)
        {
            return pictures.Where(picture => picture.Author.Username.Contains(username));
        }
        private static IQueryable<Picture> Order(IQueryable<Picture> picture, string sortOrder)
        {
            return (sortOrder == "desc") ? picture.Reverse() : picture;
        }
        private static IQueryable<Picture> SortBy(IQueryable<Picture> pictures, string sortCriteria)
        {
            switch (sortCriteria.ToLower())
            {
                case "title":
                    return pictures.OrderBy(picture => picture.Title);
                case "createdon":
                    return pictures.OrderBy(picture => picture.CreatedOn);
                case "rating":
                    return pictures.OrderBy(picture => picture.Rating);
                case "username":
                    return pictures.OrderBy(picture => picture.Author.Username);
                default:
                    break;
            }
            return pictures;
        }
        private async Task Save()
        {
            await this._context.SaveChangesAsync();
        }
        public IEnumerable<Picture> GetUserPictures(int userId)
        {
            return this._context
                .Pictures
                .Where(p => p.Author.Id == userId);
        }
        public async Task<Picture> Insert(Picture picture)
        {
            this._context.Pictures.Add(picture);
            await this.AuthorScoreIncrease(picture.Author.Id);
            await this.Save();
            
            return picture;
        }
        public async Task VotePicture(Vote vote)
        {
            await this.MarkAsVoted(vote.PictureId, vote.Rating);
            this._context.Votes.Add(vote);
            await this.Save();
        }
        private async Task AuthorScoreIncrease(int authorId)
        {
            var user = this._context.Users.FirstOrDefault(x => x.Id == authorId);
            user.Score++;
            this._context
             .Entry(user)
             .State = EntityState.Modified;
            await this.Save();
        }
        private async Task MarkAsVoted(int pictureId, int rating)
        {
            var picture = this.GetPictureById(pictureId);
            picture.IsVoted = true;
            picture.Rating += rating;
            this._context
              .Entry(picture)
              .State = EntityState.Modified;
            await this.Save();
        }
        public async Task Update(Picture picture)
        {
            this._context
             .Entry(picture)
             .State = EntityState.Modified;
            await this.Save();
        }
    }
}
