using TutorCenter.Domain.Shared.ClassInformationConsts;

namespace TutorCenter.Web.Models;

public class TotalValueModel<T> where  T : class
{
    public List<T> Models = new List<T>();
    public bool IsIncrease = false;
    public double IncreasePercentage = 0;
    public string Time = ByTime.Today;
}