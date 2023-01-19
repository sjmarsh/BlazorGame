﻿namespace BlazorGame.Models
{
    public record ViewDimensions
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public bool IsMobileDevice => Width < Constants.MaxMobileDeviceWidth;
    }
}
