using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModel
{
    public interface IConcentrationCalculations
    {
        double Calculation(double latitude, double longitude);
        double CalculationWindsRose(double latitude, double longitude);
    }
}
