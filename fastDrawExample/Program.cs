using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

Bitmap bmp = null;

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

form.Load += (o, e) =>
{
    bmp = Image.FromFile("img.jpg") as Bitmap;
    pb.Image = bmp;
};

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.A)
    {

        for (int j = 0; j < bmp.Height; j++)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                var pixel = bmp.GetPixel(i, j);
                var inverse = Color.FromArgb(
                    255 - pixel.R,
                    255 - pixel.G,
                    255 - pixel.B
                );

                bmp.SetPixel(i, j, inverse);
            }
        }
    }
    else if (e.KeyCode == Keys.S)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            for (int j = 0; j < data.Height; j++)
            {
                byte* l = p + j * data.Stride;
                for (int i = 0; i < data.Width; i++, l+=3)
                {
                    l[0] = (byte)(255 - l[0]);
                    l[1] = (byte)(255 - l[1]);
                    l[2] = (byte)(255 - l[2]);
                }
            }
        }

        bmp.UnlockBits(data);
    }
    pb.Refresh();
};

Application.Run(form);