using HKQTravelling.Models;

namespace HKQTravelling.Areas.Admin.Extension
{
    public static class checkingEndLocation
    {
        public static bool checkEndLocationName(ApplicationDBContext data, string name)
        {
            return data.endLocations.Count(u => u.EndLocationName == name) > 0;
        }
    }
}
