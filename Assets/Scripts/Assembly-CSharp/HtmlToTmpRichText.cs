using System;
using System.Collections.Generic;
using System.Text;

public static class HtmlToTmpRichText
{
	[Serializable]
	public class Style
	{
		public string HeadingColor;

		public string LinkColor;

		public string StrongColor;

		public string CodeColor;

		public string QuoteColor;

		public string BulletColor;

		public string RuleColor;

		public int H1SizePercent;

		public int H2SizePercent;

		public int H3SizePercent;

		public int H4SizePercent;

		public float ListIndentEm;

		public bool MakeLinksClickable;

		public bool ParseMarkdownMarkers;
	}

	private struct ListState
	{
		public bool Ordered;

		public int Index;
	}

	private enum LineKind
	{
		Blank = 0,
		Heading = 1,
		Bullet = 2,
		Plain = 3
	}

	private class Tag
	{
		public string Name;

		public bool IsClosing;

		public bool IsSelfClosing;

		public string Attributes;

		public string GetAttribute(string key)
		{
			return null;
		}
	}

	private static readonly Style DefaultStyle;

	private static readonly Dictionary<string, string> NamedEntities;

	public static string Convert(string html, Style style = null)
	{
		return null;
	}

	public static string ToPlainText(string html)
	{
		return null;
	}

	private static void EmitTag(StringBuilder sb, Tag tag, Style style, Stack<ListState> lists, ref bool preformatted)
	{
	}

	private static int HeadingSize(string name, Style style)
	{
		return 0;
	}

	private static void AppendListMarker(StringBuilder sb, Style style, Stack<ListState> lists)
	{
	}

	private static string ApplyMarkdownMarkers(string text, Style style)
	{
		return null;
	}

	private static LineKind KindAt(LineKind[] kinds, int index)
	{
		return LineKind.Blank;
	}

	private static int BulletLevel(string visible)
	{
		return 0;
	}

	private static string ApplyInlineBold(string line, Style style)
	{
		return null;
	}

	private static string RemoveOuterBoldMarkers(string line)
	{
		return null;
	}

	private static string RemoveLeadingBulletMarker(string line)
	{
		return null;
	}

	private static int FirstVisibleIndex(string line)
	{
		return 0;
	}

	private static string StripTmpTags(string line)
	{
		return null;
	}

	private static void AppendText(StringBuilder sb, string text, bool preformatted)
	{
	}

	private static void AppendLiteral(StringBuilder sb, string text)
	{
	}

	private static string CollapseWhitespace(string text)
	{
		return null;
	}

	private static void NewLine(StringBuilder sb)
	{
	}

	private static void StartBlock(StringBuilder sb)
	{
	}

	private static void EndBlock(StringBuilder sb)
	{
	}

	private static int TrailingNewlines(StringBuilder sb)
	{
		return 0;
	}

	private static void TrimTrailingSpaces(StringBuilder sb)
	{
	}

	private static bool EndsWithNewLineOrEmpty(StringBuilder sb)
	{
		return false;
	}

	private static string Tidy(string text)
	{
		return null;
	}

	private static bool TryReadTag(string html, int start, out Tag tag, out int next)
	{
		tag = null;
		next = default;
		return false;
	}

	private static int SkipElementBody(string html, int from, string name)
	{
		return 0;
	}

	private static bool IsBlockTag(string name)
	{
		return false;
	}

	private static bool StartsWith(string s, int index, string value)
	{
		return false;
	}

	private static string SanitizeAttribute(string value)
	{
		return null;
	}

	private static string DecodeEntities(string text)
	{
		return null;
	}

	private static string ResolveEntity(string entity)
	{
		return null;
	}
}
