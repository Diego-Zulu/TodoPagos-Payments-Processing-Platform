﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TodoPagos.Domain;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/payments")]
    public class PaymentsController : ApiController
    {

        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService service)
        {
            if (service == null) throw new ArgumentException();
            paymentService = service;
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
            Payment payment = paymentService.GetSinglePayment(id);
            return Ok(payment);
        }
    }
}