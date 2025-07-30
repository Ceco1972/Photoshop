using PhotoContest.Common.Helpers;
using PhotoContest.Models.DTOMapper;
using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using PhotoContest.Repository.Contracts;
using PhotoContest.Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Service
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly AuthorisationHelper _authorisationHelper;

        public PictureService(IPictureRepository pictureRepository, AuthorisationHelper authorizationHelper) 
        {
            _pictureRepository = pictureRepository;
            this._authorisationHelper = authorizationHelper;
        }

        public bool IsUserPictures(int id, string username)
        {
            return this._pictureRepository.IsUserPictures(id, username);
        }


        public List<Picture> Get()
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Picture> GetAllPictures(int contestId,PictureQueryParameters filterParameters)
        {
            return this._pictureRepository.GetPictures(contestId,filterParameters);
        }
        public Picture GetPictureById(int pictureId)
        {
            return this._pictureRepository.GetPictureById(pictureId);
        }
        public IEnumerable<Picture> GetPictures(int contestId)
        {
            return this._pictureRepository.GetPictures(contestId);
        }
        public IEnumerable<Picture> GetUserPictures(int userId)
        {
            return this._pictureRepository.GetUserPictures(userId);
        }

        public VoteDTO GetVote(int pictureId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Vote> GetVotes(int pictureId)
        {
            return this.GetVotes(pictureId);
        }
        public async Task Insert(PictureDTO pictureDto)
        {
            //var user = _authorisationHelper.TryGetUser(username);
            var picture = pictureDto.GetPicture();
            picture.Vote = null;
            await this._pictureRepository.Insert(picture);
        }

        public async Task Vote(VoteDTO voteDTO)
        {
            await this._pictureRepository.VotePicture(voteDTO.GetVote());
        }
    }
}
