﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using EO4SaveEdit.FileHandlers;

namespace EO4SaveEdit
{
    public static class ImageHelper
    {
        public static Dictionary<Class, ImageList> CharacterIcons { get; private set; }

        static ImageHelper()
        {
            CharacterIcons = new Dictionary<Class, ImageList>();
            foreach (Class charaClass in Enum.GetValues(typeof(Class)))
            {
                ImageList imageList = new ImageList();
                int fileClassId = (int)(charaClass + 1);

                if (charaClass == Class.None) continue;

                List<string> files = Directory.EnumerateFiles("Data\\CharaIcon", string.Format("{0:D2}_*.png", fileClassId))
                    .Concat(Directory.EnumerateFiles("Data\\CharaIcon", "11_*.png")).ToList();

                if (files.Count != 16) throw new Exception("Wrong number of character icons for class");

                for (int i = 0; i < files.Count; i++)
                {
                    Bitmap image = (Bitmap)Bitmap.FromFile(files[i]);
                    imageList.ImageSize = image.Size;
                    imageList.Images.Add(i.ToString(), image);
                }

                CharacterIcons.Add(charaClass, imageList);
            }
        }
    }
}