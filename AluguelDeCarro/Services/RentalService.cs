﻿using System;
using AluguelDeCarro.Entities;

namespace AluguelDeCarro.Services
{
    class RentalService
    {
        public double PrincePerHour { get; private set; }
        public double PricePerDay { get; private set; }

        private ITaxService _taxService;

        public RentalService(double princePerHour, double pricePerDay, ITaxService taxService)
        {
            PrincePerHour = princePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice(CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double basicPayment = 0.0;

            if (duration.TotalHours <= 12.0)
            {
                basicPayment = PrincePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double tax = _brazilTaxService.Tax(basicPayment);

            carRental.Invoice = new Invoice(basicPayment, tax);
        }
    }
}
