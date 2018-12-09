// Write your JavaScript code.
$(".onlynumber").bind("keypress", function (e) {
    var keyCode = e.which ? e.which : e.keyCode
    // Checking value weather the key between the 0-9 or not! If not we are restricting 
    var result = (keyCode >= 48 && keyCode <= 57);
    return result;
});