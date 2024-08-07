﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (Request.IsAuthenticated) { %>
    <p>
        Hello, <%: Html.ActionLink(Page.User.Identity.Name, "ChangePassword", "Account", routeValues: null, htmlAttributes: new { @class = "username", title = "Change password" }) %>!
        <%: Html.ActionLink("Log off", "LogOff", "Account") %>
    </p>
<% } else { %>
    <ul>
<!--        <li><%: Html.ActionLink("Register", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink", data_dialog_title = "Registration" })%></li> -->
        <li><%: Html.ActionLink("Log in", "Login", "Account", routeValues: null, htmlAttributes: new { id = "loginLink", data_dialog_title = "Identification" })%></li>
    </ul>
<% } %>