$(function () {
    var msg = "ViewBag.Msj";
    if (msg == '1')
    {
        alert("User Details Inserted Successfully");
        window.location.href = "@Url.Action("/Filter?CategoryId=2", "PickList")";
    }
});