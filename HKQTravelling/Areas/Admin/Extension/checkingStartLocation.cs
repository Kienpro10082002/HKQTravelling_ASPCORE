using HKQTravelling.Models;

namespace HKQTravelling.Areas.Admin.Extension
{
    public static class checkingStartLocation
    {
        public static bool checkStartLocationName(ApplicationDBContext data, string name)
        {
            return data.startLocations.Count(u => u.StartLocationName == name) > 0;
        }
    }
}
