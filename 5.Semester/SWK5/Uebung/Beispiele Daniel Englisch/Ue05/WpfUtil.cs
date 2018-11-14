using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Swk5.Wpf
{
    public static class WpfUtil
    {
        public static void DumpLogicalTree(object parent, TextWriter writer)
        {
            DumpLogicalTree(parent, writer, 0);
        }

        public static void DumpLogicalTree(object parent, TextWriter writer, int level)
        {
            string typeName = parent.GetType().Name;
            string name = null;
            DependencyObject doParent = parent as DependencyObject;
            // Not everything in the logical tree is a dependency object
            if (doParent != null)
                name = (string)(doParent.GetValue(FrameworkElement.NameProperty) ?? "");
            else
                name = parent.ToString();

            writer.WriteLine("{0}{1}: {2}",
                             new string(' ', level * 2), typeName, name);
            if (doParent == null) return;

            foreach (object child in LogicalTreeHelper.GetChildren(doParent))
                DumpLogicalTree(child, writer, level + 1);
        }

        public static void DumpVisualTree(DependencyObject parent, TextWriter writer)
        {
            DumpVisualTree(parent, writer, 0);
        }

        private static void DumpVisualTree(DependencyObject parent, TextWriter writer, int level)
        {
            string typeName = parent.GetType().Name;
            string name = (string)(parent.GetValue(FrameworkElement.NameProperty) ?? "");
            writer.WriteLine("{0}{1}: {2}",
                             new string(' ', level * 2), typeName, name);

            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
                DumpVisualTree(VisualTreeHelper.GetChild(parent, i), writer, level + 1);
        }
    }
}
