using CSharpMath.Atom;
using CSharpMath.SkiaSharp;
using SkiaSharp;

namespace BlakePlugin.LaTeXRenderer;

public static class LatexSvgRenderer
{
    public static string ToInlineSvg(string latex, bool isDisplay, float fontSize = 20f)
    {
        var painter = new MathPainter
        {
            LaTeX = latex,
            FontSize = fontSize,
            LineStyle = isDisplay ? LineStyle.Display : LineStyle.Text
        };

        var size = painter.Measure();
        using var ms = new MemoryStream();
        using (var svg = SKSvgCanvas.Create(SKRect.Create(size.Width, size.Height), ms))
        {
            painter.Draw(svg, new SKPoint(0, 0));
            svg.Flush();
        }
        ms.Position = 0;
        var raw = new StreamReader(ms).ReadToEnd();
        return raw.Replace("<svg ", "<svg role=\"img\" focusable=\"false\" class=\"blake-math-svg\" ");
    }
}


