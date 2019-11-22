﻿using System.Text;
using Markdown.SeparatorConverters;
using Markdown.SyntaxAnalysis.SyntaxTreeRealization;

namespace Markdown.SyntaxAnalysis.SyntaxTreeConverters
{
    public class SyntaxTreeConverter : ISyntaxTreeConverter
    {
        public string Convert(SyntaxTree syntaxTree, ISeparatorConverter separatorConverter)
        {
            return RenderTreeNode(syntaxTree.Root, separatorConverter, new StringBuilder(), "{0}").ToString();
        }

        private StringBuilder RenderTreeNode(SyntaxTreeNode treeNode, ISeparatorConverter separatorConverter,
            StringBuilder builder, string nodeValueFormat)
        {
            var children = treeNode.Children;
            if (treeNode.Token.IsSeparator)
            {
                var formats = separatorConverter.GetTokensFormats(treeNode.Token.Value, children.Count - 1);
                for (var i = 0; i < children.Count - 1; i++)
                {
                    RenderTreeNode(children[i], separatorConverter, builder, formats[i]);
                }
            }
            else
            {
                builder.AppendFormat(nodeValueFormat, treeNode.Token.Value);
                foreach (var treeNodeChild in children)
                {
                    RenderTreeNode(treeNodeChild, separatorConverter, builder, "{0}");
                }
            }

            return builder;
        }
    }
}