using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static CalculationModel.SimpleCalculation;

namespace CalculationModel
{
    public class ConcentrationCalculations: IConcentrationCalculations
    {
        private const int Alpha = 2;
        private readonly IList<Post> Q;
        private readonly int N; 


        public ConcentrationCalculations(IList<Post> q)
        {
            Q = q;
            N = Q.Count;
        }

        public double Calculation(double latitude, double longitude)
        {
            var p = new Point(latitude, longitude);
            if (Q.Select(q => q.Point).Contains(p))
                throw new ArgumentException("Расчётная точка совпадает с расположением одного из ПНЗА");
            double nominator = 0, denominator = 0;

            for (int k = 1; k <= N; k++)
            {
                double pow =
                    Math.Pow(GetDistanceFromLatLonInKm(latitude, longitude, Q[k].Point.Latitude, Q[k].Point.Longitude),
                        Alpha);
                nominator += Q[k].Concentration / pow;
                denominator += 1 / pow;

            }
            return nominator/denominator;
        }

        public double CalculationWindsRose(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}
