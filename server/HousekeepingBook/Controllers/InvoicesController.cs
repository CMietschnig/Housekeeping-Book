using HousekeepingBook.Entities;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousekeepingBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMonthlyInvoiceSummaryRepository _monthlyInvoiceSummaryRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository, IMonthlyInvoiceSummaryRepository monthlyInvoiceSummaryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _monthlyInvoiceSummaryRepository = monthlyInvoiceSummaryRepository;
        }

        [HttpPost("getInvoicesPerMonthAndYear")]
        public IEnumerable<Invoice> GetInvoicesPerMonthAndYear([FromBody] GetDataPerMonthAndYearModel model)
        {
            int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);

            var invoices = _invoiceRepository.GetInvoicesPerMonthlyInvoiceSummaryId(id);
            return invoices;
        }

        [HttpPost("getCommentPerMonthAndYear")]
        public string GetCommentPerMonthAndYear([FromBody] GetDataPerMonthAndYearModel model)
        {
            int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);

            var comment = _monthlyInvoiceSummaryRepository.GetCommentByMonthlyInvoiceSummaryId(id);
            return comment;
        }

        [HttpPost("addInvoiceToMonthAndYear")]
        public string AddInvoiceToMonthAndYear([FromBody] AddInvoiceToMonthAndYearModel model)
        {
            int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);

            Invoice invoice = new Invoice()
            {
                Total = model.InvoiceTotal,
                CreateTimestamp = DateTime.Now,
                UpdateTimestamp = DateTime.Now,
                MonthlyInvoiceSummaryId = id,
                Store = null // store hinzufügen
            };

            _invoiceRepository.AddInvoiceToMonthAndYear(invoice);

            
            return "AddInvoiceToMonthAndYear " + model.Month + " " + model.Year + " " + model.InvoiceTotal;
        }

        [HttpPut("updateInvoiceById")]
        public string UpdateInvoiceById([FromBody] UpdateInvoiceByIdModel model)
        {
            var oldInvoice = _invoiceRepository.GetInvoiceById(model.Id);

            Invoice newModel = new Invoice()
            {
                InvoiceId = model.Id,
                Total = model.invoiceTotal,
                CreateTimestamp = oldInvoice.CreateTimestamp,
                UpdateTimestamp = DateTime.Now,
                MonthlyInvoiceSummaryId = oldInvoice.MonthlyInvoiceSummaryId,
                Store = oldInvoice.Store
            };

            _invoiceRepository.UpdateInvoiceById(newModel);
           
            return "UpdateInvoiceById " + model.Id + " " + model.invoiceTotal;
        }

        [HttpPut("updateCommentByMonthAndYear")]
        public string UpdateCommentByMonthAndYear([FromBody] UpdateCommentByMonthAndYearModel model)
        {
            int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);

            bool commentUpdated = _monthlyInvoiceSummaryRepository.UpdateComment(id, model.Comment);

            return commentUpdated ? "sucess" : "fail";
        }

        [HttpPost("deleteInvoiceById")]
        public string DeleteInvoiceById([FromBody] DeleteInvoiceByIdModel model)
        {
            var test = _invoiceRepository.DeleteInvoiceById(model);
            return "DeleteInvoiceById " + model.Id;
        }

    }
}
