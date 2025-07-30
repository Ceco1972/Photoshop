using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Service
{
    public interface IPictureService
    {
        List<Picture> Get();
        Picture GetPictureById(int pictureId);
        IEnumerable<Picture> GetAllPictures(int contestId,PictureQueryParameters filterParameters);
        IEnumerable<Picture> GetPictures(int contestId);
        VoteDTO GetVote(int pictureId);
        Task Vote(VoteDTO voteDTO);
        IEnumerable<Picture> GetUserPictures(int userId);
        public Task Insert(PictureDTO pictureDTO);
        public bool IsUserPictures(int id, string username);

    }
}
