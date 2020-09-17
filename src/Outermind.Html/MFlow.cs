using System;
using System.Collections.Generic;
using System.Linq;
using Totem.Runtime;

namespace Totem.Html
{
	/// <summary>
	/// A path through the various parts of M documents
	/// </summary>
	public abstract class MFlow : Notion
	{
		public virtual MPart From(object value)
		{
			return When(value);
		}

		public virtual MPart When(object node)
		{
			var part = node as MPart;

			return part != null ? part.Visit(this) : M.Content(node);
		}

		public TPart WhenCast<TPart>(TPart node) where TPart : MPart
		{
			var visitedNode = When(node);

			try
			{
				return (TPart) visitedNode;
			}
			catch(InvalidCastException exception)
			{
				throw new Exception($"PartIsNotSameType: Type = ${visitedNode.GetType()}, Expected = ${typeof(TPart)}");
			}
		}

		public virtual MPart WhenDocument(MDocument node)
		{
			return node.Rewrite(node.Classes, WhenCast(node.Head), WhenCast(node.Body));
		}

		public virtual MPart WhenDocumentHead(MDocumentHead node)
		{
			var newLinks = default(Many<MDocumentLink>);

			for(var i = 0; i < node.Links.Count; i++)
			{
				var visitedLink = WhenCast(node.Links[i]);

				if(newLinks != null)
				{
					newLinks.Write.Add(visitedLink);
				}
				else
				{
					if(visitedLink != node.Links[i])
					{
						newLinks = new Many<MDocumentLink>();

						for(var priorIndex = 0; priorIndex < i; priorIndex++)
						{
							newLinks.Write.Add(node.Links[priorIndex]);
						}

						newLinks.Write.Add(visitedLink);
					}
				}
			}

			return node.Rewrite(node.Title, node.BaseHref, newLinks ?? node.Links);
		}

		public virtual MPart WhenDocumentLink(MDocumentLink node)
		{
			return node;
		}

		public virtual MPart WhenDocumentBody(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenContent(MContent node)
		{
			if(node.IsPart)
			{
				return node.Rewrite(When(node.Value));
			}

			if(!node.IsMany)
			{
				return node;
			}

			var list = (IReadOnlyList<object>) node.Value;

			var newList = default(List<object>);

			for(var i = 0; i < list.Count; i++)
			{
				var visitedItem = When(list[i]);

				if(newList != null)
				{
					newList.Add(visitedItem);
				}
				else
				{
					if(visitedItem != list[i])
					{
						newList = new List<object>(list.Count);

						for(var priorIndex = 0; priorIndex < i; priorIndex++)
						{
							newList.Add(list[priorIndex]);
						}

						newList.Add(visitedItem);
					}
				}
			}

			return newList == null ? node : M.Content(newList);
		}

		public virtual MPart WhenValue(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenGroup(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenMain(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeader(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenFooter(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenNavigation(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenArticle(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenSection(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenAside(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading1(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading2(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading3(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading4(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading5(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenHeading6(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenParagraph(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenLink(MLink node)
		{
			return node.Rewrite(node.Href, node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data, node.HrefLanguage, node.Relationship, node.ContentType, node.Download);
		}

		public virtual MPart WhenCode(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenLiteral(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenTime(MTime node)
		{
			return node.Rewrite(node.When, node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenQuotation(MQuotation node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data, node.Cite);
		}

		public virtual MPart WhenCitation(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenList(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenListItem(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenDescriptionList(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenDescriptionTerm(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenDescription(MElement node)
		{
			return node.Rewrite(node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data);
		}

		public virtual MPart WhenForm(MForm node)
		{
			return node.Rewrite(node.Name, node.Action, node.Classes, When(node.Content), node.Id, node.Title, node.Direction, node.Language, node.Hidden, node.Data, node.ContentType);
		}
	}
}