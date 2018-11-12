using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class BOSBARateCalculator
    {
        BookingDataClassesDataContext context;

        public BOSBARateCalculator()
        {
            context = new BookingDataClassesDataContext();
        }

        public RatesACI GetRates(string zipcode)
        {
            var ratesACI = (from aci in context.RatesACIs
                            where aci.ZipBegin == zipcode
                            orderby aci.Area
                            select aci).ToList();

            if (ratesACI != null && ratesACI.Count > 0)
            {
                return ratesACI[0];
            }
            else
            {
                return null;
            }
        }

        public string GetRateStateRegion(string stateCode, int customerId)
        {
            var rateState = (from state in context.BookingRateStates
                             where state.StateCode == stateCode
                                && state.CustomerID == customerId
                             select state).ToList();

            if (rateState != null && rateState.Count > 0)
            {
                return rateState[0].Region;
            }
            else
            {
                return "";
            }
        }

        public string GetRateZone(string startRegion, string endRegion, int customerId)
        {
            var rateZoneMat = (from rateZone in context.BookingRateZoneMats
                               where rateZone.Coords == startRegion + endRegion
                               && rateZone.CustomerID == customerId
                               select rateZone).ToList();

            if (rateZoneMat != null && rateZoneMat.Count > 0)
            {
                return rateZoneMat[0].Zone;
            }
            else
            {
                return "";
            }
        }

        public decimal GetRateTariffCharge(string serviceType, string zone, decimal weight, int customerId)
        {
            if (string.IsNullOrEmpty(serviceType))
                serviceType = "E";

            decimal tariffCharge = 0, minCharge = 0;
            var rateTariffs = (from tariff in context.BookingRateTariffs
                              where tariff.CustomerID == customerId
                              && tariff.Service == serviceType
                              && tariff.Zone == zone
                              orderby tariff.Weight descending
                              select tariff).ToList();

            if (rateTariffs != null && rateTariffs.Count > 0)
            {
                foreach (BookingRateTariff tariff in rateTariffs)
                {
                    if (tariff.Weight <= weight)
                    {
                        tariffCharge = (tariff.Rate * weight) / 100;
                        break;
                    }
                }

                minCharge = rateTariffs[0].Rate;

                foreach (BookingRateTariff tariff in rateTariffs)
                {
                    if (tariff.Rate < minCharge)
                        minCharge = tariff.Rate;
                }

                if (tariffCharge < minCharge)
                    tariffCharge = minCharge;
            }

            return tariffCharge;
        }

        public static decimal GetChargeableWeight(List<PackageInfo> packages, int dimFactor)
        {
            decimal totalDim = 0, totalWeight = 0;

            foreach (PackageInfo package in packages)
            {
                if (dimFactor != 0)
                    totalDim += package.Height * package.Width * package.Length / dimFactor;

                if (package.Weight != 0)
                    totalWeight += package.Weight;
            }

            return totalDim > totalWeight ? totalDim : totalWeight;
        }

        public BookingRateBeyond GetRateBeyond(string fromRegion, string toRegion, int customerId)
        {
            return (from rateBeyond in context.BookingRateBeyonds
                    where rateBeyond.FromRegion == fromRegion
                    && rateBeyond.ToRegion == toRegion
                    && rateBeyond.CustomerID == customerId
                    select rateBeyond).SingleOrDefault();
        }

        public List<BookingCustomerAccessorial> GetAccessorialRates(int customerId)
        {
            return (from accessorialRate in context.BookingCustomerAccessorials
                    where accessorialRate.CustomerID == customerId
                    select accessorialRate).ToList();
        }
    }
}
