using DevExpress.XtraBars.MVVM.Services;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Pikda.Domain.DTOs;
using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using Pikda.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikda.Win.User_Control
{
    public partial class PictureEditor : UserControl
    {
        public PictureEditor(IOcrRepository ocrRepository, Panel picturePanel, OcrModelDto model)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Id = model.Id;
            this.PicturePanel = picturePanel;
            this.MarkAsSelected();

            if (model.Image != null)
            {
                Image = model.Image;
                ImageBorder = CalcImageBorder();
    
                Console.WriteLine(" --> Count of areas : " + model.Areas.Count);

                foreach (var area in model.Areas)
                {
                    Rectangles.Add(GetRectFromAreaDto(ImageBorder,area));
                }

                Console.WriteLine(" --> Count of reactangles : " + Rectangles.Count);

                foreach (var rect in Rectangles)
                {
                    Console.WriteLine($"Rect {rect.Item2} : X:{rect.Item1.X}, Y:{rect.Item1.Y}, W:{rect.Item1.Width}, H:{rect.Item1.Height}");
                }
            }
            //Rectangles = model.Areas
            //    .Select(a => (a.ToRectangle(ImageBorder), a.Name))
            //    .ToList();


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
            
            Console.WriteLine(" --> ocr mode deposed ? : " + _ocrMode.Id+ " " + _ocrMode.Name);


            _ocrMode = await _ocrRepository.AddAreaAsync(Id, GetAreaDtoFromRect(ImageBorder,UnDefinedName,CurrentRect));
            Console.WriteLine($"after adding area : {_ocrMode}");
            StartPoint = UnDefinedPoint;

            CurrentRect = UnDefinedRect;
        }

        private AreaDto GetAreaDtoFromRect(Rectangle border ,string name, Rectangle rect)
        {
            rect = new Rectangle
                (
                    x: rect.X - (int)(((float)(this.Width - border.Width)) / 2),
                    y: rect.Y - (int)(((float)(this.Height - border.Height)) / 2),
                    width: rect.Width,
                    height: rect.Height
                );
            return AreaDto.Create(name, border, rect);
        }

        private (Rectangle,string) GetRectFromAreaDto(Rectangle border,AreaDto area)
        {
            var rect = area.ToRectangle(border);

            return (
                    new Rectangle
                    (
                        x: rect.X + (int)(((float)(this.Width - border.Width)) / 2),
                        y: rect.Y + (int)(((float)(this.Height - border.Height)) / 2),
                        width: rect.Width,
                        height: rect.Height
                    ),
                    area.Name
                );
        }

        private async Task ReCalcRectangles()
        {

            var areas = await _ocrRepository.GetOcrModelAreas(_ocrMode.Id);
            Rectangles = areas.Select(a => GetRectFromAreaDto(ImageBorder, a)).ToList();

        }
        private async void PictureEdit_Paint(object sender, PaintEventArgs e)
        {
            this.SuspendLayout();

            if (PictureEdit.Image is null) return;
            //Rectangles = Rectangles.Select(r => ReCalculateRectangle(r.Item2, r.Item1)).ToList();

            await ReCalcRectangles();


            // Create a Graphics object and a Pen object
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            // Draw all previous rectangles
            foreach (var rect in Rectangles)
            {
                g.DrawRectangle(pen, rect.Item1);
            }

            // Draw the current rectangle
            if(CurrentRect != UnDefinedRect)
                g.DrawRectangle(pen, CurrentRect);

            Console.WriteLine("Picture Edit : " + PictureEdit.Size);
            Console.WriteLine("Image        : " + PictureEdit.Image.Size);

            ImageBorder = CalcImageBorder();

            Console.WriteLine($"Image Border: {ImageBorder}");
            Console.WriteLine($"Current Rectangle: {CurrentRect}");

            pen.Color = Color.Red;

            //g.DrawRectangle(pen, ImageBorder);


            // Dispose the objects
            pen.Dispose();
            this.ResumeLayout(true);

        }

        Rectangle CalcImageBorder()
        {

            var wFactor = (double)PictureEdit.Width / PictureEdit.Image.Width;
            var hFactor = (double)PictureEdit.Height / PictureEdit.Image.Height;

            var (minFactor, isWidthMinFactor) = (wFactor < hFactor ? wFactor : hFactor, (wFactor < hFactor));

            return new Rectangle
                (
                    x: isWidthMinFactor ? 0 : (PictureEdit.Width - (int)(PictureEdit.Image.Width * minFactor)) / 2,
                    y: isWidthMinFactor ? (PictureEdit.Height - (int)(PictureEdit.Image.Height * minFactor)) / 2 : 0,
                    width: (int)(PictureEdit.Image.Width * minFactor),
                    height: (int)(PictureEdit.Image.Height * minFactor)
                );
        }
  

        private readonly IOcrRepository _ocrRepository;
        private OcrModelDto _ocrMode;
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

        private List<(Rectangle, string)> Rectangles = new List<(Rectangle, string)>();
        private Rectangle CurrentRect;
        private static readonly Rectangle UnDefinedRect = new Rectangle(-404, -404, -404, -404);

        private Rectangle PrevImageBorder = UnDefinedRect;
        private Rectangle _imageBorder = UnDefinedRect;
        private Rectangle ImageBorder
        {
            get
            {
                return _imageBorder;
            }
            set
            {
                if (PrevImageBorder == UnDefinedRect || _imageBorder == UnDefinedRect)
                {
                    PrevImageBorder = _imageBorder = value;
                    return;
                }

                PrevImageBorder = _imageBorder;
                _imageBorder = value;
            }
        }

        private static readonly Point UnDefinedPoint = new Point(-404, -404);
        private static readonly string UnDefinedName = "Not Named";
        private Point StartPoint = UnDefinedPoint;
        private Point EndPoint;


        private void PictureEdit_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            Console.WriteLine("\n\n---> Image from event : " + e.Image);
            Console.WriteLine("\n\n---> Image from editor : " + Image);
        }

        private async void PictureEdit_ImageChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"\n\n\n\n -----------> Image = {Image}");
            if (_ocrRepository is null) return;
            await _ocrRepository.ChangeImageAsync(Id, Image);

        }

        private (Rectangle,string) ReCalculateRectangle(string name,Rectangle rect)
        {
            var areaDto = GetAreaDtoFromRect(PrevImageBorder, name, rect); 
            return GetRectFromAreaDto(ImageBorder, areaDto);
        }
        private async void PictureEdit_SizeChanged(object sender, EventArgs e)
        {
            //ImageBorder = CalcImageBorder();
            //await ReCalcRectangles();
        }

    }
}
