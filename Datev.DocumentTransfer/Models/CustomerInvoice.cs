namespace Datev.DocumentTransfer.Models
{
    public class CustomerInvoice : BaseInvoice
    {
        public string CustomerName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerAccount { get; set; }

        protected override string GetName() => CustomerName ?? "";
        protected override string GetCity() => CustomerCity ?? "";
        protected override string GetPartnerAccount() => CustomerAccount ?? "";
    }

}
