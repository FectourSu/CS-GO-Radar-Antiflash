using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parser
{
    internal static class ElementExtension
    {
        public static IElement SkipLine(this IElement element, IHtmlDocument document, int skipAfterLines)
        {
            char[] numberLine = element.Id
                .SkipWhile(lineId => !lineId.IsDigit())
                .ToArray();

            var requiredNumber = int.Parse(new string(numberLine)) + skipAfterLines;

            return document
                .GetElementById($"LC{requiredNumber}");
        }
    }

    public class ParserHazedumper : IParserHazedumper
    {
        public event EventHandler<string> Working;

        private async Task<ParserResult> RunAsync(ParserObject parserObject)
        {
            var result = new List<ParserResult>();

            var loader = new HtmlLoader();
            var domParser = new HtmlParser();

            string dom = await loader.GetSourceByUri(parserObject.Uri);

            if (string.IsNullOrEmpty(dom))
                throw new NullReferenceException($"Сould not get a response from {parserObject.Uri}");

            IHtmlDocument document = await domParser.ParseDocumentAsync(dom);

            IElement domElement = document
                .QuerySelectorAll("td")
                .First(i => i.TextContent.Contains(parserObject.Pattern));

            string value = domElement
                .SkipLine(document, parserObject.SkipAfterLines)
                .QuerySelectorAll("span")
                .Last(s => s.ClassName == parserObject.ClassName).TextContent;

            return new ParserResult()
            {
                Hedar = parserObject.Pattern,
                Value = value
            };
        }

        public async Task<IEnumerable<ParserResult>> RunAsync(params ParserObject[] objects)
        {
            var resultList = new List<ParserResult>();

            foreach (var item in objects)
            {
                ParserResult result = await RunAsync(item);

                resultList.Add(result);

                Working?.Invoke(this, result.Hedar);
            }

            return resultList;
        }
    }
}
