using System;

namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface FutureCompleted
    {
        /// <summary>
        /// When the future was initially created
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// When the future was finally completed
        /// </summary>
        DateTime Completed { get; }
    }
}
