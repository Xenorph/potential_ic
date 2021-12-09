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
using Emgu.CV.Structure;


namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        Image<Bgr, byte> imgInput;
        Point center = new Point(-1, -1);
        Image<Bgr, byte> imgRevert;
        Image<Bgr, byte> imgTmp;
        Image<Bgr, byte> imgSave;
        Image<Bgr, byte> imgRevert2;
        Image<Gray, byte> imgCC;
          
        public Form1()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Image File (*.bmp, *.jpg)| *.bmp; *.jpg";
            if (DialogResult.OK == openfile.ShowDialog()) 
            {
                imgInput = new Image<Bgr, byte>(openfile.FileName);
                //imgGray = new Image<Bgr, byte>(openfile.FileName);
                pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);
            }
        }
        


        private void button2_Click(object sender, EventArgs e)
        {
            var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            pictureBox1.Image = BitmapExtension.ToBitmap(gray);

            pictureBox1.Image.Save("krumpli.jpg");
            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            var threshold = new Mat();
            CvInvoke.Threshold(imgInput, threshold, Convert.ToDouble($"{numericUpDown1.Value}"), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            pictureBox1.Image = BitmapExtension.ToBitmap(threshold);

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            var threshold = new Mat(); 
            CvInvoke.Threshold(gray, threshold, Convert.ToDouble($"{numericUpDown1.Value}"), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            */
            var kernel = new Mat();
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown2.Value)), center);

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            var opened = new Mat();
            CvInvoke.MorphologyEx(imgInput, opened, Emgu.CV.CvEnum.MorphOp.Open, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1, 0));
            pictureBox1.Image = BitmapExtension.ToBitmap(opened);

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            /*var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            var threshold = new Mat();
            CvInvoke.Threshold(gray, threshold, Convert.ToDouble($"{numericUpDown1.Value}"), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            */
            var kernel = new Mat();
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown2.Value)), center);

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            var closed = new Mat();
            CvInvoke.MorphologyEx(imgInput, closed, Emgu.CV.CvEnum.MorphOp.Close, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1, 0));
            pictureBox1.Image = BitmapExtension.ToBitmap(closed);

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            var threshold = new Mat();
            CvInvoke.Threshold(gray, threshold, Convert.ToDouble($"{numericUpDown1.Value}"), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            */
            var kernel = new Mat();
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown2.Value)), center);

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            var dilated = new Mat();
            CvInvoke.Dilate(imgInput, dilated, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1, 0));
            pictureBox1.Image = BitmapExtension.ToBitmap(dilated);

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            var threshold = new Mat();
            CvInvoke.Threshold(gray, threshold, Convert.ToDouble($"{numericUpDown1.Value}"), 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            */
            var kernel = new Mat();
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Ellipse, new Size(Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown2.Value)), center);

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            var eroded = new Mat();
            CvInvoke.Erode(imgInput, eroded, kernel, center, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1, 0));
            pictureBox1.Image = BitmapExtension.ToBitmap(eroded);

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //imgTmp = new Image<Bgr, byte>("savebutton.jpg");

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            

            var andop = new Mat();
            CvInvoke.BitwiseAnd(imgInput, imgTmp, andop);
            pictureBox2.Image = BitmapExtension.ToBitmap(andop);

            pictureBox2.Image.Save("revert2.jpg");
            imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //imgTmp = new Image<Bgr, byte>("savebutton.jpg");

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            

            var notop = new Mat();
            CvInvoke.BitwiseNot(imgInput, notop);
            pictureBox1.Image = BitmapExtension.ToBitmap(notop);

            //pictureBox2.Image.Save("revert2.jpg");
            //imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            pictureBox1.Image.Save("krumpli.jpg");
            imgInput = new Image<Bgr, byte>("krumpli.jpg");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            //imgTmp = new Image<Bgr, byte>("savebutton.jpg");

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            

            var orop = new Mat();
            CvInvoke.BitwiseOr(imgInput, imgTmp, orop);
            pictureBox2.Image = BitmapExtension.ToBitmap(orop);

            pictureBox2.Image.Save("revert2.jpg");
            imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            //imgTmp = new Image<Bgr, byte>("savebutton.jpg");

            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            

            var xorop = new Mat();
            CvInvoke.BitwiseXor(imgInput, imgTmp, xorop);
            pictureBox2.Image = BitmapExtension.ToBitmap(xorop);

            pictureBox2.Image.Save("revert2.jpg");
            imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("tempimg.jpg");
            imgTmp = new Image<Bgr, byte>("tempimg.jpg");
            pictureBox3.Image = BitmapExtension.ToBitmap(imgTmp);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image.Save("krumpli.jpg");
            pictureBox1.Image = BitmapExtension.ToBitmap(imgRevert);
            imgInput = imgRevert;
            //pictureBox1.Image = BitmapExtension.ToBitmap(imgInput);
            //imgTmp = new Image<Bgr, byte>("temp.jpg");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");

            

            var addop = new Mat();
            CvInvoke.Add(imgInput, imgTmp, addop);
            pictureBox2.Image = BitmapExtension.ToBitmap(addop);

            pictureBox2.Image.Save("revert2.jpg");
            imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("revert.jpg");
            imgRevert = new Image<Bgr, byte>("revert.jpg");
            

            var subop = new Mat();
            CvInvoke.Subtract(imgInput, imgTmp, subop);
            pictureBox2.Image = BitmapExtension.ToBitmap(subop);

            pictureBox2.Image.Save("revert2.jpg");
            imgRevert2 = new Image<Bgr, byte>("revert2.jpg");

            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("saved.jpg");
            imgSave = new Image<Bgr, byte>("saved.jpg");
            pictureBox2.Image.Save("saved2.jpg");
            imgSave = new Image<Bgr, byte>("saved2.jpg");


        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            var gray = new Mat();
            CvInvoke.CvtColor(imgInput, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            pictureBox1.Image = BitmapExtension.ToBitmap(gray);
            

            Mat imglabels = new Mat();
            CvInvoke.ConnectedComponents(gray, imglabels, Emgu.CV.CvEnum.LineType.FourConnected, Emgu.CV.CvEnum.DepthType.Cv16U);
            


            imgCC = imglabels.ToImage<Gray, byte>();
            pictureBox2.Image = BitmapExtension.ToBitmap(imgCC);


        }
         






        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = BitmapExtension.ToBitmap(imgRevert2);
            imgInput = imgRevert2;
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        
    }
}
