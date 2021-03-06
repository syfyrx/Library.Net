﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Library.Net.Common
{
    /// <summary>
    /// 二维码
    /// </summary>
    public static class QRCode
    {
        public static Bitmap Generate(string txt)
        {
            Bitmap res;
            string enCodeString = txt;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            res = qrCodeEncoder.Encode(enCodeString);
            return res;
        }
    }
}
