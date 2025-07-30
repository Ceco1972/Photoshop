using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.DTOMapper
{
    public static class VoteDTOMapper
    {
        public static VoteDTO GetDTO(this Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new VoteDTO
            {
                Picture = vote.Picture.GetDTO(),
                PictureId = vote.PictureId,
                User = vote.User.GetDTO(),
                UserId = vote.UserId,
                Contest = vote.Contest.GetDTO(),
                ContestId = vote.ContestId,
                Rating = vote.Rating,
                Comment = vote.Comment
            };
        }
        public static Vote GetVote(this VoteDTO voteDTO)
        {
            if (voteDTO == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new Vote
            {
                PictureId = voteDTO.PictureId,
                UserId = voteDTO.UserId,
                ContestId = voteDTO.ContestId,
                Rating = voteDTO.Rating,
                Comment = voteDTO.Comment
            };
        }
        public static IEnumerable<VoteDTO> GetDTO(this IEnumerable<Vote> votes)
        {
            return votes.Select(GetDTO).ToList();
        }
    }
}
