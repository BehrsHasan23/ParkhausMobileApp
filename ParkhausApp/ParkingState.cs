using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkhausApp;

public static class ParkingState
{
   public static int? Stock1Parked { get; set; } //kann Null sein entweder geparkt oder nicht
    public static int? Stock2Parked { get; set; }

    public static int Free1 = 12;
    public static int Free2 = 12;
}
