namespace HamburgerciProject.Presentation.Models.VMs
{
    public class ConfirmMailViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public int? ConfirmCode { get; set; }

    }
}
