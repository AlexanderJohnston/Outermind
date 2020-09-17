using System;
using System.Collections.Generic;
using System.Linq;

namespace Outermind.Html
{
	/// <summary>
	/// The types of parts in an M document
	/// </summary>
	public enum MPartType
	{
		Document,
		DocumentHead,
		DocumentLink,
		DocumentBody,
		Content,
		Main,
		Header,
		Footer,
		Navigation,
		Article,
		Section,
		Aside,
		Heading1,
		Heading2,
		Heading3,
		Heading4,
		Heading5,
		Heading6,
		Paragraph,
		Link,
		Code,
		Literal,
		Time,
		Quotation,
		Citation,
		List,
		ListItem,
		DescriptionList,
		DescriptionTerm,
		Description,
		Form
	}
}