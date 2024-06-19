using DevExpress.XtraBars.MVVM.Services;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikda.Win.User_Control
{
    public partial class PictureEditor : UserControl
    {
        public PictureEditor(IOcrRepository ocrRepository, Panel picturePanel, OcrModel model)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Id = model.Id;
            this.PicturePanel = picturePanel;
            this.MarkAsSelected();

            Rectangles = model.Areas
                .Select(a => (a.ToRectangle(ImageBorder), model.Name))
                .ToList();

            this._ocrRepository = ocrRepository;
            _ocrMode = model;
        }

        private void UnSelectAllAndSelectThis()
        {
            foreach (PictureEditor modelButton in this.PicturePanel.Controls)
                modelButton.MarkAsUnSelected();

            this.MarkAsSelected();
        }

        public void MarkAsSelected()
        {
            this.Visible = true; ;
            this.IsSelected = true;
        }

        public void MarkAsUnSelected()
        {
            this.Visible = false;
            this.IsSelected = false;
        }


        

        private void PictureEdit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || PictureEdit.Image is null)
                return;

            if (!ImageBorder.Contains(e.Location))
                return;

            Console.WriteLine("mouse down picture edit");
            StartPoint = e.Location;
            CurrentRect = new Rectangle(e.Location, new Size(0, 0));
        }

        private void PictureEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (StartPoint == UnDefinedPoint) return;

            if (e.Button != MouseButtons.Left || PictureEdit.Image is null)
                return;

            EndPoint = e.Location;

            // Update the current rectangle
            CurrentRect = new Rectangle
                (
                    Math.Min(StartPoint.X , EndPoint.X),
                    Math.Min(StartPoint.Y , EndPoint.Y),
                    Math.Abs(StartPoint.X - EndPoint.X),
                    Math.Abs(StartPoint.Y - EndPoint.Y)
                );
            CurrentRect.Intersect( ImageBorder );

            // Invalidate the form to trigger the paint event
            Console.WriteLine("mouse move , picture edit");
            PictureEdit.Invalidate();
        }

        private async void PictureEdit_MouseUp(object sender, MouseEventArgs e)
        {

            if (StartPoint == UnDefinedPoint) return;

            if (e.Button != MouseButtons.Left || PictureEdit.Image is null)
                return;

            if (CurrentRect.Width * CurrentRect.Height == 0)
                return;

            // Add the current rectangle to the list
            CurrentRect.Intersect(ImageBorder);
            Rectangles.Add((CurrentRect, UnDefinedName));
            _ocrMode.Areas.Add(Area.Create(Name, ImageBorder ,CurrentRect));
            Console.WriteLine(" --> ocr mode deposed ? : " + _ocrMode.Id+ " " + _ocrMode.Name);
            await _ocrRepository.UpdateOrcModelAsync(_ocrMode);

            StartPoint = UnDefinedPoint;
        }

        private void PictureEdit_Paint(object sender, PaintEventArgs e)
        {
            if (PictureEdit.Image is null) return;

            // Create a Graphics object and a Pen object
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            // Draw all previous rectangles
            foreach (var rect in Rectangles)
            {
                g.DrawRectangle(pen, rect.Item1);
            }

            // Draw the current rectangle
            g.DrawRectangle(pen, CurrentRect);

            Console.WriteLine("Picture Edit : " + PictureEdit.Size);
            Console.WriteLine("Image        : " + PictureEdit.Image.Size);

            var wFactor = (double) PictureEdit.Width / PictureEdit.Image.Width;
            var hFactor = (double) PictureEdit.Height / PictureEdit.Image.Height;

            var (minFactor, isWidthMinFactor) = (wFactor < hFactor ? wFactor : hFactor, (wFactor < hFactor)) ;

            ImageBorder = new Rectangle
                (
                    x       : isWidthMinFactor ? 0 : (PictureEdit.Width - (int) (PictureEdit.Image.Width * minFactor)) / 2,
                    y       : isWidthMinFactor? (PictureEdit.Height - (int) (PictureEdit.Image.Height * minFactor)) /2 : 0,
                    width   : (int) (PictureEdit.Image.Width * minFactor),
                    height  : (int) (PictureEdit.Image.Height * minFactor)
                );

            Console.WriteLine($"Image Border: {ImageBorder}");
            Console.WriteLine($"Current Rectangle: {CurrentRect}");

            pen.Color = Color.Red;

            //g.DrawRectangle(pen, ImageBorder);


            // Dispose the objects
            pen.Dispose();


        }

  

        private readonly IOcrRepository _ocrRepository;
        private OcrModel _ocrMode;
        public int Id { get; private set; }

        /// <summary>
        /// Use MarkAsSelected() or MarkAsUnSelected() to update this value
        /// </summary>
        public bool IsSelected { get; private set; }
        private Panel PicturePanel { get; set; }
        public Image Image
        {
            get
            {
                return PictureEdit.Image;
            }
            private set
            {
                PictureEdit.Image = value;
            }
        }
        private async void PictureEdit_LoadCompleted(object sender, EventArgs e)
        {
            _ocrMode.Image = this.Image;
            await _ocrRepository.UpdateOrcModelAsync(_ocrMode);
        }

        private List<(Rectangle, string)> Rectangles = new List<(Rectangle, string)>();
        private Rectangle CurrentRect;
        private Rectangle ImageBorder;

        private static readonly Point UnDefinedPoint = new Point(-404, -404);
        private static readonly string UnDefinedName = "Not Named";
        private Point StartPoint = UnDefinedPoint;
        private Point EndPoint;

    }
}
