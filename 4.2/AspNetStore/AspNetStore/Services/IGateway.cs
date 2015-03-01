using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetStore.ViewModels;

namespace AspNetStore.Services
{
    public interface IGateway
    {
        PaymentResult ProcessPayment(CheckoutViewModel model);
    }
}