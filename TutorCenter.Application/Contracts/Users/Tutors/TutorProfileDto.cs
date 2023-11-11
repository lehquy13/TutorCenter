using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Contracts.Users.Tutors
{
    public class TutorProfileDto
    {
        public PaginatedList<CourseRequestForListDto> RequestGettingClassForListDtos = new();
        //public TutorMainInfoDto TutorMainInfoDto = new();
    }
}
