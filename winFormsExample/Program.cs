using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

Bitmap bmp = null;

Timer tm = new Timer();
tm.Interval = 25;

form.KeyDown += (o, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    pb.Image = bmp;

    // var g = Graphics.FromImage(bmp);

    // g.DrawLine(Pens.Blue, 
    //     0, 0,
    //     pb.Width, pb.Height);



    // pb.Refresh();
    tm.Start();
};

int i = 0;
tm.Tick += (o, e) =>
{   
    float a = pb.Height / (float)pb.Width;
    bmp.SetPixel(i, (int)(a * i), Color.Blue);
    i++;

    if (i >= pb.Width)
        tm.Stop();
    
    pb.Refresh();
};

Application.Run(form);