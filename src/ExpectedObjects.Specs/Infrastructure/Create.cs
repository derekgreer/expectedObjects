using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs.Infrastructure
{
    public class Create
    {
        public static It Observation(Func<It> func)
        {
#if REVIEW
            return func();
#else
            return null;
#endif
        }
    }
}