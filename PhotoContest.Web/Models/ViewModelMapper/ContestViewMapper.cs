using PhotoContest.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.ViewModelMapper
{
    public static class ContestViewMapper
    {
        public static ContestViewModel GetContestView(this ContestDTO dtoModel)
        {
            if (dtoModel == null)
            {
                throw new ArgumentException("Null value passed.");
            }
            return new ContestViewModel
            {
                Title = dtoModel.Title,
                Category = dtoModel.Category

            };
        }

        public static IEnumerable<ContestViewModel> GetView(this IEnumerable<ContestDTO> contests)
        {
            return contests.Select(GetContestView);
        }
    }
}
