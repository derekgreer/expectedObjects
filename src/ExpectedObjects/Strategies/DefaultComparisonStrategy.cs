﻿using System;

namespace ExpectedObjects.Strategies
{
    public class DefaultComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            return true;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}