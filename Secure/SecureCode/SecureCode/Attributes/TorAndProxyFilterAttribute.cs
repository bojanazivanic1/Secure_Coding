using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

public class TorAndProxyFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var bannedIpAddresses = File.ReadAllLines("TorExitNodes.txt");
        string userIpAddress = context.HttpContext.Connection.RemoteIpAddress!.ToString();

        if (bannedIpAddresses.Contains(userIpAddress))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        base.OnActionExecuting(context);
    }
}