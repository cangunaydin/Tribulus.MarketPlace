using MassTransit;
using System;

namespace Tribulus.MarketPlace.Admin.Models
{
    public interface FutureFaulted
    {
        /// <summary>
        /// When the future was initially created
        /// </summary>
        DateTime? Created { get; }

        /// <summary>
        /// When the future faulted
        /// </summary>
        DateTime? Faulted { get; }

        /// <summary>
        /// The exception related to the fault
        /// </summary>
        ExceptionInfo[] Exceptions { get; }
    }
}
