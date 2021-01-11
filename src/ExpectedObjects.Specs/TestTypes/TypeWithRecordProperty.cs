using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithRecordProperty
    {
        public Person Person { get; set; }
        public IEnumerable<string> Scopes { get; set; }
    }
}