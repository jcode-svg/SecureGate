using System.ComponentModel.DataAnnotations;

namespace SecureGate.SharedKernel.Models
{
    public class PaginatedRequest
    {
        [Required(ErrorMessage = "Page is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than or equal to 1")]
        public int Page { get; set; } = 1;

        [Required(ErrorMessage = "PageSize is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1")]
        public int PageSize { get; set; } = 10;
    }
}
