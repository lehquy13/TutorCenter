using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Common.Errors.Courses
{
    public class NonExistRequestError : IError
    {
        public string Message { get; init; } = "This request in the class doesn't exist!";
        public Dictionary<string, object> Metadata { get; } = new();
        public List<IError> Reasons { get; } = new();
    }
}
