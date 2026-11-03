using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ParkingSystem.Application.Interfaces
{
    public interface ICheckTheDurationOfReservations
    {
        public void CheckTheDuration(object sender, ElapsedEventArgs e);
    }
}
