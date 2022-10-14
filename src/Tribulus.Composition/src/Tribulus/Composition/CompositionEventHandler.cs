using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tribulus.Composition;

public delegate Task CompositionEventHandler<in TEvent>(TEvent @event);