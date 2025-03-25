using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transacation
    {
        [Key]
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }
        public Category? category { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date {  get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return category == null? "": category.Icon + " " + category.Title;
            }

        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((category == null || category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
    }
}
