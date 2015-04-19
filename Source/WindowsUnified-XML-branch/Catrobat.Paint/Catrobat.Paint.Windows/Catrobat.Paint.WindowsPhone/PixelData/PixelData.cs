﻿using Catrobat.Paint.WindowsPhone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.PixelData
{
    class PixelData
    {

        private WriteableBitmap Bitmap;
        //private string filename;
        //private bool IsBitmapStored;
        //private BitmapDecoder Decoder;
        private SolidColorBrush ColorBrush;
        public String ReturnString;
        private int X;
        private int Y;
        public byte[] pixelsCanvas;
        public byte[] oldPixles;
        private byte[] pixelsCanvasEraser;
        //public int BitmapHeight;
        //public int BitmapWidth;
        public int pixelHeightCanvas;
        public int pixelWidthCanvas;

        #region Contructor
        public PixelData()
        {
            pixelsCanvas = null;
            pixelHeightCanvas = 0;
            pixelWidthCanvas = 0;
            ReturnString = string.Empty;
        }
        #endregion

        #region PipetteTool
        public async Task<SolidColorBrush> GetPixel(WriteableBitmap bitmap, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Bitmap = bitmap;
            return await GetPixelColor();
        }

        private async Task<SolidColorBrush> GetPixelColor()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualWidth,
                                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.ActualHeight);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            var pixels = WindowsRuntimeBufferExtensions.ToArray(buffer);

            var width = retarbi.PixelWidth;
            var height = retarbi.PixelHeight;

            double NormfactorX = (double)width / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)height / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)Y) * NormfactorY;
            double doubleX = ((double)X) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * width;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            var a = pixels[intValue + 3];
            var r = pixels[intValue + 2];
            var g = pixels[intValue + 1];
            var B = pixels[intValue];

            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, B));
        }
        #endregion

        #region FillTool
        public byte[] ConvertArray(byte[] pixles, int width, int height)
        {
            try
            {
                if (pixles != null)
                {
                    byte[] PixelsBitmap = new byte[PocketPaintApplication.GetInstance().Bitmap.PixelHeight *
                                                   PocketPaintApplication.GetInstance().Bitmap.PixelWidth *
                                                   4];

                    double NormfactorX = PocketPaintApplication.GetInstance().Bitmap.PixelWidth / (double)width;
                    double NormfactorY = PocketPaintApplication.GetInstance().Bitmap.PixelHeight / (double)height;

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {

                            double doubleY = i * NormfactorY;
                            double doubleX = j * NormfactorX;

                            int intX = (int)Math.Round(doubleX, 0);
                            int intY = (int)Math.Round(doubleY, 0);

                            int intTemp = intY * PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
                            int intXTemp = intTemp + intX;
                            int intValue = intXTemp * 4;

                            int intTempCanvas = i * width;
                            int intXTempCanvas = intTempCanvas + j;
                            int intValueCanvas = intXTempCanvas * 4;

                            PixelsBitmap[intValue] = pixles[intValueCanvas];
                            PixelsBitmap[intValue + 1] = pixles[intValueCanvas + 1];
                            PixelsBitmap[intValue + 2] = pixles[intValueCanvas + 2];
                            PixelsBitmap[intValue + 3] = pixles[intValueCanvas + 3];

                        }
                    }


                    return PixelsBitmap;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        async public Task<int> preparePaintingAreaCanvasPixel()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvas);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            pixelsCanvas = WindowsRuntimeBufferExtensions.ToArray(buffer);
            oldPixles = WindowsRuntimeBufferExtensions.ToArray(buffer);
            this.pixelHeightCanvas = retarbi.PixelHeight;
            this.pixelWidthCanvas = retarbi.PixelWidth;
            ColorBrush = new SolidColorBrush();
            return 0;
        }

        public bool FloodFill4(Point p, SolidColorBrush color)
        {
            try
            {
                string newColor = ColorToString(color);
                if (PocketPaintApplication.GetInstance().Bitmap == null)
                {
                    PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                }
                p = ConvertCoordinates(p);
                string oldColor = getPixelFromCanvas(p);
                if (oldColor == newColor)
                    return false;

                Stack<Point> list = new Stack<Point>();
                list.Push(p);
                while (list.Count > 0)
                {
                    var p2 = list.Pop();
                    SetPixel(p2, newColor);
                    if (p2.X > 1)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(p2.X - 1, p2.Y),oldColor))
                            list.Push(new Point(p2.X - 1, p2.Y));
                    }
                    if (p2.X < pixelWidthCanvas)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(p2.X + 1, p2.Y), oldColor))
                            list.Push(new Point(p2.X + 1, p2.Y));
                    }
                    if (p2.Y > 1)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(p2.X, p2.Y - 1),oldColor))
                            list.Push(new Point(p2.X, p2.Y - 1));
                    }
                    if (p2.Y < pixelHeightCanvas)
                    {
                        if (ComparePixelsColorForFloddFill(new Point(p2.X, p2.Y + 1), oldColor))
                            list.Push(new Point(p2.X, p2.Y + 1));
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FloodFill8(Point p, SolidColorBrush color)
        {
            try
            {
                string newColor = ColorToString(color);
                if (PocketPaintApplication.GetInstance().Bitmap == null)
                {
                    PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                }
                p = ConvertCoordinates(p);
                string oldColor = getPixelFromCanvas(p);
                if (oldColor == newColor)
                    return false;

                Stack<Point> list = new Stack<Point>();
                list.Push(p);
                while (list.Count > 0)
                {
                    var p2 = list.Pop();
                    SetPixel(p2, newColor);
                    //left
                    if (p2.X > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y));
                    }
                    //right
                    if (p2.X < pixelWidthCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1, p2.Y)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y));
                    }
                    //above
                    if (p2.Y > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X, p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X, p2.Y - 1));
                    }
                    //below
                    if (p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X, p2.Y + 1));
                    }
                    //left-above
                    if (p2.X > 1 && p2.Y > 1)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y - 1));
                    }
                    //right-below
                    if (p2.X > 1 && p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X - 1, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X - 1, p2.Y + 1));
                    }
                    //right-above
                    if (p2.Y > 1 && p2.X < pixelWidthCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1 , p2.Y - 1)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y - 1));
                    }
                    //right-below
                    if (p2.X < pixelWidthCanvas && p2.Y < pixelHeightCanvas)
                    {
                        if (getPixelFromCanvas(new Point(p2.X + 1, p2.Y + 1)) == oldColor)
                            list.Push(new Point(p2.X + 1, p2.Y + 1));
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetPixel(Point p, string color)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                intXTemp = intTemp + ((int)p.X - 1);
                 intValue = intXTemp * 4;

                var argb = color.Split('_');

                pixelsCanvas[intValue + 3] = Convert.ToByte(argb[0]);
                pixelsCanvas[intValue + 2] = Convert.ToByte(argb[1]);
                pixelsCanvas[intValue + 1] = Convert.ToByte(argb[2]);
                pixelsCanvas[intValue] = Convert.ToByte(argb[3]);
            }
            catch (Exception)
            {
                return;
            }
        }
        
        public async Task<bool> PixelBufferToBitmap()
        {
            try
            {
                var bitmapPixels = ConvertArray(pixelsCanvas, pixelWidthCanvas, pixelHeightCanvas);                
                var wbCroppedBitmap = new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap.PixelWidth, PocketPaintApplication.GetInstance().Bitmap.PixelHeight);
                await wbCroppedBitmap.PixelBuffer.AsStream().WriteAsync(bitmapPixels, 0, bitmapPixels.Length);
                
                Image image = new Image();
                image.Source = wbCroppedBitmap;
                image.Height = wbCroppedBitmap.PixelHeight;
                image.Width = wbCroppedBitmap.PixelWidth;
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        private Point ConvertCoordinates(Point oldPoint)
        {
            double NormfactorX = (double)pixelWidthCanvas / PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / PocketPaintApplication.GetInstance().Bitmap.PixelHeight;
            double doubleY = (oldPoint.Y) * NormfactorY;
            double doubleX = (oldPoint.X) * NormfactorX;
            return new Point(Math.Round(doubleX, 0), Math.Round(doubleY, 0));
        }

        public string getPixelFromCanvas(Point p)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                if (pixelsCanvas != null)
                {
                    intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                    intXTemp = intTemp + ((int)p.X - 1);
                    intValue = intXTemp * 4;

                    var a = pixelsCanvas[intValue + 3];
                    var r = pixelsCanvas[intValue + 2];
                    var g = pixelsCanvas[intValue + 1];
                    var b = pixelsCanvas[intValue];

                    return RGBToString(a, r, g, b);
                }
                else
                    return null;

            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool ComparePixelsColorForFloddFill(Point p, string oldColor)
        {
            int intTemp;
            int intXTemp;
            int intValue;
            try
            {
                if (pixelsCanvas != null)
                {
                    intTemp = ((int)p.Y - 1) * pixelWidthCanvas;
                    intXTemp = intTemp + ((int)p.X - 1);
                    intValue = intXTemp * 4;

                    oldColor.Split('_');
                    var aO = oldColor.Split('_')[0];
                    var rO = oldColor.Split('_')[1];
                    var gO = oldColor.Split('_')[2];
                    var bO = oldColor.Split('_')[3];
                    var a = pixelsCanvas[intValue + 3];
                    var r = pixelsCanvas[intValue + 2];
                    var g = pixelsCanvas[intValue + 1];
                    var b = pixelsCanvas[intValue];
                    if (Math.Abs(Convert.ToInt16(aO) - (int)a) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(rO) - (int)r) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(gO) - (int)g) > 70)
                        return false;
                    if (Math.Abs(Convert.ToInt16(bO) - (int)b) > 70)
                        return false;

                    return true;
                }
                else
                    return false;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string ColorToString(SolidColorBrush color)
        {
            return color.Color.A + "_" + color.Color.R + "_" + color.Color.G + "_" + color.Color.B;
        }

        public string RGBToString(int a, int r, int g, int b)
        {
            return a + "_" + r + "_" + g + "_" + b;
        }

        #endregion

        async public Task<int> preparePaintingAreaCanvasForEraser()
        {
            RenderTargetBitmap retarbi = new RenderTargetBitmap();
            await retarbi.RenderAsync(PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying,
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Width,
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Height);

            Windows.Storage.Streams.IBuffer buffer = await (retarbi.GetPixelsAsync());
            pixelsCanvasEraser = WindowsRuntimeBufferExtensions.ToArray(buffer);
            pixelHeightCanvas = retarbi.PixelHeight;
            pixelWidthCanvas = retarbi.PixelWidth;
            return 0;
        }

        public Byte getPixelAlphaFromCanvas(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            return pixelsCanvas[intValue + 3];
        }

        public Byte getPixelAlphaFromCanvasEraser(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            return pixelsCanvasEraser[intValue + 3];
        }

        public void setPixelColor(int x, int y)
        {
            double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
            double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

            double doubleY = ((double)y) * NormfactorY;
            double doubleX = ((double)x) * NormfactorX;

            int intX = (int)Math.Round(doubleX, 0);
            int intY = (int)Math.Round(doubleY, 0);

            int intTemp = intY * pixelWidthCanvas;
            int intXTemp = intTemp + intX;
            int intValue = intXTemp * 4;

            pixelsCanvas[intValue + 3] = 0;
        }

        async public void changedPixelToCanvas()
        {
            StorageFile outputFile = await KnownFolders.PicturesLibrary.CreateFileAsync("myTestFile.png", CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, writeStream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)pixelWidthCanvas, (uint)pixelHeightCanvas, 96, 96, pixelsCanvas);
                await encoder.FlushAsync();
            }
            string filename = "myTestFile" + ".png";

            await PocketPaintApplication.GetInstance().StorageIo.WriteBitmapToPngMediaLibrary(filename);
            StorageFile storageFile = await KnownFolders.PicturesLibrary.GetFileAsync(filename);
            InMemoryRandomAccessStream mrAccessStream = new InMemoryRandomAccessStream();

            Stream stream = await storageFile.OpenStreamForReadAsync(); ;
            using (var memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                memStream.Position = 0;

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(mrAccessStream, decoder);

                // write out to the stream
                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }
            }
            //render the stream to the screen
            WriteableBitmap wbCroppedBitmap = new WriteableBitmap(
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width,
                (int)PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height);
            wbCroppedBitmap.SetSource(mrAccessStream);

            Image image = new Image();
            image.Source = wbCroppedBitmap;
            image.Height = wbCroppedBitmap.PixelHeight;
            image.Width = wbCroppedBitmap.PixelWidth;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(image);
        }

        //public Byte getPixelAlphaFromCanvas(int x, int y)
        //{
        //    double NormfactorX = (double)pixelWidthCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
        //    double NormfactorY = (double)pixelHeightCanvas / (double)PocketPaintApplication.GetInstance().Bitmap.PixelHeight;

        //    double doubleY = ((double)y) * NormfactorY;
        //    double doubleX = ((double)x) * NormfactorX;

        //    int intX = (int)Math.Round(doubleX, 0);
        //    int intY = (int)Math.Round(doubleY, 0);

        //    int intTemp = intY * pixelWidthCanvas;
        //    int intXTemp = intTemp + intX;
        //    int intValue = intXTemp * 4;

        //    return pixelsCanvas[intValue + 3];
        //}

    }
}