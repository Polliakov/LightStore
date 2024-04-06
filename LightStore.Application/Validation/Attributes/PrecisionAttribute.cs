using System.ComponentModel.DataAnnotations;

namespace LightStore.Application.Validation.Attributes
{
    public class PrecisionAttribute : RegularExpressionAttribute
    {
        public PrecisionAttribute(int precision, int scale)
            : base($@"^(0|-?\d{{0,{precision - scale}}}(\.\d{{0,{scale}}})?)$") 
        {
            ErrorMessage = $"Only numbers with precision < {precision} and scale < {scale} allowed.";
        }
    }
}
