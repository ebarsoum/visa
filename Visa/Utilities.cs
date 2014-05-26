//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Visa
{
    public static class FileHelper
    {
        /// <summary>
        /// Return all files under the root folder and its subfolder.
        /// </summary>
        /// <param name="folder">Root folder</param>
        /// <returns>List of images</returns>
        public static List<FileInfo> GetFiles(DirectoryInfo folder)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();

            filesInfo.AddRange(folder.GetFiles());
            foreach (DirectoryInfo subFolder in folder.GetDirectories())
            {
                filesInfo.AddRange(GetFiles(subFolder));
            }

            return filesInfo;
        }

        /// <summary>
        /// Return all image files under the root folder and its subfolder.
        /// </summary>
        /// <param name="folder">Root folder</param>
        /// <returns>List of images</returns>
        public static List<FileInfo> GetImages(DirectoryInfo folder)
        {
            List<FileInfo> result = new List<FileInfo>();
            List<FileInfo> files = GetFiles(folder);

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() == ".png"  || 
                    file.Extension.ToLower() == ".jpg"  || 
                    file.Extension.ToLower() == ".jpeg" || 
                    file.Extension.ToLower() == ".bmp")
                {
                    result.Add(file);
                }
            }

            return result;
        }

        /// <summary>
        /// Return all annotation files under the root folder and its subfolder.
        /// </summary>
        /// <param name="folder">Root folder</param>
        /// <returns>List of images</returns>
        public static List<FileInfo> GetAnnotationFiles(DirectoryInfo folder)
        {
            List<FileInfo> result = new List<FileInfo>();
            List<FileInfo> files = GetFiles(folder);

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() == ".ant")
                {
                    result.Add(file);
                }
            }

            return result;
        } 
    }

    public static class MathHelper
    {
        public static double L1(float[] descriptor1, float[] descriptor2)
        {
            double dist = 0;

            for (int i = 0; i < descriptor1.Length; i++)
            {
                dist += Math.Abs((double)descriptor1[i] - (double)descriptor2[i]); 
            }

            return dist;
        }

        public static double L2(float[] descriptor1, float[] descriptor2)
        {
            double dist = 0;

            for (int i = 0; i < descriptor1.Length; i++)
            {
                dist += (descriptor1[i] - descriptor2[i]) * (descriptor1[i] - descriptor2[i]);
            }

            return Math.Sqrt(dist);
        } 
    }

    public static class ImageHelper
    {
        /// <summary>
        /// Get Thumbnail without loading the image file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap GetThumbnailImage(string filename, int size)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            using (Image img = Image.FromStream(fs, true, false))
            {
                int width = size;
                int height = width * img.Height / img.Width;
                if (img.Height > img.Width)
                {
                    height = size;
                    width = height * img.Width / img.Height;
                }

                Bitmap thumbnail = (Bitmap)img.GetThumbnailImage(width,
                                                                 height,
                                                                 null,
                                                                 IntPtr.Zero);

                Bitmap dest = new Bitmap(size, size, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(dest);
                int x = (dest.Width - thumbnail.Width) / 2;
                int y = (dest.Height - thumbnail.Height) / 2;

                g.Clear(Color.FromArgb(255,32,32,32));
                g.DrawImage(thumbnail, x, y);
                g.Dispose();

                return dest;
            }
        }
    }
}
