using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public abstract class App
{
    protected Form form = null;
    protected Bitmap bmp = null;
    protected Graphics g = null;

    public PointF Cursor { get; private set; } = PointF.Empty;

    /// <summary>
    /// Inicia o Aplicativo
    /// </summary>
    public void Run()
    {
        ApplicationConfiguration.Initialize();

        PictureBox pb = new PictureBox()
        {
            Dock = DockStyle.Fill
        };

        var timer = new Timer
        {
            Interval = 10,
        };

        this.form = new Form
        {
            WindowState = FormWindowState.Maximized,
            FormBorderStyle = FormBorderStyle.None,
            Controls = { pb }
        };

        pb.MouseMove += (o, e) => {
            this.Cursor = e.Location;

            this.OnMouseMove(o, e);
        };

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            pb.Image = bmp;

            g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.Clear(Color.DarkGray);
            pb.Refresh();

            this.Open();
            timer.Start();
        };

        form.KeyDown += this.OnKeyDown;

        form.KeyUp += this.OnKeyUp;

        timer.Tick += delegate
        {
            g.Clear(Color.DarkGray);

            this.OnFrame();

            pb.Refresh();
        };

        Application.Run(form);
    }

    /// <summary>
    /// A cada frame essa função será chamada
    /// </summary>
    public abstract void OnFrame();

    /// <summary>
    /// Será chamada ao abrir o aplicativo, antes de executar o primeiro frame
    /// </summary>
    public virtual void Open() {}

    /// <summary>
    /// Finaliza o programa
    /// </summary>
    public virtual void Close() { this.form.Close(); }

    /// <summary>
    /// Será chamada sempre que o mouse se movimentar sobre o aplicativo
    /// </summary>
    /// <param name="o">Objeto que disparou o evento</param>
    /// <param name="e">Dados sobre o mouse</param>
    public virtual void OnMouseMove(object o, MouseEventArgs e) {}

    /// <summary>
    /// Será chamada sempre que uma tecla for pressionada sobre o aplicativo
    /// </summary>
    /// <param name="o">Objeto que disparou o evento</param>
    /// <param name="e">Dados sobre o teclado</param>
    public virtual void OnKeyDown(object o, KeyEventArgs e) {}

    /// <summary>
    /// Será chamada sempre que uma tecla for solta sobre o aplicativo
    /// </summary>
    /// <param name="o">Objeto que disparou o evento</param>
    /// <param name="e">Dados sobre o teclado</param>
    public virtual void OnKeyUp(object o, KeyEventArgs e) {}

}