
var OnBegin = function () {
    $("#dvLoader").css('display', 'block');
}
var OnSuccess = function (response) {
    ShowMessage(response);
}

var OnFailure = function (data) {
    CustomValidateModel(data, this);

};
var OnComplated = function (xhr) {
    $("#dvLoader").css("display", "none");
};


function ShowMessage(response) {
    console.log(response);
    if (response.resultStatus === 1) {
        toastr.success(response.message, response.title);

        if (response.url !== "") {
            setInterval(function () { location.href = response.url }, 2500);
        }
    }
    else if (response.resultStatus === 2) {
        toastr["info"](response.message, response.title);
    }
    else if (response.resultStatus === 3) {
        toastr["warning"](response.message, response.title);
    }
    else if (response.resultStatus === 4) {
        toastr["error"](response.message, response.title);
    }
}

var defaultThemeMode = "light";
var themeMode;
if (document.documentElement) {
    if (document.documentElement.hasAttribute("data-theme-mode")) {
        themeMode = document.documentElement.getAttribute("data-theme-mode");
    } else {
        if (localStorage.getItem("data-theme") !== null) {
            themeMode = localStorage.getItem("data-theme");
        } else {
            themeMode = defaultThemeMode;
        }
    }
    if (themeMode === "system") {
        themeMode = window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light";
    }
    document.documentElement.setAttribute("data-theme", themeMode);
}

function CustomValidateModel(data, form) {
    var errorResponse = data.responseJSON;
    $.each(errorResponse, function (index, value) {
        var element = $(form).find(`#${value.key}`);
        element = element[0];
        highLightError(element, "input-validation-error");

        var validationMessageElement = $(`span[data-valmsg-for="${value.key}"]`);
        validationMessageElement.removeClass("field-validation-valid");
        validationMessageElement.addClass("field-validation-error");
        validationMessageElement.text(value.errors[0]);
    });
}

$.validator.setDefaults({
    ignore: [],
    highlight: highLightError,
    unhighlight: unhighLightError
});

var highLightError = function (element, errorClass) {
    element = $(element);
    element.addClass(errorClass);
}

var unhighLightError = function (element, errorClass) {
    element = $(element);
    element.removeClass(errorClass);
}

var animation = bodymovin.loadAnimation({
    container: document.getElementById('icon-container'),
    path: '/loader.json',
    renderer: 'svg',
    loop: true,
    autoplay: true,
    name: "Demo Animation"
});

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "3000",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};