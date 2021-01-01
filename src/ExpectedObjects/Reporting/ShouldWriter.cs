using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpectedObjects.Reporting
{
    public class ShouldWriter : IWriter
    {
        const int IndentLength = 2;
        readonly bool _expandObjects = true;
        readonly List<EqualityResult> _results = new List<EqualityResult>();

        public ShouldWriter()
        {
            _expandObjects = Verbosity >= WriterVerbosity.Normal;
        }

        public WriterVerbosity Verbosity { get; set; } = WriterVerbosity.Normal;

        public void Write(EqualityResult content)
        {
            _results.Add(content);
        }

        public string GetFormattedResults()
        {
            var sb = new StringBuilder();

            if (_results.Where(x => x.Status.Equals(false))
                .Any(x => IsLeaf(x) || x.ResultType == EqualityResultType.Custom))
            {
                sb.Append($"The expected object did not match the actual object.{Environment.NewLine}");
                sb.Append(Environment.NewLine);
                sb.Append($"The following issues were found:{Environment.NewLine}");
                sb.Append(Environment.NewLine);
            }

            var count = 1;

            _results
                .Where(x => x.Status.Equals(false))
                .Where(x => IsLeaf(x) || x.ResultType == EqualityResultType.Custom)
                .ToList()
                .ForEach((x, index, collection) =>
                {
                    var isLastMember = index == collection.Count() - 1;
                    var member = !string.IsNullOrEmpty(x.Member) ? x.Member : "Missing instance";
                    sb.Append($"{count++}) {member}:");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append(GetFormattedObjects(x, _expandObjects));

                    if (!isLastMember)
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                    }
                });
            return sb.ToString();
        }

        public string GetTrunkFormattedResults()
        {
            var result = _results.SingleOrDefault(IsRoot);
            return GetFormattedObjects(result, _expandObjects);
        }

        string GetFormattedObjects(EqualityResult result, bool expandObjects)
        {
            var sb = new StringBuilder();

            if (result.Actual is IMissingMember)
            {
                sb.Heading($"Expected");
                sb.Indent($"{result.Expected.ToUsefulString(expandObjects)}", 2);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Heading($"Actual");
                sb.Indent($"member was missing", 2);
                sb.Append(Environment.NewLine);
            }
            else if (result.Actual is IUnexpectedElement)
            {
                var actual = result.Actual as IUnexpectedElement;

                sb.Heading($"Expected");
                sb.Indent($"nothing", 2);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Heading($"Actual");
                sb.Indent($"{actual.Element.ToUsefulString(expandObjects)}", 2);
                sb.Append(Environment.NewLine);
            }
            else if (result.Actual is IMissingElement)
            {
                sb.Heading($"Expected");
                sb.Indent($"{result.Expected.ToUsefulString(expandObjects)}", 2);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Heading($"Actual");
                sb.Indent($"element was missing", 2);
                sb.Append(Environment.NewLine);
            }
            else if (result.ResultType == EqualityResultType.Custom)
            {
                sb.Indent($"{result.Message}");
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Heading($"Expected");
                sb.Indent($"{result.Expected.ToUsefulString(expandObjects)}", 2);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Heading($"Actual");
                sb.Indent($"{result.Actual.ToUsefulString(expandObjects)}", 2);
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public bool IsLeaf(EqualityResult result)
        {
            return !_results.Any(x => x.Member.StartsWith(result.Member + ".") ||
                                      x.Member.StartsWith(result.Member + "[") ||
                                      x.Member.Equals(result.Member) && x.ResultType == EqualityResultType.Custom);
        }

        public bool IsRoot(EqualityResult result)
        {
            return _results.Any(x => x.Member.Equals(result.Member));
        }
    }

    static class StringBuilderExtensions
    {
        const int IndentLength = 2;

        public static void Indent(this StringBuilder sb, string s, int level = 1)
        {
            var prefix = new string(' ', level * IndentLength);
            sb.Append($"{prefix}{s}".Replace(Environment.NewLine, $"{Environment.NewLine}{prefix}"));
        }

        public static void Heading(this StringBuilder sb, string s, int level = 1)
        {
            var prefix = new string(' ', level * IndentLength);
            sb.Append($"{prefix}{s}:".Replace(Environment.NewLine, $"{Environment.NewLine}{prefix}"));
            sb.Append(Environment.NewLine);
        }
    }
}