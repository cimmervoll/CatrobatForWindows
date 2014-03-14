﻿using System.Windows;
using Catrobat.Paint.Phone.View;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.Tool
{
    public abstract class ToolBase
    {
        public readonly static int Stroke1 = 1;
        public readonly static int Stroke5 = 5;
        public readonly static int Stroke15 = 15;
        public readonly static int Stroke25 = 25;
        protected ToolType ToolType;
        protected bool NeedToResetCanvas;


        public abstract void HandleDown(object arg);

        public abstract void HandleMove(object arg);

        public abstract void HandleUp(object arg);

        public abstract void Draw(object o);

        public ToolType GetToolType()
        {
            return ToolType;
        }

        protected void ResetCanvas()
        {
            // needs to be on PaintingAreaView to reset properly
            var currentPage = ((PhoneApplicationFrame)Application.Current.RootVisual).Content;
            if (!(currentPage is PaintingAreaView))
            {
                NeedToResetCanvas = true;
                return;
            }
            NeedToResetCanvas = false;

            PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
            PocketPaintApplication.GetInstance().SetBitmapAsPaintingAreaCanvasBackground();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
        }

    }

}