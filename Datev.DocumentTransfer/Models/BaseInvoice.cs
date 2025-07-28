using System;
using System.Collections.Generic;
using System.Globalization;

namespace Datev.DocumentTransfer.Models
{
    /// <summary>
    /// Base class for shared invoice fields with strong typing
    /// </summary>
    public abstract class BaseInvoice
    {
        // "Währung" (Pflicht) | 3-stellig | EUR, USD, CHF etc.
        public string Currency { get; set; }

        // "VorzBetrag" (Pflicht) | decimal (+/-) | 10,2
        public decimal Amount { get; set; }

        // "RechNr" (Pflicht) | max 36 alphanumerisch
        public string InvoiceNumber { get; set; }

        // "Belegdatum" (Pflicht) | TTMM (Jahr wird im Importdialog ergänzt)
        public string InvoiceDate { get; set; }

        // "InterneRechNr" | max 12 alphanumerisch
        public string InternalInvoiceNumber { get; set; }

        // "BU" | numerisch, max 4
        public int BookingKey { get; set; }

        // "Konto" | numerisch, max 8
        public int Account { get; set; }

        // "Kontobezeichnung" | max 30 alphanumerisch
        public string AccountName { get; set; }

        // "Ware/Leistung" | max 30 alphanumerisch
        public string Description { get; set; }

        // "Fällig_am" | TTMMJJJJ
        public DateTime DueDate { get; set; }

        // "gezahlt_am" | TTMMJJJJ
        public DateTime PaidDate { get; set; }

        // "UStSatz" | decimal, max 5 (xx,xx)
        public decimal VatRate { get; set; }

        // "USt-IdNr." | max 15 alphanumerisch
        public string VatId { get; set; }

        // "KundenNr." | max 15 alphanumerisch
        public string CustomerNumber { get; set; }

        // "KOST1" | max 36 alphanumerisch
        public string CostCenter1 { get; set; }

        // "KOST2" | max 36 alphanumerisch
        public string CostCenter2 { get; set; }

        // "KOSTmenge" | decimal 12,4
        public decimal CostAmount { get; set; }

        // "Kurs" | decimal 4,6
        public decimal ExchangeRate { get; set; }

        // "Skonto" | decimal 8,2
        public decimal CashDiscount { get; set; }

        // "Nachricht" | max 120 alphanumerisch
        public string Message { get; set; }

        // "Skto_Fällig_am" | TTMMJJJJ
        public DateTime CashDiscountDueDate { get; set; }

        // "BankKonto" | numerisch max 10
        public long BankAccount { get; set; }

        // "BankBlz" | numerisch max 8
        public int BankCode { get; set; }

        // "Bankname" | max 27 alphanumerisch
        public string BankName { get; set; }

        // "BankIban" | max 34 alphanumerisch
        public string BankIban { get; set; }

        // "BankBic" | max 11 alphanumerisch
        public string BankBic { get; set; }

        // "Skto_Proz" | decimal 2,2
        public decimal CashDiscountPercent { get; set; }

        // "Leistungsdatum" | TTMMJJJJ
        public DateTime ServiceDate { get; set; }

        /// <summary>
        /// Converts the invoice to a CSV line according to DATEV Belegtransfer
        /// </summary>
        public virtual string ToCsvLine()
        {
            var values = new List<string>
            {
                Quote(Currency),
                FormatDecimal(Amount),
                Quote(InvoiceNumber),
                Quote(InvoiceDate),
                Quote(InternalInvoiceNumber),
                Quote(GetName()),
                Quote(GetCity()),
                Quote(GetPartnerAccount()),
                BookingKey != 0 ? BookingKey.ToString() : "",
                Account != 0 ? Account.ToString() : "",
                Quote(AccountName),
                Quote(Description),
                FormatDate(DueDate),
                FormatDate(PaidDate),
                FormatDecimal(VatRate),
                Quote(VatId),
                Quote(CustomerNumber),
                Quote(CostCenter1),
                Quote(CostCenter2),
                FormatDecimal(CostAmount,4),
                FormatDecimal(ExchangeRate,6),
                FormatDecimal(CashDiscount),
                Quote(Message),
                FormatDate(CashDiscountDueDate),
                BankAccount != 0 ? BankAccount.ToString() : "",
                BankCode != 0 ? BankCode.ToString() : "",
                Quote(BankName),
                Quote(BankIban),
                Quote(BankBic),
                FormatDecimal(CashDiscountPercent,2),
                FormatDate(ServiceDate)
            };

            // Entferne trailing leere Felder
            for (int i = values.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(values[i])) break;
                values.RemoveAt(i);
            }

            return string.Join(";", values);
        }

        protected abstract string GetName();
        protected abstract string GetCity();
        protected abstract string GetPartnerAccount();

        private static string Quote(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        private static string FormatDecimal(decimal value, int scale = 2)
        {
            if (value == 0) return "";
            return value.ToString($"0.{new string('0', scale)}", new CultureInfo("de-DE"));
        }

        private static string FormatDate(DateTime date)
        {
            return date != default(DateTime) ? date.ToString("ddMMyyyy") : "";
        }
    }
}
