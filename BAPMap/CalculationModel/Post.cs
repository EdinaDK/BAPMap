using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationModel
{
    public record Point(double Latitude, double Longitude);

    public record Post(Point Point, double Concentration);

}
