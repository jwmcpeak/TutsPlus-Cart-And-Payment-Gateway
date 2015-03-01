using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Braintree;

namespace AspNetStore.Services
{
    public class PaymentGateway : IGateway
    {
        private readonly BraintreeGateway _gateway = new BraintreeGateway
        {
          Environment = Braintree.Environment.SANDBOX,
          MerchantId  = "3wv4fqk636rn588y",
          PublicKey   = "y5yfh9kcnvt8qf4x",
          PrivateKey  = "c4660e3571ca74392d20cc91f4568748"
        };

        public PaymentResult ProcessPayment(ViewModels.CheckoutViewModel model)
        {
            var request = new TransactionRequest()
            {
                Amount = model.Total,
                CreditCard = new TransactionCreditCardRequest()
                {
                    Number = model.CardNumber,
                    CVV = model.Cvv,
                    ExpirationMonth = model.Month,
                    ExpirationYear = model.Year
                },
                Options = new TransactionOptionsRequest()
                {
                    SubmitForSettlement = true
                }
            };

            var result = _gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                return new PaymentResult(result.Target.Id, true, null);
            }

            return new PaymentResult(null, false, result.Message);
        }
    }
}