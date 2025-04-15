using Imazen.WebP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;


namespace Imazen.Test.Webp
{
    [TestClass]
    public class TestSimpleDecoder
    {
        [TestMethod]
        public void TestWebPVersions()
        {
            Imazen.WebP.Extern.LoadLibrary.LoadWebPOrFail();
            Assert.AreEqual("0.5.2",SimpleDecoder.GetDecoderVersion());
        }
        [TestMethod]
        public void TestDecSimple()
        {
            Imazen.WebP.Extern.LoadLibrary.LoadWebPOrFail();

            var decoder = new SimpleDecoder();
            var fileName = "testimage.webp";
            var outFile = "testimageout.jpg";
            File.Delete(outFile);
            FileStream outStream = new FileStream(outFile, FileMode.Create);
            using (Stream inputStream = File.Open(fileName, System.IO.FileMode.Open))
            {

                var bytes = ReadFully(inputStream);
                var outBitmap = decoder.DecodeFromBytes(bytes, bytes.LongLength);
                outBitmap.Save(outStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                outStream.Close();
            }

            FileInfo finfo = new FileInfo(outFile);
            Assert.IsTrue(finfo.Exists);


        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
