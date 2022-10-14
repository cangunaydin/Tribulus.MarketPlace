using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tribulus.Composition;


public interface ICompositionErrorsHandler
{
    Task OnRequestError( HttpRequest request, Exception ex);
}