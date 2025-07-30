using PhotoContest.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Repository.Contracts
{
    public interface IPictureRepository
    {
        IEnumerable<Picture> GetPictures(int contestId, PictureQueryParameters filterparameters);
        IQueryable<Picture> GetPictures(int contestId);
        Picture GetPictureById(int pictureId);
        Task VotePicture(Vote vote);
        Vote GetPictureVote(int pictureId);
        IEnumerable<Picture> GetUserPictures(int userId);
        Task<Picture> Insert(Picture picture);
        public bool IsUserPictures(int id, string username);
        Task Update(Picture picture);

    }
}
