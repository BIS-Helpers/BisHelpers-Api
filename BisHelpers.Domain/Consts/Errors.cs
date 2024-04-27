namespace BisHelpers.Domain.Consts;

public static class Errors
{
    public static string EqualsToOne(List<string> words) =>
        "{PropertyName} must be equal to one of those words: " + string.Join(", ", words);

    public const string RequiredField = "{PropertyName} is required";
    public const string InvalidEmailAddress = "({PropertyValue}) is not consider as email";
    public const string MaxLength = "{PropertyName} cannot be more than {MaxLength} characters";
    public const string InvalidCollegeId = "College id must start with 51219 and contain 9 numbers only";
    public const string WeakPassword = "Passwords contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long";
    public const string InvalidMobileNumber = "Invalid mobile number.";
    public const string MaxMinLength = "The {PropertyName} must be at least {MinLength} and at max {MaxLength} characters long.";
    public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
    public const string MaxSize = "File cannot be more that 2 MB!";
    public const string NotAllowFutureDates = "Date cannot be in the future!";
    public const string InvalidRange = "{PropertyName} should be between {From} and {To}!";
    public const string InvalidUsername = "Username can only contain letters or digits.";
    public const string OnlyEnglishLetters = "Only English letters are allowed.";
    public const string OnlyArabicLetters = "Only Arabic letters are allowed.";
    public const string OnlyNumbersAndLetters = "Only Arabic/English letters or digits are allowed.";
    public const string DenySpecialCharacters = "Special characters are not allowed.";
    public const string InvalidNationalId = "Invalid national ID.";
    public const string InvalidStartDate = "Invalid start date.";
    public const string InvalidEndDate = "Invalid end date.";
}