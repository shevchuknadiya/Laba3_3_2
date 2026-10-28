using Lab3_3_2.Core;
using System.Text.RegularExpressions;

namespace Lab3_3_2.BLL.Models;

public class Student : Person
{
    /// <summary>
    /// Centimeters
    /// </summary>
    public required int Height
    {
        get => field;
        set
        {
            if (value <= 0)
                throw new EntityValidationException("Height can't be less or equals 0");

            field = value;
        }
    }

    /// <summary>
    /// Kilograms 
    /// </summary>
    public required int Weight
    {
        get => field;
        set
        {
            if (value <= 0)
                throw new EntityValidationException("Weight can't be less or equals 0");

            field = value;
        }
    }

    /// <summary>
    /// AA00000000
    /// </summary>
    public required string StudentIdCard
    {
        get => field;
        set
        {
            if (!Regex.IsMatch(value, "^[A-Za-z]{2}\\d{8}$"))
                throw new EntityValidationException("StudentIdCard must be in the format AA00000000");

            field = value;
        }
    }

    protected override string ToStringImp() => $"Student {FullName}. Height: {Height}, Wight: {Weight}, Passport: {Passport}, Student ID card: {StudentIdCard}";
}
