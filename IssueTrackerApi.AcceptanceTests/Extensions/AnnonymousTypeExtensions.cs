namespace IssueTrackerApi.AcceptanceTests.Extensions
{
    public static class AnnonymousTypeExtensions
    {
        public static object Value(this object thisObject, string propertyName)
        {
            return thisObject.GetType().GetProperty(propertyName).GetValue(thisObject, null);
        }
    }
}
