using Datev.DocumentTransfer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Datev.DocumentTransfer
{
    public class DocumentTransferExporter
    {
        private static readonly string Header =
            "\"Währung\";\"VorzBetrag\";\"RechNr\";\"Belegdatum\";\"InterneRechNr\";\"Name\";\"Ort\";\"Konto\";\"BU\";\"Sachkonto\";\"Kontobezeichnung\";\"Ware/Leistung\";\"Fällig_am\";\"gezahlt_am\";\"UStSatz\";\"USt-IdNr.\";\"KundenNr.\";\"KOST1\";\"KOST2\";\"KOSTmenge\";\"Kurs\";\"Skonto\";\"Nachricht\";\"Skto_Fällig_am\";\"BankKonto\";\"BankBlz\";\"Bankname\";\"BankIban\";\"BankBic\";\"Skto_Proz\";\"Leistungsdatum\";";

        public static Encoding Encoding
        {
            get
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return Encoding.GetEncoding(1252);
            }
        }

        /// <summary>
        /// Exportiert eine Liste von Rechnungen in eine DATEV-kompatible CSV-Datei.
        /// Bricht ab, wenn eine Rechnung ungültig ist.
        /// </summary>
        public static void Export<T>(IEnumerable<T> invoices, string filePath) where T : BaseInvoice
        {
            var allErrors = new List<string>();
            int index = 1;
            foreach (var invoice in invoices)
            {
                var errors = Validate(invoice);
                if (errors.Count > 0)
                    allErrors.AddRange(errors.Select(e => $"Invoice {index}: {e}"));
                index++;
            }

            if (allErrors.Count > 0)
                throw new InvalidOperationException("Validation failed:\n" + string.Join("\n", allErrors));

            var sb = new StringBuilder();
            sb.AppendLine(Header);
            foreach (var inv in invoices)
            {
                sb.AppendLine(inv.ToCsvLine());
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding); // ANSI (Windows-1252)
        }

        /// <summary>
        /// Validates an invoice according to DATEV rules.
        /// Returns a list of validation errors (empty if valid).
        /// </summary>
        public static List<string> Validate(BaseInvoice invoice)
        {
            var errors = new List<string>();

            // Pflichtfelder prüfen
            if (string.IsNullOrWhiteSpace(invoice.Currency))
                errors.Add("Currency (Währung) is required.");

            if (!Regex.IsMatch(invoice.Currency ?? "", "^(EUR|CAD|CHF|CZK|DKK|GBP|HKD|HUF|JPY|NOK|PLN|SEK|TND|USD)$"))
                errors.Add("Currency (Währung) must be a valid ISO code (EUR, USD, ...).");

            if (invoice.Amount == 0)
                errors.Add("Amount (VorzBetrag) must not be zero.");

            if (string.IsNullOrWhiteSpace(invoice.InvoiceNumber))
                errors.Add("InvoiceNumber (RechNr) is required.");
            else if (invoice.InvoiceNumber.Length > 36)
                errors.Add("InvoiceNumber (RechNr) exceeds max length (36).");

            if (string.IsNullOrWhiteSpace(invoice.InvoiceDate))
                errors.Add("InvoiceDate (Belegdatum) is required.");
            else if (!Regex.IsMatch(invoice.InvoiceDate, @"^\d{4}$"))
                errors.Add("InvoiceDate (Belegdatum) must be in TTMM format.");

            // Interne Rechnungsnummer max 12
            if (!string.IsNullOrEmpty(invoice.InternalInvoiceNumber) && invoice.InternalInvoiceNumber.Length > 12)
                errors.Add("InternalInvoiceNumber exceeds max length (12).");

            // Name max 40
            if (GetName(invoice).Length > 40)
                errors.Add("Name exceeds max length (40).");

            // Ort max 30
            if (GetCity(invoice).Length > 30)
                errors.Add("City exceeds max length (30).");

            // Konto
            if (invoice.Account < 0)
                errors.Add("Account must be positive.");
            if (invoice.Account.ToString().Length > 8)
                errors.Add("Account exceeds max length (8).");

            // BU (nur wenn gesetzt)
            if (invoice.BookingKey < 0)
                errors.Add("BookingKey (BU) must be positive.");
            if (invoice.BookingKey > 0 && invoice.BookingKey.ToString().Length > 4)
                errors.Add("BookingKey exceeds max length (4).");

            // Beschreibung
            if (!string.IsNullOrEmpty(invoice.Description) && invoice.Description.Length > 30)
                errors.Add("Description (Ware/Leistung) exceeds max length (30).");

            // Kontobezeichnung
            if (!string.IsNullOrEmpty(invoice.AccountName) && invoice.AccountName.Length > 30)
                errors.Add("AccountName exceeds max length (30).");

            // Nachricht
            if (!string.IsNullOrEmpty(invoice.Message) && invoice.Message.Length > 120)
                errors.Add("Message exceeds max length (120).");

            // USt-IdNr.
            if (!string.IsNullOrEmpty(invoice.VatId) && invoice.VatId.Length > 15)
                errors.Add("VatId exceeds max length (15).");

            // Kundennummer
            if (!string.IsNullOrEmpty(invoice.CustomerNumber) && invoice.CustomerNumber.Length > 15)
                errors.Add("CustomerNumber exceeds max length (15).");

            // KOST
            if (!string.IsNullOrEmpty(invoice.CostCenter1) && invoice.CostCenter1.Length > 36)
                errors.Add("CostCenter1 exceeds max length (36).");
            if (!string.IsNullOrEmpty(invoice.CostCenter2) && invoice.CostCenter2.Length > 36)
                errors.Add("CostCenter2 exceeds max length (36).");

            // Bankdaten
            if (!string.IsNullOrEmpty(invoice.BankName) && invoice.BankName.Length > 27)
                errors.Add("BankName exceeds max length (27).");
            if (!string.IsNullOrEmpty(invoice.BankIban) && invoice.BankIban.Length > 34)
                errors.Add("BankIban exceeds max length (34).");
            if (!string.IsNullOrEmpty(invoice.BankBic) && invoice.BankBic.Length > 11)
                errors.Add("BankBic exceeds max length (11).");

            // Beträge
            if (invoice.VatRate < 0 || invoice.VatRate > 100)
                errors.Add("VatRate must be between 0 and 100.");

            if (invoice.CashDiscountPercent < 0 || invoice.CashDiscountPercent > 100)
                errors.Add("CashDiscountPercent must be between 0 and 100.");

            // Datumskombination: Skonto
            if ((invoice.CashDiscount > 0 && invoice.CashDiscountDueDate == default(DateTime)) ||
                (invoice.CashDiscountPercent > 0 && invoice.CashDiscountDueDate == default(DateTime)))
            {
                errors.Add("CashDiscountDueDate must be set when CashDiscount or CashDiscountPercent is set.");
            }

            return errors;
        }

        private static string GetName(BaseInvoice inv)
        {
            if (inv is CustomerInvoice ci)
                return ci.CustomerName ?? "";
            if (inv is SupplierInvoice si)
                return si.SupplierName ?? "";
            return "";
        }

        private static string GetCity(BaseInvoice inv)
        {
            if (inv is CustomerInvoice ci)
                return ci.CustomerCity ?? "";
            if (inv is SupplierInvoice si)
                return si.SupplierCity ?? "";
            return "";
        }
    }
}
