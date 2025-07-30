using PhotoContest.Models.DTOs;
using PhotoContest.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models.DTOMapper
{
    public static class PostDTOMapper
    {
        public static ContestDTO GetDTO(this Contest contest)
        {
            if (contest == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new ContestDTO
            {
               Id = contest.Id,
               Title = contest.Title,
               Category = contest.Category,
               ContestType = contest.ContestType,
               FirstPhaseEnd = contest.FirstPhaseEnd,
               DueDate = contest.DueDate,
               IsOpen = contest.IsOpen,
               OrganizerId = contest.OrganizerId,
               Phase = contest.Phase,
               Pictures = contest.Pictures.GetDTO(),
               Participants = contest.Participants
            };
        }

        public static Contest GetContest(this ContestDTO contestDTO)
        {
            if (contestDTO == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new Contest
            {
                Title = contestDTO.Title,
                Category = contestDTO.Category,
                ContestType = contestDTO.ContestType,
                FirstPhaseEnd = contestDTO.FirstPhaseEnd,
                DueDate = contestDTO.DueDate,
                IsOpen = contestDTO.IsOpen,
                OrganizerId = contestDTO.OrganizerId,
                Phase = contestDTO.Phase,
            };
        }
        public static IEnumerable<ContestDTO> GetDTO(this IEnumerable<Contest> contest)
        {
            return contest.Select(GetDTO);
        }
    }
}
