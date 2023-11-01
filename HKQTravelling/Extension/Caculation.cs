namespace HKQTravelling.Extension
{
    public static class Caculation
    {
        public static int caculateAge(DateTime birthDay)
        {
            if (birthDay == null)
            {
                return 0;
            }
            else
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthDay.Year;

                if (birthDay > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
    }
}
