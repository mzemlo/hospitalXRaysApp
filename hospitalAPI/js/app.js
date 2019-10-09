function Patient(patient)
{
    this.Id = ko.observable(patient.Id);
    this.Name = ko.observable(patient.Name);
    this.Surname = ko.observable(patient.Surname);
    this.Pesel = ko.observable(patient.Pesel);
}

function Photo(photo) {
    this.Id = ko.observable(photo.Id);
    this.XrayPhotoBlobSource = ko.observable(photo.Name);
    this.ColoredPhotoBlobSource = ko.observable(photo.Surname);
    this.isColored = ko.observable(photo.Pesel);
    this.DiseaseName = ko.observable(photo.DiseaseName);
    this.DiseaseDescription = ko.observable(photo.DiseaseDescription);
}


function ViewModel()
{
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.userID = ko.observable();
    self.user = ko.observable();

    self.registerUserName = ko.observable();
    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginUserName = ko.observable();
    self.loginLogin = ko.observable();
    self.loginPassword = ko.observable();
    self.errors = ko.observableArray([]);

    self.patients = ko.observableArray([]);

    function showError(jqXHR)
    {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);

        var response = jqXHR.responseJSON;
        if (response)
        {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState)
            {
                var modelState = response.ModelState;
                for (var prop in modelState)
                {
                    if (modelState.hasOwnProperty(prop))
                    {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length)
                        {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) self.errors.push(response.error);
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    self.register = function ()
    {
        console.log("Rejestrujemy...");
        self.result('');
        self.errors.removeAll();

        var data = {
            UserName: self.registerUserName(),
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        console.log(data);

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.login = function ()
    {
        self.result('');
        self.errors.removeAll();

        var loginData = {
            grant_type: 'password',
            username: self.loginUserName(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            self.userID(data.Id);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.logout = function ()
    {
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
            self.user('');
            sessionStorage.removeItem(tokenKey);
        }).fail(showError);
    }

    self.getPatients = function ()
    {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/' + curUserId +'/Patients',
            headers: headers
        }).done(function (data) {
            var mappedPatients = $.map(data, function (item) { return new Patient(item) });
            self.patients(mappedPatients);
        }).fail(showError);
    }

    self.getPhotos = function () {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/3/photos',
            headers: headers
        }).done(function (data) {
            var mappedPhotos = $.map(data, function (item) { return new Patient(item) });
            self.patients(mappedPatients);
        }).fail(showError);
    }
}

var app = new ViewModel();
ko.applyBindings(app);