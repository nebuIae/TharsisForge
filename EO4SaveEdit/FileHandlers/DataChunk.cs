﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace EO4SaveEdit.FileHandlers
{
    // TODO: better name? more features...?
    public class DataChunk
    {
        public virtual void ReadFromStream(Stream stream)
        {
            throw new NotImplementedException(string.Format("{0} not overridden in {1}", MethodBase.GetCurrentMethod(), this.GetType().FullName));
        }

        public virtual void WriteToStream(Stream stream)
        {
            throw new NotImplementedException(string.Format("{0} not overridden in {1}", MethodBase.GetCurrentMethod(), this.GetType().FullName));
        }
    }
}
