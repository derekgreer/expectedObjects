using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpectedObjects
{
    public class ShouldWriter : IWriter
    {
        readonly List<EqualityResult> _results = new List<EqualityResult>();

        public void Write(EqualityResult content)
        {
            _results.Add(content);
        }

        public string GetFormattedResults()
        {
            var sb = new StringBuilder();
            _results
                .Where(x => x.Status.Equals(false))
                .Where(IsLeaf)
                .ToList()
                .ForEach(x =>
                    {
                        if (x.Actual is IMissingMember)
                        {
                            sb.Append(string.Format("For {0}, expected {1} but member was missing.{2}",
                                                 (!string.IsNullOrEmpty(x.Member)
                                                      ? x.Member
                                                      : ((string)null).ToUsefulString()),
                                                 x.Expected.ToUsefulString(),
                                                 Environment.NewLine));
                        }
                        else if (x.Actual is IUnexpectedElement)
                        {
                            var actual = x.Actual as IUnexpectedElement;

                            sb.Append(string.Format("For {0}, expected nothing but found {1}.{2}",
                                                 (!string.IsNullOrEmpty(x.Member)
                                                      ? x.Member
                                                      : ((string)null).ToUsefulString()),
                                                 actual.Element.ToUsefulString(),
                                                 Environment.NewLine));
                        }
                        else if (x.Actual is IMissingElement)
                        {
                            sb.Append(string.Format("For {0}, expected {1} but element was missing.{2}",
                                                 (!string.IsNullOrEmpty(x.Member)
                                                      ? x.Member
                                                      : ((string)null).ToUsefulString()),
                                                 x.Expected.ToUsefulString(),
                                                 Environment.NewLine));
                        }
                        else
                        {
                            sb.Append(string.Format("For {0}, expected {1} but found {2}.{3}",
                                                    (!string.IsNullOrEmpty(x.Member)
                                                         ? x.Member
                                                         : ((string) null).ToUsefulString()),
                                                    x.Expected.ToUsefulString(),
                                                    x.Actual.ToUsefulString(),
                                                    Environment.NewLine));
                        }
                    });
            return sb.ToString();
        }

        public bool IsLeaf(EqualityResult result)
        {
            return !_results.Any(x => x.Member.StartsWith(result.Member + ".") || x.Member.StartsWith(result.Member + "["));
        }
    }
}