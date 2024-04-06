using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Validation.Attributes
{
    public class PositiveIntegerAttribute : RangeAttribute
    {
        public PositiveIntegerAttribute()
            : base(1, int.MaxValue)
        {
            ErrorMessage = "Only positive integers allowed.";
        }
    }
}
