namespace Authorization.BLL.Constants;

public static class ExceptionConstants
{
    public const string WrongPassword = "Password can't be null or empty";
    public const string UserExists = "The user already exists.";
    public const string NoUser = "Can't find user";
    public const string WrongEmail = "Email can't be null or empty";
    public const string PasswordNotMatch = "Passwords do not match";
}
