using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public class ProductState : SagaStateMachineInstance
    {

        public int CurrentState { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid TrackingNumber { get; set; }

        public string Reason { get; set; }

        public Guid CorrelationId { get; set; }

        public ExceptionInfo ExceptionInfo { get; set; }
    }
}
