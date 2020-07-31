﻿using System.Collections.Generic;
using System.Text;

namespace LittlePdf
{
    public class Container : PageObject
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Clip { get; set; } = true;
        public List<PageObject> Children { get; } = new List<PageObject>();

        internal Container(Container parent, double x, double y, double width, double height) : base(parent)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal override void Paint(Page page, StringBuilder output)
        {
            if (Clip)
            {
                output.Append($"{AbsoluteX} {page.Height - (AbsoluteY + Height)} {Width} {Height} re W n ");
            }

            foreach (var child in Children) child.Paint(page, output);
        }

        public void AddObject(PageObject @object)
        {
            Children.Add(@object);
        }

        // Convert to extension method
        public Line AddLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line(this) { X = x1, Y = y1, XEnd = x2, YEnd = y2 };
            Children.Add(line);
            return line;
        }

        public Container AddContainer(double x, double y, double width, double height)
        {
            var container = new Container(this, x, y, width, height);
            Children.Add(container);
            return container;
        }
    }
}
