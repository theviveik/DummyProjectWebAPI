namespace BusinessAccessLayer.Models
{
    public static class HelperConst
    {
        public const string userID = "UserId";
        public const string userName = "UserName";
        public const string userType = "UserId";

    }

    public static class HelperURLConst
    {
        public const string login = "Login";
    }

    public static class MessageConst
    {
        public const string userPasswordIncorrect = "Username and password is incorrect.";
        public const string userNameExist = "Username name already exists.";
        public const string userIdNotPass = "Please pass UserId.";
        public const string userNotFound = "User not found.";
        public const string userDeleted = "User has been deleted.";
        public const string insertExplicit = "Cannot insert explicit value for identity column";
    }
}
