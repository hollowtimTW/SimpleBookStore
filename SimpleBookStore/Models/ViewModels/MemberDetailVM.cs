namespace SimpleBookStore.Models.ViewModels
{
    public class MemberDetailVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<OrderHeader> Orders { get; set; }
    }
}
