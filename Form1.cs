using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;


namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(.png; *.jpg; *.jpeg; *.bmp; *.tif;)|.png; *.jpg; *.jpeg; *.bmp; *.tif;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //A kép megnyitása és grayscale konvertálása
                Mat img =  CvInvoke.Imread(open.FileName);
                var gray = new  Mat();
                CvInvoke.CvtColor(img, gray,  Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                pictureBox1.Image = BitmapExtension.ToBitmap(img);

                //A kép thresholdolása 
                var thresh = new  Mat();
                 CvInvoke.Threshold(gray, thresh, 47, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                //47 az alap itt

                //Kell a morph műveleteknek, a vászon középpontját adja meg a (-1 -1) érték
                Point center = new Point(-1, -1);

                //Ez is kell a morph műveleteknek, ez adja a "vásznat" amin dolgoznak
                var kernel = new  Mat();
                kernel =  CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(5, 5), center);



                //Ez a close művelet a MorphologyExből kiválasztva, kétszer lefuttatva. Ez lesz a picturebox2-ben
                var closed = new  Mat();
                 CvInvoke.MorphologyEx(thresh, closed,  Emgu.CV.CvEnum.MorphOp.Close, kernel, center, 2,  Emgu.CV.CvEnum.BorderType.Default, new  Emgu.CV.Structure.MCvScalar(1, 0));
                pictureBox2.Image =  BitmapExtension.ToBitmap(closed);
                //2 az alap itt

                //Ez az erode művelet háromszor lefuttatva. Ez lesz a picturebox3-ban 
                var eroded = new  Mat();
                 CvInvoke.Erode(closed, eroded, kernel, center, 3,  Emgu.CV.CvEnum.BorderType.Default, new Emgu.CV.Structure.MCvScalar(1, 0));
                pictureBox3.Image =  BitmapExtension.ToBitmap(eroded);

                //Xor művelettel összehasonlítja a második és harmadik képet. 
                var merged = new  Mat();
                 CvInvoke.BitwiseXor(eroded, closed, merged);

                //Ezzel invertáljuk a képet
                var inverted = new  Mat();
                 CvInvoke.BitwiseNot(merged, inverted);

                //Gombokat szedjük ki thresholdolással
                var gomb = new  Mat();
                 CvInvoke.Threshold(gray, gomb, 80, 255,  Emgu.CV.CvEnum.ThresholdType.Binary);
                //pictureBox6.Image =  BitmapExtension.ToBitmap(gomb);

                var openedgomb = new  Mat();
                 CvInvoke.MorphologyEx(gomb, openedgomb,  Emgu.CV.CvEnum.MorphOp.Open, kernel, center, 1,  Emgu.CV.CvEnum.BorderType.Default, new  Emgu.CV.Structure.MCvScalar(1, 0));
                //pictureBox6.Image =  BitmapExtension.ToBitmap(openedgomb);

                var dilatedg = new  Mat();
                 CvInvoke.Dilate(openedgomb, dilatedg, kernel, center, 1,  Emgu.CV.CvEnum.BorderType.Default, new  Emgu.CV.Structure.MCvScalar(1, 0));
                pictureBox6.Image =  BitmapExtension.ToBitmap(dilatedg);

                //var closedg = new  Mat();
                // CvInvoke.MorphologyEx(openedgomb, closedg,  CvEnum.MorphOp.Close, kernel, center, 2,  CvEnum.BorderType.Default, new  Structure.MCvScalar(1, 0));

                //Dilate művelet egyszer lefuttatva, hogy a kábelek (és a törések) jobban kivehetőek legyenek. Ez lesz a végső kép a picturebox4-ben
                var dilated = new  Mat();
                 CvInvoke.Dilate(inverted, dilated, kernel, center, 1,  Emgu.CV.CvEnum.BorderType.Default, new  Emgu.CV.Structure.MCvScalar(1, 0));
                pictureBox4.Image =  BitmapExtension.ToBitmap(dilated);

                var closedd = new  Mat();
                 CvInvoke.MorphologyEx(dilated, closedd,  Emgu.CV.CvEnum.MorphOp.Close, kernel, center, 1,  Emgu.CV.CvEnum.BorderType.Default, new Emgu.CV.Structure.MCvScalar(1, 0));
                //pictureBox7.Image =  BitmapExtension.ToBitmap(closedd);


                var invertedg = new  Mat();
                 CvInvoke.BitwiseNot(dilatedg, invertedg);
                pictureBox5.Image =  BitmapExtension.ToBitmap(invertedg);


                var final = new  Mat();
                 CvInvoke.BitwiseAnd(dilated, invertedg, final);
                //pictureBox6.Image =  BitmapExtension.ToBitmap(final);

                var added = new Mat();
                Emgu.CV.CvInvoke.Add(closed, invertedg, added);
                //Emgu.CV.CvInvoke.AddWeighted(closed, 1, invertedg, 1, 0, added);
                //pictureBox7.Image = BitmapExtension.ToBitmap(added);

                var finalo = new Mat();
                //CvInvoke.Dilate(final, finald, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new Emgu.CV.Structure.MCvScalar(1, 0));
                CvInvoke.MorphologyEx(final, finalo, Emgu.CV.CvEnum.MorphOp.Open, kernel, center, 2, Emgu.CV.CvEnum.BorderType.Default, new Emgu.CV.Structure.MCvScalar(1, 0));
                pictureBox6.Image = BitmapExtension.ToBitmap(finalo);

                var finald = new Mat();
                //CvInvoke.Dilate(finalo, finald, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new Emgu.CV.Structure.MCvScalar(1, 0));
                CvInvoke.BitwiseAnd(eroded, finalo, finald);
                //pictureBox6.Image = BitmapExtension.ToBitmap(finald);






                final.Save("final.jpg");
                closedd.Save("closedd.jpg");
                dilated.Save("dilated.jpg");
                invertedg.Save("invertedg.jpg");
                eroded.Save("eroded.jpg");
                closed.Save("closed.jpg");
                finalo.Save("finalo.jpg");
                finald.Save("andeltimage.jpg");


                //var cndcmp = new  Mat();
                // CvInvoke.ConnectedComponents(inverted, cndcmp,  CvEnum.LineType.FourConnected);

            }

         }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
