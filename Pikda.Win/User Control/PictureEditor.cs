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
            _picturePanel = picturePanel;
            _ocrModel = model;
            _ocrRepository = ocrRepository;

            InitializeComponent();
            MarkAsSelected();
            SetInitialPictureAndRects();
        }

        #region Public Interface

        /// <summary>
        /// Id if the assosiated OCR Model
        /// </summary>
        public int Id => _ocrModel.Id;
        /// <summary>
        /// Use MarkAsSelected() or MarkAsUnSelected() to update this value
        /// </summary>
        public bool IsSelected { get; private set; }
        /// <summary>
        /// Current card image
        /// </summary>
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

        #endregion

        #region Private Behaviors
        private void SetInitialPictureAndRects()
        {
            if (_ocrModel.Image != null)
            {
                Image = _ocrModel.Image;
                ImageBorder = CalcImageBorder();

                Console.WriteLine(" --> Count of areas : " + _ocrModel.Areas.Count);

                foreach (var area in _ocrModel.Areas)
                {
                    Rectangles.Add(GetRectFromAreaDto(ImageBorder, area));
                }
            }
        }

        private void UnSelectAllAndSelectThis()
        {
            foreach (PictureEditor modelButton in this._picturePanel.Controls)
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

            StartPoint = e.Location;
            CurrentRect = new Rectangle(e.Location, new Size(0, 0));
        }

        private void PictureEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (StartPoint == UnDefinedPoint) return;

            if (e.Button != MouseButtons.Left || PictureEdit.Image is null)
                return;

            EndPoint = e.Location;

            CurrentRect = new Rectangle
                (
                    Math.Min(StartPoint.X, EndPoint.X),
                    Math.Min(StartPoint.Y, EndPoint.Y),
                    Math.Abs(StartPoint.X - EndPoint.X),
                    Math.Abs(StartPoint.Y - EndPoint.Y)
                );
            CurrentRect.Intersect(ImageBorder);

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

            _ocrModel = await _ocrRepository.AddAreaAsync(Id, GetAreaDtoFromRect(ImageBorder, UnDefinedName, CurrentRect));

            StartPoint = UnDefinedPoint;

            CurrentRect = UnDefinedRect;
        }

        private AreaDto GetAreaDtoFromRect(Rectangle border, string name, Rectangle rect)
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

        private (Rectangle, string) GetRectFromAreaDto(Rectangle border, AreaDto area)
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

        private void ReCalcRectangles()
        {

            var areas = _ocrModel.Areas;
            Rectangles = areas.Select(a => GetRectFromAreaDto(ImageBorder, a)).ToList();

        }

        private void PictureEdit_Paint(object sender, PaintEventArgs e)
        {
            if (PictureEdit.Image is null) return;

            ImageBorder = CalcImageBorder();
            ReCalcRectangles();

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            // Draw all previous rectangles
            foreach (var rect in Rectangles)
            {
                g.DrawRectangle(pen, rect.Item1);
            }

            // Draw the current rectangle
            if (CurrentRect != UnDefinedRect)
                g.DrawRectangle(pen, CurrentRect);

            // Dispose the objects
            pen.Dispose();
            this.ResumeLayout(true);

        }

        private Rectangle CalcImageBorder()
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

        private async void PictureEdit_ImageChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"\n\n\n\n -----------> Image = {Image}");
            if (_ocrRepository is null) return;
            await _ocrRepository.ChangeImageAsync(Id, Image);

        }

        private void PictureEdit_Resize(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        #endregion

        #region Private State

        private readonly Panel _picturePanel;
        private readonly IOcrRepository _ocrRepository;
        private OcrModelDto _ocrModel; // will be updated throw the database when change happen

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

        #endregion
    }
}
