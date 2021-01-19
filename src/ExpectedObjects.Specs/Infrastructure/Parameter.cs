using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace ExpectedObjects.Specs.Infrastructure
{
    public static class Parameter
    {
        public static TValue IsAny<TValue>()
        {
            return It.IsAny<TValue>();
        }

        public static Func<IQueryable<T>, IEnumerable<T>> IsQueryRenderingEnumerable<T>()
        {
            return It.IsAny<Func<IQueryable<T>, IEnumerable<T>>>();
        }

        public static Func<IQueryable<T>, IQueryable<T>> IsQuerySingleParameter<T>()
        {
            return It.IsAny<Func<IQueryable<T>, IQueryable<T>>>();
        }

        public static Func<IQueryable<T>, IQueryable<T>> IsQueryRenderingQueryable<T>()
        {
            return It.IsAny<Func<IQueryable<T>, IQueryable<T>>>();
        }

        public static Func<IQueryable<T>, IQueryable<T>> IsFilter<T>()
        {
            return It.IsAny<Func<IQueryable<T>, IQueryable<T>>>();
        }
    }
}