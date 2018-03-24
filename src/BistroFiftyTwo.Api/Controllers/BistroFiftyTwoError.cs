namespace BistroFiftyTwo.Api.Controllers
{
    public class BistroFiftyTwoError
    {
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string FieldName { get; set; }

        public static BistroFiftyTwoError MissingField(string fieldName)
        {
            return new BistroFiftyTwoError
            {
                ErrorType = "Missing Required Data",
                Description = $"{fieldName} is required and is missing",
                FieldName = fieldName
            };
        }

        public static BistroFiftyTwoError Invalid(string fieldName, string value)
        {
            return new BistroFiftyTwoError
            {
                ErrorType = "Invalid Required Data",
                Description = $"{fieldName} is invalid.  {value} is not valid",
                FieldName = fieldName
            };
        }

        
    }
}