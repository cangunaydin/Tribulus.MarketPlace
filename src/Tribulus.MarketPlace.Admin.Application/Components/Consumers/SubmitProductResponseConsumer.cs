﻿using MassTransit;
using MassTransit.Courier.Contracts;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Admin.Models;

namespace Tribulus.MarketPlace.Admin.Components.Consumers
{
    public class SubmitProductResponseConsumer :
         RoutingSlipResponseConsumer<SubmitProduct, SubmitProductCompleted, SubmitProductFaulted>
    {
        protected override Task<SubmitProductCompleted> CreateResponseMessage(ConsumeContext<RoutingSlipCompleted> context, SubmitProduct request)
        {
            var productId = context.GetVariable<Guid>("ProductId");

            HasVariable(context.Message.Variables, "Product", out Product product);
            return MessageInitializerCache<SubmitProductCompleted>.InitializeMessage(context, new
            {
                productId,
                product
            }).Select(x => x.Message);

        }

        protected override Task<SubmitProductFaulted> CreateFaultedResponseMessage(ConsumeContext<RoutingSlipFaulted> context, SubmitProduct request,
            Guid requestId)
        {
            var productId = context.GetVariable<Guid>("ProductId");

            IEnumerable<ExceptionInfo> exceptions = context.Message.ActivityExceptions.Select(x => x.ExceptionInfo);

            var reason = exceptions.FirstOrDefault()?.Message ?? "Unknown";
            return MessageInitializerCache<SubmitProductFaulted>.InitializeMessage(context, new
            {
                productId,
                Reason = reason,
                request
            }).Select(x => x.Message);
        }
    }
}