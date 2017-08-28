using System;
using System.Collections.Generic;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public class WriterContext : IDisposable
    {
        static readonly Stack<IWriter> Stack = new Stack<IWriter>();

        public WriterContext(IWriter writer)
        {
            Stack.Push(writer);
        }

        public static IWriter Current => Stack.Peek();

        public void Dispose()
        {
            Stack.Pop();
        }
    }
}