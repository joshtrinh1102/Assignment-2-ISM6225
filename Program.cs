using System;

Console.Write("Choose a question (1-10): ");
if (!int.TryParse(Console.ReadLine(), out int question) || question < 1 || question > 10)
{
    Console.WriteLine("Enter a question number from 1 through 10.");
    return;
}

switch (question)
{
    case 1: Question1(); break;
    case 2: Question2(); break;
    case 3: Question3(); break;
    case 4: Question4(); break;
    case 5: Question5(); break;
    case 6: Question6(); break;
    case 7: Question7(); break;
    case 8: Question8(); break;
    case 9: Question9(); break;
    case 10: Question10(); break;
}

static void Question1()
{
    int customers = ReadNonnegativeInt("Current number of customers: ");
    decimal monthlyPrice = ReadNonnegativeDecimal("Monthly price per customer: $");
    decimal increasePercent = ReadNonnegativeDecimal("Percentage price increase: ");
    decimal currentRevenue = customers * monthlyPrice;
    decimal projectedRevenue = customers * monthlyPrice * (1 + increasePercent / 100);
    Console.WriteLine($"Current monthly revenue: {currentRevenue:C2}");
    Console.WriteLine($"Projected monthly revenue: {projectedRevenue:C2}");
    Console.WriteLine($"Dollar increase: {projectedRevenue - currentRevenue:C2}");
}

static void Question2()
{
    double percent = ReadPositiveDouble("Annual return rate (%): ");
    double rate = percent / 100;
    double years = Math.Log(2) / Math.Log(1 + rate);
    string category = years < 10 ? "Fast" : years <= 20 ? "Moderate" : "Slow";
    Console.WriteLine($"Doubling time: {years:F2} years");
    Console.WriteLine($"Classification: {category}");
}

static void Question3()
{
    try
    {
        Console.Write("Side a: ");
        double a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Side b: ");
        double b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Side c: ");
        double c = Convert.ToDouble(Console.ReadLine());
        if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(c) ||
            a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
            Console.WriteLine("Invalid triangle: sides must be positive and satisfy the triangle rule.");
        else
        {
            double s = (a + b + c) / 2;
            Console.WriteLine($"Triangle area: {Math.Sqrt(s * (s - a) * (s - b) * (s - c)):F2}");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Enter numeric side lengths.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Input is outside the supported numeric range.");
    }
}

static void Question4()
{
    decimal hours = ReadNonnegativeDecimal("Compute hours used: ");
    decimal usageCharge;
    if (hours <= 100) usageCharge = hours * 0.12m;
    else if (hours <= 500) usageCharge = 100 * 0.12m + (hours - 100) * 0.09m;
    else usageCharge = 100 * 0.12m + 400 * 0.09m + (hours - 500) * 0.06m;
    decimal fee = usageCharge * 0.05m;
    Console.WriteLine($"Usage charge: {usageCharge:C2}");
    Console.WriteLine($"Service fee: {fee:C2}");
    Console.WriteLine($"Total invoice: {usageCharge + fee:C2}");
}

static void Question5()
{
    decimal total = 0;
    int count = 0;
    while (true)
    {
        Console.Write("Sale amount (-1 to finish): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal sale))
        {
            Console.WriteLine("Invalid input. Enter a numeric amount.");
            continue;
        }
        if (sale == -1) break;
        if (sale < 0) { Console.WriteLine("Sale cannot be negative."); continue; }
        total += sale;
        count++;
    }
    if (count == 0) Console.WriteLine("No valid sales were entered.");
    else Console.WriteLine($"Count: {count}; total: {total:C2}; average: {total / count:C2}");
}

static void Question6()
{
    decimal investment = ReadPositiveDecimal("Initial investment: $");
    decimal benefit = ReadPositiveDecimal("Expected monthly benefit: $");
    decimal cumulative = 0;
    int month = 0;
    while (cumulative < investment && month < 600)
    {
        month++;
        cumulative += benefit;
        Console.WriteLine($"Month {month}: cumulative {cumulative:C2}; unrecovered {Math.Max(0, investment - cumulative):C2}");
    }
    if (cumulative >= investment) Console.WriteLine($"Payback month: {month}; cumulative benefits: {cumulative:C2}");
    else Console.WriteLine("Payback was not reached within 600 months.");
}

static void Question7()
{
    int[] demand = new int[7];
    for (int i = 0; i < demand.Length; i++)
        demand[i] = ReadNonnegativeInt($"Demand for day {i + 1}: ");
    for (int i = 0; i < demand.Length; i++)
    {
        Console.Write($"{i + 1}: {demand[i]} ");
        for (int star = 0; star < demand[i] / 10; star++) Console.Write("*");
        Console.WriteLine();
    }
}

static void Question8()
{
    int missedPayments = ReadNonnegativeInt("Missed payments: ");
    int supportTickets = ReadNonnegativeInt("Support tickets: ");
    int monthsInactive = ReadNonnegativeInt("Months inactive: ");
    long scoreCheck = (long)missedPayments * 20 + (long)supportTickets * 5 + (long)monthsInactive * 8;
    if (scoreCheck > int.MaxValue) { Console.WriteLine("Inputs produce a score outside the supported integer range."); return; }
    int score = ComputeRiskScore(missedPayments, supportTickets, monthsInactive);
    string category = score < 30 ? "Low" : score < 60 ? "Moderate" : "High";
    Console.WriteLine($"Risk score: {score}; classification: {category}");
}

static int ComputeRiskScore(int missedPayments, int supportTickets, int monthsInactive)
{
    return missedPayments * 20 + supportTickets * 5 + monthsInactive * 8;
}

static void Question9()
{
    int count = ReadIntBetween("Number of transactions (3-20): ", 3, 20);
    decimal[] amounts = new decimal[count];
    for (int i = 0; i < count; i++)
        amounts[i] = ReadNonnegativeDecimal($"Transaction {i + 1}: $");
    decimal min = amounts[0], max = amounts[0], total = 0;
    for (int i = 0; i < count; i++)
    {
        if (amounts[i] < min) min = amounts[i];
        if (amounts[i] > max) max = amounts[i];
        total += amounts[i];
    }
    decimal mean = total / count;
    Console.WriteLine($"Minimum: {min:C2}; maximum: {max:C2}; total: {total:C2}; mean: {mean:C2}");
    for (int i = 0; i < count; i++)
        Console.WriteLine($"{amounts[i]:C2}: {(amounts[i] > 1.5m * mean ? "Review" : "Normal")}");
}

static void Question10()
{
    decimal[] sales = new decimal[7];
    // Add a specific student revision note here after reviewing the GenAI-assisted solution.
    for (int i = 0; i < sales.Length; i++)
        sales[i] = ReadNonnegativeDecimal($"Sales for day {i + 1}: $");
    for (int i = 0; i < sales.Length; i++) Console.WriteLine($"Day {i + 1}: {sales[i]:C2}");
    decimal total = TotalSales(sales), average = AverageSales(sales);
    int above = DaysAboveAverage(sales);
    string classification = above >= 4 ? "Strong" : above >= 2 ? "Balanced" : "Concentrated";
    Console.WriteLine($"Total: {total:C2}; average: {average:C2}");
    Console.WriteLine($"Highest: {HighestSales(sales):C2}; lowest: {LowestSales(sales):C2}");
    Console.WriteLine($"Days above average: {above}; week: {classification}");
}
static decimal TotalSales(decimal[] values)
{
    decimal total = 0;
    for (int i = 0; i < values.Length; i++) total += values[i];
    return total;
}
static decimal AverageSales(decimal[] values)
{
    return TotalSales(values) / values.Length;
}
static decimal HighestSales(decimal[] values)
{
    decimal highest = values[0];
    for (int i = 1; i < values.Length; i++) if (values[i] > highest) highest = values[i];
    return highest;
}
static decimal LowestSales(decimal[] values)
{
    decimal lowest = values[0];
    for (int i = 1; i < values.Length; i++) if (values[i] < lowest) lowest = values[i];
    return lowest;
}
static int DaysAboveAverage(decimal[] values)
{
    decimal average = AverageSales(values);
    int count = 0;
    for (int i = 0; i < values.Length; i++) if (values[i] > average) count++;
    return count;
}
static int ReadNonnegativeInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= 0) return value;
        Console.WriteLine("Invalid input. Enter a nonnegative whole number.");
    }
}

static int ReadIntBetween(string prompt, int minimum, int maximum)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= minimum && value <= maximum) return value;
        Console.WriteLine($"Enter a whole number from {minimum} through {maximum}.");
    }
}

static decimal ReadNonnegativeDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0) return value;
        Console.WriteLine("Invalid input. Enter a nonnegative number.");
    }
}

static decimal ReadPositiveDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0) return value;
        Console.WriteLine("Invalid input. Enter a positive number.");
    }
}

static double ReadPositiveDouble(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (double.TryParse(Console.ReadLine(), out double value) && value > 0 && !double.IsInfinity(value))
            return value;
        Console.WriteLine("Invalid input. Enter a finite rate greater than zero.");
    }
}
