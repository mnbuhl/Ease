namespace Ease.App.Common.Helpers;

public record Toast(string Message, string Type, int Timeout = 3000);