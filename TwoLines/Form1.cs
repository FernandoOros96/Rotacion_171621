using System.Drawing;
using System.Windows.Forms;

namespace TwoLines
{
    public partial class Form1 : Form
    {
        static Bitmap bmp;
        static Graphics g;

        private PointF a,b,c,d;
        private int Sx, Sy;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            PCT_CANVAS.Image = bmp;

            //Líneas para ubicar punto medio
            a = new PointF(0, PCT_CANVAS.Height / 2);
            b = new PointF(PCT_CANVAS.Width, PCT_CANVAS.Height / 2);
            c = new PointF(PCT_CANVAS.Width / 2, 0);
            d = new PointF(PCT_CANVAS.Width / 2, PCT_CANVAS.Height);

            g.DrawLine(Pens.Gray, a, b);
            g.DrawLine(Pens.Gray, c, d);
        }

        //Método de rotación
        private void Render(PointF a, PointF b, int degrees)
        {
            PointF a2, b2;
            double angle = Math.PI * degrees / 180.0;

            a2 = new PointF(Sx + a.X, Sy - a.Y);
            b2 = new PointF(Sx + b.X, Sy - b.Y);
            a2.X = Sx + (float)((a.X * Math.Cos(angle)) - (a.Y * Math.Sin(angle)));
            a2.Y = Sy - (float)((a.X * Math.Sin(angle)) + (a.Y * Math.Cos(angle)));
            b2.X = Sx + (float)((b.X * Math.Cos(angle)) - (b.Y * Math.Sin(angle)));
            b2.Y = Sy - (float)((b.X * Math.Sin(angle)) + (b.Y * Math.Cos(angle)));
            g.DrawLine(Pens.White, a2, b2);
            PCT_CANVAS.Refresh();
        }

        //Teniendo como pivote el origen 0,0 en el centro de la pantalla.
        private void button2_Click(object sender, EventArgs e)
        {
            //Rotar cuadrado
            Sx = (bmp.Width / 2);
            Sy = (bmp.Height / 2);

            a = new PointF(0, 0);
            b = new PointF(0,100);
            c = new PointF(100, 100);
            d = new PointF(100, 0);
            rotate(a, b, c, d);
        }

        //Rotar la figura colocando su centro en el origen en el centro de la pantalla.
        private void button3_Click(object sender, EventArgs e)
        {
            Sx = (bmp.Width / 2);
            Sy = (bmp.Height / 2);

            a = new PointF(-50, -50);
            b = new PointF(-50, 50);
            c = new PointF(50, 50);
            d = new PointF(50, -50);
            rotate(a, b, c, d);

        }
        //Rotación de acuerdo al pivote de la figura en su centro.
        private void button5_Click(object sender, EventArgs e)
        {
            Sx = (bmp.Width / 2) + 50;
            Sy = (bmp.Height / 2) - 50;

            a = new PointF(-50,-50);
            b = new PointF(-50,50);
            c = new PointF(50, 50);
            d = new PointF(50, -50);
            rotate(a, b, c, d);

        }

        //Recibir puntos y grados para rotar
        private void rotate(PointF a, PointF b, PointF c, PointF d)
        {
            int degrees = 0;
            if (Int32.TryParse(textBox1.Text, out degrees))
            {
                Render(a,b, degrees);
                Render(b, c, degrees);
                Render(c, d, degrees);
                Render(d, a, degrees);
            }
            else
            {
                MessageBox.Show("¡Ingresa un valor entero en el TextBox!", "Error");
            }
        }

        //Asignar el valor de los grados de la barra al textBox
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        //Crear nuevo objeto de la clase principal para resetear
        private void resetButton_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }
    }
}