﻿// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");

// input response
string? resp = Console.ReadLine();

//file
string file = "sleepData.txt";


if (resp == "1")
{
    // TODO: create data file
    StreamWriter sw = new(file);

   Console.WriteLine("How many weeks of data is needed?");

    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());

    // determine start and end date
    DateTime today = DateTime.Now;

    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);

    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

        // random number generator
    Random rnd = new();

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }

        // M/d/yyyy,#|#|#|#|#|#|#
        sw.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }

    sw.Close();

}
else if (resp == "2")
{
    if (File.Exists(file))
    {
        StreamReader sr = new(file);

        while(!sr.EndOfStream)
        {
            string? line = sr.ReadLine();


            string[] arr = String.IsNullOrEmpty(line) ? [] : line.Split(',');
            string[] num = arr[1].Split('|');

            DateTime date = DateTime.Parse(arr[0]);

            int sundayHours = int.Parse(num[0]);
            int mondayHours = int.Parse(num[1]);
            int tuesdayHours = int.Parse(num[2]);
            int wednesdayHours = int.Parse(num[3]);
            int thursdayHours = int.Parse(num[4]);
            int fridayHours = int.Parse(num[5]);
            int saturdayHours = int.Parse(num[6]);

            int totalHours = sundayHours + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours + saturdayHours;
            double avgHours = totalHours / 7.0;

            Console.WriteLine("\nWeek of {0:MMM}, {0:dd}, {0:yyyy}", date);
            Console.WriteLine($" Su Mo Tu We Th Fr Sa Tot Avg\n -- -- -- -- -- -- -- --- ---\n{sundayHours,3} {mondayHours,2} {tuesdayHours,2} {wednesdayHours,2} {thursdayHours,2} {fridayHours,2} {saturdayHours,2} {totalHours,3} {avgHours, 3:N1}");

        }
        
        sr.Close();

    }else
    {
        Console.WriteLine("File does not exist!");
    }
}