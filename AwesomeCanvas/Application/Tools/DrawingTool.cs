﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AwesomeCanvas
{
    //-------------------------------------------------------------------------
    // Generic class for all drawing tools to impliment
    //-------------------------------------------------------------------------
    public class DrawingTool : PictureTool
    {
        private int m_size;
        protected int m_halfSize;
        protected Rectangle m_toolArea;
        protected float m_halfSquared;
        protected Color m_colour;
        protected bool m_toolDown;
        protected Point m_postion;
        protected Point m_lastPosition;

        //-------------------------------------------------------------------------
        // Constructor
        //-------------------------------------------------------------------------
        public DrawingTool()
        {
            SetSize(1);
            m_colour = Color.Black;
        }

        //-------------------------------------------------------------------------
        // Mouse click event
        //-------------------------------------------------------------------------
        public override void MouseClick(CanvasWindow sender, MouseEventArgs current, MouseEventArgs previouse)
        {
            //Draw(sender.GetPicture().CurrentLayer, current.Location);
        }

        //---------------------------------------------------------------------
        // A mouse button has been pressed down
        //---------------------------------------------------------------------
        public override void MouseDown(CanvasWindow sender, MouseEventArgs current, MouseEventArgs previouse)
        {
            m_toolDown = true; 
        }
        public override void MouseUp(CanvasWindow sender, MouseEventArgs current, MouseEventArgs previouse)
        {
            m_toolDown = false;

            IssueDrawCommand(sender.GetPicture().CurrentLayer, previouse.Location, current.Location);

            // Temporary solution. Need to issue redraw command
            sender.canvasBox.Invalidate();
        }
        public override void MouseMove(CanvasWindow sender, MouseEventArgs current, MouseEventArgs previouse)
        {
            IssueDrawCommand(sender.GetPicture().CurrentLayer, previouse.Location, current.Location);

            // Temporary solution. Need to issue redraw command
            sender.canvasBox.Invalidate();
        }

        //---------------------------------------------------------------------
        // Check the status of the mouse and issue the draw command
        //---------------------------------------------------------------------
        private void IssueDrawCommand(Layer layer, Point start, Point end = new Point())
        {
            if (m_toolDown == true)
            {
                Draw(layer, start, end);
                //TODO: decide routine for when to push a re-draw
                //sender.canvasBox.Invalidate();
            }
        }

        //-------------------------------------------------------------------------
        // Generic draw function all drawing tools impliment
        //-------------------------------------------------------------------------
        public virtual void Draw(Layer layer, Point start, Point end = new Point())
        { 
            
        }

        //-------------------------------------------------------------------------
        // Given a line gradient and step length, find the step amout in the other axis
        //-------------------------------------------------------------------------
        public float GetStep(float gradient, float step)
        { 
            // y = mx + c
            float m = gradient;
            float x = step; 
            // Ignore c

            // Return the y step
            return m * x;
        }

        //-------------------------------------------------------------------------
        // Find the gradient of a line given by two points
        //-------------------------------------------------------------------------
        public float GetGradient(Point a, Point b)
        { 
            // Get the vector
            Point vector = new Point();
            vector.X = b.X - a.X;
            vector.Y = b.Y - a.Y;

            // Return the gradiant (amount of y per x)
            return vector.Y / vector.X;
        }

        //-------------------------------------------------------------------------
        // Get methods
        //-------------------------------------------------------------------------
        public int GetSize() { return m_size; }
        public int GetHalfSize() { return m_halfSize; }
        public Rectangle GetToolArea() { return m_toolArea; }

        //-------------------------------------------------------------------------
        // Set the tool size and all dependant values
        //-------------------------------------------------------------------------
        public void SetSize(int size)
        {
            m_size = size;
            m_halfSize = size / 2;
            m_toolArea.Height = size;
            m_toolArea.Width = size; 
            m_halfSquared = m_halfSize * m_halfSize;
        }
    }
}
