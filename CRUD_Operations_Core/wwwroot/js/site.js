
function ShowCountryCreateModal() {
    $.ajax({
        url: "/country/CreateModalForm",
        type: 'get',
        success: function (responce) {
            $("#DivCreateDialog").html(responce);
            ShowCreateModalForm();
        }
    });
    return;
}

function ShowCityCreateModal() {

    var lstCountryCtrl = document.getElementById("lstCountryId")
    var countryid = lstCountryCtrl.options[lstCountryCtrl.selectedIndex].value;

    $.ajax({
        url: "/city/CreateModalForm?countryid=" + countryid,
        type: 'get',
        success: function (responce) {
            $("#DivCreateDialog").html(responce);
            ShowCreateModalForm();
        }
    });
    return;
}

        function FillCities(lstCountryCtrl, lstCityId) {
            var lstCities = $("#" + lstCityId);
        lstCities.empty();

        lstCities.append($('<option />',
        {
            value: null,
        text: "Select City"
                }));

        var selectedCountry = lstCountryCtrl.options[lstCountryCtrl.selectedIndex].value;

        if (selectedCountry != null && selectedCountry != '') {
            $.getJSON("/Home/GetCitiesByCountry", { countryId: selectedCountry }, function (cities) {
                if (cities != null && !jQuery.isEmptyObject(cities)) {
                    $.each(cities, function (index, city) {
                        lstCities.append($('<option/>',
                            {
                                value: city.value,
                                text: city.text
                            }));
                    });
                };
            });
            }
        return;
        }

$(".custom-file-input").on("change", function () {

    var fileName = $(this).val().split("\\").pop();

    document.getElementById('PreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('PhotoUrl').value = fileName;

});

//function InputFile(){

//    var fileName = $(".custom-file-input").val().split("\\").pop();

//    document.getElementById('PreviewPhoto').src = window.URL.createObjectURL(document.querySelector(".custom-file-input").files[0]);

//    document.getElementById('PhotoUrl').value = fileName;
//    return;
//}


function ShowCreateModalForm() {
    $("#DivCreateDialogHolder").modal('show');
    return;
}


$(document).ready(function () {
    
    $('#dupSubmitBtn').click(function () {
      
        $('#btnSubmit').click();
    });
});

$(document).ready(function () {

    $('#dupSubmitBtn').click(function () {

        $('#btnSubmit2').click();
    });
});


function refreshCountryList() {
    var btnBack = document.getElementById('dupBackBtn');
    btnBack.click();
    FillCountries("lstCountryId");
}

function refreshCityList() {
    var btnBack = document.getElementById('dupBackBtn');
    btnBack.click();
    var lstCountryCtrl = document.getElementById('lstCountryId');
    FillCities(lstCountryCtrl,"lstCity")
}

function FillCountries(lstCountryId) {
    var lstCountries = $("#" + lstCountryId);
    lstCountries.empty();

    lstCountries.append($('<option/>',
        {
            value: null,
            text: "Select Country"
        }));

    $.getJSON("/country/GetCountries", function (countries) {
        if (countries != null && !jQuery.isEmptyObject(countries)) {
            $.each(countries, function (index, country) {
                lstCountries.append($('<option/>',
                    {
                        value: country.value,
                        text: country.text
                    }));
            });
        };
});
return;
}



    

