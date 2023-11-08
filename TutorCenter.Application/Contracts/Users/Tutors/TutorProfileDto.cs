using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Contracts.Users.Tutors
{
    public class TutorProfileDto
    {
        public PaginatedList<RequestGettingClassForListDto> RequestGettingClassForListDtos = new();
        public TutorMainInfoDto TutorMainInfoDto = new();
    }
}
