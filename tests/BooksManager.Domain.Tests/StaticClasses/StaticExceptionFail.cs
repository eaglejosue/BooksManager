using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooksManager.Domain.Tests.StaticClasses
{
    public static class StaticExceptionFail
    {
        public static void New(System.Exception ex)
        {
            var errorMessage = $"{ex.Message} :: {ex.StackTrace}";

            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
                errorMessage += $" :: {ex.InnerException?.Message}";

            Assert.Fail(errorMessage);
        }
    }
}
