using Imazen.WebP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;

namespace Imazen.Test.WebP
{
    [TestClass]
    public class TestSimpleEncoder
    {
        [TestMethod]
        public void TestVersion(){
            Imazen.WebP.Extern.LoadLibrary.LoadWebPOrFail();
            Assert.AreEqual("0.5.2",SimpleEncoder.GetEncoderVersion());
        }
        [TestMethod]
        public void TestEncSimple()
        {
            Imazen.WebP.Extern.LoadLibrary.LoadWebPOrFail();

            var encoder = new SimpleEncoder();
            var fileName = "testimage.jpg";
            var outFileName = "testimageout.webp";
            File.Delete(outFileName);

            Bitmap mBitmap;
            FileStream outStream = new FileStream(outFileName, FileMode.Create);
            using (Stream BitmapStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image img = Image.FromStream(BitmapStream);

                mBitmap = new Bitmap(img);

                encoder.Encode(mBitmap, outStream, 100);
            }

            FileInfo finfo = new FileInfo(outFileName);
            Assert.IsTrue(finfo.Exists);
        }
    }
}
