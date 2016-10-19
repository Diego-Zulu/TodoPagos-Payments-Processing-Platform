using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using TodoPagos.Domain;
using TodoPagos.Domain.DataAccess;
using TodoPagos.Domain.Repository;
using TodoPagos.Web.Api.Models;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/payments")]

    public class PaymentsController : ApiController
    {

        private readonly IPaymentService paymentService;
        private readonly string signedInUsername;

        public PaymentsController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            paymentService = new PaymentService(unitOfWork);
            signedInUsername = HttpContext.Current.User.Identity.Name;
        }

        public PaymentsController(IPaymentService service)
        {
            CheckForNullPaymentService(service);
            paymentService = service;
            signedInUsername = "TESTING";
        }

        public PaymentsController(string oneUsername)
        {
            FailIfUsernameArgumentIsNull(oneUsername);
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            paymentService = new PaymentService(unitOfWork);
            signedInUsername = oneUsername;
        }

        private void FailIfUsernameArgumentIsNull(string oneUsername)
        {
            if (oneUsername == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckForNullPaymentService(IPaymentService service)
        {
            if (service == null) throw new ArgumentException();
        }

        [HttpGet]
        public IHttpActionResult GetPayments()
        {
            IEnumerable<Payment> payments = paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet]
        [ResponseType(typeof(Payment))]
        public IHttpActionResult GetPayment(int id)
        {
            try
            {
                Payment payment = paymentService.GetSinglePayment(id);
                return Ok(payment);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(Payment))]
        public IHttpActionResult PostPayment([ModelBinder(typeof(PaymentModelBinder))] Payment newPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return TryToCreatePayment(newPayment);
        }

        private IHttpActionResult TryToCreatePayment(Payment newPayment)
        {
            try
            {
                int id = paymentService.CreatePayment(newPayment);
                return CreatedAtRoute("TodoPagosApi", new { id = newPayment.ID }, newPayment);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}