using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ShapePackerForm
{
    public partial class ShapePackerForm : Form
    {
        public ShapePackerForm()
        {
            InitializeComponent();
        }

        List<Shape> Shapes = new List<Shape>();

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            int width = pictureBoxGrid.DisplayRectangle.Width;
            int height = pictureBoxGrid.DisplayRectangle.Height;

            Grid grid = new Grid(width / 2, height / 2);

            if (Shapes.Count != 0)
            {
                Shape[] shapes = Shapes.ToArray();

                new Thread(() => FillGridAndDisplay(grid, shapes)).Start();
            }
            else
            {
                MessageBox.Show("No shapes to insert.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void FillGridAndDisplay(Grid grid, Shape[] shapes)
        {
            Invoke((MethodInvoker)delegate
            {
               progressBar1.Value = 0;
            });
            Invoke((MethodInvoker)delegate
            {
                progressBar1.Maximum = shapes.Length;
            });

            int i = 30;

            foreach (Shape shape in shapes)
            {
                shape.InsertIntoGrid(grid, i);

                if (i < 256)
                    i += 30;
                else
                    i = 30;
                
                Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value++;
                });
            }

            Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = shapes.Length;
            });

            grid.Trim();

            Bitmap bitmap = FromTwoDimIntArrayGray(grid.grid);

            bitmap.RotateFlip(RotateFlipType.Rotate270FlipY);

            //Resize the bmp to fit the display so you can use nearest neighbour scaling and reduce blurry edges
            if(pictureBoxGrid.DisplayRectangle.Width > pictureBoxGrid.DisplayRectangle.Height)
                ResizeBitmap(bitmap, pictureBoxGrid.DisplayRectangle.Height / grid.Y * grid.X, pictureBoxGrid.DisplayRectangle.Height);
            else
                ResizeBitmap(bitmap, pictureBoxGrid.DisplayRectangle.Width, pictureBoxGrid.DisplayRectangle.Width / grid.X * grid.Y);
            

            pictureBoxGrid.Image = bitmap;

            Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = 0;
            });
        }

        public static Bitmap FromTwoDimIntArrayGray(int[,] data)
        {
            // Transform 2-dimensional int array to 1-byte-per-pixel byte array
            int width = data.GetLength(0);
            int height = data.GetLength(1);
            int byteIndex = 0;
            Byte[] dataBytes = new Byte[height * width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // logical AND to be 100% sure the int value fits inside
                    // the byte even if it contains more data (like, full ARGB).
                    dataBytes[byteIndex] = (Byte)(((uint)data[x, y]) & 0xFF);
                    // More efficient than multiplying
                    byteIndex++;
                }
            }
            // generate palette
            Color[] palette = new Color[256];
            for (int b = 0; b < 256; b++)
                palette[b] = Color.FromArgb(b, b, b);
            // Build image
            return BuildImage(dataBytes, width, height, width, PixelFormat.Format8bppIndexed, palette, null);
        }

        public static Bitmap BuildImage(Byte[] sourceData, int width, int height, int stride, PixelFormat pixelFormat, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            int newDataWidth = ((Image.GetPixelFormatSize(pixelFormat) * width) + 7) / 8;
            // Compensate for possible negative stride on BMP format.
            Boolean isFlipped = stride < 0;
            stride = Math.Abs(stride);
            // Cache these to avoid unnecessary getter calls.
            int targetStride = targetData.Stride;
            Int64 scan0 = targetData.Scan0.ToInt64();
            for (int y = 0; y < height; y++)
                Marshal.Copy(sourceData, y * stride, new IntPtr(scan0 + y * targetStride), newDataWidth);
            newImage.UnlockBits(targetData);
            // Fix negative stride on BMP format.
            if (isFlipped)
                newImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            // For indexed images, set the palette.
            if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            {
                ColorPalette pal = newImage.Palette;
                for (int i = 0; i < pal.Entries.Length; i++)
                {
                    if (i < palette.Length)
                        pal.Entries[i] = palette[i];
                    else if (defaultColor.HasValue)
                        pal.Entries[i] = defaultColor.Value;
                    else
                        break;
                }
                newImage.Palette = pal;
            }
            return newImage;
        }

        public Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        private void ShapePackerForm_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxGrid.Height = this.Height - 95;
        }

        private void ShapePackerForm_Load(object sender, EventArgs e)
        {
            pictureBoxGrid.Height = this.Height - 95;
        }

        private void buttonShapes_Click(object sender, EventArgs e)
        {
            if (buttonShapes.Text == "Shapes")
            {
                pictureBoxGrid.Visible = false;
                buttonShapes.Text = "Image";
            }
            else
            {
                pictureBoxGrid.Visible = true;
                buttonShapes.Text = "Shapes";
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDownCount.Value; i++)
            {
                switch (comboBoxShapes.SelectedItem.ToString())
                {
                    case "Rectangle":
                        Shapes.Add(new Rectangle((int)numericUpDownX.Value, (int)numericUpDownY.Value));
                        break;
                    case "Circle":
                        Shapes.Add(new Circle((int)numericUpDownX.Value));
                        break;
                    case "Triangle":
                        Shapes.Add(new Triangle((int)numericUpDownX.Value, (int)numericUpDownY.Value));
                        break;
                    case "Square":
                        Shapes.Add(new Square((int)numericUpDownX.Value));
                        break;
                }
            }
        }

        private void comboBoxShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxShapes.SelectedItem.ToString() == "Square" || comboBoxShapes.SelectedItem.ToString() == "Circle")
            {
                numericUpDownY.Enabled = false;
            }
            else
            {
                numericUpDownY.Enabled = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Shapes = new List<Shape>();
        }
    }
}