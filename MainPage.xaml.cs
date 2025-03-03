using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Graphics;

namespace MauiColorPicker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateColor(); // Ustawienie początkowego koloru
        }

        private void OnColorChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            int red = (int)SliderRed.Value;
            int green = (int)SliderGreen.Value;
            int blue = (int)SliderBlue.Value;

            Color newColor = Color.FromRgb(red, green, blue);
            this.BackgroundColor = newColor;

            string hex = $"#{red:X2}{green:X2}{blue:X2}";
            HexLabel.Text = hex;
            HexEntry.Text = hex; // Automatycznie aktualizuje pole HEX
        }

        private void OnRandomColorClicked(object sender, EventArgs e)
        {
            Random random = new Random();
            SliderRed.Value = random.Next(0, 256);
            SliderGreen.Value = random.Next(0, 256);
            SliderBlue.Value = random.Next(0, 256);
        }

        private void OnResetColorClicked(object sender, EventArgs e)
        {
            SliderRed.Value = 255;
            SliderGreen.Value = 255;
            SliderBlue.Value = 255;
        }

        private async void OnHexLabelTapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(HexLabel.Text);
            await DisplayAlert("Skopiowano!", "Kod HEX został skopiowany do schowka.", "OK");
        }

        private void OnHexEntryChanged(object sender, TextChangedEventArgs e)
        {
            if (Color.FromHex(e.NewTextValue) is Color newColor)
            {
                this.BackgroundColor = newColor;
                SliderRed.Value = (int)(newColor.Red * 255);
                SliderGreen.Value = (int)(newColor.Green * 255);
                SliderBlue.Value = (int)(newColor.Blue * 255);
            }
        }
    }
}
