using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcCustomBinder.CustomBinders
{
    [Serializable]
    public class CustomBinderException : Exception
    {
        public CustomBinderException(string message) : base(message) { }
    }
}
