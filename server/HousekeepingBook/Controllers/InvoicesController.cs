using HousekeepingBook.Entities;
using HousekeepingBook.Entities.Enums;
using HousekeepingBook.Interfaces;
using HousekeepingBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public IActionResult GetInvoicesPerMonthAndYear([FromBody] GetDataPerMonthAndYearModel model)
        {
            try
            {
                int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);
                if (id == 0)
                {
                    return NotFound($"No MonthlyInvoiceSummary found for month {model.Month} and year {model.Year}");
                }

                var invoices = _invoiceRepository.GetInvoicesPerMonthlyInvoiceSummaryId(id);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing GetInvoicesPerMonthAndYear: " + ex.Message);
            }
        }

        [HttpPost("getCommentPerMonthAndYear")]
        public IActionResult GetCommentPerMonthAndYear([FromBody] GetDataPerMonthAndYearModel model)
        {
            try
            {
                int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);
                if (id == 0)
                {
                    return NotFound($"No MonthlyInvoiceSummary found for month {model.Month} and year {model.Year}");
                }

                var comment = _monthlyInvoiceSummaryRepository.GetCommentByMonthlyInvoiceSummaryId(id);

                return comment != null ? Ok(comment) : NotFound($"Comment for month {model.Month} and year {model.Year} not found.");
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing GetCommentPerMonthAndYear: " + ex.Message);
            }
        }

        [HttpPost("getMonthTotalsForYear")]
        public IActionResult GetMonthTotalsForYear([FromBody] string year)
        {
            try 
            {
                // Create an array to hold the 12 monthly totals
                double[] monthTotals = new double[12];

                // Iterate through each month and calculate the total
                for (int i = 0; i < 12; i++)
                {
                    int monthlyInvoiceSummaryId = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(i, year);
                    IEnumerable<Invoice> invoices = _invoiceRepository.GetInvoicesPerMonthlyInvoiceSummaryId(monthlyInvoiceSummaryId);
                    monthTotals[i] = invoices.Sum(invoice => invoice.Total);
                }
                return Ok(monthTotals);
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing GetMonthTotalsForYear: " + ex.Message);
            }
        }

        [HttpPost("addInvoiceToMonthAndYear")]
        public IActionResult AddInvoiceToMonthAndYear([FromBody] AddInvoiceToMonthAndYearModel model)
        {
            try
            {
                int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);
                if (id == 0)
                {
                    return NotFound($"No MonthlyInvoiceSummary found for month {model.Month} and year {model.Year}");
                }

                Invoice invoice = new Invoice()
                {
                    Total = model.InvoiceTotal,
                    CreateTimestamp = DateTime.Now,
                    UpdateTimestamp = DateTime.Now,
                    MonthlyInvoiceSummaryId = id,
                    Store = null // store hinzufügen
                };

                bool invoiceAdded = _invoiceRepository.AddInvoiceToMonthAndYear(invoice);

                return invoiceAdded ? Ok() : NotFound($"Invoice with total {model.InvoiceTotal} not added.");
            }
            catch (Exception ex) 
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing AddInvoiceToMonthAndYear: " + ex.Message);
            }
        }

        [HttpPut("updateInvoiceById")]
        public IActionResult UpdateInvoiceById([FromBody] UpdateInvoiceByIdModel model)
        {
            try
            {
                Invoice? oldInvoice = _invoiceRepository.GetInvoiceById(model.Id);
                if (oldInvoice == null)
                {
                    return NotFound($"No invoice found for id {model.Id}");
                }
                
                Invoice newModel = new Invoice()
                {
                    InvoiceId = model.Id,
                    Total = model.invoiceTotal,
                    CreateTimestamp = oldInvoice.CreateTimestamp,
                    UpdateTimestamp = DateTime.Now,
                    MonthlyInvoiceSummaryId = oldInvoice.MonthlyInvoiceSummaryId,
                    Store = oldInvoice.Store
                };

                bool invoiceUpdated = _invoiceRepository.UpdateInvoiceById(newModel);

                return invoiceUpdated ? Ok() : NotFound($"Invoice with id {model.Id} not updated.");
                
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing UpdateInvoiceById: " + ex.Message);

            }
        }

        [HttpPut("updateCommentByMonthAndYear")]
        public IActionResult UpdateCommentByMonthAndYear([FromBody] UpdateCommentByMonthAndYearModel model)
        {
            try
            {
                int id = _monthlyInvoiceSummaryRepository.GetMonthlyInvoiceSummaryId(model.Month, model.Year);
                if (id == 0)
                {
                    return NotFound($"No MonthlyInvoiceSummary found for month {model.Month} and year {model.Year}");
                }

                bool commentUpdated = _monthlyInvoiceSummaryRepository.UpdateComment(id, model.Comment);


                if(commentUpdated) 
                {
                    string updatedComment = _monthlyInvoiceSummaryRepository.GetCommentByMonthlyInvoiceSummaryId(id)!;
                    return Ok(updatedComment);
                }
                else
                {
                    return NotFound($"Comment for month {model.Month} and year {model.Year} not updated.");
                }
            }
            catch (Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing UpdateCommentByMonthAndYear: " + ex.Message);
            }
        }

        [HttpPost("deleteInvoiceById")]
        public IActionResult DeleteInvoiceById([FromBody]int id)
        {
            try
            {
                bool invoiceDeleted = _invoiceRepository.DeleteInvoiceById(id);

                return invoiceDeleted ? Ok() : NotFound($"Invoice with id { id } not deleted.");
            }
            catch(Exception ex)
            {
                // Todo: add loggin for exeptions

                return StatusCode(500, "Error occurred while executing DeleteInvoiceById: " + ex.Message);
            }
        }

    }
}
