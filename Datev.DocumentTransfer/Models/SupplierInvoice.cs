namespace Datev.DocumentTransfer.Models
{
    public class SupplierInvoice : BaseInvoice
    {
        public string SupplierName { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierAccount { get; set; }

        protected override string GetName() => SupplierName ?? "";
        protected override string GetCity() => SupplierCity ?? "";
        protected override string GetPartnerAccount() => SupplierAccount ?? "";
    }
}
