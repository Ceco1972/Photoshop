using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.DTOMapper
{
    public static class PictureDTOMapper
    {
        public static PictureDTO GetDTO(this Picture picture)
        {
            if (picture == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new PictureDTO
            {
                Id = picture.Id,
                Title = picture.Title,
                Story = picture.Story,
                Author = picture.Author,
                AuthorId = picture.UserId,
                Rating = picture.Rating,
                VoteId = picture.VoteId,
                IsVoted = picture.IsVoted,
                CreatedOn = picture.CreatedOn,
                ContestId = picture.ContestId,
                ImageFile = picture.ImageFile,
            };
        }

        public static Picture GetPicture(this PictureDTO pictureDTO)
        {
            if (pictureDTO == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new Picture
            {
                Id = pictureDTO.Id,
                Title = pictureDTO.Title,
                Story = pictureDTO.Story,
                Author = pictureDTO.Author,
                UserId = pictureDTO.AuthorId,
                Rating = pictureDTO.Rating,
                VoteId = pictureDTO.VoteId,
                IsVoted = pictureDTO.IsVoted,
                CreatedOn = pictureDTO.CreatedOn,
                ContestId = pictureDTO.ContestId,
                ImageFile = pictureDTO.ImageFile,

            };
        }
        public static IEnumerable<PictureDTO> GetDTO(this IEnumerable<Picture> pictures)
        {
            return pictures.Select(GetDTO).ToList();
        }
    }
}
