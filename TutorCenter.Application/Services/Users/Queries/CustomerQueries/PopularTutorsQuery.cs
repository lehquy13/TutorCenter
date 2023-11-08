using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries
{
    public class PopularTutorsQuery : IRequest<List<TutorForDetailDto>>
    {

    }

}
