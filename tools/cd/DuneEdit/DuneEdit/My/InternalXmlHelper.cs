using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace DuneEdit.My
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal sealed class InternalXmlHelper
    {
        private static dynamic source;
        private static XName name;

        [CompilerGenerated]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerNonUserCode]
        private sealed class RemoveNamespaceAttributesClosure
        {
            private readonly string[] m_inScopePrefixes;

            private readonly XNamespace[] m_inScopeNs;

            private readonly List<XAttribute> m_attributes;

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal RemoveNamespaceAttributesClosure(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes)
            {
                m_inScopePrefixes = inScopePrefixes;
                m_inScopeNs = inScopeNs;
                m_attributes = attributes;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal XElement ProcessXElement(XElement elem)
            {
                return RemoveNamespaceAttributes(m_inScopePrefixes, m_inScopeNs, m_attributes, elem);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal object ProcessObject(object obj)
            {
                //Discarded unreachable code: IL_002e, IL_0035
                XElement xElement = obj as XElement;
                if (xElement != null)
                {
                    return RemoveNamespaceAttributes(m_inScopePrefixes, m_inScopeNs, m_attributes, xElement);
                }
                return obj;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private InternalXmlHelper()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XAttribute CreateAttribute(XName name, object value)
        {
            if (value == null)
            {
                return null;
            }
            return new XAttribute(name, RuntimeHelpers.GetObjectValue(value));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XAttribute CreateNamespaceAttribute(XName name, XNamespace ns)
        {
            XAttribute xAttribute = new XAttribute(name, ns.NamespaceName);
            xAttribute.AddAnnotation(ns);
            return xAttribute;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, object obj)
        {
            //Discarded unreachable code: IL_002a
            if (obj != null)
            {
                XElement xElement = obj as XElement;
                if (xElement != null)
                {
                    return RemoveNamespaceAttributes(inScopePrefixes, inScopeNs, attributes, xElement);
                }
                IEnumerable enumerable = obj as IEnumerable;
                if (enumerable != null)
                {
                    return RemoveNamespaceAttributes(inScopePrefixes, inScopeNs, attributes, enumerable);
                }
            }
            return obj;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, IEnumerable obj)
        {
            //Discarded unreachable code: IL_003b, IL_0060
            if (obj != null)
            {
                IEnumerable<XElement> enumerable = obj as IEnumerable<XElement>;
                if (enumerable != null)
                {
                    return enumerable.Select(new RemoveNamespaceAttributesClosure(inScopePrefixes, inScopeNs, attributes).ProcessXElement);
                }
                return obj.Cast<object>().Select(new RemoveNamespaceAttributesClosure(inScopePrefixes, inScopeNs, attributes).ProcessObject);
            }
            return obj;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static XElement RemoveNamespaceAttributes(string[] inScopePrefixes, XNamespace[] inScopeNs, List<XAttribute> attributes, XElement e)
        {
            checked
            {
                if (e != null)
                {
                    XAttribute xAttribute = e.FirstAttribute;
                    while (xAttribute != null)
                    {
                        XAttribute nextAttribute = xAttribute.NextAttribute;
                        if (xAttribute.IsNamespaceDeclaration)
                        {
                            XNamespace xNamespace = xAttribute.Annotation<XNamespace>();
                            string localName = xAttribute.Name.LocalName;
                            if ((object)xNamespace != null)
                            {
                                if ((inScopePrefixes != null && inScopeNs != null) ? true : false)
                                {
                                    int num = inScopePrefixes.Length - 1;
                                    int num2 = num;
                                    int num3 = 0;
                                    while (true)
                                    {
                                        int num4 = num3;
                                        int num5 = num2;
                                        if (num4 > num5)
                                        {
                                            break;
                                        }
                                        string value = inScopePrefixes[num3];
                                        XNamespace xNamespace2 = inScopeNs[num3];
                                        if (localName.Equals(value))
                                        {
                                            if (xNamespace == xNamespace2)
                                            {
                                                xAttribute.Remove();
                                            }
                                            xAttribute = null;
                                            break;
                                        }
                                        num3++;
                                    }
                                }
                                if (xAttribute != null)
                                {
                                    if (attributes != null)
                                    {
                                        int num6 = attributes.Count - 1;
                                        int num7 = num6;
                                        int num8 = 0;
                                        while (true)
                                        {
                                            int num9 = num8;
                                            int num5 = num7;
                                            if (num9 > num5)
                                            {
                                                break;
                                            }
                                            XAttribute xAttribute2 = attributes[num8];
                                            string localName2 = xAttribute2.Name.LocalName;
                                            XNamespace xNamespace3 = xAttribute2.Annotation<XNamespace>();
                                            if ((object)xNamespace3 != null && localName.Equals(localName2))
                                            {
                                                if (xNamespace == xNamespace3)
                                                {
                                                    xAttribute.Remove();
                                                }
                                                xAttribute = null;
                                                break;
                                            }
                                            num8++;
                                        }
                                    }
                                    if (xAttribute != null)
                                    {
                                        xAttribute.Remove();
                                        attributes.Add(xAttribute);
                                    }
                                }
                            }
                        }
                        xAttribute = nextAttribute;
                    }
                }
                return e;
            }
        }
    }
}