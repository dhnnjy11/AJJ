using Ajj.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ajj.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthorizationAttribute : TypeFilterAttribute
    {
        public BasicAuthorizationAttribute(string realm = null)
            : base(typeof(BasicAuthorizationFilter))
        {
            Arguments = new object[]
            {
                realm
            };
        }
    }
}