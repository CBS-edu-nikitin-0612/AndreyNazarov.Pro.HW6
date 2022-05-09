using System;

namespace Task2
{
    public class Converter
    {
        private double tempK;
        public Converter(double temp, TempScale tempScale)
        {
            switch (tempScale)
            {
                case TempScale.Fahrenheit:
                    tempK = (temp - 32) / 1.8 + 273.15;
                    break;
                case TempScale.Celsius:
                    tempK = temp + 273.15;
                    break;
                case TempScale.Kelvin:
                    tempK = temp;
                    break;
            }
            if (tempK < -273.15)
            {
                throw new ArgumentException("Невозможное значени температуры: " + tempK);
            }
        }

        public double TempK { get => tempK; }
        public double TempC { get => tempK - 273.15; }
        public double TempF { get => (tempK - 273.15) * 1.8 + 32; }

        public enum TempScale
        {
            Fahrenheit = 0,
            Celsius = 1,
            Kelvin = 2
        }
    }
}
