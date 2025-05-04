// SmoothSlider.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public class SmoothSlider : Control
    {
        private float _value = 50;
        private bool _isDragging = false;
        private int _thumbSize = 20;

        public float Minimum { get; set; } = 0;
        public float Maximum { get; set; } = 100;
        public bool SnapToInt { get; set; } = false;

        public event EventHandler ValueChanged;

        public float Value
        {
            get => _value;
            set
            {
                _value = Clamp(value, Minimum, Maximum);
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        private float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public SmoothSlider()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            this.Height = 40;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                UpdateValue(e.X);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isDragging) UpdateValue(e.X);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isDragging = false;
            base.OnMouseUp(e);
        }

        private void UpdateValue(int mouseX)
        {
            float percent = (mouseX - _thumbSize / 2) / (float)(Width - _thumbSize);
            float v = Minimum + (Maximum - Minimum) * percent;
            if (SnapToInt)
                v = (float)Math.Round(v);
            Value = v;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen trackPen = new Pen(Color.LightGray, 4))
            {
                int trackY = Height / 2;
                e.Graphics.DrawLine(trackPen, _thumbSize / 2, trackY, Width - _thumbSize / 2, trackY);
            }

            float thumbPosition = (Value - Minimum) / (Maximum - Minimum) * (Width - _thumbSize);

            using (SolidBrush thumbBrush = new SolidBrush(Color.DodgerBlue))
            {
                e.Graphics.FillEllipse(thumbBrush, thumbPosition, Height / 2 - _thumbSize / 2, _thumbSize, _thumbSize);
            }
        }
    }
}
