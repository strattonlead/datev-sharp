using Datev.DocumentTransfer;
using Datev.DocumentTransfer.Models;
using System;
using System.IO;

namespace DatevSharp.Tests
{
    public class DocumentTransferExporterTests
    {
        private CustomerInvoice GetValidInvoice()
        {
            return new CustomerInvoice
            {
                Currency = "EUR",
                Amount = -120.00m,
                InvoiceNumber = "12345",
                InvoiceDate = "1201",
                CustomerName = "Muster GmbH",
                CustomerCity = "Nürnberg",
                VatRate = 19.0m
            };
        }

        [Fact]
        public void Validate_ShouldReturnNoErrors_ForValidInvoice()
        {
            var invoice = GetValidInvoice();
            var errors = DocumentTransferExporter.Validate(invoice);
            Assert.Empty(errors);
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenAmountIsZero()
        {
            var invoice = GetValidInvoice();
            invoice.Amount = 0;
            var errors = DocumentTransferExporter.Validate(invoice);
            Assert.Contains(errors, e => e.Contains("Amount"));
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenCurrencyInvalid()
        {
            var invoice = GetValidInvoice();
            invoice.Currency = "XXX";
            var errors = DocumentTransferExporter.Validate(invoice);
            Assert.Contains(errors, e => e.Contains("Currency"));
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenInvoiceNumberTooLong()
        {
            var invoice = GetValidInvoice();
            invoice.InvoiceNumber = new string('A', 37);
            var errors = DocumentTransferExporter.Validate(invoice);
            Assert.Contains(errors, e => e.Contains("InvoiceNumber"));
        }

        [Fact]
        public void Export_ShouldWriteCsvFile_WithHeaderAndData()
        {
            var invoice = GetValidInvoice();
            string path = Path.GetTempFileName();
            DocumentTransferExporter.Export(new[] { invoice }, path);

            string[] lines = File.ReadAllLines(path, DocumentTransferExporter.Encoding);
            Assert.Equal(2, lines.Length);
            Assert.StartsWith("\"Währung\"", lines[0]);
            Assert.Contains("EUR", lines[1]);
            Assert.Contains("-120,00", lines[1]);
        }

        [Fact]
        public void Export_ShouldThrow_WhenInvoiceInvalid()
        {
            var invoice = GetValidInvoice();
            invoice.Currency = "INVALID";

            string path = Path.GetTempFileName();
            Assert.Throws<InvalidOperationException>(() =>
                DocumentTransferExporter.Export(new[] { invoice }, path));
        }
    }
}
