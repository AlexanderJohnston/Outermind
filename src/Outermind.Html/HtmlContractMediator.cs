using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Totem.Runtime;
using Totem.Runtime.Json;

namespace Outermind.Html
{
  public class HtmlContractMediator
  {

    public HtmlContractMediator()
    {
    }

    public List<HtmlProperty> CreateProperties(Type type)
    {
      const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

      var props = Enumerable.Empty<MemberInfo>()
        .Concat(type.GetFields(flags))
        .Concat(type.GetProperties(flags))
        .Where(IsDurableProperty)
        .Select(member => CreateProperty(member))
        .ToList();

      return props;
    }

    HtmlProperty CreateProperty(MemberInfo member)
    {
      var property = new HtmlProperty(member);
      if (member is FieldInfo)
      {
        property.Readable = true;
        property.Writable = true;
      }
      else if (member.ReflectedType != member.DeclaringType)
      {
        property = new HtmlProperty(member.DeclaringType.GetProperty(member.Name));
      }
      return property;
    }

    static bool IsDurableProperty(MemberInfo member) =>
      !member.IsDefined(typeof(TransientAttribute))
      && !member.IsDefined(typeof(CompilerGeneratedAttribute))
      && member.DeclaringType != typeof(Notion);
  }

  public class HtmlProperty
  {
    public bool Readable;
    public bool Writable;
    public readonly string Name;
    public readonly string Type;

    public HtmlProperty()
    {

    }
    public HtmlProperty(MemberInfo member)
    {
      Name = member.Name;
      Type = member.MemberType.ToString();
    }

    public HtmlProperty(PropertyInfo property)
    {
      Name = property.Name;
      Type = property.PropertyType.ToString();
    }
  }
}
