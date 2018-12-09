var Helper = (function () {
    function VerifyUserName(control) {
        control.addClass("loading");
        $.get("/Account/VerifyUserName",
            { username: control.val() },
            function (data) {
                if (data !== true) {
                    alert('User name alerady exits');
                    $(control).val('');

                    console.log(data);
                }
            }).done(function () {
            }).fail(function (xhr, status, error) {
                console.log(xhr);
                alert('Error in fetching data');
            }).always(function () {
                control.removeClass("loading");
            });
    }

    function deleteConfimation() {
        var r = confirm("Do you want to delete ?");
        if (r === true) {
            console.log("successfully deleted");
            return true;
        }
        else {
            return false;
        }
    }

    function GetJobCategory(sourceControl, targetControl) {
        $(targetControl).addClass("loading");
        $.get("/Clients/Job/JobSubCategory",
            { id: $(sourceControl).val() },
            function (data) {
                console.log(data);
                $(targetControl).empty();
                $.each(data, function (i, val) {
                    var newOption = $('<option value="' + val.id + '">' + val.categoryName_JP + '</option>');
                    $(targetControl).append(newOption);
                })
            }).done(function () {
            }).fail(function (xhr, status, error) {
                console.log(xhr);
                alert('Error in fetching data');
            }).always(function () {
                $(targetControl).removeClass("loading");
            });
    }

    function AutoFillByPostal() {
        //disable on page load
        $('#CityName').prop('readonly', true);
        $('#ProvinceID').prop('readonly', true);
        $('#ProvinceID').attr("style", "pointer-events: none;background-color:#eee");
        $('#Town').prop('readonly', true);
        $("#postalAddress1").keyup(function () {
            if (this.value.length === this.maxLength) {
                $("#postalAddress2").focus();
            }
        });
        $("#postalAddress2").keyup(function () {
            GetPostalDetail(this);
        });
        $("#postalAddress2").focusout(function () {
            GetPostalDetail(this);
        });
    }

    function GetPostalDetail(input) {
        if (input.value.length === input.maxLength) {
            $('#CityName').addClass("loading");
            $('#ProvinceID').addClass("loading");
            $('#Town').addClass("loading");

            $.get("/Account/getPostalCodeDetail",
                { postalcode: $("#postalAddress1").val() + "-" + $(input).val() },
                function (data) {
                    if (data !== '') {
                        $('#CityName').val(data[0]);
                        $('#ProvinceID').val(data[1]); $('#Town').val(data[2]);
                        $('#postalnotfound').hide();
                        $('#ProvinceID').attr("style", "pointer-events: none;background-color:#eee");
                        $('#ProvinceID').prop('readonly', true);
                        $('#CityName').prop('readonly', true);
                        $('#Town').prop('readonly', true);
                    }
                    else {
                        $('#CityName').val('');
                        $('#ProvinceID').val('');
                        $('#Town').val('');
                        $('#postalnotfound').show();
                        //$('#ProvinceID').attr("style", "pointer-events: auto;background-color:#fff");
                        //$('#ProvinceID').prop('readonly', false);
                        //$('#CityName').prop('readonly', false);
                        //$('#Town').prop('readonly', false);
                        //alert('Postal Code not found');
                    }
                }).done(function () {
                }).fail(function (xhr, status, error) {
                    console.log(xhr);
                    alert('Data relevant to entered postal code not find');
                    $('#postalnotfound').show();
                }).always(function () {
                    $('#CityName').removeClass("loading");
                    $('#ProvinceID').removeClass("loading");
                    $('#Town').removeClass("loading");
                });
        }
    }

    function CheckTransporation(value) {
        if (value === 'Y') {

            $('#maxTransporationDiv').show();
            $('#TransportationFee').show();
        }
        else {
            $('#maxTransporationDiv').hide();
            $('#TransportationFee').hide();
        }
    }

    function comparePassword() {
        var password = document.getElementById("Password")
            , confirm_password = document.getElementById("ConfirmPassword");

        function validatePassword() {
            if (password.value !== confirm_password.value) {
                confirm_password.setCustomValidity("Passwords Don't Match");
            } else {
                confirm_password.setCustomValidity('');
            }
        }

        password.onchange = validatePassword;
        confirm_password.onkeyup = validatePassword;
    }

    function PostAjaxRequest(url, data) {
        var result;
        $.ajax({
            url: url,
            data: data,
            type: "POST"
        })
            .success(function (data) {
                if (data === "OK") {
                    //just add if want to add
                }
                else {
                    alert("Data not saved");
                }
                result =  "success";
            })
            .done(function () {
                
            });

        return result;
    }

    return {
        AutoFillByPostal: AutoFillByPostal,
        comparePassword: comparePassword,
        VerifyUserName: VerifyUserName,
        GetJobCategory: GetJobCategory,
        deleteConfimation: deleteConfimation,
        CheckTransporation: CheckTransporation,
        PostAjaxRequest: PostAjaxRequest
    };
})();