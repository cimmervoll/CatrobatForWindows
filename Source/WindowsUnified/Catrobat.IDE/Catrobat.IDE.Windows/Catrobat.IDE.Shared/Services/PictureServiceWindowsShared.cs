﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class PictureServiceWindowsShared : IPictureService
    {
        private static readonly List<string> SupportedFileNames = new List<string>
        {
            ".jpg", ".jpeg", ".png"
        };

        public async Task<PictureServiceResult> ChoosePictureFromLibraryAsync()
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            foreach (var extension in SupportedFileNames)
                openPicker.FileTypeFilter.Add(extension);

            StorageFile file;

            try
            {
                file = await openPicker.PickSingleFileAsync(); 
                // TODO: use that instead: PickSingleFileAndContinue
                // Use this for more information: http://channel9.msdn.com/Events/Build/2014/2-520
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }


            if (file != null)
            {
                var fileStream = await file.OpenAsync(FileAccessMode.Read);
                var memoryStream = new MemoryStream();
                fileStream.AsStreamForRead().CopyTo(memoryStream);

                fileStream.Seek(0);
                var imagetobind = new BitmapImage();
                imagetobind.SetSource(fileStream);
                var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
                await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
                memoryStream.Seek(0, SeekOrigin.Begin);
                var portableImage = new PortableImage(writeableBitmap)
                {
                    Width = writeableBitmap.PixelWidth,
                    Height = writeableBitmap.PixelHeight,
                    EncodedData = memoryStream
                };

                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Success,
                    Image = portableImage
                };
            }
            else
            {
                return new PictureServiceResult
                {
                    Status = PictureServiceStatus.Cancelled
                };
            }
        }

        public async Task<PictureServiceResult> TakePictureAsync()
        {
            throw new Exception("Move this code to store project");
            //var cam = new CameraCaptureUI();
            //var file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            //if (file != null)
            //{
            //    var fileStream = await file.OpenAsync(FileAccessMode.Read);
            //    var memoryStream = new MemoryStream();
            //    fileStream.AsStreamForRead().CopyTo(memoryStream);

            //    fileStream.Seek(0);
            //    var imagetobind = new BitmapImage();
            //    imagetobind.SetSource(fileStream);
            //    var writeableBitmap = new WriteableBitmap(imagetobind.PixelWidth, imagetobind.PixelHeight);
            //    await writeableBitmap.FromStream(memoryStream.AsRandomAccessStream());
            //    memoryStream.Seek(0, SeekOrigin.Begin);
            //    var portableImage = new PortableImage(writeableBitmap)
            //    {
            //        Width = writeableBitmap.PixelWidth,
            //        Height = writeableBitmap.PixelHeight,
            //        EncodedData = memoryStream
            //    };

            //    return new PictureServiceResult
            //    {
            //        Status = PictureServiceStatus.Success,
            //        Image = portableImage
            //    };
            //}
            //else
            //{
            //    return new PictureServiceResult
            //    {
            //        Status = PictureServiceStatus.Cancelled
            //    };
            //}
        }

        public async Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            const string catrobatImageFileName = "program.catrobat_paint_png";
            var localFolder = ApplicationData.Current.TemporaryFolder;

            var file = await localFolder.CreateFileAsync(catrobatImageFileName, CreationCollisionOption.ReplaceExisting);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false, 
                PreferredApplicationPackageFamilyName = "f53a5122-9a9c-486b-86cf-3a3e60e0fd2f_bcp11qa1rfadr" };

            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            if (success)
            {
                // File launch OK
            }
            else
            {
                // File launch failed
            }

            return new PictureServiceResult(); // TODO: get file from app launch
        }

    }
}
