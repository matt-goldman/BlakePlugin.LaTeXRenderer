using Blake.BuildTools;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace BlakePlugin.LaTeXRenderer;

public partial class Plugin : IBlakePlugin
{
    static readonly Regex _blockMath = BlockMathRegex();
    static readonly Regex _inlineMath = InlineMathRegex();

    public Task BeforeBakeAsync(BlakeContext context, ILogger? logger = null)
    {
        logger?.LogInformation("LaTeXRenderer plugin: BeforeBakeAsync called.");

        for (int i = 0; i < context.MarkdownPages.Count; i++)
        {
            var page = context.MarkdownPages[i];
            var md = page.RawMarkdown;

            // $$...$$ (display)
            md = _blockMath.Replace(md, m => {
                var tex = m.Groups[1].Value.Trim();
                var svg = LatexSvgRenderer.ToInlineSvg(tex, isDisplay: true);
                return "\n" + svg + "\n";
            });

            // $...$ (inline)
            md = _inlineMath.Replace(md, m => {
                var tex = m.Groups[1].Value.Trim();
                var svg = LatexSvgRenderer.ToInlineSvg(tex, isDisplay: false);
                return $"<span class=\"blake-math-inline\">{svg}</span>";
            });

            if (!ReferenceEquals(md, page.RawMarkdown) && md != page.RawMarkdown)
            {
                logger?.LogDebug("Updated LaTeX in page: {page}", page.Slug);
                context.MarkdownPages[i] = page with { RawMarkdown = md };
            }
            else
            {
                logger?.LogDebug("No LaTeX changes in page: {page}", page.Slug);
            }
        }

        return Task.CompletedTask;
    }

    [GeneratedRegex(@"\$\$([\s\S]+?)\$\$", RegexOptions.Compiled)]
    private static partial Regex BlockMathRegex();

    [GeneratedRegex(@"(?<!\$)\$(.+?)\$(?!\$)", RegexOptions.Compiled)]
    private static partial Regex InlineMathRegex();
}
