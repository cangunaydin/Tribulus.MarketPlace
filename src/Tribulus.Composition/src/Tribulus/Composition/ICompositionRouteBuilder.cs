using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.Conventions;

namespace Tribulus.Composition;


public interface ICompositionRouteBuilder
{
    string Build(
   string rootPath,
   string className,
   MethodInfo methodInfo,
   string httpMethod,
   [CanBeNull] ConventionalControllerSetting configuration
);
}
