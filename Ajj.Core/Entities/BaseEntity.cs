using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
