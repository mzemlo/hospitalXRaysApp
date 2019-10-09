var tokenKey = 'accessToken';
var UserId = 'tuBedzieId';
var isNotDoctor;
var isNotAdmin;
var isLogged = false;
var curUrl = window.location.href;
var url = curUrl.split("#!");
curUrl = url[0];
var login = function () {
    var curUrl = window.location.href;
    var loginData = {
        grant_type: 'password',
        username: $("#username").val(),
        password: $("#password").val()
    };
    $("#loader").addClass("wait");
    $("#animation").css("display", "block");
    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData
    }).done(function (data) {
        // Cache the access token in session storage.

        sessionStorage.setItem(tokenKey, data.access_token);
        isLogged = true;
        getAspUser();       
        getUsers();
        getPhotos();
        window.location.replace(curUrl + "home");
        }).fail(function () {
            $("#loader").removeClass("wait");
            $("#animation").css("display", "none");
            alert("Błąd logowania");
        });
}
var logout = function () {

    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout',
        headers: headers
    }).done(function (data) {
        curUserId = "";
        curUserRole = "";
        curUserId = "";
        curUserName = "";
        curUserSurname = "";
        curUserPesel = "";
        curUserContract = "";
        curUserLicense = "";
        modelDoctors = [];
        modelPatients = [];
        sessionStorage.removeItem(tokenKey);
        getAspUser();
        getAllPatients();
        getPatients();
    }).fail(showError);
}

var changePassword = function () {

    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    var changePasswordData = {
        OldPassword: $("#old-password").val(),
        NewPassword: $("#new-password").val(),
        ConfirmPassword: $("#confirm-new-password").val()
    };
    $.ajax({
        type: 'POST',
        url: 'api/Account/ChangePassword',
        headers: headers,
        data: changePasswordData
    }).done(function (data) {
        // Cache the access token in session storage.

        alert("Zmieniono hasło");
    }).fail(function () {
        alert("Zmiana hasła nie powiodła się");
    });
}

var setPassword = function () {

    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    var setPasswordData = {
        UserId: $("#doctor-Id").text(),
        NewPassword: $("#new-password").val(),
        ConfirmPassword: $("#confirm-new-password").val()
    };
    $.ajax({
        type: 'POST',
        url: 'api/Account/SetPassword',
        headers: headers,
        data: setPasswordData
    }).done(function (data) {
        // Cache the access token in session storage.

        alert("Zmieniono hasło");
    }).fail(function () {
        alert("Zmiana hasła nie powiodła się");
    });
}

var register = function () {

    var registerData = {        
        UserName: $("#doctor-login").val(),
        Email: $("#doctor-email").val(),
        Password: $("#doctor-pass").val(),
        ConfirmPassword: $("#doctor-pass2").val(),
        Role: $("#user-role").val(),
        roleOfUser: $("#user-role").val(),
        Name: $("#doctor-firstname").val(),
        Surname: $("#doctor-surname").val(),
        Contract: $("#doctor-contract-number").val(),
        License: $("#doctor-license-number").val(),
        PESEL: $("#doctor-pesel").val()
    };


    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(registerData)
    }).done(function (data) {
        alert("Dodano uzytkownika!");
        getUsers();
        }).fail(function () {
            alert("Błąd rejestracji");
        });
}

var editUser = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    var registerData = {
        Id: $("#doctor-Id").text(),
        UserName: "A",
        Name: $("#doctor-firstname").val(),
        Surname: $("#doctor-surname").val(),
        Contract: $("#doctor-contract-number").val(),
        License: $("#doctor-license-number").val(),
        PESEL: $("#doctor-pesel").val()
    };


    $.ajax({
        type: 'POST',
        url: '/api/Account/Edit',
        contentType: 'application/json; charset=utf-8',
        headers: headers,
        data: JSON.stringify(registerData)
    }).done(function (data) {
        alert("Edytowano uzytkownika!");
        getUsers();
    }).fail(function () {
        alert("Błąd edycji");
    });
}


var logout = function () {
    // Log out from the cookie based logon.
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout',
        headers: headers
    }).done(function (data) {
        // Successfully logged out. Delete the token.
        sessionStorage.removeItem(tokenKey);
    }).fail();
}

var getPhotos = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'GET',
        url: "/api/photos",
        dataType: 'json',
        headers: headers,
        success: function (data) {
            modelPhotos = data;
            if (curUserRole == "Ordynator") {
                getAllPatients();
            }
            if (curUserRole == "Lekarz") {
                getPatients();
            }
        }
    })
}

var getPatients = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    $.ajax({
        type: 'GET',
        url: "/api/" + curUserId + "/Patients",
        dataType: 'json',
        headers: headers,
        success: function (data) {
            modelPatients = data;
        }
    });
};

var getAllPatients = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    $.ajax({
        type: 'GET',
        url: "/api/Patients",
        dataType: 'json',
        headers: headers,
        success: function (data) {

            modelPatients = data;
        }
    });
};

var getOptions = function () {
    $.ajax({
        type: 'GET',
        url: "/js/menuOptions.json",
        dataType: 'json',
        success: function (data) {
            modelOptions = data.menuOptions;
        }
    });
};
//getOptions();

var getUsers = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    $.ajax({
        type: 'GET',
        url: "/api/Users",
        dataType: 'json',
        headers: headers,
        success: function (data) {

            modelDoctors = data;
        }
    });
};


var curUser;
var curUserId;
var curUserRole;
var curUserName;
var curUserSurname;
var curUserPesel;
var curUserContract;
var curUserLicense;
var curUserEmail;

var getAspUser = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    $.ajax({
        type: 'GET',
        url: "api/Account/UserInfo",
        dataType: 'json',
        headers: headers,
        success: function (data) {
            curUserRole = data.Role;
            curUserId = data.Id;    
            isLogged = true;
            if (data.Role == "Ordynator") {
                getAllPatients();
            }
            if (data.Role == "Lekarz") {
                getPatients();
            }
            if (data.Role == "Admin" || data.Role == "Ordynator") {
                isNotDoctor = true;
            }
            if (data.Role == "Lekarz" || data.Role == "Ordynator") {
                isNotAdmin = true;
            }
            curUserName = data.Name;
            curUserSurname = data.Surname;
            curUserPesel = data.PESEL;
            curUserContract = data.Contract;
            curUserLicense = data.License;
            curUserEmail = data.Email;
            curUser = data;
            loadMenu();
            window.location.replace(curUrl + "#!/home");
        }
    });
}
getAspUser();

var addPatient = function () {
    $('#patient-add-form').submit(function (e) {
        e.preventDefault();
        var patient = {
            Name: $("#patient-firstname").val(),
            Surname: $("#patient-surname").val(),
            Pesel: $("#patient-Pesel").val(),
            PostCode: $("#patient-PostCode").val(),
            City: $("#patient-City").val(),
            Street: $("#patient-Street").val(),
            NoHouse: $("#patient-NoHouse").val(),
            NoFlat: $("#patient-NoFlat").val(),
            UserId: curUserId
        };

        var error = "";
        const numReg = /\d/;
        const nameReg = /^[a-zA-Z]{3,}/;
        const peselReg = /^[0-9]{11}$/;
        const codeReg = /[0-9]{2}-[0-9]{3}/;
        const houseReg = /^\d+[a-zA-Z]*$/;


        if (!nameReg.test(patient.Name))// && !numReg.test(patient.Name) && isNullOrWhiteSpace(patient.Name))
            error += "-Imię pacjenta jest nieprawidłowe.<br>\n\r";

        if (!nameReg.test(patient.Surname))
            error += "-Nazwisko pacjenta jest nieprawidłowe.<br>\n\r";

        if (!peselReg.test(patient.Pesel))
            error += "-Pesel pacjenta jest nieprawidłowy.<br>\n\r";

        if (!codeReg.test(patient.PostCode))
            error += "-Kod pocztowy pacjenta jest nieprawidłowy.<br>\n\r";

        if (!nameReg.test(patient.City))
            error += "-Miasto pacjenta jest nieprawidłowe.<br>\n\r";

        if (isNullOrWhiteSpace(patient.Street))
            error += "-Ulica pacjenta jest nieprawidłowa.<br>\n\r";

        if (!houseReg.test(patient.NoHouse))
            error += "-Nr domu pacjenta jest nieprawidłowy.<br>\n\r";
        /*
        if (patient.NoFlat == "")
            error += "-Nr mieszkania pacjenta nie może być puste.<br>\n\r";
        */

        console.log("error: " + error);

        if (error != "")
        {
            document.getElementById("validation-error").innerHTML = error;
            return false;
        }
        
        $.ajax
        ({
            type: 'POST',
            url: "/api/Patients",
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(patient),
            success: function (data)
            {
                alert("Dodano pacjenta!");
                if (curUserRole == "Ordynator")
                {
                    getAllPatients();
                }
                if (curUserRole == "Lekarz")
                {
                    getPatients();
                }
            }
        })
    });
};

function isNullOrWhiteSpace(str) {
    return (!str || str.length === 0 || /^\s*$/.test(str))
}

var editPatient = function () {
    $('#patient-add-form').submit(function (e) {
        e.preventDefault();
        var patient = {
            Name: $("#patient-firstname").val(),
            Surname: $("#patient-surname").val(),
            Pesel: $("#patient-Pesel").val(),
            PostCode: $("#patient-PostCode").val(),
            City: $("#patient-City").val(),
            Street: $("#patient-Street").val(),
            NoHouse: $("#patient-NoHouse").val(),
            NoFlat: $("#patient-NoFlat").val(),
            UserId: curUserId,
            Id: $("#patient-id").val()
        };
        var Id = $("#patient-id").val();
        $.ajax({
            type: 'PUT',
            url: "/api/Patients/" + Id,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(patient),
            success: function (data) {

                if (curUserRole == "Ordynator") {
                    getAllPatients();
                }
                if (curUserRole == "Lekarz") {
                    getPatients();
                }
                alert("Zmieniono dane!");
                window.location.replace(curUrl + "#!/patient-search/");
            }
        })
    });
};

var deletePatient = function () {
    $('#patient-delete-btn').click(function (e) {
        e.preventDefault();
        var Id = $("#patient-id").html();
        $.ajax({
            type: 'DELETE',
            url: "/api/Patients/" + Id,
            dataType: 'json',
            contentType: 'application/json',
            data: Id,
            success: function (data) {
                alert("Usunięto pacjenta!");
                if (curUserRole == "Ordynator") {
                    getAllPatients();
                }
                if (curUserRole == "Lekarz") {
                    getPatients();
                }
                window.location.replace(curUrl + "#!/home");
            }
        })
    });
};

var loadMenu = function () {
    if (curUserRole == "Admin") {
        $("#main-nav").load("mainMenu.html");
    }
    if (curUserRole == "Lekarz") {
        $("#main-nav").load("doctorMenu.html");
    }
    if (curUserRole == "Ordynator") {
        $("#main-nav").load("headMenu.html");
    }
    if (curUserRole == "") {
        $("#main-nav").load("doctorMenu.html");    
    }
}

var addPhoto = function () {
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    $("#loader").addClass("wait");
    $("#animation").css("display", "block");
    $("#loader-info").css("display", "block");
    if (window.FormData !== undefined) {

        var fileUpload = $("#add-photo-input").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        var fileName = $("#add-photo-name").val();
        var fileDesc = $("#add-photo-desc").val();
        var patientId = Number($("#patient-id").html());
        //// Adding one more key to FormData object  
        fileData.append('name', fileName);
        fileData.append('desc', fileDesc);
        fileData.append('patientId', patientId);

        $.ajax({
            url: '/api/Photos',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            headers: headers,
            data: fileData,
            success: function (result) {
                $("#loader").removeClass("wait");
                $("#animation").css("display", "none");
                $("#loader-info").css("display", "none");
                getPatients();
                getPhotos();
                window.location.reload();
                window.location.replace(curUrl + "#!/patient/"+patientId);
            },
            error: function (err) {
                $("#loader").removeClass("wait"); 
                $("#animation").css("display", "none");
                $("#loader-info").css("display", "none");
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }  
};
getPhotos();

var deletePhoto = function () {
        var Id = $("#photo-Id").html();
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            type: 'DELETE',
            url: "/api/Photos/" + Id,
            dataType: 'json',
            contentType: 'application/json',
            headers: headers,
            data: Id,
            success: function (data) {
                alert("Usunięto zdjęcie!");
                getPhotos();
                window.location.replace(curUrl + "#!/home");
            },
            error: function (err) {
                alert(err.statusText);
            }
        })
    };



